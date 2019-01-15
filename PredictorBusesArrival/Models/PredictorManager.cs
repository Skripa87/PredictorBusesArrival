using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PredictorBusesArrival.Models
{
    public class PredictorManager
    {
        static HttpClient httpClient;

        public PredictorManager()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://glonass.ufagortrans.ru/php/")
            };
            httpClient.DefaultRequestHeaders
                      .Accept
                      .Clear();
            httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetAllStations(string path)
        {
            string result = null;
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            var jResult = JToken.Parse(result).ToObject<IEnumerable<Station>>();
            return result;
        }
    }
}