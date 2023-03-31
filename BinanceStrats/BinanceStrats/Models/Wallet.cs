using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BinanceStrats.Models
{
    public class Wallet
    {
        public string Id { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Money { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal BTC { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal BorrowedBTC { get; set; }
    }
}