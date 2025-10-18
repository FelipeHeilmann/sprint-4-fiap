using Microsoft.EntityFrameworkCore;
using WiseBuddy.Api.Data.Context;
using WiseBuddy.Api.Models;
using WiseBuddy.Api.Repositories;

namespace WiseBuddy.Api.Data.Repositories;

public class SuitabilityRepository : ISuitabilityRepository
{
    private readonly ApplicationDbContext _context;

    public SuitabilityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Suitability> AddAsync(Suitability suitability)
    {
        _context.Suitabilities.Add(suitability);
        await _context.SaveChangesAsync();
        return suitability;
    }

    public async Task<Suitability?> GetByIdAsync(int id)
    {
        return await _context.Suitabilities
            .Include(s => s.Respostas)
            .Include(s => s.Usuario)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Suitability?> GetLatestByUsuarioAsync(int usuarioId)
    {
        return await _context.Suitabilities
            .Include(s => s.Respostas)
            .Where(s => s.UsuarioId == usuarioId)
            .OrderByDescending(s => s.DataTeste)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Suitability>> GetHistoryByUsuarioAsync(int usuarioId)
    {
        return await _context.Suitabilities
            .Include(s => s.Respostas)
            .Where(s => s.UsuarioId == usuarioId)
            .OrderByDescending(s => s.DataTeste)
            .ToListAsync();
    }

    public async Task<SuitabilityResposta> AddRespostaAsync(SuitabilityResposta resposta)
    {
        _context.SuitabilityRespostas.Add(resposta);
        await _context.SaveChangesAsync();
        return resposta;
    }

    public async Task<IEnumerable<SuitabilityResposta>> GetRespostasBySuitabilityAsync(int suitabilityId)
    {
        return await _context.SuitabilityRespostas
            .Where(r => r.SuitabilityId == suitabilityId)
            .OrderBy(r => r.PerguntaId)
            .ToListAsync();
    }
}