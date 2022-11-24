namespace la_mia_pizzeria_static.Models.Form
{
    public class PizzaForm
    {
        public Pizza Pizza { get; set; }

        public List<Category>? Categories { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
