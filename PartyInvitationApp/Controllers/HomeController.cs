using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PartyInvitationApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            if (Request.Cookies["FirstVisit"] == null)
            {
                Response.Cookies.Append("FirstVisit", DateTime.Now.ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1) 
                });
                ViewBag.FirstVisitMessage = "Welcome! This is your first time using the app.";
            }
            else
            {
                ViewBag.FirstVisitMessage = $"Welcome back! You first visited on {Request.Cookies["FirstVisit"]}.";
            }

            return View();
        }
    }
}
