using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeShopAPI.Models;
using CoffeeShopAPI.Repository;
using CoffeeShopAPI.Exceptions;
using Microsoft.Extensions.Logging;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CoffeeShopsController : ControllerBase
    {
        private readonly ICoffeeShopAsyncRepository _repository;
        private readonly ILogger _logger;

        public CoffeeShopsController(ICoffeeShopAsyncRepository coffeeShopRepository, ILogger<CoffeeShopsController> logger)
        {
            _repository = coffeeShopRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get all Coffee Shop Items.
        /// </summary>
        /// <response code="200">Returns all coffee shop items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CoffeeShop>>> GetCoffeeShops()
        {
            _logger.LogInformation($"Getting all coffee shop items");
            
            IEnumerable<CoffeeShop> coffeeShops = await _repository.GetAll();
            _logger.LogInformation($"Returned all Shop Items");

            return Ok(coffeeShops);
        }

        /// <summary>
        /// Get a specific Coffee Shop Item.
        /// </summary>
        /// <param name="id"></param> 
        /// <response code="200">Returns the coffee shop item from specific id</response>
        /// <response code="404">Not found the coffee shop item from specific id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CoffeeShop>> GetCoffeeShop(long id)
        {
            _logger.LogInformation($"Getting coffee shop item with id: {id}");
            CoffeeShop coffeeShop = await _repository.GetById(id);

            if (coffeeShop != null)
            {
                _logger.LogInformation($"Returning coffee shop item: {coffeeShop.ToString()}");
                return Ok(coffeeShop);
            }

            _logger.LogInformation($"Not found coffee shop item: {id}");
            return NotFound();
        }

        /// <summary>
        /// Creates a Coffee Shop Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///{
        ///    "id": 1,
        ///    "name": "Café Saci",
        ///    "description": "Cafeteria",
        ///    "address": "Av Folclore",
        ///    "webSiteURL": "www.cafetando.com"
        ///}
        /// </remarks>
        /// <param name="coffeeShop"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created Coffee Shop Item</response>
        /// <response code="400">If the coffee shop item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CoffeeShop>> PostCoffeeShop(CoffeeShop coffeeShop)
        {
            _logger.LogInformation($"Creating coffee shop item: {coffeeShop.ToString()}");
            await _repository.AddCoffeeShop(coffeeShop);
            _logger.LogInformation($"Created coffee shop item: {coffeeShop.ToString()}");
            return CreatedAtAction(nameof(GetCoffeeShop), new { id = coffeeShop.Id }, coffeeShop);
        }

        /// <summary>
        /// Updates a specific Coffee Shop Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///{
        ///    "id": 1,
        ///    "name": "Café Saci",
        ///    "description": "Cafeteria",
        ///    "address": "Av Folclore",
        ///    "webSiteURL": "www.cafetando.com"
        ///}
        /// </remarks>
        /// <param name="id"></param> 
        /// <param name="coffeeShop"></param> 
        /// <response code="204">Updates the coffee shop item from specific id</response>
        /// <response code="404">Not found the coffee shop item from specific id</response>
        /// <response code="400">Id from path different from body item</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCoffeeShop(long id, CoffeeShop coffeeShop)
        {
            _logger.LogInformation($"Updating coffee shop item with id: {id} and body item: {coffeeShop.ToString()}");
            try
            {
                if (id != coffeeShop.Id)
                {
                    _logger.LogError($"Id from path: {id} is different from body's item id: {coffeeShop?.Id}");
                    return BadRequest();
                }

                await _repository.UpdateCoffeeShop(coffeeShop);
                _logger.LogInformation($"Updated coffee shop item with id: {id}");
                return NoContent();

            }
            catch (CoffeeShopNotFoundException)
            {
                _logger.LogError($"Not Found coffee shop item with id: {id}");
                return NotFound();
            }
        }


        /// <summary>
        /// Deletes a specific Coffee Shop Item.
        /// </summary>
        /// <param name="id"></param> 
        /// <response code="204">Deleted sucessfully the specific coffee shop item from id</response>
        /// <response code="404">Not found the specific coffee shop item from id</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CoffeeShop>> DeleteCoffeeShop(long id)
        {
            _logger.LogInformation($"Deleting coffee shop item with id: {id}");
            try
            {
                await _repository.DeleteCoffeeShop(id);
                _logger.LogInformation($"Deleted coffee shop item with id: {id}");
                return NoContent();
            }

            catch (CoffeeShopNotFoundException)
            {
                _logger.LogError($"Not Found coffee shop item with id: {id}");
                return NotFound();
            }
        }
    }
}
