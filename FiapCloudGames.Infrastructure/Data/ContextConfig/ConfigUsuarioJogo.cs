using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Data.ContextConfig
{
    public class ConfigUsuarioJogo : IEntityTypeConfiguration<ItemBiblioteca>
    {
        public void Configure(EntityTypeBuilder<ItemBiblioteca> builder)
        {
            builder.HasOne<Usuario>()
               .WithMany()
               .HasForeignKey(h => h.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Jogo>()
               .WithMany()
               .HasForeignKey(h => h.JogoId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
