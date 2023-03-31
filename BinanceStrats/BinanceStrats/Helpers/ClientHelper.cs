using Binance.Common;

using Microsoft.Extensions.Logging;

using System.Net.Http.Headers;

namespace BinanceStrats.Helpers
{
    public class ClientHelper
    {
        public ClientHelper()
        {
        }

        public HttpClient GenerateClient()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
            });

            ILogger logger = loggerFactory.CreateLogger<ClientHelper>();

            HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: logger);

            var apiClient = new HttpClient(handler: loggingHandler);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return apiClient;
        }
    }
}