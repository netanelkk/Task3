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
            try
            {
                DatabaseMock.ingredients.Add(value);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Get All Ingredients
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(DatabaseMock.ingredients);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
