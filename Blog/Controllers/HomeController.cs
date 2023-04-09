using Blog.Models;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace Blog.Controllers
{
	public class HomeController : Controller
	{
        AdminManager am = new AdminManager(new EFAdminDal());
        public IActionResult Index(string f, int p = 1)
		{
            if (f == null)
                f = "";
            var admins = am.TGetList().Where(x => x.AdminName.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
            return View(admins);
        }

        [HttpPost]
        public IActionResult AddAdmin(Admin admin)
        {
            am.TAdd(admin);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAdmin(int id)
        {
            var admin = am.TGetByID(id);
            if (admin.AdminStatus == true)
                admin.AdminStatus = false;
            else
                admin.AdminStatus = true;
            am.TDelete(admin);
            return RedirectToAction("Index");
        }

		[HttpGet]
		public IActionResult EditAdmin(int id)
		{
			var value = am.TGetByID(id);
			return View(value);
		}
		[HttpPost]
		public IActionResult EditAdmin(Admin admin)
		{
			am.TUpdate(admin);
			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}