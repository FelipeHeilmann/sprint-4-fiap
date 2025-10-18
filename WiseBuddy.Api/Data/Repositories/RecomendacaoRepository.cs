using Microsoft.EntityFrameworkCore;
using WiseBuddy.Api.Data.Context;
using WiseBuddy.Api.Models;
using WiseBuddy.Api.Repositories;

namespace WiseBuddy.Api.Data.Repositories;

public class RecomendacaoRepository : IRecomendacaoRepository
{
    private readonly ApplicationDbContext _context;

    public RecomendacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Recomendacao> AddAsync(Recomendacao recomendacao)
    {
        _context.Recomendacoes.Add(recomendacao);
        await _context.SaveChangesAsync();
        return recomendacao;
    }

    public async Task<IEnumerable<Recomendacao>> GetActiveByUsuarioAsync(int usuarioId)
    {
        return await _context.Recomendacoes
            .Where(r => r.UsuarioId == usuarioId && r.Ativa)
            .OrderByDescending(r => r.DataCriacao)
            .ToListAsync();
    }

    public async Task DeactivateByUsuarioAsync(int usuarioId)
    {
        var recomendacoes = await _context.Recomendacoes
            .Where(r => r.UsuarioId == usuarioId && r.Ativa)
            .ToListAsync();

        foreach (var rec in recomendacoes)
        {
            rec.Ativa = false;
        }

        await _context.SaveChangesAsync();
    }
}
