using CurrencyListApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace CurrencyListApp.Services
{
    /// <summary>
    /// Mock Service
    /// </summary>
    public class DataService
    {
        private HttpClient _client;

        public HttpClient client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                }

                return _client;
            }

            set
            {
                if (_client != value)
                {
                    _client = value;
                }
            }
        }

        // Get the namespace for the embedded JSON data files
        private readonly string ResourcePath = typeof(DataService).Namespace;

        // Get the assembly for the embedded JSON data files
        private Assembly ThisAssembly = typeof(DataService).GetTypeInfo().Assembly;

        private static string GetClosestLanguage(string locale)
        {
            var langs = new[] { "es", "de", "zh-cn", "zh-tw", "pt-br", "pt-pt" };
            var result = "";

            foreach (var lang in langs)
            {
                if (locale.StartsWith(lang, StringComparison.CurrentCultureIgnoreCase))
                {
                    result = $"{lang}";
                    break;
                }
            }

            return result;
        }

        public List<CurrencySymbol> GetCurrencySymbols()
        {
            var json = "";

            var stream = ThisAssembly.GetManifestResourceStream($"{ResourcePath}.currencysymbols.json");

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<CurrencySymbol>>(json);
        }

        public List<CurrencyName> GetExchangeCurrencies(string locale)
        {
            var language = GetClosestLanguage(locale);
            var json = "";

            var stream = ThisAssembly.GetManifestResourceStream($"{ResourcePath}.currencies-{language}.json") ??
                         ThisAssembly.GetManifestResourceStream($"{ResourcePath}.currencies.json");

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<CurrencyName>>(json);

        }

        public async Task<ExchangeRate> GetExchangeRate()
        {
            return await GetExchangeRate(client);
        }

        public async Task<ExchangeRate> GetExchangeRate(HttpClient client)
        {
            /*	
                var client = GetClient();

                var s = client.GetStringAsync("https://openexchangerates.org/api/latest.json?app_id=YOUR_API_KEY");

                var jsonData = await s;

                var exchangeRates = ExchangeRate.FromJson(jsonData);
            */

            string jsonData = "";

            var stream = ThisAssembly.GetManifestResourceStream($"{ResourcePath}.exchange.json");

            using (var reader = new StreamReader(stream))
            {
                jsonData = await reader.ReadToEndAsync();
            }

            var exchangeRates = ExchangeRate.FromJson(jsonData);

            return exchangeRates;
        }
    }
}