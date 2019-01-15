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
        public async Task<ActionResult> Index()
        {
            PredictorManager manager = new PredictorManager();
            await manager.GetAllStations("http://glonass.ufagortrans.ru/php/getStations.php?city=ufagortrans&info=12345&_=1517558480807");
            return View();
        } 
    }
}