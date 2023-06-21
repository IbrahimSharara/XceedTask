﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogWebApplication.DAL.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
