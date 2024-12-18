namespace Main.Day9
{
    public class File : DiskItem
    {
        public File(int size, int id) : base(size)
        {
            Id = id;
        }

        public int Id { get; }

        private int _blocksMoved = 0;

        public bool IsFullyMoved => _blocksMoved == Size;
        public bool HasBlockBeenMoved => _blocksMoved > 0;

        public void MoveBlockOfFile()
        {
            if (IsFullyMoved)
            {
                throw new Exception("All the file blocks have been moved");
            }

            _blocksMoved++;
        }

        public void MoveFile()
        {
            if (HasBeenProcessed || HasBlockBeenMoved)
            {
                throw new Exception("The file has already been moved or processed");
            }

            _blocksMoved = Size;
        }

        public override long[] GetBlocks()
        {
            var remainingUnmovedBlocks = Size - _blocksMoved;
            return Enumerable.Range(0, remainingUnmovedBlocks).Select(x => (long)Id).ToArray();
        }
    }
}
