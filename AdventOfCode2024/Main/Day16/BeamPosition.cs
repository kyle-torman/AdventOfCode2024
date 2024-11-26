namespace Main.Day16
{
    public record BeamPosition
    {
        public BeamPosition(int x, int y, Direction direction)
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
            return $"{X}-{Y}-{Direction}";
        }
    }
}
