$(document).ready(function () {
    console.log("Document ready, fetching departments...");
    fetchDepartments();

    $('#department').change(function () {
        var departmentId = $(this).val();
        console.log("Department selected:", departmentId);
        fetchPositions(departmentId);
    });
});


// Show / Hide Modal
function showModal(modalId) {
    $(`#${modalId}`).modal('show');
    console.log("Modal shown:", modalId);
}

function hideModal(modalId) {
    $(`#${modalId}`).modal('hide');
}

// Specific Grabs 
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
            console.log("Fetch Positions")
            $('#position').empty().append('<option selected="true" disabled="disabled">Select Position</option>');
            $.each(data, function (key, entry) {
                $('#position').append($('<option></option>').attr('value', entry.Poid).text(entry.Name));
            });
            console.log(data)
        }
    });
}

// Gonna change this to grab  level and look for all people that are one level above that position in same department
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

// Employee
function createEmployee() {
    let formData = $('#createEmployeeForm').serializeArray();
    $.ajax({
        type: "POST",
        url: "?handler=AddNewEmployee",
        data: formData,
        success: function (formData) {
            console.log("Employee Created!");
            $('#addEmployeeModal').modal('hide');
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.error("Error creating employee:", error);
        }
    });
}

function updateEmployeeModal(emid) {
    fetchDepartments();

    $.ajax({
        type: "GET",
        url: `?handler=EditEmployee`,
        data: { emid: emid }, 
        success: function (data) {
            console.log(data);
            console.log(data.Emid, "This should be employee data");
            
            $('#emid').val(data.Emid); 
            $('#updateName').val(data.Name); 
            $('#updateEmail').val(data.Email);


            showModal('updateEmployeeModal');
        },
        error: function () {
            alert("Failed to fetch employee details.");
        }
    });
}

function updateEmployee() {
    let formData = $('#updateEmployeeForm').serialize();
    $.ajax({
        type: "POST",
        url: "?handler=UpdateEmployee",
        data: formData,
        success: function () {
            $('#updateEmployeeModal').modal('hide');
            alert('Employee updated successfully');
            window.location.reload();
        },
        error: function () {
            alert('Error updating employee');
        }
    });
}

function deleteEmployee() {
    let emid = $('#deleteEmployeeId').val(); 
    $.ajax({
        type: "POST",
        url: "?handler=DeleteEmployee",
        data: { emid: emid },
        success: function () {
            $('#deleteEmployeeModal').modal('hide');
            alert('Employee deleted successfully');
            window.location.reload();
        },
        error: function () {
            alert('Error deleting employee');
        }
    });
}

// positions 