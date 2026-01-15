using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Data.ContextConfig
{
    public class ConfigJogo : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.HasData(new[]
             {
                new Jogo {
                    Id = 1,
                    Titulo = "Elden Ring",
                    Descricao = "RPG",
                    Preco = 249.90m,
                    CadastradoEm = DateTime.Now
                },
                new Jogo {
                    Id = 2,
                    Titulo = "EA FC 26",
                    Descricao = "Esporte",
                    Preco = 249.90m,
                    CadastradoEm = DateTime.Now
                },
            });
        }
    }
}
