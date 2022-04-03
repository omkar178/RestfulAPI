using Microsoft.AspNetCore.Mvc;
using NationalParkWeb.Models;
using NationalParkWeb.Models.ViewModel;
using NationalParkWeb.Repository.IRepository;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace NationalParkWeb.Controllers
{
    public class TrailController : Controller
    {

        private readonly ITrailRepository _trail;
        private readonly INationalParkRepository _npRepo;
        public TrailController(ITrailRepository trail, INationalParkRepository npRepo)
        {
            _trail = trail;
            _npRepo = npRepo;
        }
        public IActionResult Index()
        {
            return View(new Trail());
        }
        public async Task<IActionResult> Upsert(int? Id)
        {
            IEnumerable<NationalPark> nationalsList = await _npRepo.GetAllAsync(StaticData.BaseUrl+StaticData.NationatiolParkURL);
            TrailsVM trailsVM = new TrailsVM()
            {
                NationalParkList = nationalsList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                trail = new Trail()
            };

            if (Id == null)
            {
                return View(trailsVM);
            }

            trailsVM.trail = await _trail.GetAsync(StaticData.BaseUrl + StaticData.TrailURL, Id);

            if (trailsVM.trail == null)
            {
                return NotFound();
            }
            return View(trailsVM);

        }
        [HttpPost]
        public async Task<IActionResult> Upsert(TrailsVM trailVM)
        {
            if (ModelState.IsValid)
            {
                if (trailVM.trail.Id == 0)
                {
                    await _trail.CreateAsync(StaticData.BaseUrl + StaticData.TrailURL,trailVM.trail);
                }
                else
                {
                    await _trail.UpdateAsync(StaticData.BaseUrl + StaticData.TrailURL + trailVM.trail.Id, trailVM.trail);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<NationalPark> nationalsList = await _npRepo.GetAllAsync(StaticData.BaseUrl + StaticData.NationatiolParkURL);
                TrailsVM trails = new TrailsVM()
                {
                    NationalParkList = nationalsList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    trail = trailVM.trail
                };

                return View(trails);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var obj = await _npRepo.DeleteAsync(StaticData.BaseUrl + StaticData.TrailURL, id);
            if (obj)
            {
                return Json(new { success = true, message = "Delete Successfully..." });
            }
            return Json(new { success = false, message = "Getting error while deleting.." });
        }
        public async Task<IActionResult> GetAllTrail()
        {
            return Json(new { data = await _trail.GetAllAsync(StaticData.BaseUrl + StaticData.TrailURL) });
        }
    }
}
