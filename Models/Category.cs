namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Pizza> Pizze { get; set; }
    }
}
