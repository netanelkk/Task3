﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task3_Server.Models
{
    public class Recipe
    {
        private static int index = 1;
        private int id;
        private string name;
        private string image;
        private string cookingMethod;
        private int time;
        private List<Ingredient> ingredients;

        public Recipe(string name, string image, string cookingMethod, int time) : this(index++, name, image, cookingMethod, time)
        {

        }

        public Recipe(int id, string name, string image, string cookingMethod, int time) 
        {
            this.id = id;
            this.name = name;
            this.image = image;
            this.cookingMethod = cookingMethod;
            this.time = time;
            ingredients = new List<Ingredient>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
        }

        public void ClearIngredients()
        {
            ingredients.Clear();
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string CookingMethod { get => cookingMethod; set => cookingMethod = value; }
        public int Time { get => time; set => time = value; }
        public List<Ingredient> Ingredients { get => ingredients; }
    }
}