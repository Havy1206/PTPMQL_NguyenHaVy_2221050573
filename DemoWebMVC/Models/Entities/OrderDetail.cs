using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        // Liên kết tới Đơn hàng (Một đơn hàng có nhiều chi tiết)
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order? Order { get; set; }

        // Liên kết tới Sản phẩm (Một sản phẩm có thể xuất hiện trong nhiều chi tiết)
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product? Product { get; set; }

        // Thuộc tính riêng của chi tiết đơn hàng
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải ít nhất là 1")]
        public int Quantity { get; set; }
        
        public decimal UnitPrice { get; set; } // Lưu giá tại thời điểm mua
    }
}