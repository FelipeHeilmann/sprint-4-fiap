using WiseBuddy.Api.Models;

namespace WiseBuddy.Api.Repositories;

public interface ISuitabilityRepository
{
    Task<Suitability> AddAsync(Suitability suitability);
    Task<Suitability?> GetByIdAsync(int id);
    Task<Suitability?> GetLatestByUsuarioAsync(int usuarioId);
    Task<IEnumerable<Suitability>> GetHistoryByUsuarioAsync(int usuarioId);
    Task<SuitabilityResposta> AddRespostaAsync(SuitabilityResposta resposta);
    Task<IEnumerable<SuitabilityResposta>> GetRespostasBySuitabilityAsync(int suitabilityId);
}