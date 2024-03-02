using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Category
{
    public class Category
    {
        [Key]
        public int Id {get;set; }
        [Required(ErrorMessage ="")]
        [RegularExpression(@"^[a-zA-Z]\D+$",ErrorMessage ="")]
        [DisplayName("Category Name")]
        public string Name {get;set;}
        [Required(ErrorMessage ="")]
        [Range(1, Int32.MaxValue, ErrorMessage ="")]
        [DisplayName("Display Name")]
        public int DisplayOrder { get; set; }
    }
}