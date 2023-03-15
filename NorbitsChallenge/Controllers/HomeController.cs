using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        public IActionResult EditCar(string LicensePlate, string ProductionModel, string Manufacturer, string Description, int TireCount, int companyId)
        {
            CarUpdate editCar = new CarUpdate();
            editCar.LicensePlate = LicensePlate;
            editCar.Manufacturer = Manufacturer;
            editCar.ProductionModel = ProductionModel;
            editCar.Desc = Description;
            editCar.TireCount = TireCount;
            editCar.CompanyId = companyId;

            return View(editCar);
        }

        [HttpPost]
        public IActionResult EditCar(UpdateCar editCar)
        {
            var carDb = new CarDb(_config);
            carDb.UpdateCar(editCar.LicensePlate, editCar.Manufacturer, editCar.ProductionModel, editCar.Desc, editCar.TireCount, editCar.CompanyId);
            return RedirectToAction("AllCars");
        }

        public IActionResult RemoveCar(int CompanyId, string LicensePlate)
        {
            var carDb = new CarDb(_config);
            carDb.DeleteCar(CompanyId, LicensePlate);
            return RedirectToAction("AllCars");
        }
        public IActionResult AddCar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCar(NewCar model)
        {
            var CarDb = new CarDb(_config);
            var companyModel = GetCompanyModel();
            var checkCar = CarDb.SearchCar(companyModel.CompanyId, model.LicensePlate);
            if (!String.IsNullOrEmpty(checkCar.LicensePlate))
            {
                ViewData["ErrorMessage"] = "Car already exists, Please enter a different license plate.";
                return View();
            }
            
            else if (ModelState.IsValid) { 
            
                CarDb.CreateCar(model.LicensePlate, model.Manufacturer, model.ProductionModel, model.Desc, model.TireCount,model.CompanyId);
                return RedirectToAction("Index");
            }
            return View();
        }

       //Not Implemented

        //public IActionResult SearchCar()
        //{
        //    var model = GetCompanyModel();
        //    CarDb cardb = new CarDb(_config);
            
        //    return View(model);
        //}
        //[HttpPost]
        //public IActionResult SearchCar(string LicensePlate)
        //{
        //    var model = GetCompanyModel();
        //    var CompanyId = model.CompanyId;
        //    Car searchedCar = new Car();
        //    var carDb = new CarDb(_config);
        //    var cars = carDb.GetAllCars(CompanyId);
        //    searchedCar = carDb.SearchCar(CompanyId, LicensePlate);
        //    cars.Clear();
        //    cars.Add(searchedCar);
        //    return View(cars);
        //}


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
