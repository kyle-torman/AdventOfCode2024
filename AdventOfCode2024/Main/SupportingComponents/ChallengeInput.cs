namespace Main.SupportingComponents
{
    public record ChallengeInput
    {
        public string TestInput { get; init; } = "";
        public string PuzzleInput { get; init; } = "";

        public string GetInput(InputType inputType) =>
            inputType switch
            {
                InputType.Test => TestInput,
                InputType.Puzzle => PuzzleInput,
                _ => throw new NotImplementedException()
            };
    }

    public enum InputType
    {
        Test,
        Puzzle
    }
}
