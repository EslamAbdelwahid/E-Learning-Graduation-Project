using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Orders;
using E_Learning.GraduationProject.Core.Entities.Orders;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(
            IOrderService orderService
            )
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromQuery] string basketId , int studentId)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(buyerEmail))
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Email claim not found."));

            var order = await _orderService.CreateOrderAsync(studentId , buyerEmail, basketId);

            if (order == null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Order Creation Failed"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(buyerEmail))
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Email claim not found."));

            var orders = await _orderService.GetAllOrdersForSpecificUserAsync(buyerEmail);
            
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Id"));

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(buyerEmail))
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Email claim not found."));

            var order = await _orderService.GetOrderByIdForSpecificUserAsync(buyerEmail, id.Value);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}
