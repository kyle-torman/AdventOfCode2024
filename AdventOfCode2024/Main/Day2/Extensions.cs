namespace Main.Day2
{
    public static class Extensions
    {
        public static T[] RemoveAt<T>(this T[] array, int indexToRemove) =>
            array.Where((item, index) => index != indexToRemove).ToArray();
    }
}
