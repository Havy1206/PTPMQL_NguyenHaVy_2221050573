// Biến toàn cục để quản lý phân trang cho Student
let currentStudentPage = 1;
let studentPageSize = 10;

$(document).ready(function () {
    // Tự động load danh sách sinh viên lần đầu khi trang Index sẵn sàng
    if (typeof loadStudents === "function") {
        loadStudents(currentStudentPage);
    }
});