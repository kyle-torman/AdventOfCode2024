using Main.SupportingComponents;

namespace Main.Day16
{
    public static class Extensions
    {
        public static IEnumerable<BeamPosition> GetNextBeamPositions(this BeamPosition currentPosition, Direction[] nextDirections)
        {
            foreach (var direction in nextDirections)
            {
                yield return direction switch
                {
                    Direction.Up => new BeamPosition(currentPosition.X, currentPosition.Y - 1, direction),
                    Direction.Down => new BeamPosition(currentPosition.X, currentPosition.Y + 1, direction),
                    Direction.Left => new BeamPosition(currentPosition.X - 1, currentPosition.Y, direction),
                    Direction.Right => new BeamPosition(currentPosition.X + 1, currentPosition.Y, direction),
                    _ => throw new NotImplementedException()
                };
            }
        }

        public static int GetEnergizedOutputFromStartingPosition(this Tile[,] contraption, BeamPosition startingPosition)
        {
            var beamPositions = new Queue<BeamPosition>(new[] { startingPosition });
            while (beamPositions.Any())
            {
                var currentPosition = beamPositions.Dequeue();
                var nextPositions = contraption.FollowBeam(currentPosition);
                foreach (var position in nextPositions)
                {
                    beamPositions.Enqueue(position);
                }
            }

            return contraption.GetNumberOfEnergizedTiles();
        }

        private static BeamPosition[] FollowBeam(this Tile[,] contraption, BeamPosition currentBeamPosition)
        {
            var currentTile = contraption[currentBeamPosition.Y, currentBeamPosition.X];
            var processedBeamResults = currentTile.HandleIncomingBeam(currentBeamPosition.Direction);
            if (processedBeamResults.CompletedPath)
            {
                return Array.Empty<BeamPosition>();
            }

            var nextPositions = currentBeamPosition.GetNextBeamPositions(processedBeamResults.NextDirections);
            return contraption.FilterValidPositions(nextPositions).ToArray();
        }

        private static int GetNumberOfEnergizedTiles(this Tile[,] contraption)
        {
            var numberOfEnergizedTiles = 0;
            foreach (var tile in contraption)
            {
                if (tile.IsEnergized)
                {
                    numberOfEnergizedTiles++;
                }
            }

            return numberOfEnergizedTiles;
        }

        public static IEnumerable<BeamPosition> FilterValidPositions(this Tile[,] contraption, IEnumerable<BeamPosition> positions)
        {
            foreach (var position in positions)
            {
                if (position.IsWithinContraption(contraption))
                {
                    yield return position;
                }
            }
        }

        private static bool IsWithinContraption(this BeamPosition position, Tile[,] contraption) =>
            position.X >= 0 &&
            position.Y >= 0 &&
            position.X < contraption.NumberOfColumns() &&
            position.Y < contraption.NumberOfRows();
    }
}
