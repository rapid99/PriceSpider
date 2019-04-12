using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceSpider.Web.Interfaces;
using PriceSpider.Web.Models;
using PriceSpider.Web.Services;

namespace PriceSpider.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFergusonService _fergusonService;
        private readonly ISupplyHouseService _supplyHouseService;

        public HomeController(IFergusonService fergusonService, ISupplyHouseService supplyHouseService)
        {
            _fergusonService = fergusonService;
            _supplyHouseService = supplyHouseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(ProductModel model)
        {
            var results = new List<ProductModel>();
            var fergusonResult = _fergusonService.Search(model.ManufactoringID).Result;
            var supplyHouseResult = _supplyHouseService.Search(model.ManufactoringID).Result;

            foreach(var f in fergusonResult)
            {
                f.ManufactoringID = model.ManufactoringID;
                results.Add(f);
            }

            foreach (var s in supplyHouseResult)
                results.Add(s);


            return View("Results", results);
        }

        public IActionResult Results(List<ProductModel> model)
        {                      
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
