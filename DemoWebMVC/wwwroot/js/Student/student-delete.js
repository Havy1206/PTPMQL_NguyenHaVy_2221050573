function showDeleteModal(id) {
    $.ajax({
        url: '/Student/Delete/' + id,
        type: 'GET',
        success: function (res) {
            $('#modalContainer').html(res);
            const modal = new bootstrap.Modal(document.getElementById('deleteStudentModal'));
            modal.show();
        }
    });
}

$(document).on('submit', '#deleteStudentForm', function (e) {
    e.preventDefault();
    $.ajax({
        url: '/Student/Delete',
        type: 'POST',
        data: $(this).serialize(),
        success: function (res) {
            if (res.success) {
                bootstrap.Modal.getInstance(document.getElementById('deleteStudentModal')).hide();
                loadStudents(1);
            }
        }
    });
});
