namespace FiapCloudGames.Domain.Extensions
{
    public static class ListExtentions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list)
        {
            return list == null || !list.Any();
        }
    }

}
