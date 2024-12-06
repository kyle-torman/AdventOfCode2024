namespace Main.Day6
{
    public abstract class Tile
    {
        protected Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public abstract GuardPosition HandlePatrol(Direction currentPatrolDirection, GuardPosition currentPosition);

        protected readonly HashSet<Direction> DirectionsHandled = new HashSet<Direction>();

        public bool IsVisited { get; protected set; }
        public int X { get; }
        public int Y { get; }

        public bool IsLoopDetected(Direction currentDirection) => DirectionsHandled.Contains(currentDirection);

        protected GuardPosition GetNextGuardPosition(GuardPosition currentPosition, Direction currentDirection)
        {
            return currentDirection switch
            {
                Direction.Up => new GuardPosition(currentPosition.X, currentPosition.Y - 1, currentDirection),
                Direction.Down => new GuardPosition(currentPosition.X, currentPosition.Y + 1, currentDirection),
                Direction.Left => new GuardPosition(currentPosition.X - 1, currentPosition.Y, currentDirection),
                Direction.Right => new GuardPosition(currentPosition.X + 1, currentPosition.Y, currentDirection),
                _ => throw new NotImplementedException()
            };
        }

        protected GuardPosition GetNextGuardPositionWithObstacle(GuardPosition currentPosition, Direction currentDirection)
        {
            var nextDirection = currentDirection switch
            {
                Direction.Left => Direction.Up,
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                _ => throw new NotImplementedException()
            };

            return nextDirection switch
            {
                Direction.Up => new GuardPosition(currentPosition.X + 1, currentPosition.Y - 1, nextDirection),
                Direction.Down => new GuardPosition(currentPosition.X - 1, currentPosition.Y + 1, nextDirection),
                Direction.Left => new GuardPosition(currentPosition.X - 1, currentPosition.Y - 1, nextDirection),
                Direction.Right => new GuardPosition(currentPosition.X + 1, currentPosition.Y + 1, nextDirection),
                _ => throw new NotImplementedException()
            };
        }

        public static Tile Create(char type, int row, int column) =>
            type switch
            {
                '.' => new EmptyTile(column, row),
                '#' => new ObstructionTile(column, row),
                '^' => new GuardStartingTile(column, row),
                _ => throw new NotImplementedException()
            };
    }
}
