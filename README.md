 My Project Nguyen Ha Vy - 2221050573
1.Cấu trúc thư mục dự án .NET MVC
    Mô hình MVC (Model-View-Controller) chia ứng dụng thành 3 thành phần chính.
    ProjectName/
│
├── App_Start/          # Chứa các lớp cấu hình khi ứng dụng khởi chạy
│   ├── RouteConfig.cs  # Cấu hình định tuyến (URL routing)
│   └── BundleConfig.cs # Cấu hình gộp và nén file CSS/JS
│
├── Controllers/        # Chứa các lớp Controller (Xử lý logic điều hướng)
│   └── HomeController.cs
│
├── Models/             # Chứa các lớp đại diện cho dữ liệu và logic nghiệp vụ
│   └── User.cs
│
├── Views/              # Chứa các file giao diện (.cshtml)
│   ├── Home/           # Thư mục View tương ứng với HomeController
│   │   └── Index.cshtml
│   └── Shared/         # Chứa layout và view dùng chung (như _Layout.cshtml)
│
├── Content/            # Chứa các file tĩnh như CSS, hình ảnh (Static files)
├── Scripts/            # Chứa các file thư viện JavaScript
└── Web.config          # File cấu hình chính của toàn bộ ứng dụng (kết nối DB, biến môi trường...)

Controllers: Nơi nhận request từ người dùng, xử lý logic và quyết định trả về dữ liệu gì hoặc hiển thị view nào.

Views: Nơi chứa giao diện người dùng (UI). Các file này thường có đuôi .cshtml (Razor View Engine), cho phép viết mã C# lồng trong HTML.

Models: Định nghĩa cấu trúc dữ liệu (Object) và các quy tắc nghiệp vụ (Validation).

2. Namespace trong C#

### Khái niệm
**Namespace** (Không gian tên) trong C# là một cơ chế giúp tổ chức các lớp (class), giao diện (interface), struct... thành các nhóm logic để dễ quản lý. Nó giống như "Họ" trong tên người hoặc cấu trúc thư mục trong Windows.

### Tại sao cần Namespace?
a.  **Tránh xung đột tên:** Cho phép tạo các class có tên giống nhau nhưng nằm ở các namespace khác nhau (Ví dụ: `ProjectA.User` khác với `ProjectB.User`).
b.  **Tổ chức code:** Giúp code gọn gàng, dễ tìm kiếm. Trong mô hình MVC, Namespace thường đi theo cấu trúc thư mục vật lý.

### Ví dụ
```csharp
namespace QuanLySinhVien.Controllers
{
    public class HomeController : Controller 
    {
        // Code xử lý...
    }
}

3.Định tuyến (Routing) trong .NET MVC
    Routing là cơ chế ánh xạ một URP (đường dẫn trên trình duyệt) vào một ACTION cụ thể bên trong một CONTROLLER

    Trong file Program.cs, cấu hình mặc định thường như sau: 
    File cấu hình thường nằm tại App_Start/RouteConfig.cs.
    public class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
    }
}
Nguyên tắc hoạt động (Pattern): {controller}/{action}/{id}
{controller}: Tên của Controller (bỏ hậu tố "Controller").

{action}: Tên phương thức (Method) bên trong Controller đó.

{id}: Tham số tùy chọn (thường dùng để tìm kiếm ID bản ghi).

Ví dụ:

URL: domain.com/Product/Detail/5

Sẽ gọi đến class ProductController

Chạy hàm Detail(int id)

Với tham số id = 5
{controller}: Phân đoạn đầu tiên của URL xác định tên Controller (bỏ đuôi "Controller"). Mặc định là Home.

{action}: Phân đoạn thứ hai xác định tên hàm (method) trong Controller đó. Mặc định là Index.

{id?}: Phân đoạn thứ ba là tham số (thường dùng cho ID). Dấu ? nghĩa là tham số này là tùy chọn (có hoặc không cũng được).

Ví dụ thực tế
URL: / (Trang chủ) -> Gọi HomeController, hàm Index.

URL: /Student -> Gọi StudentController, hàm Index.

URL: /Student/Edit/5 -> Gọi StudentController, hàm Edit, tham số id = 5.

3. Controller và View trong .NET MVC
Đây là hai thành phần tương tác trực tiếp để tạo ra phản hồi cho người dùng.

A.Controller (Bộ điều khiển)
Vai trò: Là bộ não xử lý luồng đi của ứng dụng. Nó nhận yêu cầu (Request) từ trình duyệt thông qua Route, xử lý logic nghiệp vụ, lấy dữ liệu từ Model và quyết định trả về View nào.
Đặc điểm:

Là một class kế thừa từ lớp cha Controller.

Chứa các phương thức public được gọi là Action Methods.

Các Action thường trả về kiểu IActionResult.

vidu
    public class HomeController : Controller
{
    // Đây là một Action
    public ActionResult Index()
    {
        // Xử lý logic tại đây (lấy dữ liệu từ Model, tính toán...)
        string message = "Xin chào từ Controller!";
        
        // Truyền dữ liệu sang View qua ViewBag
        ViewBag.Message = message;

        // Trả về View tương ứng (Mặc định là Views/Home/Index.cshtml)
        return View();
    }
}

B. View (Giao diện)
Là nơi hiển thị dữ liệu cho người dùng. View sử dụng Razor Syntax (ký hiệu @) để hiển thị dữ liệu động từ Controller gửi sang.
Mối quan hệ: Controller truyền dữ liệu sang View thông qua các cơ chế như: ViewBag, ViewData, hoặc Model (Strongly typed).

Ví dụ file Views/Home/Index.cshtml:

@{
    ViewBag.Title = "Trang chủ";
}

<h2>@ViewBag.Title</h2>

<p>@ViewBag.Message</p> ```

### Luồng hoạt động (Workflow)
1.  **Request:** Người dùng truy cập URL.
2.  **Routing:** Hệ thống xác định Controller và Action cần gọi.
3.  **Controller:** Thực thi logic, lấy dữ liệu (nếu cần).
4.  **View:** Controller trả dữ liệu về View để render ra HTML.
5.  **Response:** HTML được trả về trình duyệt người dùng.


Quy trình hoạt động tóm tắt
User nhập URL.

Routing tìm đúng Controller và Action.

Controller xử lý, gọi dữ liệu từ Model (nếu cần).

Controller trả về một View cụ thể.

View render ra mã HTML và gửi trả về trình duyệt cho User.
---
