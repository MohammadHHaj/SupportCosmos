using Microsoft.AspNetCore.Mvc;
using SupportCosmos.Server.Services;
using SupportCosmos.Shared.Models;

namespace SupportCosmos.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportController : ControllerBase
    {
        private readonly CosmosService _cosmosService;

        public SupportController(CosmosService cosmosService)
        {
            _cosmosService = cosmosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _cosmosService.GetAllItemsAsync();
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] SupportMessage message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cosmosService.AddItemAsync(message);
            return Ok(message);
        }
    }
}