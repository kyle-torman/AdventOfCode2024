namespace Main.SupportingComponents
{
    public interface IChallenge
    {
        Day Day { get; }
        Task<Solution> SolvePart1Async(InputType inputType);
        Task<Solution> SolvePart2Async(InputType inputType);
    }
}
