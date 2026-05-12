using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class PhieuXuat
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Ngày xuất")]
        public DateTime NgayXuat { get; set; }
        public decimal TongTien { get; set; }
        public virtual ICollection<ChiTietPhieuXuat>? ChiTietPhieuXuats { get; set; }
    }
}