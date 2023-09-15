using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Models
{
    [Table("Recipes")]
    public class RecipeModel
    {
        [Key]
        public int recipeId { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name field is required!")]
        [MaxLength(50)]
        public string name { get; set; }
        [DisplayName("Overall time")]
        [MaxLength(20)]
        public string time { get; set; }
        [DisplayName("Ingredients")]
        [MaxLength(500)]
        public string ingredients { get; set; }
        [DisplayName("Preparation")]
        [MaxLength(1000)]
        public string preparation { get; set; }

    }
}
