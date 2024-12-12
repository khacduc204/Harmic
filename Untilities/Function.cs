namespace Harmic.Untilities
{
    public class Function
    {
        // Tạo alias slug từ title
        public static string TitleSlugGenerationAlias(string title)
        {
            return SlugGenerator.SlugGenerator.GenerateSlug(title);
        }
    }
}
