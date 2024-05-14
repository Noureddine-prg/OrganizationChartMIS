
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
                id: "NodeId",
                parentId: "ReportsToNodeId",
                fields: {
                    NodeId: { type: "string", nullable: false },
                    ReportsToNodeId: { type: "string", nullable: true },
                    PositionName: { type: "string" },
                    EmployeeName: { type: "string", nullable: true },
                    EmployeeEmail: { type: "string", nullable: true },
                    DepartmentName: { type: "string", nullable: true }
                }
            }
        }
    });

    $("#orgchart").kendoOrgChart({
        dataSource: orgChartDataSource,
        template: kendo.template(
            "<div class='custom-node'>" +
            "<div class='field'><strong>Position:</strong> #: PositionName #</div>" +
            "<div class='field'><strong>Name:</strong> #: EmployeeName ? EmployeeName : 'Vacant' #</div>" +
            "<div class='field'><strong>Email:</strong> #: EmployeeEmail ? EmployeeEmail : 'No email' #</div>" +
            "<div class='field'><strong>Department:</strong> #: DepartmentName ? DepartmentName : 'No department' #</div>" +
            "</div>"
        ),
    });
    console.log("OrgChart initialized with data source:", orgChartDataSource);


    console.log("OrgChart initialized");
}

function editNode(nodeId) {
    
}

function deleteNode(nodeId) {

}

function addNode(){

}