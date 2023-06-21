using Microsoft.AspNetCore.Mvc;
using ProductCatalogWebApplication.BLL.InterFaces;
using ProductCatalogWebApplication.DAL.Entities;
using ProductCatalogWebApplication.WebUI.ForDataSeeding;
using System.Text.Json;

namespace ProductCatalogWebApplication.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(ICategoryRepository category)
        {
            Category = category;
        }

        public ICategoryRepository Category { get; }

        public IActionResult Index()
        {
            var model = Category.GetAll();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory(Category tbl)
        {
            await Category.Add(tbl);
            return Redirect("/EditCategory/" + tbl.Id);
        }

        [Route("/EditCategory/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await Category.GetByID(id);
            return View(model);
        }
        [HttpPost]
        [Route("/EditCategory")]
        public async Task<IActionResult> Edit(Category tbl)
        {
            var old = await Category.GetByID(tbl.Id);
            old.Name = tbl.Name;
            await Category.Update(old);
            return View(old);
        }

        public IActionResult ByCategory(int id)
        {
            var products = Category.GetProductByCategory(id);
            return PartialView("_ProductsPartial" , products);
        }
    }
}
