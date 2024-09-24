using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Web.Controllers
{
    public class DashboardController : Controller
    {

        [Authorize(Roles = "Admin")]
        public IActionResult Index() 
        { 
            return View(); 
        }

    }
}
