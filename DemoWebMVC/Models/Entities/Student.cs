using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Cần dòng này để nhận diện [ForeignKey]

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

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }

        

        // 1. Khai báo cột khóa ngoại (Foreign Key)
        public int? FacultyID { get; set; }

        // 2. Navigation Property: Cho phép truy cập thông tin Khoa từ Sinh viên
        [ForeignKey("FacultyID")]
        public virtual Faculty? Faculty { get; set; }
    } // Kết thúc class Student
} // Kết thúc namespace