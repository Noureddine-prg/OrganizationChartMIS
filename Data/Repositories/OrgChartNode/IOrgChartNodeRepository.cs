using OrganizationChartMIS.Data.Context;
using OrgChartNodeObject = OrganizationChartMIS.Data.Models.OrgChartNode;

namespace OrganizationChartMIS.Data.Repositories.OrgChartNode
{
    public interface IOrgChartNodeRepository
    {
        List<OrgChartNodeObject> GetAllNodes();
        OrgChartNodeObject GetNodeById(int nid);
        void AddNode(OrgChartNodeObject node);
        void UpdateNode(OrgChartNodeObject node); 
        void DeleteNode(int nid);
    }
}
