using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo nome non può essere vuoto")]
        public string Name { get; set; }

        [Column(TypeName = "text")]

        [Required(ErrorMessage = "Il campo Descrizione non può essere vuoto")]
        public string Description { get; set; }

        //[Range(Int32.MinValue, 1, ErrorMessage ="Il campo deve essere minimo di 1")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Il campo Immagine non può essere vuoto")]
        public string Image { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public Pizza()
        {

        }

        public Pizza(string name, string description, double price, string image)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Image = image;
        }
    }
}
