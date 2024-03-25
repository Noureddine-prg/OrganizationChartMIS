

$(document).ready(function ()
{
    fetchDepartments();
    $('#openModalButton').click(function () {
        $('#addEmployeeModal').modal('show');
    });

    $('#department').change(function () {
        var department = $(this).val();
        fetchPositions(department);
        fetchSupervisors(department);
    });
});

function showModal(modalId) {
    $(`#${modalId}`).modal('show');
    console.log("working");
}

function hideModal(modalId) {
    $(`#${modalId}`).modal('hide');
}

function fetchDepartments()
{
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

function fetchPositions(department)
{
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

function fetchSupervisors(department)
{
    $.ajax({
        type: "GET",
        url: "?handler=Supervisors",
        data: { department: department },
        contentType: "application/json",
        success: function (data) {
            var supervisorSelect = $('#supervisor');
            supervisorSelect.empty().append('<option selected="true" disabled="disabled">Select Supervisor</option>');
            $.each(data, function (key, entry) {
                supervisorSelect.append($('<option></option>').attr('value', entry.Emid).text(entry.Name));
            });
        }
    });
}

function createEmployee() {
    let formData = $('#createEmployeeForm').serializeArray(); 
    formData.push({ name: "Status", value: $('#status').val() }); 

    console.log(formData)

    let formUrlEncodedData = formData.map(obj => `${encodeURIComponent(obj.name)}=${encodeURIComponent(obj.value)}`).join('&');

    $.ajax({
        type: "POST",
        url: "?handler=AddNewEmployee",
        data: formUrlEncodedData,
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
