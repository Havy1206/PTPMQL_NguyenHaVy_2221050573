using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;

namespace DemoMVC.Controllers
{
    public class LoaiThietBiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiThietBiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. HIỂN THỊ DANH SÁCH + TÌM KIẾM ---
        public async Task<IActionResult> Index(string searchString)
        {
            var loaiTB = from l in _context.LoaiThietBis select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                loaiTB = loaiTB.Where(s => s.TenLoai!.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await loaiTB.ToListAsync());
        }

        // --- 2. FORM THÊM MỚI (GET) ---
        public IActionResult Create()
        {
            return View();
        }

        // --- 3. XỬ LÝ THÊM MỚI (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiThietBi loaiThietBi)
        {
            if (ModelState.IsValid)
            {
                _context.LoaiThietBis.Add(loaiThietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiThietBi);
        }

        // --- 4. XEM CHI TIẾT (GET) ---
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var loaiThietBi = await _context.LoaiThietBis.FirstOrDefaultAsync(m => m.Id == id);
            if (loaiThietBi == null) return NotFound();
            return View(loaiThietBi);
        }

        // --- 5. FORM SỬA (GET) ---
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var loaiThietBi = await _context.LoaiThietBis.FindAsync(id);
            if (loaiThietBi == null) return NotFound();
            return View(loaiThietBi);
        }

        // --- 6. XỬ LÝ SỬA (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoaiThietBi loaiThietBi)
        {
            if (id != loaiThietBi.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(loaiThietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiThietBi);
        }

        // --- 7. FORM XÁC NHẬN XÓA (GET) ---
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var loaiThietBi = await _context.LoaiThietBis.FirstOrDefaultAsync(m => m.Id == id);
            if (loaiThietBi == null) return NotFound();
            return View(loaiThietBi);
        }

        // --- 8. XỬ LÝ XÓA (POST) ---
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiThietBi = await _context.LoaiThietBis.FindAsync(id);
            if (loaiThietBi != null)
            {
                _context.LoaiThietBis.Remove(loaiThietBi);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}