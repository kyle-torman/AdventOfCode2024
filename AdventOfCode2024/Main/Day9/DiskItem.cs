namespace Main.Day9
{
    public abstract class DiskItem
    {
        protected DiskItem(int size)
        {
            Size = size;
        }

        public abstract long[] GetBlocks();

        public int Size { get; }
        public bool HasBeenProcessed { get; set; }
    }
}
