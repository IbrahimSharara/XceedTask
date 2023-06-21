using ProductCatalogWebApplication.DAL.Entities;

namespace ProductCatalogWebApplication.BLL.InterFaces
{
    public interface IProductRepository : IGeneralRepository<Product>
    {
        List<Product> GetByName(string name);
    }
}
