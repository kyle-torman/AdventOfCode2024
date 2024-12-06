using Main.SupportingComponents;

namespace Main.Day6
{
    public static class Extensions
    {
        public static bool IsLoopDetected(this Tile[,] map)
        {
            var startingPosition = map.GetGuardStartingPosition();
            var currentPosition = map.FollowGuard(startingPosition);
            var isLoopDetected = false;

            while (currentPosition.IsWithinMap(map) && !isLoopDetected)
            {
                currentPosition = map.FollowGuard(currentPosition);
                isLoopDetected = currentPosition.IsWithinMap(map) &&
                                 map[currentPosition.Y, currentPosition.X].IsLoopDetected(currentPosition.Direction);
            }

            return isLoopDetected;
        }

        public static int GetVisitedTilesFromStartingPosition(this Tile[,] map)
        {
            var startingPosition = map.GetGuardStartingPosition();
            var nextPosition = map.FollowGuard(startingPosition);
            while (nextPosition.IsWithinMap(map))
            {
                nextPosition = map.FollowGuard(nextPosition);
            }

            return map.GetNumberOfVisitedTiles();
        }

        private static GuardPosition GetGuardStartingPosition(this Tile[,] map)
        {
            foreach (var tile in map)
            {
                if (tile is GuardStartingTile guardStartingTile)
                {
                    return new GuardPosition(guardStartingTile.X, guardStartingTile.Y, Direction.Up);
                }
            }

            throw new Exception("Failed to find starting position");
        }

        private static GuardPosition FollowGuard(this Tile[,] map, GuardPosition currentGuardPosition)
        {
            var currentTile = map[currentGuardPosition.Y, currentGuardPosition.X];
            var nextPosition = currentTile.HandlePatrol(currentGuardPosition.Direction, currentGuardPosition);

            return nextPosition;
        }

        private static int GetNumberOfVisitedTiles(this Tile[,] contraption)
        {
            var numberOfEnergizedTiles = 0;
            foreach (var tile in contraption)
            {
                if (tile.IsVisited)
                {
                    numberOfEnergizedTiles++;
                }
            }

            return numberOfEnergizedTiles;
        }

        private static bool IsWithinMap(this GuardPosition position, Tile[,] contraption) =>
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X < contraption.NumberOfColumns() &&
            position.Y < contraption.NumberOfRows();
    }
}
