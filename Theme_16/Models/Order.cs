namespace Theme_16.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public string Mail { get; set; } = string.Empty;
        public int ItemCode { get; set; }
        public string ItemName { get; set; } = string.Empty;
    }
}
