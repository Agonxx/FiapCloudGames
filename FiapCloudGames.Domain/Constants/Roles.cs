namespace FiapCloudGames.Domain.Constants
{
    public static class Roles
    {
        public const string Usuario = nameof(Usuario);
        public const string Administrador = nameof(Administrador);

        public const string AdminAccess = $"{Administrador}";
        public const string UsuarioAccess = $"{Administrador},{Usuario}";
    }
}
