using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogWebApplication.BLL.InterFaces;
using ProductCatalogWebApplication.DAL.Entities;
using ProductCatalogWebApplication.ViewModels.ViewModels;

namespace ProductCatalogWebApplication.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IProductRepository Product { get; }
        public ICategoryRepository Category { get; }
        public UserManager<ApplicationUser> _userManager { get; }

        public ProductController(IProductRepository product, ICategoryRepository category , UserManager<ApplicationUser> user)
        {
            Product = product;
            Category = category;
            _userManager = user;
        }

        public async Task<IActionResult> Details(int id)
        {
            var product =await Product.GetByID(id);
            return View(product);
        }

            public IActionResult Index()
        {
            var model = new ForAllProducts
            {
                Products = Product.ProductsWithCategory(),
                Categories = Category.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(Product tbl)
        {
            tbl.CreationDate = DateTime.Now;
            tbl.StartDate = DateTime.Now;
            tbl.Details = "";
            string? userName = GetCurrentUserAsync().Result.UserName;
            tbl.CreatedBy = userName?? "";
            tbl.LastUpdatedBy = userName?? "";
            await Product.Add(tbl);
            return Redirect("/EditProduct/" + tbl.Id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Route("/EditProduct/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            EditProduct model = new EditProduct
            {
                Categories = Category.GetAll(),
                Product = await Product.GetByID(id)
            };
            return View(model);
        }
        [HttpPost]
        [Route("/EditProduct")]
        public async Task<IActionResult> Edit(Product tbl)
        {
            var old = await Product.GetByID(tbl.Id);
            old.Name = tbl.Name;
            old.Price = tbl.Price;
            old.StartDate = tbl.StartDate;
            old.Duration = tbl.Duration;
            old.CategoryId = tbl.CategoryId;
            old.Details = tbl.Details;
            string? userName = GetCurrentUserAsync().Result.UserName;
            old.LastUpdatedBy = userName ?? "";
            await Product.Update(old);
            return Redirect("/EditProduct/"+tbl.Id);
        }
        
        public async Task<IActionResult> ProductImage(IFormFile image , int id)
        {
            var old =await Product.GetByID(id);
            if(image != null)
            {
                string imageName = image.FileName;
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName);
                image.CopyTo(new FileStream(imagePath, FileMode.Create));
                old.Image = imageName;
                string? userName = GetCurrentUserAsync().Result.UserName;
                old.LastUpdatedBy = userName ?? "";
                await Product.Update(old);
            }
            
            return Redirect("/EditProduct/" + old.Id);
        }

        public async Task<IActionResult> ProductName(string name)
        {
            var products = Product.GetByName(name);
            return PartialView("_ProductsPartial" , products);
        }


        }
}
