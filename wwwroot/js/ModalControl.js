function showModal(modalId) {
    $(`#${modalId}`).modal('show');
    console.log("This is working");
}

function hideModal(modalId) {
    $(`#${modalId}`).modal('hide');
}

$(document).ready(function () {
    fetchDepartments();

    $('#department').change(function () {
        var department = $(this).val();
        fetchPositions(department);
        fetchSupervisors(department);
    });
});

function fetchDepartments() {
    $.ajax({
        type: "GET",
        url: "?handler=Departments",
        contentType: "application/json",
        success: function (data) {
            var departmentSelect = $('#department');
            departmentSelect.empty().append('<option selected="true" disabled="disabled">Select a Department</option>');
            $.each(data, function (key, entry) {
                departmentSelect.append($('<option></option>').attr('value', entry).text(entry));
            });
        }
    });
}

function fetchPositions(department) {
    $.ajax({
        type: "GET",
        url: "?handler=Positions",
        data: { department: department },
        contentType: "application/json",
        success: function (data) {
            var positionSelect = $('#position');
            positionSelect.empty().append('<option selected="true" disabled="disabled">Select Position</option>');
            $.each(data, function (key, entry) {
                positionSelect.append($('<option></option>').attr('value', entry.Poid).text(entry.Name));
            });
        }
    });
}

function fetchSupervisors(department) {
    $.ajax({
        type: "GET",
        url: "?handler=Supervisors",
        data: { department: department },
        contentType: "application/json",
        success: function (data) {
            var supervisorSelect = $('#supervisor');
            supervisorSelect.empty().append('<option selected="true" disabled="disabled">Select Supervisor</option>');
            $.each(data, function (key, entry) {
                // Assuming 'Emid' and 'Name' are the correct properties for supervisor ID and name
                supervisorSelect.append($('<option></option>').attr('value', entry.Emid).text(entry.Name));
            });
        }
    });
}