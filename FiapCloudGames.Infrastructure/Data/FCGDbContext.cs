using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Infrastructure.Data.ContextConfig;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.Infrastructure.Data;

public class FCGDbContext : DbContext
{
    public FCGDbContext(DbContextOptions<FCGDbContext> options) : base(options)
    {
    }

    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<ItemBiblioteca> ItensBiblioteca { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ConfigJogo());
        modelBuilder.ApplyConfiguration(new ConfigUsuario());
        modelBuilder.ApplyConfiguration(new ConfigUsuarioJogo());
    }
}
