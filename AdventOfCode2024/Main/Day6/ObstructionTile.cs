namespace Main.Day6
{
    public class ObstructionTile : Tile
    {
        public ObstructionTile(int x, int y) : base(x, y) { }

        public override GuardPosition HandlePatrol(Direction currentPatrolDirection, GuardPosition currentPosition)
        {
            DirectionsHandled.Add(currentPatrolDirection);

            return GetNextGuardPositionWithObstacle(currentPosition, currentPatrolDirection);
        }
    }
}
