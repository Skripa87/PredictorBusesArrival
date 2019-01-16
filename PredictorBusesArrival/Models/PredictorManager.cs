using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        private void WriteInfoStationsToBase(IEnumerable<Station> stations)
        {
            try
            {
                using (var db = new BusStopsDataBaseEntities())
                {
                    foreach(var item in stations)
                    {
                        Station station = new Station
                        {
                            Active = false,
                            Descr = item.Descr,
                            Id = item.Id,
                            Lat = item.Lat,
                            Lng = item.Lng,
                            Name = item.Name,
                            Type = item.Type
                        };
                        db.Stations.Add(station);
                        db.SaveChanges();
                    }                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IEnumerable<Station>> GetAllStations(string path)
        {
            string result = null;
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            var jResult = JToken.Parse(result).ToObject<IEnumerable<Station>>();
            WriteInfoStationsToBase(jResult);
            return jResult;
        }

        public IEnumerable<StationForecast> GetStationForecast(string idStation)
        {
            string result = null;
            var path = "http://glonass.ufagortrans.ru/php/getStationForecasts.php?sid=" + idStation + "&type=0&city=ufagortrans&info=12345&_=1517558480816";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            var jResult = JToken.Parse(result).ToObject<IEnumerable<StationForecast>>();            
            return jResult;
        }
    }
}