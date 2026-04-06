using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên là bắt buộc")]
        public string StudentCode { get; set; } = default!;

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Họ tên phải từ 3 đến 50 ký tự")]
        public string FullName { get; set; } = default!;

        [Range(1, 100, ErrorMessage = "Tuổi phải từ 1 đến 100")]
        public int? Age { get; set; }
        
        // Bạn có thể thêm các thuộc tính mới nếu muốn bài tập phong phú hơn
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }
    }
}