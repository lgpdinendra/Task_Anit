using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task1.Services;

namespace Task1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController()
        {
            _userService = new UserService();
        }

         //Display Data Details 
        public async Task<ActionResult> Index()
        {
            try
            {
                var user = await _userService.GetRandomUser();

                // Validate user data
                if (user == null || string.IsNullOrEmpty(user.Email))
                {
              
                    return View("Error", new HandleErrorInfo(new Exception("Invalid user data"), "Home", "Index"));
                }

                return View(user);
            }
            catch (Exception ex)
            {
                
                return View("Error", new HandleErrorInfo(ex, "Home", "Index"));
            }
        }
    }

}