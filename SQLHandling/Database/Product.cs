namespace SQLHandling
{
    public class Product : IDatabaseTableEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }

        public void SetProperties()
        {

        }
    }
}
