using ApiTask.Dto;
using ApiTask.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombinationsController : ControllerBase
    {
        private readonly ICombinationService service;

        public CombinationsController(ICombinationService service)
        {
            this.service = service;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<CombinationResponseDto>> GenerateCombinations([FromBody] CombinationRequestDto request)
        {
            if (request.Items == null || request.Length < 1 || request.Length > request.Items.Count)
            {
                return BadRequest("Invalid input");
            }

            var items = await service.GenerateCombinations(request);

            return Ok(items);
        }
    }
}
