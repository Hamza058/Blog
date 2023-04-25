using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Text;
using System.Xml.Linq;
using X.PagedList;

namespace Blog.Controllers
{
    public class WritingController : Controller
    {
        WritingManager wm = new WritingManager(new EFWritingDal());
        AboutManager am = new AboutManager(new EFAboutDal());

        public IActionResult Index(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var writings = wm.TGetList().Where(x => x.Heading.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
            return View(writings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddWriting(Writing writing, IFormFile file)
        {
            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine("wwwroot", "img", newFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                writing.Image = newFileName;
            }
            writing.CreatedDate = DateTime.Now;
            writing.Status = true;
            wm.TAdd(writing);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteWriting(int id)
        {
            var writing = wm.TGetByID(id);
            if (writing.Status == true)
                writing.Status = false;
            else
                writing.Status = true;
            wm.TDelete(writing);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteSqlWriting(int id)
        {
            var writing = wm.TGetByID(id);
            var filePath = Path.Combine("wwwroot", "img", writing.Image);
            System.IO.File.Delete(filePath);
            wm.TDeleteSql(writing);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditWriting(int id)
        {
            var value = wm.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditWriting(Writing writing, IFormFile file)
        {
            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine("wwwroot", "img", newFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                writing.Image = newFileName;
            }
            wm.TUpdate(writing);
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult Home(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var values = wm.TGetList().Where(x => x.Heading.ToLower().Contains(f.ToLower()) && x.Status).OrderByDescending(x=>x.CreatedDate).ToPagedList(p, 10);
            ViewBag.about = am.TGetList().First();
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult Single(int id)
        {
            var value = wm.TGetByID(id);
            ViewBag.about = am.TGetList().First();
            return View(value);
        }
    }
}
