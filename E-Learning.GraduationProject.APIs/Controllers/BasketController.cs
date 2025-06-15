using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Baskets;
using E_Learning.GraduationProject.Core.Dtos.Courses;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core.Specifications.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
       

        public BasketController(
            IBasketService basketService
            )
        {
            _basketService = basketService;
            
        }

     

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseToReturnDto>> GetBasketById(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest , "Invalid id "));

            var basket = await _basketService.GetBasketByIdAsync(id);
            if (basket is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketToReturnDto>> CreateOrUpdateBasket(BasketDto? model)
        {
            if (model is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
           
            var basket = await _basketService.CreateOrUpdateBasketAsync(model);
            if (basket is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest , "Error while setting the basket"));

            return Ok(basket);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Invalid Id"));
            var res = await _basketService.DeleteBasketAsync(id);
            if (!res) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return NoContent();
        }
    }
}
