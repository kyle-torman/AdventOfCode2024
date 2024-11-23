namespace Main.SupportingComponents
{
    public abstract class BaseChallenge : IChallenge
    {
        public abstract Day Day { get; }

        public virtual Task<Solution> SolvePart1Async(InputType inputType)
        {
            Console.WriteLine("TODO: Implement Solution Part2");
            return Task.FromResult(new Solution("TODO"));
        }

        public virtual Task<Solution> SolvePart2Async(InputType inputType)
        {
            Console.WriteLine("TODO: Implement Solution Part2");
            return Task.FromResult(new Solution("TODO"));
        }

        protected virtual ChallengeInput Input { get; } = new ChallengeInput();
    }
}
