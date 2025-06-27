using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Contacts;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.TestHelpers.Terminal;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(
            IContactUsService contactUsService
            )
        {
            _contactUsService = contactUsService;
        }

        [HttpPost]
        public async Task<ActionResult<ContactUsResponseDto>> SendMessage(ContactUsDto contactDto)
        {
            var result = await _contactUsService.CreateContactAsync(contactDto);
            if (result is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(result);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactUsResponseDto>>> GetAllMessages()
        {
            var contacts = await _contactUsService.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUsResponseDto>> GetMessageById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var contact = await _contactUsService.GetContactByIdAsync(id.Value);
            return Ok(contact);

        }
    }
}
