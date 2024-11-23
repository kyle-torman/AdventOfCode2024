using System.Text;

namespace Main.Day14
{
    public static class Extensions
    {
        public static string GetCacheKey(this char[,] array2D)
        {
            var builder = new StringBuilder();
            foreach (var symbol in array2D)
            {
                builder.Append(symbol);
            }
            return builder.ToString();
        }
        public static bool IsMovableRock(this char symbol) => symbol is Symbols.MovableRock;
        public static bool IsStationaryRock(this char symbol) => symbol == Symbols.StationaryRock;
        public static bool IsEmpty(this char symbol) => symbol == Symbols.Empty;
    }
}
