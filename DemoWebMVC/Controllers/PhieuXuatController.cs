using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;

namespace DemoMVC.Controllers
{
    public class PhieuXuatController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PhieuXuatController(ApplicationDbContext context) { _context = context; }

        // 1. HIỂN THỊ DANH SÁCH PHIẾU XUẤT
        public async Task<IActionResult> Index()
        {
            var phieuXuats = await _context.PhieuXuats
                .OrderByDescending(p => p.NgayXuat)
                .ToListAsync();
            return View(phieuXuats);
        }

        // 2. FORM TẠO PHIẾU XUẤT (GET)
        public IActionResult Create()
        {
            ViewBag.ThietBiId = new SelectList(_context.ThietBis, "Id", "TenTB");
            return View();
        }

        // 3. XỬ LÝ TẠO PHIẾU XUẤT + TRỪ TỒN KHO (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhieuXuat px, int ThietBiId, int SoLuong, decimal DonGiaXuat) 
        {
            // B1: Tạo phiếu xuất
            px.NgayXuat = DateTime.Now; 
            px.TongTien = SoLuong * DonGiaXuat; 
            
            _context.PhieuXuats.Add(px);
            await _context.SaveChangesAsync(); 

            // B2: Tạo Chi tiết phiếu xuất
            var chiTiet = new ChiTietPhieuXuat
            {
                PhieuXuatId = px.Id,
                ThietBiId = ThietBiId,
                SoLuong = SoLuong,
                DonGiaXuat = DonGiaXuat 
            };
            _context.ChiTietPhieuXuats.Add(chiTiet);

            // B3: CẬP NHẬT TRỪ TỒN KHO
            var thietBi = await _context.ThietBis.FindAsync(ThietBiId);
            if (thietBi != null)
            {
                thietBi.SoLuongTon -= SoLuong;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 4. XEM CHI TIẾT PHIẾU XUẤT
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var phieuXuat = await _context.PhieuXuats
                .Include(p => p.ChiTietPhieuXuats!)
                    .ThenInclude(ct => ct.ThietBi)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (phieuXuat == null) return NotFound();

            return View(phieuXuat);
        }
    }
}