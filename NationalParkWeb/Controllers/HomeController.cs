using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NationalParkWeb.Models;
using NationalParkWeb.Models.ViewModel;
using NationalParkWeb.Repository;
using NationalParkWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INationalParkRepository _npRepo;
        private readonly ITrailRepository _trail;

        public HomeController(ILogger<HomeController> logger, INationalParkRepository npRepo, ITrailRepository trail)
        {
            _logger = logger;
            _npRepo = npRepo;
            _trail = trail;
        }

        public async Task<IActionResult> Index()
        {
            NationalParkTrailVM nationalParkTrailVM = new NationalParkTrailVM()
            {
                nationalParks = await _npRepo.GetAllAsync(StaticData.BaseUrl + StaticData.NationatiolParkURL),
                trails = await _trail.GetAllAsync(StaticData.BaseUrl + StaticData.TrailURL)

            };
            return View(nationalParkTrailVM);
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
