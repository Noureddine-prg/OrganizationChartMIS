
$(document).ready(function () {
    fetchOrgChartData();
});

function showModal(modalId) {
    $(`#${modalId}`).modal('show');
    console.log("Modal shown:", modalId);
}

function hideModal(modalId) {
    $(`#${modalId}`).modal('hide');
}

function fetchOrgChartData() {
    $.ajax({
        type: "GET",
        url: "?handler=OrgChartData", 
        dataType: "json", 
        success: function (data) {
            console.log("Org Chart Data Loaded");
            initializeOrgChart(data); 
        },
        error: function (error) {
            console.error("Error fetching Org Chart data:", error);
        }
    });
}

function initializeOrgChart(data) {
    console.log("Initializing OrgChart with data:", data);

    var orgChartDataSource = new kendo.data.OrgChartDataSource({
        data: data,
        schema: {
            model: {
                id: "NodeId",
                parentId: "ReportsToNodeId",
                fields: {
                    NodeId: { field: "NodeId", type: "string", nullable: false },
                    ReportsToNodeId: { field: "ReportsToNodeId", type: "string", nullable: true },
                    PositionName: { field: "PositionName", type: "string" },
                    EmployeeName: { field: "EmployeeName", type: "string", nullable: true },
                    EmployeeEmail: { field: "EmployeeEmail", type: "string", nullable: true },
                    DepartmentName: { field:  "DepartmentName", type: "string", nullable: true }
                }
            }
        }
    });


    $("#orgchart").kendoOrgChart({
        dataSource: orgChartDataSource,
        dataTextField: "parentId",
        template: $("#customOrgChartTemplate").html(),
        tools: ["create","edit", "delete"],
        layout: "tree"
    });

    console.log("OrgChart initialized with data source:", orgChartDataSource);
}

function toggleMenu(button) {
    var menu = $(button).next('ul');
    $('.k-orgchart-tools ul').not(menu).hide();
    menu.toggle();
}

$(document).click(function (event) {
    if (!$(event.target).closest('.k-orgchart-tool, .k-orgchart-tools').length) {
        $('.k-orgchart-tools ul').hide();
    }
});

function populatePositions(departmentId) {
    console.log("Populating positions for departmentId:", departmentId);
    $.ajax({
        type: "GET",
        url: `?handler=PositionsByDepartment&departmentId=${departmentId}`,
        success: function (positions) {
            console.log("Positions fetched:", positions);
            $('#positionId').empty(); 
            positions.forEach(function (position) {
                $('#positionId').append(new Option(position.Name, position.Poid));
            });
        },
        error: function (error) {
            console.error("Error fetching positions:", error);
        }
    });
}

function populateEmployees() {
    console.log("Populating employees");
    $.ajax({
        type: "GET",
        url: `?handler=AllEmployees`,
        success: function (employees) {
            console.log("Employees fetched:", employees);
            $('#employeeId').empty();
            employees.forEach(function (employee) {
                $('#employeeId').append(new Option(employee.Name, employee.Emid));
            });
        },
        error: function (error) {
            console.error("Error fetching employees:", error);
        }
    });
}


function saveNode() {
    var nodeData = {
        PositionId: $('#positionId').val(),
        EmployeeId: $('#employeeId').val(),
        TeamId: null,
        ReportsToNodeId: $('#parentId').val()
    };

    $.ajax({
        type: "POST",
        url: "?handler=CreateNode",
        data: JSON.stringify(nodeData),
        contentType: "application/json",
        success: function () {
            $('#createNodeModal').modal('hide');
            fetchOrgChartData();
        },
        error: function (error) {
            console.error("Error saving node:", error);
        }
    });
}

function createNodeModal(parentNodeId) {
    console.log("Create node called with parentNodeId:", parentNodeId);
    console.log(parentNodeId.DepartmentName)
    $.ajax({
        type: "GET",
        url: `?handler=Node&nodeId=${parentNodeId}`,
        success: function (parentNode) {
            console.log("Parent node details fetched:", parentNode);
            $('#parentId').val(parentNode.NodeId);
            $('#departmentId').val(parentNode.DepartmentName);
            populatePositions(parentNode.DepartmentName); 
            showModal('createNodeModal'); 
        },
        error: function (error) {
            console.error("Error fetching parent node details:", error);
        }
    });
}