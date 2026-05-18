function showEditModal(id) {
    $.ajax({
        url: '/Student/Edit/' + id,
        type: 'GET',
        success: function (res) {
            $('#modalContainer').html(res);
            const modal = new bootstrap.Modal(document.getElementById('editStudentModal'));
            modal.show();
        }
    });
}

$(document).on('submit', '#editStudentForm', function (e) {
    e.preventDefault();
    let form = $(this);
    $.ajax({
        url: '/Student/Edit',
        type: 'POST',
        data: form.serialize(),
        success: function (res) {
            if (res.success) {
                bootstrap.Modal.getInstance(document.getElementById('editStudentModal')).hide();
                loadStudents(1);
            }
        }
    });
});