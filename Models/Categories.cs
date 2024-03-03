using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category
    {
        [Key]
        public int Id {get;set; }
        [Required(ErrorMessage = "Field Must Not be Empty")]
        [RegularExpression(@"^[a-zA-Z]\D+$",ErrorMessage = "Field only accepts Letters")]
        [DisplayName("Category Name")]
        public string? Name {get;set;}
        [Required(ErrorMessage = "Field Must Not be Empty")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Field does not accept Negative Numbers")]
        [DisplayName("Display Name")]
        public int DisplayOrder { get; set; }
    }
}