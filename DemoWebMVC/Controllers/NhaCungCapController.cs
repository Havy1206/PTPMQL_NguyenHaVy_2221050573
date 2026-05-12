using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;

namespace DemoMVC.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhaCungCapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. HIỂN THỊ DANH SÁCH ---
        public async Task<IActionResult> Index()
        {
            var data = await _context.NhaCungCaps.ToListAsync();
            return View(data);
        }

        // --- 2. FORM THÊM MỚI (GET) ---
        public IActionResult Create()
        {
            return View();
        }

        // --- 3. XỬ LÝ THÊM MỚI (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                _context.NhaCungCaps.Add(ncc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ncc);
        }

        // --- 4. XEM CHI TIẾT (GET) ---
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ncc = await _context.NhaCungCaps.FirstOrDefaultAsync(m => m.Id == id);
            if (ncc == null) return NotFound();
            return View(ncc);
        }

        // --- 5. FORM SỬA (GET) ---
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ncc = await _context.NhaCungCaps.FindAsync(id);
            if (ncc == null) return NotFound();
            return View(ncc);
        }

        // --- 6. XỬ LÝ SỬA (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NhaCungCap ncc)
        {
            if (id != ncc.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(ncc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ncc);
        }

        // --- 7. FORM XÁC NHẬN XÓA (GET) ---
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ncc = await _context.NhaCungCaps.FirstOrDefaultAsync(m => m.Id == id);
            if (ncc == null) return NotFound();
            return View(ncc);
        }

        // --- 8. XỬ LÝ XÓA (POST) ---
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ncc = await _context.NhaCungCaps.FindAsync(id);
            if (ncc != null)
            {
                _context.NhaCungCaps.Remove(ncc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}