
using Microsoft.AspNetCore.Mvc;

namespace DemoWebMVC.Controllers
{
    public class Buoi4Controller : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Gửi dữ liệu từ Controller sang View
            ViewBag.Message = "Thông báo!";
            
            return View(); 
        }

        [HttpPost]
        public IActionResult Index(string hoTen) 
        {
            // Xử lý: Gửi thông báo kèm họ tên ngược lại View
            ViewBag.LoiChao = "Xin chào " + hoTen;

            return View();
        }  
    }
}