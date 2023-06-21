using ProductCatalogWebApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogWebApplication.ViewModels.ViewModels
{
    public class ForAllProducts
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
