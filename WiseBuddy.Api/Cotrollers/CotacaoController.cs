using Microsoft.AspNetCore.Mvc;
using WiseBuddy.Api.Dto;
using WiseBuddy.Api.Services;

namespace WiseBuddy.Api.Cotrollers;

[ApiController]
[Route("api/cotacoes")]
public class CotacaoController : ControllerBase
{
    private readonly CotacaoService _cotacaoService;

    public CotacaoController(CotacaoService cotacaoService)
    {
        this._cotacaoService = cotacaoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CotacaoResponse>), 200)]
    public async Task<IActionResult> GetCotacaoes([FromQuery] int limit = 10)
    {
        var cotacoes = await this._cotacaoService.GetCotacoes(limit);
        return Ok(cotacoes);
    }
}
