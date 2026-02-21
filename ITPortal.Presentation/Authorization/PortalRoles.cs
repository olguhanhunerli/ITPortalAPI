using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Presentation.Authorization
{
    public static class PortalRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string SystemAdmin = "SystemAdmin";
        public const string OrgAdmin = "OrgAdmin";

        public const string Employee = "Employee";

        public const string ServiceDeskAgent = "ServiceDeskAgent";
        public const string ServiceDeskLead = "ServiceDeskLead";

        public const string AccessApprover = "AccessApprover";
        public const string ChangeManager = "ChangeManager";
        public const string IncidentManager = "IncidentManager";
        public const string ProblemManager = "ProblemManager";

        public const string AssetManager = "AssetManager";
        public const string Procurement = "Procurement";

        public const string KnowledgeAuthor = "KnowledgeAuthor";
        public const string KnowledgeReviewer = "KnowledgeReviewer";

        public const string ReportAdmin = "ReportAdmin";
        public const string ReportViewer = "ReportViewer";
    }

    public static class RoleGroups
    {
        public const string SuperAdmins =
            PortalRoles.SuperAdmin;

        public const string SystemAdmins =
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string OrgAdmins =
            PortalRoles.OrgAdmin + "," + PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string FullAccess =
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string AnyAdmin =
            PortalRoles.OrgAdmin + "," + PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string PortalUsers =
            PortalRoles.Employee + "," +
            PortalRoles.ServiceDeskAgent + "," + PortalRoles.ServiceDeskLead + "," +
            PortalRoles.AccessApprover + "," + PortalRoles.ChangeManager + "," +
            PortalRoles.IncidentManager + "," + PortalRoles.ProblemManager + "," +
            PortalRoles.AssetManager + "," + PortalRoles.Procurement + "," +
            PortalRoles.KnowledgeAuthor + "," + PortalRoles.KnowledgeReviewer + "," +
            PortalRoles.ReportViewer + "," + PortalRoles.ReportAdmin + "," +
            PortalRoles.OrgAdmin + "," + PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string TicketCreate =
            PortalRoles.Employee + "," +
            PortalRoles.ServiceDeskAgent + "," + PortalRoles.ServiceDeskLead + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string TicketReadWrite =
            PortalRoles.ServiceDeskAgent + "," + PortalRoles.ServiceDeskLead + "," +
            PortalRoles.IncidentManager + "," + PortalRoles.ProblemManager + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string TicketConfig =
            PortalRoles.ServiceDeskLead + "," + PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string AccessApproval =
            PortalRoles.AccessApprover + "," + PortalRoles.ServiceDeskLead + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string ChangeApproval =
            PortalRoles.ChangeManager + "," + PortalRoles.ServiceDeskLead + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string MajorIncidentManage =
            PortalRoles.IncidentManager + "," + PortalRoles.ServiceDeskLead + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string ProblemManage =
            PortalRoles.ProblemManager + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string AssetManage =
            PortalRoles.AssetManager + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string ProcurementManage =
            PortalRoles.Procurement + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string KnowledgeRead =
            PortalRoles.Employee + "," +
            PortalRoles.KnowledgeAuthor + "," + PortalRoles.KnowledgeReviewer + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string KnowledgeWrite =
            PortalRoles.KnowledgeAuthor + "," + PortalRoles.KnowledgeReviewer + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string KnowledgePublish =
            PortalRoles.KnowledgeReviewer + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string ReportsView =
            PortalRoles.ReportViewer + "," + PortalRoles.ReportAdmin + "," +
            PortalRoles.ServiceDeskLead + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string ReportsAdmin =
            PortalRoles.ReportAdmin + "," +
            PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;

        public const string MasterDataWrite =
            PortalRoles.OrgAdmin + "," + PortalRoles.SystemAdmin + "," + PortalRoles.SuperAdmin;
    }
}
