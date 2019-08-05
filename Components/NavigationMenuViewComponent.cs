using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private Models.ICategoryRepository repository;
        public NavigationMenuViewComponent(Models.ICategoryRepository prod)
        {
            this.repository = prod;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategoryID = RouteData?.Values["categoryID"];
            return View(repository.Categories.Where(c => c.IsDelete == false));
        }
    }
}
