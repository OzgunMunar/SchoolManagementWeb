using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Web.Controllers
{
    public class DashboardController : Controller
    {

        //private readonly UserManager<ApplicationUser> userManager;

        //public DashboardController(UserManager<ApplicationUser> _userManager)
        //{
        //    userManager = _userManager;
        //}

        //[Authorize]
        //public async Task<IActionResult> Index()
        //{

        //    ApplicationUser user = await userManager.GetUserAsync(User);

        //    if (user != null)
        //    {
        //        ViewBag.User = user;
        //    }

        //    return View();
        //}

        [Authorize]
        public IActionResult Index() 
        { 
            return View(); 
        }

    }
}
