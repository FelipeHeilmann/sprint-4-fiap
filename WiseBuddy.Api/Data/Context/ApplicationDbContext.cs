using Microsoft.EntityFrameworkCore;
using WiseBuddy.Api.Models;

namespace WiseBuddy.Api.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Suitability> Suitabilities { get; set; }
    public DbSet<Recomendacao> Recomendacoes { get; set; }
    public DbSet<SuitabilityResposta> SuitabilityRespostas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Telefone).HasMaxLength(15);
            entity.Property(e => e.DataCadastro).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<Suitability>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PerfilInvestidor).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ObjetivoInvestimento).HasMaxLength(100);
                entity.Property(e => e.RendaMensal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DataTeste).HasDefaultValueSql("NOW()");

                entity.HasOne(e => e.Usuario)
                        .WithMany(u => u.Suitabilities)
                        .HasForeignKey(e => e.UsuarioId)
                        .OnDelete(DeleteBehavior.Cascade);

                // Índices para performance
                entity.HasIndex(e => e.UsuarioId);
                entity.HasIndex(e => e.DataTeste);
                entity.HasIndex(e => new { e.UsuarioId, e.DataTeste });
            });

        modelBuilder.Entity<SuitabilityResposta>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.Property(e => e.TextoPergunta).IsRequired().HasMaxLength(500);
               entity.Property(e => e.RespostaSelecionada).IsRequired().HasMaxLength(200);
               entity.Property(e => e.DataResposta).HasDefaultValueSql("NOW()");

               entity.HasOne(e => e.Suitability)
                      .WithMany(s => s.Respostas)
                      .HasForeignKey(e => e.SuitabilityId)
                      .OnDelete(DeleteBehavior.Cascade);

               entity.HasIndex(e => e.SuitabilityId);
               entity.HasIndex(e => e.PerguntaId);

           });

        modelBuilder.Entity<Recomendacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TipoAtivo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).IsRequired().HasColumnType("text");
            entity.Property(e => e.NivelRisco).HasMaxLength(50);
            entity.Property(e => e.PercentualSugerido).HasColumnType("decimal(5,2)");
            entity.Property(e => e.RentabilidadeEsperada).HasColumnType("decimal(5,2)");
            entity.Property(e => e.DataCriacao).HasDefaultValueSql("NOW()");

            entity.HasOne(e => e.Usuario)
                  .WithMany(u => u.Recomendacoes)
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
