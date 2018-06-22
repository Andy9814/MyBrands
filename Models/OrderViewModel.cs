namespace MyBrands.Models
{
    public class orderViewModel
    {
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public int Qty { get; set; }
        public int QtySold { get; set; }
        public int QtyOrdered { get; set; }
        public int QtyBackOrdered { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string DateCreated { get; set; }
        public string ProductName { get; set; }
        public decimal MSRP { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal OrderTotal { get; set; }

    }
}