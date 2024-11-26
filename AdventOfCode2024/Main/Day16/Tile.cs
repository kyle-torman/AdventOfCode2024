namespace Main.Day16
{
    public abstract class Tile
    {
        protected readonly HashSet<Direction> DirectionsHandled = new HashSet<Direction>();

        public virtual (bool CompletedPath, Direction[] NextDirections) HandleIncomingBeam(Direction currentBeamDirection)
        {
            if (DirectionsHandled.Contains(currentBeamDirection))
            {
                return (true, Array.Empty<Direction>());
            }

            IsEnergized = true;
            DirectionsHandled.Add(currentBeamDirection);
            return (false, Array.Empty<Direction>());
        }

        public bool IsEnergized { get; private set; }

        public static Tile Create(char type) =>
            type switch
            {
                '.' => new EmptyTile(),
                '/' => new BackwardMirrorTile(),
                '\\' => new ForwardMirrorTile(),
                '-' => new HorizontalSplitterTile(),
                '|' => new VerticalSplitterTile(),
                _ => throw new NotImplementedException()
            };
    }
}
