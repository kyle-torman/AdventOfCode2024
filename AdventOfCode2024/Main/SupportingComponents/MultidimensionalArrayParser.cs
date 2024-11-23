namespace Main.SupportingComponents
{
    public static class MultidimensionalArrayParser
    {
        public static char[,] Parse2DArray(string input) => Parse2DArray(input, c => c);
        public static T[,] Parse2DArray<T>(string input, Func<char, T> symbolParser)
        {
            var rows = input.Split("\r\n");
            var numOfRows = rows.Length;
            var numOfColumns = rows[0].Length;

            var output = new T[numOfRows, numOfColumns];
            for (int i = 0; i < numOfRows; i++)
            {
                for (int j = 0; j < numOfColumns; j++)
                {
                    output[i, j] = symbolParser(rows[i][j]);
                }
            }

            return output;
        }
    }
}
