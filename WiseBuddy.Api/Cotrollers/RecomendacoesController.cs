using Microsoft.AspNetCore.Mvc;
using WiseBuddy.Api.Dto;
using WiseBuddy.Api.Services;

namespace WiseBuddy.Api.Controllers;

[ApiController]
[Route("api/recomendacoes")]
public class RecomendacoesController : ControllerBase
{
    private readonly RecomendacaoService _recomendacaoService;

    public RecomendacoesController(RecomendacaoService recomendacaoService)
    {
        _recomendacaoService = recomendacaoService;
    }

    [HttpPost("usuarios/{usuarioId:int}/gerar")]
    public async Task<ActionResult<IEnumerable<RecomendacaoResponseDto>>> Gerar(int usuarioId)
    {
        return Ok(await _recomendacaoService.GenerateRecommendationsAsync(usuarioId));
    }

    [HttpGet("usuarios/{usuarioId:int}")]
    public async Task<ActionResult<IEnumerable<RecomendacaoResponseDto>>> GetByUsuario(int usuarioId)
    {
        return Ok(await _recomendacaoService.GetByUsuarioAsync(usuarioId));
    }

    [HttpGet("usuarios/{usuarioId:int}/exportar")]
    public async Task<IActionResult> Exportar(int usuarioId)
    {
        var bytes = await _recomendacaoService.ExportRecommendationsToTxtAsync(usuarioId);
        return File(bytes, "text/plain", $"recomendacoes_{usuarioId}.txt");
    }
}
