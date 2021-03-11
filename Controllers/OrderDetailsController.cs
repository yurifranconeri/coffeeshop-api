using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeShopAPI.Models;
using Microsoft.Extensions.Logging;
using CoffeeShopAPI.Application.Constants;
using System.Text.RegularExpressions;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly ILogger _logger;

        public OrderDetailsController(ILogger<OrderDetailsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get Order Details HTML.
        /// </summary>
        /// <response code="200">Returns order details html</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> CreateOrderDetailsHTML(OrderDetails orderDetails)
        {
                _logger.LogInformation($"Getting order details html");
            var p = System.IO.File.ReadAllText(".\\Application\\Resources\\page.html");
            _logger.LogInformation($"Returned order details html");

            return Ok(p);
        }
    }
}
