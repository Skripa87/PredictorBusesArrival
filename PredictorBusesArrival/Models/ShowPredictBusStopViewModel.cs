using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PredictorBusesArrival.Models
{
    public class ShowPredictBusStopViewModel
    {
        public SelectList BusStops { get; set; }
        public IEnumerable<StationForecast> StationForecasts { get; set; }
        public Station SelectedBusStop { get; set; }
        
        public ShowPredictBusStopViewModel(List<Station> stations, int selectedItemId, PredictorManager manager)
        {
            var list = new List<SelectListItem>(); 
            foreach (var item in stations)
            {
                var listItem = new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                };
                if (item.Id == selectedItemId)
                {
                    listItem.Selected = true;
                }
                list.Add(listItem);
            }
            if (!list.Any(a => a.Selected))
            {
                list[0].Selected = true;
            }
            SelectedBusStop = stations.Find(f => f.Id == selectedItemId) 
                            ?? stations.FirstOrDefault();
            StationForecasts = manager.GetStationForecast(selectedItemId.ToString()).Result;
        }

        public void SelectBusStop(int selectedItemId, List<Station> stations, PredictorManager manager)
        {
            SelectedBusStop = stations.Find(f => f.Id == selectedItemId);
            if (((List<SelectListItem>) BusStops.Items).Any(a => a.Value == selectedItemId.ToString()))
            {
                ((List<SelectListItem>) BusStops.Items).FindAll(a => a.Value == selectedItemId.ToString())[0].Selected = true;
            }
            StationForecasts = manager.GetStationForecast(selectedItemId.ToString()).Result;
        }

    }
}