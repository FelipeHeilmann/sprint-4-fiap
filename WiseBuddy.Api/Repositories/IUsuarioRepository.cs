using WiseBuddy.Api.Models;

namespace WiseBuddy.Api.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task UpdateAsync(Usuario usuario);
    Task<bool> DeleteAsync(int id);
}
