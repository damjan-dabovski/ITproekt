using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITproekt.Models {
    public class Product {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}