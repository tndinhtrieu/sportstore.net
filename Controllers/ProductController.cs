﻿using System;
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
        public ViewResult List(int categoryID, int productPage = 1)
        {
            ProductsListViewModel productsListViewModel = new ProductsListViewModel();
            var datatemplate = productRepository.Products
                                                .Where(p => categoryID == 0 || p.CategoryID == categoryID)
                                                .OrderBy(p => p.ProductID);
            productsListViewModel.Products = datatemplate.Skip((productPage - 1) * PageSize).Take(PageSize);
            productsListViewModel.PagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = datatemplate.Count()
            };
            productsListViewModel.CurrentCategoryID = categoryID;
            return View(productsListViewModel);
        }



        [HttpGet]
        public ProductsListViewModel ProductList(int categoryID, int productPage = 1)
        {
            ProductsListViewModel productsListViewModel = new ProductsListViewModel();
            var datatemplate = productRepository.Products
                                                .Where(p => categoryID == 0 || p.CategoryID == categoryID)
                                                .OrderBy(p => p.ProductID);
            productsListViewModel.Products = datatemplate.Skip((productPage - 1) * PageSize).Take(PageSize);
            productsListViewModel.PagingInfo = new PagingInfo()
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = datatemplate.Count()
            };
            productsListViewModel.CurrentCategoryID = categoryID;
            return productsListViewModel;
        }
        //[Route("/Product/GetPstring/{id}")]
        [HttpGet("[controller]/[action]/{id}")]
        public Models.Product GetPstring(int id)
        {
            return productRepository.Products.FirstOrDefault(c => c.ProductID == id);
        }

    }
}