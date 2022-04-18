using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Countries.Models;

namespace Countries.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataProvider _dataProvider;

        private int test = 0;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dataProvider = new DataProvider();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Countries()
        {
            int x = test;
            mult();
            int y = test;
            return View();
        }

        private void mult()
        {
            test=test*test;
        }

        /// <summary>
        /// Returns Json Array of Countries
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetCountriesJson()
        {
            
            List<Country> countries = new List<Country>();
           
            try
            {
                countries = await _dataProvider.GetCountries();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error reading file", ex);
            }

            return Json(countries);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
