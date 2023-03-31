using Binance.Spot;
using Binance.Spot.Models;

using BinanceStrats.Converters;
using BinanceStrats.Helpers;
using BinanceStrats.Models;

using Newtonsoft.Json;

namespace BinanceStrats.Utilities
{
    public class KlineCandleSticksGetter
    {
        private readonly ClientHelper clientHelper;
        public static int secondsCount = 0;

        public System.Timers.Timer timer = new System.Timers.Timer(60000);

        public DateTime startTime;
        public DateTime endTime;

        public DateTime startTimeForCall;
        public DateTime endTimeForCall;

        private string apiKey = "your_api_key";
        private string secretKey = "your_secret_key";

        public KlineCandleSticksGetter()
        {
            this.clientHelper = new ClientHelper();
        }

        public void GetKlinesForPeriodOfTime(int candleSticksNumber, string symbol, DateTime startTime, DateTime endTime, Interval interval)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.startTimeForCall = this.startTime;

            timer.Elapsed += async (sender, e) => await GetKlineCandleSticksForIntervalAsync(candleSticksNumber, symbol, startTime, endTime, interval);
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
            Console.ReadKey();
        }

        public async Task<List<KLineFiveMinute>> GetKlineCandleSticksForIntervalAsync(int? candleSticksNumber, string symbol, DateTime startTime, DateTime endTime, Interval interval)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.startTimeForCall = this.startTime;

            var httpClient = clientHelper.GenerateClient();
            var spotAccountTrade = new SpotAccountTrade(httpClient, apiKey: apiKey, apiSecret: secretKey);

            var market = new Market(httpClient);

            var klineFiveMinutesData = new List<KLineFiveMinute>();

            for (int i = 1; i < 1152; i++)
            {
                this.endTimeForCall = this.startTimeForCall.AddMinutes(5000);

                var startTimeMilliseconds = new DateTimeOffset(this.startTimeForCall).ToUnixTimeMilliseconds();
                var endTimeMilliseconds = new DateTimeOffset(this.endTimeForCall).ToUnixTimeMilliseconds();

                var klineCandlestickDataResult = await market.KlineCandlestickData(symbol, interval, startTimeMilliseconds, endTimeMilliseconds, limit: candleSticksNumber);

                var settings = new JsonSerializerSettings
                {
                    Converters = { new ObjectToArrayConverter<BinanceKlineData>() },
                };

                var klineCandlestickData = JsonConvert.DeserializeObject<List<BinanceKlineData>>(klineCandlestickDataResult, settings);

                var mappedKlineCandlestick = new KLineFiveMinute();

                foreach (var klineCandlestick in klineCandlestickData)
                {
                    mappedKlineCandlestick = new KLineFiveMinute
                    {
                        Id = Guid.NewGuid().ToString(),
                        OpenTime = DateTimeConverter.UnixTimeToDateTime(klineCandlestick.OpenTime),
                        Open = klineCandlestick.Open,
                        High = klineCandlestick.High,
                        Low = klineCandlestick.Low,
                        Close = klineCandlestick.Close,
                        CloseTime = DateTimeConverter.UnixTimeToDateTime(klineCandlestick.CloseTime),
                        IsGreen = klineCandlestick.Open < klineCandlestick.Close,
                        Change = ((klineCandlestick.Close - klineCandlestick.Open) / Math.Max(klineCandlestick.Close, klineCandlestick.Open)) * 100
                    };

                    if (mappedKlineCandlestick.OpenTime >= this.endTime)
                    {
                        break;
                    }

                    klineFiveMinutesData.Add(mappedKlineCandlestick);
                }

                this.startTimeForCall = this.endTimeForCall;

                if (mappedKlineCandlestick.OpenTime >= this.endTime)
                {
                    i = 1152;
                }
            }

            return klineFiveMinutesData;
        }
    }
}