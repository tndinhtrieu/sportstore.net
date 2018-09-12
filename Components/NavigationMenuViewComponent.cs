using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private Models.IProductRepository repository;
        public NavigationMenuViewComponent(Models.IProductRepository prod)
        {
            this.repository = prod;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products.Select(c => c.Category).Distinct().OrderBy(x => x));
        }
    }
}
