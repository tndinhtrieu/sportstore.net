using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    
    public class ProductController : Controller
    {
        public Models.IProductRepository productRepository;
        public int PageSize = 4;
        public ProductController(Models.IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public ViewResult List(string category, int productPage = 1)
        {
            ProductsListViewModel productsListViewModel = new ProductsListViewModel();
            var datatemplate = productRepository.Products
                                                .Where(p => category == null || p.Category == category)
                                                .OrderBy(p => p.ProductID);
            productsListViewModel.Products = datatemplate.Skip((productPage - 1) * PageSize).Take(PageSize);
            productsListViewModel.PagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = datatemplate.Count()
            };
            productsListViewModel.CurrentCategory = category;
            return View(productsListViewModel);
        }


        [HttpGet]
        public ProductsListViewModel ProductList(string category, int productPage = 1)
        {
            ProductsListViewModel productsListViewModel = new ProductsListViewModel();
            var datatemplate = productRepository.Products
                                                .Where(p => category == null || p.Category == category)
                                                .OrderBy(p => p.ProductID);
            productsListViewModel.Products = datatemplate.Skip((productPage - 1) * PageSize).Take(PageSize);
            productsListViewModel.PagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = datatemplate.Count()
            };
            productsListViewModel.CurrentCategory = category;
            return productsListViewModel;
        }
    }
}