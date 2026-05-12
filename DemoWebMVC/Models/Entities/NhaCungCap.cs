using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class NhaCungCap
    {
        [Key] // Đánh dấu đây là khóa chính (Primary Key)
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [Display(Name = "Tên nhà cung cấp")]
        public string? TenNCC { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? SoDienThoai { get; set; }

        // Mối quan hệ: Một nhà cung cấp có nhiều thiết bị
        public virtual ICollection<ThietBi>? ThietBis { get; set; }
    }
}