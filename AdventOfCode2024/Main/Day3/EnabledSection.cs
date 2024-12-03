namespace Main.Day3
{
    public class EnabledSection
    {
        private readonly int _startIndex;
        private readonly int _endIndex;

        public EnabledSection(int startIndex, int endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
        }

        public bool IsWithinSection(int index) =>
            _startIndex <= index && _endIndex >= index;
    }
}
