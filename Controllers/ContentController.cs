using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : Controller
    {
        Models.IProductRepository repository;
        public ContentController(Models.IProductRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("string")]
        public string GetString()=> "This is a string response";
        [HttpGet("object")]
        public Models.Product GetObject() => repository.Products.FirstOrDefault();

        [HttpGet("GetProduct/{id}", Name ="GetProduct")]
        public Models.Product GetObject(int id) => repository.Products.FirstOrDefault(c=>c.ProductID==id);

        [HttpGet("List",Name ="List")]
        public IEnumerable< Models.Product> GetList() => repository.Products;

        [HttpPost("Create")]
        public IActionResult Create([FromBody]Models.Product item)
        {
            repository.SaveProduct(item);
            return CreatedAtRoute("GetProduct", new { id = item.ProductID }, item);
        }
    }
}