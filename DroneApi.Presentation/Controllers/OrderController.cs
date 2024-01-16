//using DroneApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DroneApi.Services.Contracts;
using DroneApi.Core.Dtos.ErrorModel;
using DroneApi.Core.Dtos.OrderModel;
using DroneApi.Core.Entities;

namespace DroneApi.Presentation.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private static int _orderStatus = 0;
        private readonly IServiceManager _serviceManager;

        public OrderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] OrderDto obj)
        {
            if (obj == null)
            {
                BadRequest(string.Empty);
            }
            string[] result = { obj.BusinessLocation, obj.DropoffLocation };

            return Ok(result);
        }

        [HttpPost("submittedOrder")]
        public ActionResult<string> PostOrder([FromBody] OrderDto order)
        {
            if (order is null)
            {
                return BadRequest("The array must contain exactly 3 items.");
            }

            return Ok("Order received");
        }

        [HttpPost("cancelOrder")]
        public IActionResult CancelOrder()
        {
            //Set 0 as canccelled order status
            _orderStatus = 0;
            return Ok("Order has been cancelled");
        }

        [HttpPost("completeOrder")]
        public IActionResult completeOrder()
        {
            if (_orderStatus == 10)
            {
                return Ok("Order has been sucessfully completed");
            }
            else
            {
                return BadRequest("Order not yet completed");
            }
        }

        [HttpGet("orderStatus")]
        public ActionResult<string> GetOrderStatus()
        {
            _orderStatus = (_orderStatus % 10) + 1;

            string statusMessage;
            switch (_orderStatus)
            {
                case 0:
                    statusMessage = "Order Cancelled";
                    break;
                case 1:
                    statusMessage = "Order received";
                    break;
                case 2:
                    statusMessage = "Processing order";
                    break;
                case 3:
                    statusMessage = "Initializing order delivery";
                    break;
                case 4:
                    statusMessage = "Order in transit";
                    break;
                case 5:
                    statusMessage = "We're on our way to pick up your order";
                    break;
                case 6:
                    statusMessage = "Picking up your order";
                    break;
                case 7:
                    statusMessage = "Your order is on its way";
                    break;
                case 8:
                    statusMessage = "Get ready to receive your order";
                    break;
                case 9:
                    statusMessage = "Finalizing order - lowering";
                    break;
                case 10:
                    statusMessage = "Order Completed";
                    break;
                default:
                    statusMessage = "Unknown order status";
                    break;
            }

            return Ok(statusMessage);
        }

        [HttpPost("createOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdOrder = await _serviceManager.OrderService.CreateOrderAsync(order);
            return CreatedAtRoute("GetOrderById", new { id = createdOrder.Id }, createdOrder);
        }


        [HttpGet("{id:guid}", Name = "GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var testModel = await _serviceManager.OrderService.GetOrderByIdAsync(id, trackChanges: false);
            return Ok(testModel);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrders()
        {
            var testModels = await _serviceManager.OrderService.GetAllOrderAsync(trackChanges: false);
            return Ok(testModels);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateUserByIdAsync(Guid id, [FromBody] OrderDto updateOrder)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            await _serviceManager.OrderService.UpdateOrderAsync(id, updateOrder);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = (typeof(ErrorDetailsDto)))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = (typeof(ErrorDetailsDto)))]
        public async Task<IActionResult> DeleteOrderById(Guid id)
        {
            await _serviceManager.OrderService.DeleteOrderAsync(id, trackChanges: false);
            return NoContent();
        }
    }
}