namespace Main.Day16
{
    public class VerticalSplitterTile : Tile
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
                Direction.Left or Direction.Right => new[] { Direction.Up, Direction.Down },
                Direction.Up or Direction.Down => new[] { currentBeamDirection },
                _ => throw new NotImplementedException()
            };

            return (false, nextDirections);
        }
    }
}
