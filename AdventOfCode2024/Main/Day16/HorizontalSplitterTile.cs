namespace Main.Day16
{
    public class HorizontalSplitterTile : Tile
    {
        public override (bool CompletedPath, Direction[] NextDirections) HandleIncomingBeam(Direction currentBeamDirection)
        {
            var baseHandle = base.HandleIncomingBeam(currentBeamDirection);

            if (baseHandle.CompletedPath)
            {
                return baseHandle;
            }

            var nextDirections = currentBeamDirection switch
            {
                Direction.Left or Direction.Right => new[] { currentBeamDirection },
                Direction.Up or Direction.Down => new[] { Direction.Left, Direction.Right },
                _ => throw new NotImplementedException()
            };

            return (false, nextDirections);
        }
    }
}
