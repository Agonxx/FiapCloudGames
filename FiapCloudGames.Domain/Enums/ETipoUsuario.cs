using System.ComponentModel;

namespace FiapCloudGames.Domain.Enums
{
    public enum ETipoUsuario
    {
        [Description("Usuário")]
        Usuario = 1,

        [Description("Administrador")]
        Administrador = 2,
    }
}
