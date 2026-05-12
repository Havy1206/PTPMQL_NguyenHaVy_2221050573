using DemoMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // --- CÁC BẢNG CŨ (Giữ nguyên) ---
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        // --- CÁC BẢNG MỚI BUỔI 12 (Thêm vào dưới đây) ---
        
        // 1. Quản lý Nhà cung cấp
         public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        
        // 2. Quản lý Loại thiết bị
          public DbSet<LoaiThietBi> LoaiThietBis { get; set; }

        // 3. Quản lý Thiết bị 
         public DbSet<ThietBi> ThietBis { get; set; }

        // 4. Quản lý Nhập kho (Phần nâng cao)
         public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        // 5. Quản lý Xuất kho (Phần nâng cao)
         public DbSet<PhieuXuat> PhieuXuats { get; set; }
         public DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
    }
}