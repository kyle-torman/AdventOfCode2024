namespace Main.SupportingComponents
{
    public static class Extensions
    {
        public static async Task DisplaySolution(this IChallenge challenge, InputType inputType, ChallengePart part)
        {
            Console.WriteLine();
            Console.WriteLine($"*************************");
            Console.WriteLine($"Challenge: {challenge.Day}");
            Console.WriteLine();
            var solution = part switch
            {
                ChallengePart.One => await challenge.SolvePart1Async(inputType),
                ChallengePart.Two => await challenge.SolvePart2Async(inputType),
                _ => throw new NotImplementedException()
            };
            Console.WriteLine();
            Console.WriteLine($"Solution:");
            Console.WriteLine(solution.Value);
            Console.WriteLine($"************************");
        }

        public static void WriteToConsole<T>(this T[,] array2D)
        {
            for (int i = 0; i < array2D.NumberOfRows(); i++)
            {
                string row = "";
                for (int j = 0; j < array2D.NumberOfColumns(); j++)
                {
                    row += array2D[i, j];
                }
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }

        public static int NumberOfRows<T>(this T[,] array) => array.GetLength(0);
        public static int NumberOfColumns<T>(this T[,] array) => array.GetLength(1);
    }
}
