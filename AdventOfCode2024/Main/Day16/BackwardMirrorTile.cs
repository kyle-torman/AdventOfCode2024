namespace Main.Day16
{
    public class BackwardMirrorTile : Tile
    {
        public override (bool CompletedPath, Direction[] NextDirections) HandleIncomingBeam(Direction currentBeamDirection)
        {
            var baseHandle = base.HandleIncomingBeam(currentBeamDirection);

            if (baseHandle.CompletedPath)
            {
                return baseHandle;
            }

            var nextDirection = currentBeamDirection switch
            {
                Direction.Left => Direction.Down,
                Direction.Right => Direction.Up,
                Direction.Up => Direction.Right,
                Direction.Down => Direction.Left,
                _ => throw new NotImplementedException()
            };

            return (false, new[] { nextDirection });
        }
    }
}
