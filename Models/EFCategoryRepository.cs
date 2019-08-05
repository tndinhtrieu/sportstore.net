using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext context;
        public EFCategoryRepository(ApplicationDbContext ctx)
        {
            context = ctx;
            Categories = context.Categories;
        }
        public IQueryable<Category> Categories { get; }

    }
}
