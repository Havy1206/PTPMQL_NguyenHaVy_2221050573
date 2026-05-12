using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;

namespace DemoMVC.Controllers
{
    public class ThietBiController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ThietBiController(ApplicationDbContext context) { _context = context; }

        // 1. DANH SÁCH + TÌM KIẾM
        public async Task<IActionResult> Index(string searchString)
        {
            var thietBis = _context.ThietBis
                .Include(t => t.LoaiThietBi)
                .Include(t => t.NhaCungCap)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                thietBis = thietBis.Where(s => s.TenTB!.Contains(searchString));
            }
            ViewData["CurrentFilter"] = searchString;
            return View(await thietBis.ToListAsync());
        }

        // 2. THÊM MỚI (GET)
        public IActionResult Create()
        {
            ViewBag.LoaiThietBiId = new SelectList(_context.LoaiThietBis, "Id", "TenLoai");
            ViewBag.NhaCungCapId = new SelectList(_context.NhaCungCaps, "Id", "TenNCC");
            return View();
        }

        // 3. THÊM MỚI (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThietBi tb)
        {
            if (ModelState.IsValid)
            {
                _context.ThietBis.Add(tb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.LoaiThietBiId = new SelectList(_context.LoaiThietBis, "Id", "TenLoai", tb.LoaiThietBiId);
            ViewBag.NhaCungCapId = new SelectList(_context.NhaCungCaps, "Id", "TenNCC", tb.NhaCungCapId);
            return View(tb);
        }

        // 4. SỬA (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var tb = await _context.ThietBis.FindAsync(id);
            if (tb == null) return NotFound();
            
            ViewBag.LoaiThietBiId = new SelectList(_context.LoaiThietBis, "Id", "TenLoai", tb.LoaiThietBiId);
            ViewBag.NhaCungCapId = new SelectList(_context.NhaCungCaps, "Id", "TenNCC", tb.NhaCungCapId);
            return View(tb);
        }

        // 5. SỬA (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ThietBi tb)
        {
            if (id != tb.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(tb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.LoaiThietBiId = new SelectList(_context.LoaiThietBis, "Id", "TenLoai", tb.LoaiThietBiId);
            ViewBag.NhaCungCapId = new SelectList(_context.NhaCungCaps, "Id", "TenNCC", tb.NhaCungCapId);
            return View(tb);
        }

        // 6. CHI TIẾT (GET)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var tb = await _context.ThietBis
                .Include(t => t.LoaiThietBi)
                .Include(t => t.NhaCungCap)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tb == null) return NotFound();
            return View(tb);
        }

        // 7. XÓA (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var tb = await _context.ThietBis
                .Include(t => t.LoaiThietBi)
                .Include(t => t.NhaCungCap)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tb == null) return NotFound();
            return View(tb);
        }

        // 8. XÓA (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tb = await _context.ThietBis.FindAsync(id);
            if (tb != null) {
                _context.ThietBis.Remove(tb);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}