using PredictorBusesArrival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PredictorBusesArrival.Controllers
{
    public class HomeController : Controller
    {
        private BusStopsDataBaseEntities2 db;
        private static PredictorManager manager;
        private static List<Station> stations;

        public static ShowPredictBusStopViewModel ViewModel { get; set; }

        private void Initializate()
        {
            db = new BusStopsDataBaseEntities2();
            stations = db.Stations.ToList();
            manager = new PredictorManager();
        }

        public async Task<ActionResult> Index()
        {
            Initializate();
            ViewModel = new ShowPredictBusStopViewModel(stations, 0, manager);
            return View();
        }

        public PartialViewResult SelectBusArrival(string parametr)
        {
            var id = parametr.Split(';')[0];
            ViewModel.SelectBusStop(Convert.ToInt32(id), stations, manager);
            return PartialView(ViewModel.StationForecasts.ToList());
        }
    }
}