using ProductCatalogWebApplication.DAL.Entities;

namespace ProductCatalogWebApplication.BLL.InterFaces
{
    public interface ICategoryRepository : IGeneralRepository<Category>
    {
        List<Product> GetProductByCategory(int id);
    }
}
