namespace FileProcessor.Dto
{
    public class ItemUploads
    {

        public List<Item> Items { get; set; }

        public ItemUploads()
        {
            Items = new List<Item>();
        }
    }
}
