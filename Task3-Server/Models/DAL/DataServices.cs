using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Task3_Server.Models
{
    public class DataServices
    {
        /*
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

        */

        // Add new ingredient
        public bool CreateRecipe(PostRecipe recipe)
        {
            SqlConnection con = null;
            try
            {
                con = Connect();

                int insertId = InsertRecipe(recipe, con);
                recipe.Id = insertId;
                bool result = InsertIngredientInRecipes(recipe, con);

                con.Close();
                return result;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        // Insert recipe
        private int InsertRecipe(Recipe recipe, SqlConnection con)
        {
                SqlCommand command = new SqlCommand();

                command.Parameters.AddWithValue("name", recipe.Name);
                command.Parameters.AddWithValue("image", recipe.Image);
                command.Parameters.AddWithValue("cookingMethod", recipe.CookingMethod);
                command.Parameters.AddWithValue("time", recipe.Time);

                command.CommandText = "spInsertRecipe";
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 10; // in seconds

                return (int)command.ExecuteScalar();
        }

        // Insert ingredients to recipe
        private bool InsertIngredientInRecipes(PostRecipe recipe, SqlConnection con)
        {
            SqlCommand command;

            foreach (int ingredientId in recipe.Ingredients_ids)
            {
                command = new SqlCommand();
                command.CommandText = "spIngredientInRecipes";
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 10; // in seconds

                command.Parameters.AddWithValue("recipeId", recipe.Id);
                command.Parameters.AddWithValue("ingredientId", ingredientId);

                command.ExecuteNonQuery();
            }

            return true;
        }


        // Get all recipes
        public List<Recipe> GetRecipes()
        {
            SqlConnection con = null;
            try
            {
                con = Connect();

                SqlCommand command = new SqlCommand();

                command.CommandText = "spGetRecipes";
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 10; // in seconds

                List<Recipe> recipes = new List<Recipe>();
                using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.Default))
                {
                    while (dr.Read())
                    {
                        recipes.Add(new Recipe(int.Parse(dr["id"].ToString()),
                                                 dr["name"].ToString(),
                                                 dr["image"].ToString(),
                                                 dr["cookingMethod"].ToString(),
                                                 int.Parse(dr["time"].ToString())));
                    }
                }



                con.Close();
                return recipes;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private void AssignIngredientsToRecipe(Recipe recipe, SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "spGetIngredientInRecipe";
            command.Connection = con;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.Default))
            {
                while (dr.Read())
                {
                    recipe.Ingredients.Add(int.Parse(dr["ingredientId"].ToString()));
                }
            }
        }

        // Add new ingredient
        public bool InsertIngredient(Ingredient ingredient)
        {
            SqlConnection con = null;
            try
            {
                con = Connect();
                SqlCommand command = new SqlCommand();

                command.Parameters.AddWithValue("name", ingredient.Name);
                command.Parameters.AddWithValue("image", ingredient.Image);
                command.Parameters.AddWithValue("calories", ingredient.Calories);

                command.CommandText = "spInsertIngredient";
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 10; // in seconds

                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        // Fetch all ingredients
        public List<Ingredient> GetIngredients()
        {
            SqlConnection con = null;
            try
            {
                con = Connect();

                SqlCommand command = new SqlCommand();

                command.CommandText = "spGetIngredients";
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 10; // in seconds

                List<Ingredient> ingredients = new List<Ingredient>();
                using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.Default))
                {
                    while (dr.Read())
                    {
                        ingredients.Add(new Ingredient(int.Parse(dr["id"].ToString()),
                                                 dr["name"].ToString(),
                                                 dr["image"].ToString(),
                                                 int.Parse(dr["calories"].ToString())));
                    }
                }
                con.Close();
                return ingredients;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private SqlConnection Connect()
        {


            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();

            return con;

        }

    }
}