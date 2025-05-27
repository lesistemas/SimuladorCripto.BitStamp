using BitstampSimulador.Domain.Entities;

public class ExibidorEstatisticas
{
    private readonly Dictionary<string, List<OrderBookSnapshot>> _historico = new();
    private readonly object _trava = new();

    public void Adicionar(OrderBookSnapshot snapshot)
    {
        lock (_trava)
        {
            if (!_historico.ContainsKey(snapshot.Ativo))
                _historico[snapshot.Ativo] = new();

            _historico[snapshot.Ativo].Add(snapshot);
        }
    }

    public async Task ExibirPeriodicamente(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.Clear();
            Console.WriteLine($" Estatísticas - {DateTime.Now:HH:mm:ss}");

            lock (_trava)
            {
                foreach (var (ativo, lista) in _historico)
                {
                    if (lista.Count == 0)
                    {
                        Console.WriteLine($" {ativo} - Nenhum dado disponível.");
                        continue;
                    }

                    var ultimos = lista.TakeLast(10).ToList();

                    var precos = ultimos.SelectMany(s => s.Bids.Concat(s.Asks).Select(o => o.Preco)).ToList();
                    var quantidades = ultimos.SelectMany(s => s.Bids.Concat(s.Asks).Select(o => o.Quantidade)).ToList();

                    var maior = precos.Max();
                    var menor = precos.Min();
                    var media = precos.Average();
                    var mediaQtd = quantidades.Average();

                    Console.WriteLine($" Ativo: {ativo}");
                    Console.WriteLine($"  - Maior preço: {maior:F2}");
                    Console.WriteLine($"  - Menor preço: {menor:F2}");
                    Console.WriteLine($"  - Média de preço: {media:F2}");
                    Console.WriteLine($"  - Média quantidade: {mediaQtd:F4}");
                    Console.WriteLine($"  - Total de snapshots: {lista.Count}");
                }

                _historico.Clear();
            }

            await Task.Delay(5000, cancellationToken);
        }
    }
}