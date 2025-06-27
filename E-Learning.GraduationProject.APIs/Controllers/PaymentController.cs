using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Baskets;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
      
        private readonly string _webhookSecret;

        public PaymentController(
            IPaymentService paymentService,
             IConfiguration configuration
            
            )
        {
            _paymentService = paymentService;
            _webhookSecret = configuration["Stripe:WebhookSecret"]
                ?? throw new ArgumentNullException("Stripe:WebhookSecret is not configured");
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketToReturnDto>> CreatePaymentIntent(string basketId)
        {
            if (basketId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var customerBasketDto = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (customerBasketDto is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(customerBasketDto);
        }
    

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            if (string.IsNullOrEmpty(json))
            {
              
                return BadRequest("Empty request body");
            }

            var signatureHeader = Request.Headers["Stripe-Signature"];

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                signatureHeader,
                _webhookSecret,
                throwOnApiVersionMismatch: true
            );

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    if (paymentIntent == null)
                    {
                        
                        return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
                    }
                    var order =  await _paymentService.GetOrderByPaymentIntentId(paymentIntent.Id);
                    if (order == null || order.BuyerId == 0)
                        return BadRequest(new ApiErrorResponse(400, "Student not found"));
                    await _paymentService.HandlePaymentIntentSucceeded(paymentIntent.Id, order.BuyerId);
                    break;

                case EventTypes.PaymentIntentPaymentFailed:
                    var failedIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (failedIntent == null)
                    {
                        return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
                    }

                    await _paymentService.HandlePaymentIntentFailed(failedIntent.Id);
                    break;

                default:
                    break;
            }

            return Ok();
        }
    }
}
