
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
                    DepartmentName: { field: "DepartmentName", type: "string", nullable: true }
                }
            }
        }
    });


    $("#orgchart").kendoOrgChart({
        dataSource: orgChartDataSource,
        dataTextField: "parentId",
        template: $("#customOrgChartTemplate").html(),
        tools: ["edit", "delete"],
        layout: "tree"
    });

    console.log("OrgChart initialized with data source:", orgChartDataSource);
}

function editNode(nodeId) {
    
}

function deleteNode(nodeId) {

}

function addNode(){

}

function toggleMenu(button) {
    var menu = button.nextElementSibling;
    $(menu).toggle(); 
}

$(document).click(function (event) {
    if (!$(event.target).closest('.k-orgchart-tool, .k-orgchart-tools').length) {
        $('.k-orgchart-tools ul').hide();
    }
});