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
        private BusStopsDataBaseEntities db;
        private static PredictorManager manager;
        private static List<Station> stations;

        public static ShowPredictBusStopViewModel ViewModel { get; set; }

        private void Initializate()
        {
            db = new BusStopsDataBaseEntities();
            stations = db.Stations.ToList();
            manager = new PredictorManager();
        }

        public async Task<ActionResult> Index()
        {
            Initializate();
            //var result = manager.GetAllStations("http://glonass.ufagortrans.ru/php/getStations.php?city=ufagortrans&info=12345&_=1517558480807");
            var stationsActive = stations.FindAll(s => s.Active == true);
            ViewModel = new ShowPredictBusStopViewModel(stationsActive, 548, manager);
            return View(ViewModel);
        }

        public PartialViewResult SelectBusArrival(string parametr)
        {
            var id = parametr.Split(';')[0];
            ViewModel.SelectBusStop(Convert.ToInt32(id), stations, manager);
            return PartialView(ViewModel.StationForecasts.ToList());
        }
    }
}