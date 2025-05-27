using BitstampSimulador.Domain.Interfaces;
using BitstampSimulador.Infrastructure;

namespace BitstampSimulador.Application.Simulacoes
{
    public class ServicoSimulacao : IServicoSimulacao
    {
        private readonly MongoService _mongoService;

        public ServicoSimulacao(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        public ResultadoSimulacao Simular(string ativo, string tipo, decimal quantidade)
        {
            var snapshot = _mongoService.BuscarUltimoSnapshot(ativo);
            if (snapshot == null) return null;

            var ordens = tipo.ToLower() switch
            {
                "compra" => snapshot.Asks.OrderBy(o => o.Preco).ToList(),
                "venda" => snapshot.Bids.OrderByDescending(o => o.Preco).ToList(),
                _ => null
            };

            if (ordens == null) return null;

            decimal total = 0;
            decimal restante = quantidade;

            foreach (var ordem in ordens)
            {
                if (restante <= 0) break;

                var usado = Math.Min(restante, ordem.Quantidade);
                total += usado * ordem.Preco;
                restante -= usado;
            }

            if (restante > 0) return null;

            return new ResultadoSimulacao
            {
                Ativo = ativo,
                Tipo = tipo,
                Quantidade = quantidade,
                Total = total,
                PrecoMedio = total / quantidade
            };
        }
    }
}
