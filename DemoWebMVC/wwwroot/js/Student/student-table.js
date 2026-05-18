// wwwroot/js/Student/student-table.js

function loadStudents(page) {
    // Nếu không truyền page vào thì lấy giá trị hiện tại, mặc định là 1
    let targetPage = page || currentStudentPage || 1;

    $.ajax({
        url: '/Student/GetStudents',
        type: 'GET',
        data: { 
            page: targetPage, 
            pageSize: studentPageSize // Dùng biến từ student.js
        },
        success: function (res) {
            // Đổ dữ liệu vào khung chứa bảng
            $('#studentTableContainer').html(res);
            
            // QUAN TRỌNG: Cập nhật lại trang hiện tại sau khi load thành công
            currentStudentPage = targetPage;
        },
        error: function () {
            alert('Lỗi khi tải danh sách sinh viên!');
        }
    });
}

