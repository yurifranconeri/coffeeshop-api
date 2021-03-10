using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeShopAPI.Models;
using CoffeeShopAPI.Repository;
using CoffeeShopAPI.Exceptions;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerAsyncRepository _repository;
        private readonly ILogger _logger;

        public CustomersController(ICustomerAsyncRepository customerRepository, ILogger<CustomersController> logger)
        {
            _repository = customerRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get all Customers.
        /// </summary>
        /// <response code="200">Returns all customers items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            _logger.LogInformation($"Getting all customer items");
            
            IEnumerable<Customer> customer = await _repository.GetAll();
            _logger.LogInformation($"Returned all {customer.Count()} customer items");

            return Ok(customer);
        }

        /// <summary>
        /// Get a specific Customer Item.
        /// </summary>
        /// <param name="cpf"></param> 
        /// <response code="200">Returns the coffee shop item from specific id</response>
        /// <response code="404">Not found the coffee shop item from specific id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> GetCustomer(long cpf)
        {
            _logger.LogInformation($"Getting customer item with id: {cpf}");
            Customer customer = await _repository.GetById(cpf);

            if (customer != null)
            {
                _logger.LogInformation($"Returning customer item: {customer}");
                return Ok(customer);
            }

            _logger.LogInformation($"Not found customer item: {cpf}");
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
        /// <param name="customer"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created Coffee Shop Item</response>
        /// <response code="400">If the coffee shop item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Customer>> PostCoffeeShop(Customer customer)
        {
            _logger.LogInformation($"Creating customer item: {customer}");
            await _repository.AddCustomer(customer);
            _logger.LogInformation($"Created customer item: {customer}");
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CPF }, customer);
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
        /// <param name="cpf"></param> 
        /// <param name="customer"></param> 
        /// <response code="204">Updates the coffee shop item from specific id</response>
        /// <response code="404">Not found the coffee shop item from specific id</response>
        /// <response code="400">Id from path different from body item</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCoffeeShop(long cpf, Customer customer)
        {
            _logger.LogInformation($"Updating customer item with CPF: {cpf} and body item: {customer}");
            try
            {
                if (cpf != customer.CPF)
                {
                    _logger.LogError($"CPF from path: {cpf} is different from body's item CPF: {customer?.CPF}");
                    return BadRequest();
                }

                await _repository.UpdateCustomer(customer);
                _logger.LogInformation($"Updated coffee shop item with id: {cpf}");
                return NoContent();

            }
            catch (CustomerNotFoundException)
            {
                _logger.LogError($"Not Found coffee shop item with id: {cpf}");
                return NotFound();
            }
        }


        /// <summary>
        /// Deletes a specific Coffee Shop Item.
        /// </summary>
        /// <param name="cpf"></param> 
        /// <response code="204">Deleted sucessfully the specific coffee shop item from id</response>
        /// <response code="404">Not found the specific coffee shop item from id</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> DeleteCustomer(long cpf)
        {
            _logger.LogInformation($"Deleting coffee shop item with id: {cpf}");
            try
            {
                await _repository.DeleteCustomer(cpf);
                _logger.LogInformation($"Deleted coffee shop item with id: {cpf}");
                return NoContent();
            }

            catch (CustomerNotFoundException)
            {
                _logger.LogError($"Not Found coffee shop item with id: {cpf}");
                return NotFound();
            }
        }
    }
}
