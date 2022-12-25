using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task3_Server.Models
{
    static public class DatabaseMock
    {
        public static List<Ingredient> ingredients = new List<Ingredient>()
        {
            new Ingredient("Flour", "https://res.cloudinary.com/shufersal/image/upload/f_auto,q_auto/v1551800922/prod/product_images/products_large/HIF56_L_P_7296073021568_1.png", 465),
            new Ingredient("Bread Crumbs", "https://res.cloudinary.com/shufersal/image/upload/f_auto,q_auto/v1551800922/prod/product_images/products_large/RXI60_L_P_7296073081449_1.png", 230),
            new Ingredient("Chicken Breast", "https://d3m9l0v76dty0.cloudfront.net/system/photos/4825229/large/e69015678acf1306cbdb280703488336.jpg", 344),
        };

        private static List<Recipe> recipes = new List<Recipe>()
         {
            new Recipe("Schnizel", "https://www.tavshilim.co.il/wp-content/uploads/2016/03/IMG_7878.jpg", "Frying", 30)
         };
        

        private static List<IngredientsInRecipes> ingredientsInRecipes = new List<IngredientsInRecipes>()      
         {
            new IngredientsInRecipes(recipes[0].Id, ingredients[0].Id),
            new IngredientsInRecipes(recipes[0].Id, ingredients[1].Id),
            new IngredientsInRecipes(recipes[0].Id, ingredients[2].Id)
        };


        // Adding ingredients to every recipe, and returning list of all recipes
        public static List<Recipe> GetRecipes()
        {
            foreach (Recipe recipe in recipes)
            {
                recipe.ClearIngredients(); // To avoid duplications
                foreach(IngredientsInRecipes ir in ingredientsInRecipes)
                {
                    if(ir.RecipeId == recipe.Id)
                    {
                        recipe.AddIngredient(GetIngredientById(ir.IngredientId));
                    }
                }
            }
            return recipes;
        }

        // Adding new recipe object, and ingredients in that recipe
        public static void AddRecipe(PostRecipe postrecipe)
        {
            Recipe newrecipe = new Recipe(postrecipe.Name,
                       postrecipe.Image,
                       postrecipe.CookingMethod,
                       postrecipe.Time);
            recipes.Add(newrecipe);

            for (int i = 0; i < postrecipe.Ingredients_ids.Length; i++)
            {
                ingredientsInRecipes.Add(new IngredientsInRecipes(newrecipe.Id, postrecipe.Ingredients_ids[i]));
            }
        }

        private static Ingredient GetIngredientById(int id)
        {
            foreach(Ingredient ing in ingredients)
            {
                if(ing.Id == id)
                {
                    return ing;
                }
            }
            return null;
        }


    }
}