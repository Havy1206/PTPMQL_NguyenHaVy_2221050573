using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class PhieuNhap
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Ngày nhập")]
        public DateTime NgayNhap { get; set; }
        public decimal TongTien { get; set; }
        public virtual ICollection<ChiTietPhieuNhap>? ChiTietPhieuNhaps { get; set; }
    }
}