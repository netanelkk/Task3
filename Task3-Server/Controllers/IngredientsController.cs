using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task3_Server.Models;

namespace Task3_Server.Controllers
{
    public class IngredientsController : ApiController
    {
        // Add New Ingredient
        public IHttpActionResult Post([FromBody] Ingredient value)
        {
            if(Ingredient.AddIngredient(value))
            {
                return Ok("Success");
            }

            return BadRequest("Error while inserting object");
        }

        // Get All Ingredients
        public IHttpActionResult Get()
        {
            List<Ingredient> ingredients = Ingredient.GetIngredients();
            if(ingredients != null)
            {
                return Ok(ingredients);
            }

            return BadRequest("Error while fetching object");
        }
    }
}
