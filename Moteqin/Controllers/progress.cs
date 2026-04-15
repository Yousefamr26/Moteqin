using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Moteqin.Controllers
{
    [ApiController]
    [Route("api/progress")]
    [Authorize]
    public class ProgressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProgressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private string GetUserIdFromToken()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User id not found in token.");

            return userId;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddProgress(AddOrUpdateProgressCommand command)
        {
            command.UserId = GetUserIdFromToken();

            var result = await _mediator.Send(command);

            return Ok(new
            {
                success = true,
                data = result
            });
        }

       
       
        [HttpGet]
        public async Task<IActionResult> GetProgress()
        {
            var userId = GetUserIdFromToken();

            var result = await _mediator.Send(new GetUserProgressQuery());

            return Ok(new
            {
                success = true,
                data = result
            });
        }

      
        [HttpGet("surah/{surahId}")]
        public async Task<IActionResult> GetBySurah(int surahId)
        {
            var userId = GetUserIdFromToken();

            var result = await _mediator.Send(new GetProgressBySurahQuery(surahId));

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        
        [HttpGet("percentage")]
        public async Task<IActionResult> GetPercentage()
        {
            var userId = GetUserIdFromToken();

            var result = await _mediator.Send(new GetProgressPercentageQuery(userId));

            return Ok(new
            {
                success = true,
                data = result
            });
        }

      
        [HttpGet("ayah/{ayahId}")]
        public async Task<IActionResult> GetAyahStatus(int ayahId)
        {
            var userId = GetUserIdFromToken();

            var result = await _mediator.Send(new GetAyahStatusQuery( ayahId));

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        
        [HttpPatch("ayah/{ayahId}/complete")]
        public async Task<IActionResult> MarkAsMemorized(int ayahId)
        {
            var command = new AddOrUpdateProgressCommand
            {
                UserId = GetUserIdFromToken(),
                AyahId = ayahId,
                Status = ProgressStatus.Memorized
            };

            var result = await _mediator.Send(command);

            return Ok(new
            {
                success = true,
                message = "Ayah marked as memorized",
                data = result
            });
        }

       
        [HttpDelete("surah/{surahId}")]
        public async Task<IActionResult> ResetSurah(int surahId)
        {
            var userId = GetUserIdFromToken();

            var result = await _mediator.Send(new ResetSurahProgressCommand( surahId));

            return Ok(new
            {
                success = true,
                message = result
            });
        }
    }
}