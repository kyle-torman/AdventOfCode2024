using Main.SupportingComponents;

namespace Main.Day10
{
    public static class Extensions
    {
        public static int GetTrailheadScore(this int[,] map, int row, int column) => GetTrailheadPaths(map, row, column, false);

        public static int GetTrailheadRating(this int[,] map, int row, int column) => GetTrailheadPaths(map, row, column, true);

        private static int GetTrailheadPaths(this int[,] map, int row, int column, bool includeDistinctPaths)
        {
            if (map[row, column] != 0)
            {
                throw new Exception("This is not a valid trailhead");
            }

            var validPaths = new List<(int, int)>() { (row, column) };
            var currentHeight = 0;
            while (currentHeight < 9 && validPaths.Any())
            {
                var nextValidPaths = map.GetNextValidTrailPaths(validPaths, currentHeight);
                validPaths = includeDistinctPaths ? nextValidPaths.ToList() : nextValidPaths.Distinct().ToList();
                currentHeight++;
            }

            return validPaths.Count;
        }

        private static IEnumerable<(int Row, int Column)> GetNextValidTrailPaths(this int[,] map, IEnumerable<(int Row, int Column)> positionsToCheck, int currentTrailHeight)
        {
            foreach (var positionToCheck in positionsToCheck)
            {
                var nextPositions = map.GetNextPositionsToCheck(positionToCheck).ToList();
                foreach (var nextPosition in map.GetNextPositionsToCheck(positionToCheck))
                {
                    if (map[nextPosition.Row, nextPosition.Column] == currentTrailHeight + 1)
                    {
                        yield return nextPosition;
                    }
                }
            }
        }

        private static IEnumerable<(int Row, int Column)> GetNextPositionsToCheck(this int[,] map, (int Row, int Column) currentPosition)
        {
            //Left
            if (currentPosition.Column > 0)
            {
                yield return (currentPosition.Row, currentPosition.Column - 1);
            }

            //Up
            if (currentPosition.Row > 0)
            {
                yield return (currentPosition.Row - 1, currentPosition.Column);
            }

            //Right
            if (currentPosition.Column < map.NumberOfColumns() - 1)
            {
                yield return (currentPosition.Row, currentPosition.Column + 1);
            }

            //Down
            if (currentPosition.Row < map.NumberOfRows() - 1)
            {
                yield return (currentPosition.Row + 1, currentPosition.Column);
            }
        }
    }
}
