using System;
using System.Collections.Generic;
using System.Data;

namespace Harness_Traceability.Models
{
    public class ClsClaims
    {
        // Insert a new claim
        public static int InsertClaim(
            string title,
            string description,
            string issueType,
            string severity,
            int supplierId,
            int siteId,
            string connectorPN,
            string attachmentPN,
            DateTime dueDate,
            int createdByUserId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@Title", title);
            param.Add("@Description", description);
            param.Add("@IssueType", issueType);
            param.Add("@Severity", severity);
            param.Add("@SupplierId", supplierId);
            param.Add("@SiteId", siteId);
            param.Add("@ConnectorPN", connectorPN);
            param.Add("@AttachmentPN", attachmentPN);
            param.Add("@DueDate", dueDate);
            param.Add("@CreatedByUserId", createdByUserId);

            // Stored procedure returns ClaimId in @outer
            return ClsData.ExecuteProcedure("sp_InsertClaim", param, true);
        }

        // Insert attachment
        public static void InsertAttachment(int claimId, string fileName, string fileType, byte[] fileData, int userId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@ClaimId", claimId);
            param.Add("@FileName", fileName);
            param.Add("@FileType", fileType);
            param.Add("@FileData", fileData);
            param.Add("@UploadedByUserId", userId);

            ClsData.ExecuteProcedure("sp_InsertClaimAttachment", param, false);
        }

        // Insert event
        public static void InsertEvent(int claimId, string eventType, string newStatus, int userId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@ClaimId", claimId);
            param.Add("@EventType", eventType);
            param.Add("@NewStatus", newStatus);
            param.Add("@CreatedByUserId", userId);

            ClsData.ExecuteProcedure("sp_InsertClaimEvent", param, false);
        }
    }
}
