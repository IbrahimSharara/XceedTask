using Microsoft.EntityFrameworkCore;
using ProductCatalogWebApplication.BLL.InterFaces;
using ProductCatalogWebApplication.DAL.Entities;

namespace ProductCatalogWebApplication.BLL.Repositories
{
    public class ProductRepository : GeneralRepository<Product>, IProductRepository
    {
        public ProductRepository(SystemContext dB) : base(dB)
        {
        }

        public List<Product> GetByName(string name)
        {
            var products = DB.Products.Where(x => x.Name.StartsWith(name)).ToList();
            return products;
        }

        public List<Product> ProductsWithCategory()
        {
            var products = DB.Products.Include(x=>x.Category).ToList();
            return products;
        }
    }
}
