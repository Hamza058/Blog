using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using X.PagedList;

namespace Blog.Controllers
{
    public class WritingController : Controller
    {
        WritingManager wm = new WritingManager(new EFWritingDal());
		private readonly IFileProvider _fileProvider;
		public WritingController(IFileProvider fileProvider)
		{
			_fileProvider = fileProvider;
		}
		public IActionResult Index(string f, int p = 1)
        {
            if (f == null)
                f = "";
            var writings = wm.TGetList().Where(x => x.Heading.ToLower().Contains(f.ToLower())).ToPagedList(p, 10);
            return View(writings);
        }

        [HttpPost]
        public IActionResult AddWriting(Writing writing)
        {

			writing.CreatedDate = DateTime.Now;
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
    }
}
