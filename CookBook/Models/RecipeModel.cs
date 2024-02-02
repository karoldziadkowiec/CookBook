using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Models
{
    [Table("Recipes")]
    public class RecipeModel
    {
        [Key]
        public int RecipeID { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name field is required!")]
        [MaxLength(50)]
        public string Name { get; set; }
        [DisplayName("Overall time")]
        [MaxLength(20)]
        public string Time { get; set; }
        [DisplayName("Ingredients")]
        [MaxLength(500)]
        public string Ingredients { get; set; }
        [DisplayName("Preparation")]
        [MaxLength(1000)]
        public string Preparation { get; set; }
        [DisplayName("Followed")]
        public bool IsFollowed { get; set; }
    }
}
