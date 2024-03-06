using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Ecommerce.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [DisplayName("Categories")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
    }
}