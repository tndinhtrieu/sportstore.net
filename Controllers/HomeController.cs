using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index() => View(GetData(nameof(Index)));

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Users")]
        public IActionResult OtherAction() => View("Index", GetData(nameof(OtherAction)));
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Dictionary<string, object> GetData(string actionName) =>
                                          new Dictionary<string, object>
                                          {
                                              ["Action"] = actionName,
                                              ["User"] = HttpContext.User.Identity.Name,
                                              ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
                                              ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
                                              ["In Users Role"] = HttpContext.User.IsInRole("Users")
                                          };

        public IActionResult Body() => View();

        [HttpPost]
        public Product Body([FromBody]Product model) => model;

    }
}
