using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class LoginController : Controller
    {
        AdminManager am = new AdminManager(new EFAdminDal());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Admin admin)
        {
            var value = am.TGetList().Where(x => x.AdminName == admin.AdminName && x.AdminPassword == admin.AdminPassword);
            if (value.Any())
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Message = "Wrong username or password";
                return View();
            }
        }
    }
}
