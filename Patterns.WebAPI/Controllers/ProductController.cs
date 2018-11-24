using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Patterns.WebAPI.Models;

namespace Patterns.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController: ControllerBase
    {
        private static readonly IList<Product> ProductCollection = new List<Product>();
        
        /// <summary>
        /// Select all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public IActionResult GetAll()
        {
            if (ProductCollection.Any())
                return Ok(ProductCollection);
            
            return NoContent();
        }
        
        /// <summary>
        /// Select a single product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "GetById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetById([FromRoute]string id)
        {
            //Avoiding invalid id to call database
            if (string.IsNullOrEmpty(id)) 
                return BadRequest();            
            
            var product = ProductCollection.SingleOrDefault(pdt => pdt.Id == id);
            if (product != null)
                return Ok(product);

            return NotFound();
        }
        
        /// <summary>
        /// Creates a single product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult Register([FromBody] Product product)
        {
            ProductCollection.Add(product);
            return Created(nameof(GetById), product);
        }

        /// <summary>
        /// Updates a single product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update([FromRoute]string id, [FromBody] Product product)
        {
            //Avoiding invalid id to call database
            if (string.IsNullOrEmpty(id)) 
                return BadRequest();
            
            if (ProductCollection.All(pdt => pdt.Id != id))
                return NotFound();

            var index = ProductCollection.IndexOf(ProductCollection.SingleOrDefault(pdt => pdt.Id == id));
            ProductCollection[index] = product.WithId(id);
            return Accepted(product);
        }
        
                
        /// <summary>
        /// Removes a single product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Delete([FromRoute]string id)
        {
            //Avoiding invalid id to call database
            if (string.IsNullOrEmpty(id)) 
                return BadRequest();
            
            var product = ProductCollection.SingleOrDefault(pdt => pdt.Id == id);
            if (product is null) 
                return NotFound();
            
            ProductCollection.Remove(product);
            return Ok();
        }
    }
}