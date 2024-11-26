namespace Main.Day16
{
    public class EmptyTile : Tile
    {
        public override (bool CompletedPath, Direction[] NextDirections) HandleIncomingBeam(Direction currentBeamDirection)
        {
            var baseHandle = base.HandleIncomingBeam(currentBeamDirection);

            if (baseHandle.CompletedPath)
            {
                return baseHandle;
            }

            return (false, new[] { currentBeamDirection });
        }
    }
}
