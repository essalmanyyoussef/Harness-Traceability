using Harness_Traceability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harness_Traceability
{
    internal class ClsTrackingLog
    {
        public static int InsertTrackingLog(
            string serialNumber,
            string site,
            string username,
            DateTime dateTime,
            string additionalInfo,
            string type)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            param.Add("@SerialNumber", serialNumber);
            param.Add("@Site", site);
            param.Add("@Username", username);
            param.Add("@DateTime", dateTime);
            param.Add("@AdditionalInfo", additionalInfo);
            param.Add("@Type", type);

            return ClsData.ExecuteProcedure("sp_InsertTrackingLog", param, true);
        }
    }
}
