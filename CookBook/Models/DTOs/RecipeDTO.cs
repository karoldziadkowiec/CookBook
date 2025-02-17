namespace CookBook.Models
{
    public class RecipeDTO
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public bool IsFollowed { get; set; }
    }
}