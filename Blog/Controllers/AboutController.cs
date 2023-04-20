using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AboutController : Controller
    {
        AboutManager am = new AboutManager(new EFAboutDal());
        public IActionResult Index()
        {
            var value = am.TGetList().First();
            return View(value);
        }

        [AllowAnonymous]
        public IActionResult Home()
        {
            var value = am.TGetList().First();
            return View(value);
        }

        [HttpPost]
        public IActionResult EditAbout(About about)
        {
            am.TUpdate(about);
            return Json(new { IsSuccess = "true" });
        }
    }
}
