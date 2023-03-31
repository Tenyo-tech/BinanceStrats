using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BinanceStrats.Models
{
    public class KLineFiveMinute
    {
        private DateTime openTime;
        private DateTime closeTime;

        public string? Id { get; set; }

        public DateTime OpenTime
        {
            get => openTime;
            set => openTime = new DateTime(value.Ticks, DateTimeKind.Utc);
        }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Open { get; set; } // Or string, if you prefer

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal High { get; set; } // Or string, if you prefer

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Low { get; set; } // Or string, if you prefer

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Close { get; set; } // Or string, if you prefer

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Change { get; set; }

        public bool IsGreen { get; set; }

        public DateTime CloseTime
        {
            get => closeTime;
            set => closeTime = new DateTime(value.Ticks, DateTimeKind.Utc);
        }
    }
}