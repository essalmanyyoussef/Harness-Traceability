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


        public static int Insert_Statistics_TrackingLog(
            string Host_Name,
            string username,
            DateTime start_dateTime,
            DateTime end_datetime,
            string additionalInfo)
        {
            Dictionary<string, object> param =
                new Dictionary<string, object>();

            param.Add("@Hostname", Host_Name);
            param.Add("@Username", username);
            param.Add("@Start_DateTime", start_dateTime);
            param.Add("@End_DateTime", end_datetime);
            param.Add("@AdditionalInfo", additionalInfo);

            return ClsData.ExecuteProcedure(
                "sp_Insert_Statistics_TrackingLog",
                param,
                false);
        }

    }
}
