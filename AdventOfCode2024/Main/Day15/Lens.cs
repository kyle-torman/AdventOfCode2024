namespace Main.Day15
{
    public record Lens
    {
        public Lens(string label, int focalLength)
        {
            Label = label;
            FocalLength = focalLength;
        }

        public string Label { get; }
        public int FocalLength { get; }
    }
}
