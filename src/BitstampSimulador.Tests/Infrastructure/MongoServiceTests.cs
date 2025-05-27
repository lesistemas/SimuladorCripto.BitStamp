using Xunit;
using BitstampSimulador.Infrastructure;
using BitstampSimulador.Domain.Entities;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;

namespace BitstampSimulador.Tests.Infrastructure
{
    public class MongoServiceTests
    {
        [Fact]
        public void SalvarSnapshot_DeveInserirSnapshotNaCollection()
        {
            var mongo = new MongoServiceStub();

            var snapshot = new OrderBookSnapshot
            {
                Ativo = "BTCUSD",
                Timestamp = DateTime.UtcNow,
                Bids = new List<Ordem> { new Ordem { Preco = 10000, Quantidade = 0.5m } },
                Asks = new List<Ordem> { new Ordem { Preco = 10500, Quantidade = 0.3m } }
            };

            mongo.SalvarSnapshot(snapshot);

            var ultimo = mongo.BuscarUltimoSnapshot("BTCUSD");

            Assert.NotNull(ultimo);
            Assert.Equal("BTCUSD", ultimo.Ativo);
            Assert.Single(ultimo.Bids);
            Assert.Single(ultimo.Asks);
        }
    }

    // Implementação fake para simular Mongo sem banco real
    public class MongoServiceStub : MongoService
    {
        private readonly Dictionary<string, List<OrderBookSnapshot>> _db = new();

        public override void SalvarSnapshot(OrderBookSnapshot snapshot)
        {
            var key = snapshot.Ativo.ToLower();
            if (!_db.ContainsKey(key))
                _db[key] = new List<OrderBookSnapshot>();

            _db[key].Add(snapshot);
        }

        public override OrderBookSnapshot BuscarUltimoSnapshot(string ativo)
        {
            var key = ativo.ToLower();
            if (!_db.ContainsKey(key)) return null;
            return _db[key].OrderByDescending(s => s.Timestamp).FirstOrDefault();
        }
    }
}