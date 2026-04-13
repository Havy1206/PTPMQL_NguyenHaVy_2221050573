namespace DemoMVC.Models
{
    public class StudentVM
    {
        public string StudentCode { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string FacultyName { get; set; } = default!; // Thuộc tính này để chứa tên Khoa lấy từ bảng Faculty
        public int? Age { get; set; }
    }
}