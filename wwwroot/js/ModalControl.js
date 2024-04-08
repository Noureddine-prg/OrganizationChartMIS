$(document).ready(function () {
    console.log("Document ready, fetching departments...");
    fetchDepartments();

    $('#openModalButton').click(function () {
        $('#addEmployeeModal').modal('show');
    });

    $('#department').change(function () {
        var departmentId = $(this).val();
        console.log("Department selected:", departmentId);
        fetchPositions(departmentId);
    });
});

function showModal(modalId) {
    $(`#${modalId}`).modal('show');
    console.log("Modal shown:", modalId);
}

function hideModal(modalId) {
    $(`#${modalId}`).modal('hide');
}

function fetchDepartments() {
    $.ajax({
        type: "GET",
        url: "?handler=Departments",
        success: function (data) {
            var departmentSelect = $('#department');
            departmentSelect.empty().append('<option selected="true" disabled="disabled">Select a Department</option>');
            $.each(data, function (key, entry) {
                departmentSelect.append($('<option></option>').attr('value', entry.doid).text(entry.Name));
            });
        },
        error: function (xhr, status, error) {
            console.error("Error fetching departments:", status, error);
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
            $('#position').empty().append('<option selected="true" disabled="disabled">Select Position</option>');
            $.each(data, function (key, entry) {
                console.log(entry.Poid)
                console.log(entry.Name)
                $('#position').append($('<option></option>').attr('value', entry.Poid).text(entry.Name));
            });
            console.log(data)
        }
    });
}

//gonna change this to grab  level and look for all people that are one level above that position in same department
function fetchSupervisors(departmentId) {

    $.ajax({
        type: "GET",
        url: "?handler=Supervisors",
        data: { departmentId: departmentId },
        contentType: "application/json",
        success: function (data) {
            $('#supervisor').empty().append('<option selected="true" disabled="disabled">Select Supervisor</option>');
            $.each(data, function (key, entry) {
                $('#supervisor').append($('<option></option>').attr('value', entry.emid).text(entry.name));
            });
        }
    });
}

function createEmployee() {
    let formData = $('#createEmployeeForm').serializeArray();

    console.log(formData);

    $.ajax({
        type: "POST",
        url: "?handler=AddNewEmployee",
        data: formData,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (response) {
            console.log("Employee Created!", response);
            $('#addEmployeeModal').modal('hide');
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error("Error creating employee:", error);
        }
    });
}

