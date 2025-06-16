using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Baskets;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(
            IPaymentService paymentService
            )
        {
            _paymentService = paymentService;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketToReturnDto>> CreatePaymentIntent(string basketId)
        {
            if (basketId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var customerBasketDto = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (customerBasketDto is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(customerBasketDto);
        }
    }
}
