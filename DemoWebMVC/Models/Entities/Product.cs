using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        // Quan hệ: Một sản phẩm có thể xuất hiện trong nhiều Chi tiết đơn hàng
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}