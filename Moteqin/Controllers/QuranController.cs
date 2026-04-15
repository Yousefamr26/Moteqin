using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/quran")]
public class QuranController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuranController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("surahs")]
    public async Task<IActionResult> GetSurahs()
    {
        var result = await _mediator.Send(new GetSurahsQuery());
        return Ok(result);
    }

    [HttpGet("ayahs/{surahId}")]
    public async Task<IActionResult> GetAyahs(int surahId)
    {
        var result = await _mediator.Send(new GetAyahsQuery
        {
            SurahId = surahId
        });

        return Ok(result);
    }
}