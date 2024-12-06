namespace Main.Day6
{
    public record GuardPosition
    {
        public GuardPosition(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; }
        public int Y { get; }
        public Direction Direction { get; }
        public override string ToString()
        {
            return $"{Y}-{X}-{Direction}";
        }
    }
}
