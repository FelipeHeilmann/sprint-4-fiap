using Microsoft.AspNetCore.Mvc;
using WiseBuddy.Api.Dto;
using WiseBuddy.Api.Services;

namespace WiseBuddy.Api.Controllers;

[ApiController]
[Route("api/suitability")]
public class SuitabilityController : ControllerBase
{
    private readonly SuitabilityService _suitabilityService;

    public SuitabilityController(SuitabilityService suitabilityService)
    {
        _suitabilityService = suitabilityService;
    }

    [HttpGet("questionario")]
    [ProducesResponseType(typeof(IEnumerable<QuestionarioResponseDto>), 200)]
    public ActionResult<IEnumerable<QuestionarioResponseDto>> GetQuestionario()
    {
        var questionario = _suitabilityService.ObterQuestionario();
        return Ok(questionario);
    }


    [HttpPost]
    [ProducesResponseType(typeof(SuitabilityResponseDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<SuitabilityResponseDto>> Create([FromBody] SuitabilityCreateDto dto)
    {
        try
        {
            var suitability = await _suitabilityService.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetLatestByUsuario),
                new { usuarioId = suitability.UsuarioId },
                suitability
            );
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("usuarios/{usuarioId:int}/recentes")]
    [ProducesResponseType(typeof(SuitabilityResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<SuitabilityResponseDto>> GetLatestByUsuario(int usuarioId)
    {
        var result = await _suitabilityService.GetLatestByUsuarioAsync(usuarioId);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpGet("usuarios/{usuarioId:int}/historico")]
    [ProducesResponseType(typeof(IEnumerable<SuitabilityResponseDto>), 200)]
    public async Task<ActionResult<IEnumerable<SuitabilityResponseDto>>> GetHistoryByUsuario(int usuarioId)
    {
        var history = await _suitabilityService.GetHistoryByUsuarioAsync(usuarioId);
        return Ok(history);
    }

}
