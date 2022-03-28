using Microsoft.AspNetCore.Mvc;
using NationalParkWeb.Repository.IRepository;

namespace NationalParkWeb.Controllers
{
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trail;
        public TrailController(ITrailRepository trail)
        {
            _trail = trail;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
