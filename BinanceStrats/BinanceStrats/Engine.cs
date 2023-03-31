using Binance.Spot.Models;

using BinanceStrats.Models;
using BinanceStrats.Utilities;

namespace BinanceStrats
{
    public class Engine
    {
        private List<KLineFiveMinute> last1000KlinesApiCall = new List<KLineFiveMinute>();
        private List<Position> positionsCollection = new List<Position>();

        public Wallet Wallet = new Wallet
        {
            Id = "1",
            Money = 1000m,
            BTC = 0,
            BorrowedBTC = 0,
        };

        public Engine()
        {
        }

        public async Task StartAsync()
        {
            var klinesGetter = new KlineCandleSticksGetter();

            var symbol = "BTCUSDT";
            var startTime = new DateTime(2023, 3, 1);
            var endTime = new DateTime(2023, 3, 30);

            var klinesCollection = await klinesGetter.GetKlineCandleSticksForIntervalAsync(1000, symbol, startTime, endTime, Interval.FIVE_MINUTE);

            this.last1000KlinesApiCall = klinesCollection.Take(999).ToList();
        }
    }
}