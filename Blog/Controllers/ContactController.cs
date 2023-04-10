using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Blog.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EFContactDal());

        public IActionResult Index(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var values = cm.TGetList().Where(x => x.FullName.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
            return View(values);
        }
        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Home(Contact contact)
        {
            contact.CreatedDate = DateTime.Now;
            cm.TAdd(contact);
            return RedirectToAction("Home","Writing");
        }
    }
}
