namespace Main.Day9
{
    public class FreeSpace : DiskItem
    {
        public FreeSpace(int size) : base(size)
        {

        }

        public bool IsFilled => _fileBlocksAdded.Count == Size;
        public int RemainingSize => Size - _fileBlocksAdded.Count;

        private readonly List<long> _fileBlocksAdded = new List<long>();

        public void AddFileBlock(File file)
        {
            if (IsFilled)
            {
                throw new Exception("The free space is already filled");
            }

            file.MoveBlockOfFile();
            _fileBlocksAdded.Add(file.Id);
        }

        public void AddWholeFile(File file)
        {
            if (RemainingSize < file.Size)
            {
                throw new Exception("The free space cannot contain the file");
            }

            file.MoveFile();
            _fileBlocksAdded.AddRange(Enumerable.Range(0, file.Size).Select(x => (long)file.Id));
        }

        public override long[] GetBlocks()
        {
            return _fileBlocksAdded.Concat(Enumerable.Range(0, RemainingSize).Select(x => 0L)).ToArray();
        }
    }
}
