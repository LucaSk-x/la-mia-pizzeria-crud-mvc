using Azure;
using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        PizzeriaDbContext db;

        public PizzaController() : base()
        {
            db = new PizzeriaDbContext();
        }
        public IActionResult Index()
        {
            //PizzeriaDbContext db = new PizzeriaDbContext();
            List<Pizza> listaPizza = db.Pizze.Include(pizza => pizza.Category).ToList();
            return View(listaPizza);
        }

        public IActionResult Detail(int id)
        {
            //PizzeriaDbContext db = new PizzeriaDbContext();
            Pizza pizza = db.Pizze.Where(p => p.Id == id).Include("Category").FirstOrDefault();
            return View(pizza);
        }

        public IActionResult Create()
        {
            PizzaForm formData = new PizzaForm();

            formData.Pizza = new Pizza();
            formData.Categories = db.Categories.ToList();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientList = db.Ingredients.ToList();

            foreach (Ingredient ingredient in ingredientList)
            {
                formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
            }

            return View(formData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = db.Categories.ToList();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> tagList = db.Ingredients.ToList();

                foreach (Ingredient ingredient in tagList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
                }
                return View(formData);
            }

            formData.Pizza.Ingredients = new List<Ingredient>();

            foreach(int ingredientId in formData.SelectedIngredients)
            {
                Ingredient ingredient = db.Ingredients.Where(i => i.Id == ingredientId).FirstOrDefault();
                formData.Pizza.Ingredients.Add(ingredient);
            }

            db.Pizze.Add(formData.Pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
   

        public IActionResult Edit(int id)
        {

            Pizza pizza = db.Pizze.Where(pizza => pizza.Id == id).Include(p => p.Ingredients).FirstOrDefault();

            if (pizza == null)
                return NotFound();

            PizzaForm formData = new PizzaForm();

            formData.Pizza = pizza;
            formData.Categories = db.Categories.ToList();
            formData.Ingredients = new List<SelectListItem>();

            List<Ingredient> ingredientList = db.Ingredients.ToList();

            foreach (Ingredient ingredient in ingredientList)
            {
                formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString(), pizza.Ingredients.Any(i => i.Id == ingredient.Id)));
            }

            return View(formData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Pizza.Id = id;
                formData.Categories = db.Categories.ToList();
                formData.Ingredients = new List<SelectListItem>();

                List<Ingredient> ingredientList = db.Ingredients.ToList();

                foreach (Ingredient ingredient in ingredientList)
                {
                    formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString()));
                }

                return View(formData);
            }

            Pizza pizzaItem = db.Pizze.Where(pizza => pizza.Id == id).Include(p => p.Ingredients).FirstOrDefault();

            if (pizzaItem == null)
                return NotFound();

            pizzaItem.Name = formData.Pizza.Name;
            pizzaItem.Image = formData.Pizza.Image;
            pizzaItem.Description = formData.Pizza.Description;
            pizzaItem.CategoryId = formData.Pizza.CategoryId;

            pizzaItem.Ingredients.Clear();

            if (formData.SelectedIngredients == null)
            {
                formData.SelectedIngredients = new List<int>();
            }

            foreach (int ingredientId in formData.SelectedIngredients)
            {
                Ingredient ingredient = db.Ingredients.Where(p => p.Id == ingredientId).FirstOrDefault();
                pizzaItem.Ingredients.Add(ingredient);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Pizza pizza = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizza == null)
            {
                return NotFound();
            }

            db.Pizze.Remove(pizza);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
