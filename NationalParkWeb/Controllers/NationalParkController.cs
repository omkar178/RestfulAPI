using Microsoft.AspNetCore.Mvc;
using NationalParkWeb.Models;
using NationalParkWeb.Repository.IRepository;
using System.IO;
using System.Threading.Tasks;

namespace NationalParkWeb.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _npRepo;
        public NationalParkController(INationalParkRepository npRepo)
        {
            _npRepo = npRepo;
        }
        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }

        public async Task<IActionResult> Upsert(int? Id)
        {
            NationalPark obj = new NationalPark();
            if (Id == null)
            {
                return View(obj);
            }

            obj = await _npRepo.GetAsync(StaticData.BaseUrl + StaticData.NationatiolParkURL, Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upsert(NationalPark objNationalPark)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] data = null;
                    using (var file = files[0].OpenReadStream())
                    {
                        using (var memory = new MemoryStream())
                        {
                            file.CopyTo(memory);
                            data = memory.ToArray();
                        }
                    }
                    objNationalPark.Picture = data;
                }
                else 
                {
                    var obj = await _npRepo.GetAsync(StaticData.BaseUrl + StaticData.NationatiolParkURL,objNationalPark.Id);
                    objNationalPark.Picture = obj.Picture;
                }
                if (objNationalPark.Id == 0)
                {
                    await _npRepo.CreateAsync(StaticData.BaseUrl + StaticData.NationatiolParkURL,objNationalPark);
                }
                else 
                {
                    await _npRepo.UpdateAsync(StaticData.BaseUrl + StaticData.NationatiolParkURL+objNationalPark.Id,objNationalPark);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(objNationalPark);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var obj = await _npRepo.DeleteAsync(StaticData.BaseUrl + StaticData.NationatiolParkURL, id);
            if (obj)
            {
                return Json(new { success = true,message = "Delete Successfully..." });
            }
            return Json(new { success = false, message = "Getting error while deleting.." });
        }

        public async Task<IActionResult> GetAllNationalPark()
        {
            return Json(new { data = await _npRepo.GetAllAsync(StaticData.BaseUrl + StaticData.NationatiolParkURL) });
        }
    }
}
