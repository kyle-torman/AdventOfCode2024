namespace Main.Day15
{
    public record Box
    {
        private readonly OrderedDictionary<string, Lens> _lenses = new OrderedDictionary<string, Lens>();

        public Box(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public void AddLens(string label, int focalLength)
        {
            _lenses[label] = new Lens(label, focalLength);
        }

        public void RemoveLens(string label)
        {
            _lenses.Remove(label);
        }

        public int FocalPowerOfLenses => _lenses.Values.Select((lens, index) => (Id + 1) * (index + 1) * lens.FocalLength).Sum();
    }
}
