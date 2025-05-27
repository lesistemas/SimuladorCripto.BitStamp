using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BitstampSimulador.Domain.Entities
{
    public class OrderBookSnapshot
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Ativo { get; set; }
        public DateTime Timestamp { get; set; }
        public List<Ordem> Bids { get; set; } = new();
        public List<Ordem> Asks { get; set; } = new();
    }

    public class Ordem
    {
        public decimal Preco { get; set; }
        public decimal Quantidade { get; set; }
    }
}