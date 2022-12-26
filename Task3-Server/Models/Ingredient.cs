using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task3_Server.Models
{
    public class Ingredient
    {
        private int id;
        private string name;
        private string image;
        private int calories;

        public Ingredient(int id, string name, string image, int calories)
        {
            this.id = id;
            this.name = name;
            this.image = image;
            this.calories = calories;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public int Calories { get => calories; set => calories = value; }


        public static List<Ingredient> GetIngredients()
        {
            DataServices ds = new DataServices();
            return ds.GetIngredients();
        }

        public static bool AddIngredient(Ingredient ingredint)
        {
            DataServices ds = new DataServices();
            return ds.InsertIngredient(ingredint);
        }
    }
}