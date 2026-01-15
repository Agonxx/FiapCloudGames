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
                    Nome="Rafael Santos",
                    Email="rafhita1@gmail.com",
                    Nivel= ETipoUsuario.Administrador,
                    Senha ="jx2A6WDVUKiccfcAYTCJJg=="//R123
                },
                new Usuario {
                    Id = 2,
                    Nome = "Admin FCG",
                    Email = "admin@fcg.com",
                    Nivel= ETipoUsuario.Administrador,
                    Senha = "IuL0vUhf2mtqMeh2otN5GQ=="//A123
                },
                new Usuario {
                    Id = 3,
                    Nome = "User FCG",
                    Email = "user@fcg.com",
                    Nivel= ETipoUsuario.Usuario,
                    Senha = "BxZkP2nHgsa2DbpyDZLDRQ=="//U123
                },
            });
        }
    }
}
