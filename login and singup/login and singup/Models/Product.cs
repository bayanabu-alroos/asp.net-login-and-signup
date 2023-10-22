
using login_and_singup.Models.Enum;

namespace login_and_singup.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; } 
        public string Image {  get; set; }
        public bool InStock { get; set; }
        public Size Size { get; set; }
        public bool IsDeleted { get; set; }
    }
}
