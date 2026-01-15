namespace FiapCloudGames.Domain.Extensions
{
    public static class StringExtentions
    {
        public static bool IsNullOrEmpty(this string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

    }

}
