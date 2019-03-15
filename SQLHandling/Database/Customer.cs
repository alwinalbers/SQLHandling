namespace SQLHandling
{
    public class Customer : IDatabaseTableEntity
    {
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public void SetProperties()
        {
            //TODO
        }
    }
}
