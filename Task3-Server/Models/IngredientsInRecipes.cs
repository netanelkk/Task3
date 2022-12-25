using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task3_Server.Models
{
    public class IngredientsInRecipes
    {
        private int recipeId;
        private int ingredientId;

        public IngredientsInRecipes(int recipeId, int ingredientId)
        {
            this.recipeId = recipeId;
            this.ingredientId = ingredientId;
        }

        public int RecipeId { get => recipeId; set => recipeId = value; }
        public int IngredientId { get => ingredientId; set => ingredientId = value; }
    }
}