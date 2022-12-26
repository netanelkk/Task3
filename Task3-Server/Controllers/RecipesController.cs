using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task3_Server.Models;

namespace Task3_Server.Controllers
{
    public class RecipesController : ApiController
    {
        // Get All Recipes
        public IHttpActionResult Get()
        {
            List<Recipe> recipes = Recipe.GetRecipes();
            if (recipes != null)
            {
                return Ok(recipes);
            }

            return BadRequest("Error while fetching object");
        }

        // Add New Recipe
        public IHttpActionResult Post([FromBody] PostRecipe value)
        {
            if (Recipe.AddRecipe(value))
            {
                return Ok("Success");
            }

            return BadRequest("Error while inserting object");
        }
    }
}
