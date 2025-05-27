using BitstampSimulador.Domain.Entities;
using MongoDB.Driver;

namespace BitstampSimulador.Infrastructure
{
    public class MongoService
    {
        private readonly IMongoDatabase _db;

        public MongoService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _db = client.GetDatabase("BookDB");
        }

        public OrderBookSnapshot BuscarUltimoSnapshot(string ativo)
        {
            var collection = _db.GetCollection<OrderBookSnapshot>(ativo.ToLower());
            return collection.Find(Builders<OrderBookSnapshot>.Filter.Empty)
                             .SortByDescending(s => s.Timestamp)
                             .FirstOrDefault();
        }

        public void SalvarSnapshot(OrderBookSnapshot snapshot)
        {
            var collection = _db.GetCollection<OrderBookSnapshot>(snapshot.Ativo.ToLower());
            collection.InsertOne(snapshot);
        }
    }
}