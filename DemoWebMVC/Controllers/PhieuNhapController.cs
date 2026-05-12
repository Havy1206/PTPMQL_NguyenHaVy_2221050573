using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;

namespace DemoMVC.Controllers
{
    public class PhieuNhapController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PhieuNhapController(ApplicationDbContext context) { _context = context; }

        // 1. HIỂN THỊ DANH SÁCH PHIẾU NHẬP
        public async Task<IActionResult> Index()
        {
            var phieuNhaps = await _context.PhieuNhaps
                .OrderByDescending(p => p.NgayNhap)
                .ToListAsync();
            return View(phieuNhaps);
        }

        // 2. FORM TẠO PHIẾU NHẬP (GET)
        public IActionResult Create()
        {
            ViewBag.ThietBiId = new SelectList(_context.ThietBis, "Id", "TenTB");
            return View();
        }

        // 3. XỬ LÝ TẠO PHIẾU NHẬP + CỘNG TỒN KHO (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhieuNhap pn, int ThietBiId, int SoLuong, decimal DonGiaNhap) 
        {
            // B1: Lưu Phiếu Nhập 
            pn.NgayNhap = DateTime.Now; 
            pn.TongTien = SoLuong * DonGiaNhap; 
            
            _context.PhieuNhaps.Add(pn);
            await _context.SaveChangesAsync(); 

            // B2: Lưu Chi Tiết Phiếu Nhập
            var chiTiet = new ChiTietPhieuNhap
            {
                PhieuNhapId = pn.Id, 
                ThietBiId = ThietBiId,
                SoLuong = SoLuong,
                DonGiaNhap = DonGiaNhap 
            };
            _context.ChiTietPhieuNhaps.Add(chiTiet);

            // B3: CẬP NHẬT CỘNG TỒN KHO
            var thietBi = await _context.ThietBis.FindAsync(ThietBiId);
            if (thietBi != null)
            {
                thietBi.SoLuongTon += SoLuong; 
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 4. XEM CHI TIẾT PHIẾU NHẬP
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // Lấy phiếu nhập kèm theo danh sách chi tiết và tên thiết bị
            var phieuNhap = await _context.PhieuNhaps
                .Include(p => p.ChiTietPhieuNhaps!)
                    .ThenInclude(ct => ct.ThietBi)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (phieuNhap == null) return NotFound();

            return View(phieuNhap);
        }
    }
}