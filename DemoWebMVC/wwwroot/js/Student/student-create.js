$(document).on('click', '#btnAddStudent', function () {
    $.ajax({
        url: '/Student/Create',
        type: 'GET',
        success: function (res) {
            $('#modalContainer').html(res);
            const modal = new bootstrap.Modal(document.getElementById('createStudentModal'));
            modal.show();
        },
        error: function () {
            alert('Không thể tải form thêm sinh viên');
        }
    });
});

$(document).on('submit', '#createStudentForm', function (e) {
    e.preventDefault();
    let form = $(this);
    $.ajax({
        url: '/Student/Create',
        type: 'POST',
        data: form.serialize(),
        success: function (res) {
            if (res.success) {
                bootstrap.Modal.getInstance(document.getElementById('createStudentModal')).hide();
                loadStudents(1); // Load lại bảng
            } else {
                $('#modalContainer').html(res);
            }
        }
    });
});