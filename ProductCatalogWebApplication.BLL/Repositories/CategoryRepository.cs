using ProductCatalogWebApplication.BLL.InterFaces;
using ProductCatalogWebApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogWebApplication.BLL.Repositories
{
    public class CategoryRepository : GeneralRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SystemContext dB) : base(dB)
        {
        }

        public List<Product> GetProductByCategory(int id)
        {
            var products = DB.Products.Where(x => x.CategoryId == id).ToList();
            return products;
        }
    }
}
