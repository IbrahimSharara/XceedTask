using ProductCatalogWebApplication.DAL.Entities;
using System.Text.Json;

namespace ProductCatalogWebApplication.WebUI.ForDataSeeding
{
    public class Seed
    {
        public static void SeedAsyc(IApplicationBuilder applicationBuilder)
        {
            using (var service = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = service.ServiceProvider.GetService<SystemContext>();
                if (!context.Categories.Any())
                {
                    var CategoriesData = System.IO.File.ReadAllText("../ProductCatalogWebApplication.WebUI/Models/types.json");
                    var Categories = JsonSerializer.Deserialize<List<Category>>(CategoriesData);
                    context.Categories.AddRange(Categories);
                    context.SaveChanges();
                }
            }
        }
    }
}
