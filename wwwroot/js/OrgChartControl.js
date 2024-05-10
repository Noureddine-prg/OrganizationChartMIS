

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "?handler=OrgChartNodes",
        dataType: "json",
        success: function (data) {
            $("#orgChart").kendoOrgChart({
                dataSource: {
                    data: data, 
                    schema: {
                        model: {
                            id: "NodeId",
                            parentId: "ReportsToNodeId",
                            fields: {
                                NodeId: { type: "string" },
                                ReportsToNodeId: { type: "string" },
                                PositionName: { type: "string" },
                                EmployeeName: { type: "string" },
                                EmployeeEmail: { type: "string" }
                            }
                        }
                    }
                },
                dataTextField: "PositionName",
                renderAs: "canvas"
            });
        },
        error: function (xhr, status, error) {
            console.error("Error fetching OrgChart data: ", status, error);
        }
    });
});
