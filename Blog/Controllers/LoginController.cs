using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Controllers
{
    public class LoginController : Controller
    {
        AdminManager am = new AdminManager(new EFAdminDal());

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(Admin admin)
        {
            var value = am.TGetList().FirstOrDefault(x => x.AdminName == admin.AdminName && x.AdminPassword == admin.AdminPassword && x.AdminStatus);

            if (value !=null)
            {
                HttpContext.Session.SetString("AdminName", admin.AdminName);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, value.AdminName)
                };
                var useridentity = new ClaimsIdentity(claims, "A");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Wrong username or password";
                return View();
            }
        }
    }
}
