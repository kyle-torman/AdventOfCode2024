namespace Main.Day6
{
    public class EmptyTile : Tile
    {
        public EmptyTile(int x, int y) : base(x, y) { }

        public override GuardPosition HandlePatrol(Direction currentPatrolDirection, GuardPosition currentPosition)
        {
            IsVisited = true;
            DirectionsHandled.Add(currentPatrolDirection);
            return TreatAsObstacle ? GetNextGuardPositionWithObstacle(currentPosition, currentPatrolDirection) : GetNextGuardPosition(currentPosition, currentPatrolDirection);
        }

        public bool TreatAsObstacle { get; set; }
    }
}
