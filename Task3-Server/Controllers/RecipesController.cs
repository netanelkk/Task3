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
            try
            {
                return Ok(DatabaseMock.GetRecipes());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Add New Recipe
        public IHttpActionResult Post([FromBody] PostRecipe value)
        {
            try
            {
                DatabaseMock.AddRecipe(value);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
