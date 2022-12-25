using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task3_Server.Models
{
    /*
     * Recipe object that came from client
     * Essential since the client sending recipe with ingredients ids
     */

    public class PostRecipe : Recipe
    {
        private int[] ingredients_ids;

        public PostRecipe(string name, string image, string cookingMethod, int time, int[] ingredients) :base(0, name,image,cookingMethod,time)
        {
            ingredients_ids = ingredients;
        }

        public int[] Ingredients_ids { get => ingredients_ids; set => ingredients_ids = value; }
    }

}