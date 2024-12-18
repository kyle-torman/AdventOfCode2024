namespace Main.Day9
{
    public static class Extensions
    {
        public static List<DiskItem> ParseDiskItems(this string diskMap)
        {
            var diskItems = new List<DiskItem>();
            var fileId = 0;
            for (var i = 0; i < diskMap.Length; i++)
            {
                var diskItemSize = int.Parse(diskMap[i].ToString());
                var isFile = i % 2 == 0;
                if (isFile)
                {
                    diskItems.Add(new File(diskItemSize, fileId));
                    fileId++;
                }
                else if (diskItemSize > 0)
                {
                    diskItems.Add(new FreeSpace(diskItemSize));
                }
            }

            return diskItems;
        }
    }
}
