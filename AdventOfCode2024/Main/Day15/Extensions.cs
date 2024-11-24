namespace Main.Day15
{
    public static class Extensions
    {
        public static int GetHash(this char character, int currentValue)
        {
            var asciiCode = (int)character;
            var hash = currentValue + asciiCode;
            hash *= 17;
            hash %= 256;
            return hash;
        }

        public static int GetHash(this string input)
        {
            var hash = 0;
            foreach (var character in input)
            {
                hash = character.GetHash(hash);
            }
            return hash;
        }
    }
}
