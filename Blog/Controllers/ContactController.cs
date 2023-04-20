using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Blog.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EFContactDal());
        AboutManager am = new AboutManager(new EFAboutDal());

        public IActionResult Index(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var values = cm.TGetList().Where(x => x.FullName.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
            return View(values);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Home()
        {
            ViewBag.about=am.TGetList().First();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Home(Contact contact)
        {
            contact.CreatedDate = DateTime.Now;
            cm.TAdd(contact);
            return RedirectToAction("Home","Writing");
        }
        [HttpGet]
        public IActionResult GetInfoContact(int id)
        {
            var value = cm.TGetByID(id);
            return Json(value);
        }
    }
}
