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
                            Recipe recipe = new Recipe(int.Parse(dr["id"].ToString()),
                         dr["name"].ToString(),
                         dr["image"].ToString(),
                         dr["cookingMethod"].ToString(),
                         int.Parse(dr["time"].ToString()));
                            GetIngredientInRecipe(recipe);
                            recipes.Add(recipe);
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

        // Assign ingredients to a recipe
        private void GetIngredientInRecipe(Recipe recipe)
        {
            SqlConnection con = Connect();

            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("recipeId", recipe.Id);

            command.CommandText = "spGetIngredientInRecipe";
            command.Connection = con;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.Default))
            {
                while (dr.Read())
                {
                    recipe.AddIngredient(new Ingredient(int.Parse(dr["id"].ToString()),
                                                        dr["name"].ToString(),
                                                        dr["image"].ToString(),
                                                        int.Parse(dr["calories"].ToString())
                                         ));
                }
            }

            con.Close();
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