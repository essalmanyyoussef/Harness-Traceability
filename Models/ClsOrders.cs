using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harness_Traceability.Models
{
     
    public class ClsOrders
    {


        //// SP View     public static DataTable ExecuteView(String View, String Fields, String Condition)
        ///
        public static DataTable GetTraceability(string p)
        {
            return ClsData.ExecuteView("correlations_trial", "DISTINCT Ref AS Reference, BC AS [Travel Ticket Label], Test AS Station, \r\n       IN_Barcode_1 AS [2nd Label], IN_Barcode_2 AS [3rd Travel Ticket Label], \r\n       IN_Barcode_3 AS [4th Travel Ticket Label], OUT_Barcode AS [Final Label], \r\n       DateEnd AS [Test Date], Rework AS Reworked, Rework_ID AS [Rework ID] , Hostname", "OUT_Barcode='"+p+ "'");
        }
        public static DataTable GetHostnameByOutBarcode(string p)
        {
            return ClsData.ExecuteView("correlations_trial", "DISTINCT Hostname, Test AS Station", "OUT_Barcode='" + p + "'");
        }
        public static DataTable GetHostnameByRef(string p)
        {
            return ClsData.ExecuteView("correlations_trial", "DISTINCT Hostname, Test AS Station", "Ref='" + p + "'");
        }
        public static DataTable GetHostnameByTT(string p)
        {
            return ClsData.ExecuteView("correlations_trial", "DISTINCT Hostname, Test AS Station", "BC='" + p + "'");
        }
        public static DataTable GetTraceability2(string TT_label)
        {
            return ClsData.ExecuteView("correlations_trial", "DISTINCT Ref AS Reference, BC AS [Travel Ticket Label], Test AS Station, \r\n       IN_Barcode_1 AS [2nd Label], IN_Barcode_2 AS [3rd Label], \r\n       IN_Barcode_3 AS [4th Label], OUT_Barcode AS [Final Label], \r\n       DateEnd AS [Test Date], Rework AS Reworked, Rework_ID AS [Rework ID] , Hostname ", "BC='" + TT_label + "' order by DateEnd asc");
        }
        //ClsData.ExecuteView("Ref, COUNT(*)", "correlations_trial", "Test LIKE '%EL%' AND[DateEnd] >= '2024-07-25'  AND[DateEnd] < '2024-07-26'");
        public static DataTable Statistics(string Hostname,string Date1, string Date2)
        {
            return ClsData.ExecuteView("correlations_trial", "Ref as [Reference], BC AS [Travel Ticket Label], Test AS Station, \r\n       IN_Barcode_1 AS [2nd Label], IN_Barcode_2 AS [3rd Travel Ticket Label], \r\n       IN_Barcode_3 AS [4th Travel Ticket Label], OUT_Barcode AS [Final Label], \r\n       DateEnd AS [Test Date], Rework AS Reworked, Rework_ID AS [Rework ID] , Hostname", "Hostname LIKE '"+Hostname+"' AND[DateEnd] >= '" + Date1 + "'  AND[DateEnd] < '" + Date2 + "'");
            //return ClsData.ExecuteView("correlations_trial", "COUNT(*) AS Qty, Ref, CONVERT(DATE, DateEnd) AS Date", "Test LIKE '%EL%' AND[DateEnd] >= '" + Date1 + "'  AND[DateEnd] < '" + Date2 + "' GROUP BY     Ref,     CONVERT(DATE, DateEnd) order by Qty desc");
            //Reference, BC AS [Travel Ticket Label], Test AS Station, \r\n       IN_Barcode_1 AS [2nd Label], IN_Barcode_2 AS [3rd Travel Ticket Label], \r\n       IN_Barcode_3 AS [4th Travel Ticket Label], OUT_Barcode AS [Final Label], \r\n       DateEnd AS [Test Date], Rework AS Reworked, Rework_ID AS [Rework ID] , Hostname
        }
        public static DataTable Harness_Details(string Harn_ID, string Host)
        {
            return ClsData.ExecuteView("dbo.tests", "Hostname, Bank AS Family, Ref AS Reference, Batch, Part AS [Travel Ticket], Worker, DateBegin AS [Start Test], DateEnd AS [End Test], TimeU, State, Incident, Type, Counterpart1, Counterpart2, Object, Wire1, Wire2, Way", "Hostname = '" + Host + "' AND Part = '" + Harn_ID + "'");
            //return ClsData.ExecuteView("dbo.tests", "Hostname, Bank AS Family, Ref AS Reference, Batch, Part AS [Travel Ticket], Worker, DateBegin AS [Start Test], DateEnd AS [End Test], TimeU, State, Incident, Type, Object", "Hostname = '" + Host + "' AND Part = '" + Harn_ID + "'");
        }
        public static DataTable SelvedError(string p1)
        {
            //public static DataTable SelvedError(string p1, string p2, string p3)
            //return ClsData.ExecuteView("[dbo].[solvederrors]", "*", "[Part]='" + p1 + "' or [Part]='" + p2 + "' or [Part]='" + p3 + "'");
            return ClsData.ExecuteView("[dbo].[solvederrors]", "Ref AS Reference, Batch, Part AS [Travel Ticket Label], Worker AS [Worker ID], Type AS [Error Type], Counterpart1 AS [TMP 01], Counterpart2 AS [TMP 02], Object, Wire1, Wire2, Way, DateBegin AS [Start Date], \r\n                         DateEnd AS [End Date], TimeU AS [Time U], CPV1, CPV2, CPV3, CPV4,Hostname\r\n", "[Part]='" + p1 + "'");
            //return ClsData.ExecuteView("[dbo].[solvederrors]", "Ref AS Reference, Batch, Part AS [Travel Ticket Label], Worker AS [Worker ID], Type AS [Error Type], Counterpart1 AS [TMP 01], Counterpart2 AS [TMP 02], Object, Wire1, Wire2, Way, DateBegin AS [Start Date], \r\n                         DateEnd AS [End Date], TimeU AS [Time U], CPV1, CPV2, CPV3, CPV4\r\n", "[Part]='" + p1 + "' or [Part]='" + p3 + "' or [Part]='" + p3 + "'");
        }

        public static DataTable SelvedError_by_TT(string p1)
        {
            //public static DataTable SelvedError(string p1, string p2, string p3)
            //return ClsData.ExecuteView("[dbo].[solvederrors]", "*", "[Part]='" + p1 + "' or [Part]='" + p2 + "' or [Part]='" + p3 + "'");
            return ClsData.ExecuteView("[dbo].[solvederrors]", "Ref AS Reference, Batch, Part AS [Travel Ticket Label], Worker AS [Worker ID], Type AS [Error Type], Counterpart1 AS [TMP 01], Counterpart2 AS [TMP 02], Object, Wire1, Wire2, Way, DateBegin AS [Start Date], \r\n                         DateEnd AS [End Date], TimeU AS [Time U], CPV1, CPV2, CPV3, CPV4,Hostname\r\n", "[Part]='" + p1 + "'");
            //return ClsData.ExecuteView("[dbo].[solvederrors]", "Ref AS Reference, Batch, Part AS [Travel Ticket Label], Worker AS [Worker ID], Type AS [Error Type], Counterpart1 AS [TMP 01], Counterpart2 AS [TMP 02], Object, Wire1, Wire2, Way, DateBegin AS [Start Date], \r\n                         DateEnd AS [End Date], TimeU AS [Time U], CPV1, CPV2, CPV3, CPV4\r\n", "[Part]='" + p1 + "' or [Part]='" + p3 + "' or [Part]='" + p3 + "'");
        }
        public static DataTable SelvedError_by_Date(DateTime D1,DateTime D2,string Hostname_Station)
        {
            //public static DataTable SelvedError(string p1, string p2, string p3)
            //return ClsData.ExecuteView("[dbo].[solvederrors]", "*", "[Part]='" + p1 + "' or [Part]='" + p2 + "' or [Part]='" + p3 + "'");
            return ClsData.ExecuteView("[dbo].[solvederrors]", "Ref AS Reference, Batch, Part AS [Travel Ticket Label], Worker AS [Worker ID], Type AS [Error Type], Counterpart1 AS [TMP 01], Counterpart2 AS [TMP 02], Object, Wire1, Wire2, Way, DateBegin AS [Start Date], \r\n                         DateEnd AS [End Date], TimeU AS [Time U], CPV1, CPV2, CPV3, CPV4,Hostname\r\n", "Hostname = '" + Hostname_Station + "' and DateBegin between '" + D1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "' and '" + D2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "'");
            //return ClsData.ExecuteView("[dbo].[solvederrors]", "Ref AS Reference, Batch, Part AS [Travel Ticket Label], Worker AS [Worker ID], Type AS [Error Type], Counterpart1 AS [TMP 01], Counterpart2 AS [TMP 02], Object, Wire1, Wire2, Way, DateBegin AS [Start Date], \r\n                         DateEnd AS [End Date], TimeU AS [Time U], CPV1, CPV2, CPV3, CPV4\r\n", "[Part]='" + p1 + "' or [Part]='" + p3 + "' or [Part]='" + p3 + "'");
        }
        public static DataTable GetDates(string Hostname_PC, DateTime Date_End)
        {
            return ClsData.ExecuteView("tests", "TOP (1) *", "DateBegin < '" + Date_End.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "'  and Hostname = '" + Hostname_PC + "'order by DateEnd desc");
        }

        //public static DataTable Harness_SN(string Hostname, string Date1, string Date2)
        public static DataTable Harness_SN(string Hostname, string Date1, string Date2)
        {
            return ClsData.ExecuteView("wtr.dbo.solvederrors se WITH (NOLOCK) LEFT JOIN wtr.dbo.correlations_trial ct WITH (NOLOCK) ON se.Hostname = se.Hostname AND ct.BC = se.Part", "DISTINCT  se.Hostname, ct.Ref AS 'Reference', se.Part as 'TT Label', se.Type as 'Error Code', ct.OUT_Barcode as 'Harness S/N'", " se.Hostname = '"+Hostname+"' AND se.DateBegin BETWEEN '"+Date1+ "' AND '"+Date2+"';");
            //return ClsData.ExecuteView("correlations_trial", "COUNT(*) AS Qty, Ref, CONVERT(DATE, DateEnd) AS Date", "Test LIKE '%EL%' AND[DateEnd] >= '" + Date1 + "'  AND[DateEnd] < '" + Date2 + "' GROUP BY     Ref,     CONVERT(DATE, DateEnd) order by Qty desc");
            //Reference, BC AS [Travel Ticket Label], Test AS Station, \r\n       IN_Barcode_1 AS [2nd Label], IN_Barcode_2 AS [3rd Travel Ticket Label], \r\n       IN_Barcode_3 AS [4th Travel Ticket Label], OUT_Barcode AS [Final Label], \r\n       DateEnd AS [Test Date], Rework AS Reworked, Rework_ID AS [Rework ID] , Hostname
        }
    }
}