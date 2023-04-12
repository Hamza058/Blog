using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using X.PagedList;

namespace Blog.Controllers
{
    public class WritingController : Controller
    {
        WritingManager wm = new WritingManager(new EFWritingDal());

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
            var fileExtension = Path.GetExtension(file.FileName);
            var newFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine("wwwroot", "img", newFileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);
            writing.CreatedDate = DateTime.Now;
            writing.Image = newFileName;
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
        [HttpGet]
        public IActionResult EditWriting(int id)
        {
            var value = wm.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditWriting(Writing writing)
        {
            wm.TUpdate(writing);
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult Home(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var values= wm.TGetList().Where(x=>x.Heading.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
            return View(values);
        }
    }
}
