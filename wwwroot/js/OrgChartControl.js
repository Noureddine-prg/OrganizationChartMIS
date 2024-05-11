
$(document).ready(function () {
    fetchOrgChartData();
});

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
console.log("Initializing OrgChart with data", data);

    var orgChartDataSource = new kendo.data.OrgChartDataSource({
        data: data,
        schema: {
            model: {
                Id: "id", 
                parentId: "parentId", 
                fields: {
                    id: { field: "NodeId", type: "string", nullable: false },
                    parentId: { field: "ReportsToNodeId", type: "string", nullable: true },
                    title: { field: "PositionName", type: "string" },
                    name: { field: "EmployeeName", type: "string", nullable: true },
                    EmployeeEmail: { field: "EmployeeEmail", type: "string", nullable: true },
                    DepartmentName: { field: "DepartmentName", type: "string", nullable: true }
                }
            }
        }
    });

    $("#orgchart").kendoOrgChart({
        dataSource: orgChartDataSource,
        dataTextField: "PositionName",
    });

console.log("OrgChart initialized");
}