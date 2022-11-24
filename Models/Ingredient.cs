using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome non può essere oltre i 50 caratteri")]
        public string Name { get; set; }
        public List<Pizza>? Pizze { get; set; }

    }
}
