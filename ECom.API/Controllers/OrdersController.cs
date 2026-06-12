using ECom.Core.DTO;
using ECom.Core.Entites.Order;
using ECom.infrastrucrure.Repositries.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[AllowAnonymous]
    public class OrdersController : ControllerBase
    {
        public IOrderServices OrdersService { get; }


        public OrdersController(IOrderServices ordersService)
        {
            OrdersService = ordersService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OdrerDTO odrerDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            Console.WriteLine($"Email: {email}, BasketId: {odrerDTO.basketId}");

            var order = await OrdersService.CreateOrderAsync(odrerDTO, email); 
            if (order == null) return BadRequest("Problem creating order");
            return Ok(order);

        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<ActionResult<Orders>> GetOrderById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await OrdersService.GetOrderByIdAsync(id, email);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet ("GetOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<Orders>>> GetOrdersForUser(string buyerId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await OrdersService.GetOrdersForUserAsync(email);
            return Ok(orders);
        }
        [HttpGet("GetDeliveryMethod")]
        public async Task<ActionResult<List<DeliveryMethod>>> GetDeliveryMethod( )
        {
            var deliveryMethod = await OrdersService.GetDeliveryMethodsAsync(  );

            if (deliveryMethod == null)
                return NotFound();

            return Ok(deliveryMethod);
        }

    }
}
