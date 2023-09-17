using System;
using System.Linq;
using System.Web.Mvc;
using MRIAMS.Models;
using System.Web.Security;
using System.Linq;

namespace MRIAMS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Implement user authentication logic here
                var user = Verify(model.UserName, model.Password);

                if (user != null)
                {
                    // Authentication successful, create a forms authentication ticket
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    // Redirect to the user's dashboard or another page
                    return RedirectToAction("Dashboard", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View(model);
        }

        private User Verify(string username, string password)
        {
            using (var dbContext = new DbContext())
            {
                // Attempt to find a user with the provided username
                var user = dbContext.Users.FirstOrDefault(u => u.UserName == username);

                if (user != null)
                {
                    if (user.Password == password)
                    {
                        // Authentication successful; return the user object
                        return user;
                    }
                }

                // If authentication fails or user not found, return null
                return null;
            }
        }


        // GET: Logout
        public ActionResult Logout()
        {
            // Sign out the user and redirect to the login page
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
