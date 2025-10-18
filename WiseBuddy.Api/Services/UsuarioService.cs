using AutoMapper;
using WiseBuddy.Api.Dto;
using WiseBuddy.Api.Models;
using WiseBuddy.Api.Repositories;

namespace WiseBuddy.Api.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<UsuarioResponseDto> CreateAsync(UsuarioCreateDto dto)
    {
        var usuario = new Usuario()
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone
        };
        var created = await _usuarioRepository.AddAsync(usuario);
        return new UsuarioResponseDto
        {
            Id = created.Id,
            Nome = created.Nome,
            Email = created.Email,
            Telefone = created.Telefone,
            Ativo = created.Ativo,
            DataCadastro = created.DataCadastro
        };
    }

    public async Task<UsuarioResponseDto?> GetByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        return usuario != null ? new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Ativo = usuario.Ativo,
            DataCadastro = usuario.DataCadastro
        } : null;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return usuarios.Select(usuario => new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Ativo = usuario.Ativo,
            DataCadastro = usuario.DataCadastro
        });
    }

    public async Task<bool> UpdateAsync(int id, UsuarioCreateDto dto)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return false;

        _mapper.Map(dto, usuario);
        await _usuarioRepository.UpdateAsync(usuario);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _usuarioRepository.DeleteAsync(id);
    }
}
