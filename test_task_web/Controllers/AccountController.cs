using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test_task_web.Infrastructure.Abstract;
using test_task_web.Infrastructure.Concrete;
using test_task_web.Models.Models;

namespace test_task_web.Controllers
{
    public class AccountController : Controller
    {
        FormsAuthProvider authProvider;
            public AccountController()
            {
                authProvider = new FormsAuthProvider();
            }
            public ViewResult Login()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Login(LoginViewModel model, string returnUrl)
            {
                if (ModelState.IsValid)
                {
                    if (authProvider.Authenticate(model.UserName, model.Password))
                    {
                        return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                    }
                    else
                    {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            } 
      else 
      { 
        return View();
        }
    }
} 
}