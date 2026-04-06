using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data; 
using DemoMVC.Models.Entities; // Phải dùng Entities vì Student nằm ở đây
using System.Linq;

namespace DemoWebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. HIỂN THỊ DANH SÁCH (Read) ---
        public IActionResult Index()
        {
            var data = _context.Students.ToList();
            return View(data);
        }

        // --- 2. THÊM MỚI (Create) ---
        // --- 2. THÊM MỚI (Create) ---

// Hàm này chỉ để mở trang nhập liệu (GET)
public IActionResult Create()
{
    return View();
}

// Hàm này xử lý khi người dùng bấm nút "Lưu lại" (POST)
[HttpPost]
[ValidateAntiForgeryToken] // Thêm lớp bảo mật này vào
public IActionResult Create(Student std) // std là đối tượng Student nhận từ Form
{
    // Bước kiểm tra quan trọng nhất của buổi học số 7
    if (ModelState.IsValid)
    {
        // Nếu các luật [Required], [Range]... ở Model đều thỏa mãn:
        _context.Students.Add(std);
        _context.SaveChanges();
        
        // Lưu xong thì quay về trang danh sách (Index)
        return RedirectToAction("Index");
    }

    // Nếu dữ liệu vi phạm luật (ví dụ: FullName để trống)
    // Trả lại View "Create" kèm theo đối tượng "std" để hiển thị lỗi ra màn hình
    return View(std);
}
        // --- 3. CHỈNH SỬA (Edit) ---
        // Đổi 'int id' thành 'string id' vì StudentCode là kiểu chuỗi
        public IActionResult Edit(string id) 
        {
            if (id == null) return NotFound();
            var std = _context.Students.Find(id);
            if (std == null) return NotFound();
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(std);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(std);
        }

     // --- 4. XÓA (Delete) ---

// Bước 1: Lấy thông tin sinh viên và hiển thị trang xác nhận (GET)
[HttpGet]
public IActionResult Delete(string id)
{
    if (id == null) return NotFound();

    // Tìm sinh viên trong CSDL bằng khóa chính (StudentCode)
    var std = _context.Students.Find(id);
    
    if (std == null) return NotFound();

    // Trả về View Delete.cshtml cùng dữ liệu sinh viên đó
    return View(std);
}

// Bước 2: Thực hiện xóa sau khi người dùng bấm nút "Xác nhận Xóa" (POST)
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken] // Bảo mật chống giả mạo request
public IActionResult DeleteConfirmed(string id)
{
    var std = _context.Students.Find(id);
    if (std != null)
    {
        _context.Students.Remove(std);
        _context.SaveChanges();
    }
    
    // Xóa xong quay về trang danh sách
    return RedirectToAction(nameof(Index));
}
    }

}