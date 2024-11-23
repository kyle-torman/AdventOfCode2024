namespace Main.SupportingComponents
{
    public record Solution
    {
        public Solution(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
