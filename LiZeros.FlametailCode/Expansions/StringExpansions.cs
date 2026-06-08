using LiZeros.Flametail.FlametailCode;

namespace LiZeros.FlametailCode.Expansions
{
    public static class StringExpansions
    {
        public static string ImagePath(this string path)
        {
            return Path.Combine(MainFile.MOD_ID, "Images", path);
        }

        public static string CardImagePath(this string path)
        {
            return Path.Combine(MainFile.MOD_ID, "Images", "Cards", path);
        }

        public static string BigCardImagePath(this string path)
        {
            return Path.Combine(MainFile.MOD_ID, "Images", "Cards", "Big", path);
        }

        public static string PowerImagePath(this string path)
        {
            return Path.Combine(MainFile.MOD_ID, "Images", "Powers", path);
        }

        public static string RelicesImagePath(this string path)
        {
            return Path.Combine(MainFile.MOD_ID, "Images", "Relices", path);
        }

        public static string BigRelicesImage(this string path)
        {
            return Path.Combine(MainFile.MOD_ID, "Images", "Relices", "Big", path);
        }

        public static string UiImagePath(this string path)
        {
            return Path.Combine(MainFile.MOD_ID, "Images", "Ui", path);
        }
    }
}
