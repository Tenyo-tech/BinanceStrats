using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BinanceStrats.Models
{
    public class Position
    {
        public string? Id { get; set; }
        public string Type { get; set; } = string.Empty;

        public string Strategy { get; set; } = string.Empty;

        public string SubStrategy { get; set; } = string.Empty;

        public string Trend { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Last5HighestOrLowest { get; set; }

        public DateTime OpenTime { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Open { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Amount { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? Close { get; set; }

        public DateTime? CloseTime { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? ClosePercent { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Target { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TargetPercent { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Stop { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal StopPercent { get; set; }

        public bool IsActive { get; set; }
    }
}