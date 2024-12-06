namespace Main.Day6
{
    public class GuardStartingTile : Tile
    {
        public GuardStartingTile(int x, int y) : base(x, y) { }

        public override GuardPosition HandlePatrol(Direction currentPatrolDirection, GuardPosition currentPosition)
        {
            DirectionsHandled.Add(currentPatrolDirection);
            IsVisited = true;
            return GetNextGuardPosition(currentPosition, currentPatrolDirection);
        }
    }
}
