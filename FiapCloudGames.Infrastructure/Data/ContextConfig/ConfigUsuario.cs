using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Data.ContextConfig
{
    public class ConfigUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasData(new[]
             {
                new Usuario {
                    Id = 1,
                    Nome = "Admin FCG",
                    Email = "admin@fcg.com",
                    Nivel= ETipoUsuario.Administrador,
                    SenhaHash = "IuL0vUhf2mtqMeh2otN5GQ==",//A123
                    CadastradoEm = DateTime.Now.AddDays(-25),
                    Ativo = true
                },
                new Usuario {
                    Id = 2,
                    Nome = "User FCG",
                    Email = "user@fcg.com",
                    Nivel= ETipoUsuario.Usuario,
                    SenhaHash = "BxZkP2nHgsa2DbpyDZLDRQ==",//U123
                    CadastradoEm = DateTime.Now.AddDays(-5),
                    Ativo = true
                },
            });
        }
    }
}
