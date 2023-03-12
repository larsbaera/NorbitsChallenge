using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            //Henter Company Model og Company Name
            var model = GetCompanyModel();
            //var CompanyId = model.CompanyId;
            //string licensePlate = "SU84886";
            //var CarDb = new CarDb(_config);
            // add a method to request data from server
            //var car = CarDb.GetCarMakeModel(CompanyId, licensePlate );

            //model.MakeModel = car;

            return View(model);

        }

        [HttpPost]
        public JsonResult Index(int companyId, string licensePlate)
        {
            var tireCount = new CarDb(_config).GetTireCount(companyId, licensePlate);
            var model = GetCompanyModel();

            model.TireCount = tireCount;

            return Json(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AllCars()
        {

            var model = GetCompanyModel();
            var CompanyId = model.CompanyId;
            var CarDb = new CarDb(_config);
            // add a method to request data from server
            var cars = CarDb.GetAllCars(CompanyId);

            model.Cars = cars;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Her bør det

        private HomeModel GetCompanyModel()
        {
            //Setter CompanyID som 1 eller henter den aktuelle instillingen
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            //henter navnet som er linket til Det aktuelle CompanyID variablen.
            var companyName = new SettingsDb(_config).GetCompanyName(companyId);
            //returner ny HomeModel Objekt med de aktuelle verdiene satt.
            return new HomeModel { CompanyId = companyId, CompanyName = companyName };
        }
    }
}
