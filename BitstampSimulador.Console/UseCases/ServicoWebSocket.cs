using BitstampSimulador.Domain.Entities;
using BitstampSimulador.Infrastructure;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

public class ServicoWebSocket : BackgroundService
{
    private readonly MongoService _mongo;
    private readonly ExibidorEstatisticas _estatisticas;

    public ServicoWebSocket(MongoService mongo)
    {
        _mongo = mongo;
        _estatisticas = new ExibidorEstatisticas();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = _estatisticas.ExibirPeriodicamente(stoppingToken); // Inicia exibição em paralelo

        var socket = new ClientWebSocket();
        await socket.ConnectAsync(new Uri("wss://ws.bitstamp.net"), stoppingToken);

        var canais = new[] { "order_book_btcusd", "order_book_ethusd" };

        foreach (var canal in canais)
        {
            var mensagem = new
            {
                @event = "bts:subscribe",
                data = new { channel = canal }
            };

            var json = JsonSerializer.Serialize(mensagem);
            var bytes = Encoding.UTF8.GetBytes(json);
            await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, stoppingToken);
        }

        var buffer = new byte[8192];

        while (!stoppingToken.IsCancellationRequested)
        {
            var resultado = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);
            if (resultado.MessageType == WebSocketMessageType.Close) break;

            var msg = Encoding.UTF8.GetString(buffer, 0, resultado.Count);
            ProcessarMensagem(msg);
        }
    }

    private void ProcessarMensagem(string mensagem)
    {
        try
        {
            using var doc = JsonDocument.Parse(mensagem);
            var canal = doc.RootElement.GetProperty("channel").GetString();
            if (canal is null || !canal.StartsWith("order_book_")) return;

            var dados = doc.RootElement.GetProperty("data");
            var bids = dados.GetProperty("bids");
            var asks = dados.GetProperty("asks");

            var bidsList = new List<(decimal preco, decimal quantidade)>();
            var asksList = new List<(decimal preco, decimal quantidade)>();

            foreach (var bid in bids.EnumerateArray())
                bidsList.Add((decimal.Parse(bid[0].GetString()), decimal.Parse(bid[1].GetString())));

            foreach (var ask in asks.EnumerateArray())
                asksList.Add((decimal.Parse(ask[0].GetString()), decimal.Parse(ask[1].GetString())));

            var snapshot = new OrderBookSnapshot
            {
                Ativo = canal.Replace("order_book_", "").ToUpper(),
                Timestamp = DateTime.UtcNow,
                Bids = bidsList.Select(b => new Ordem { Preco = b.preco, Quantidade = b.quantidade }).ToList(),
                Asks = asksList.Select(a => new Ordem { Preco = a.preco, Quantidade = a.quantidade }).ToList()
            };

            _mongo.SalvarSnapshot(snapshot);
            _estatisticas.Adicionar(snapshot);
        }
        catch { }
    }
}