namespace Main.SupportingComponents
{
    public static class MultidimensionalArrayParser
    {
        public static char[,] Parse2DArray(string input) => Parse2DArray(input, (c, row, column) => c);
        public static T[,] Parse2DArray<T>(string input, Func<char, T> symbolParser) => Parse2DArray(input, (c, row, column) => symbolParser(c));
        public static T[,] Parse2DArray<T>(string input, Func<char, int, int, T> symbolParser)
        {
            var rows = input.Split("\r\n");
            var numOfRows = rows.Length;
            var numOfColumns = rows[0].Length;

            var output = new T[numOfRows, numOfColumns];
            for (int row = 0; row < numOfRows; row++)
            {
                for (int column = 0; column < numOfColumns; column++)
                {
                    output[row, column] = symbolParser(rows[row][column], row, column);
                }
            }

            return output;
        }
    }
}
