using WiseBuddy.Api.Models;

namespace WiseBuddy.Api.Repositories;

public interface IRecomendacaoRepository
{
    Task<Recomendacao> AddAsync(Recomendacao recomendacao);
    Task<IEnumerable<Recomendacao>> GetActiveByUsuarioAsync(int usuarioId);
    Task DeactivateByUsuarioAsync(int usuarioId);
}