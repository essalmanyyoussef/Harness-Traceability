using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harness_Traceability.Models
{
    public class ClsData
    {
        #region Properties

 
        private static string cnxString = "";
        #endregion



        #region Public Methods
        public static bool Connect(string ServerName, string DataBaseName, string user, string password)
    {
        bool cnxStatus = false;

        cnxString = "Data Source=" + ServerName + ";Initial Catalog=" + DataBaseName + ";Integrated Security=false; user id=" + user + "; password= " +  password ;
        SqlConnection sqlConn = new SqlConnection(cnxString);
        sqlConn.Open();
        if (sqlConn.State == ConnectionState.Open)
        {
            cnxStatus = true;
            sqlConn.Close();
        }
        return cnxStatus;
    }

    #region StoredProcedure
    public static Int32 ExecuteProcedure(String procedureName, Dictionary<string, object> parameters, bool paramDirection)
    {
        SqlConnection sqlConn = new SqlConnection(cnxString);
        DataSet ds = new DataSet();
        Int32 result = 9999;
        try
        {
            SqlCommand sqlCmd = new SqlCommand(procedureName.ToString(), sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    sqlCmd.Parameters.AddWithValue(parameter, parameters[parameter]);
                }
            }
            if (paramDirection)
            {
                sqlCmd.Parameters.Add("@outer", SqlDbType.Int);
                sqlCmd.Parameters["@outer"].Direction = ParameterDirection.Output;
            }

            sqlConn.Open();
            sqlCmd.ExecuteNonQuery();
            if (paramDirection)
                result = (Int32)sqlCmd.Parameters["@outer"].Value;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Print(
            string.Format("An exception occured when executing the {0} stored procedure. {1}",
            procedureName, ex.Message));

    
        }
        finally
        {
            if (sqlConn.State == ConnectionState.Open) sqlConn.Close();
            sqlConn.Dispose();
        }
        return result;
    }
    // 
    public static DataTable ExecuteProcedureTbl(String procedureName, Dictionary<string, object> parameters)
    {
        SqlConnection sqlConn = new SqlConnection(cnxString);
        DataSet ds = new DataSet();

        try
        {
            SqlCommand sqlCmd = new SqlCommand(procedureName.ToString(), sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    sqlCmd.Parameters.AddWithValue(parameter, parameters[parameter]);
                }
            }
            sqlConn.Open();
            //reader = sqlCmd.ExecuteReader();
            SqlDataAdapter myAdapter = new SqlDataAdapter(sqlCmd);
            myAdapter.Fill(ds);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Print(
            string.Format("An exception occured when executing the {0} stored procedure. {1}",
            procedureName, ex.Message));
        }
        finally
        {
            if (sqlConn.State == ConnectionState.Open) sqlConn.Close();
            sqlConn.Dispose();
        }
        return ds.Tables[0];
    }

    #endregion

    #region StoredProcedure KeyValuePair
    public static KeyValuePair<Int32, String> ExecuteProcedure(String procedureName, Dictionary<string, object> parameters, String ValueOuterParam)
    {
        SqlConnection sqlConn = new SqlConnection(cnxString);
        DataSet ds = new DataSet();
        //  SqlCommand sqlCmd = new SqlCommand();
        KeyValuePair<Int32, String> keyValue = new KeyValuePair<int, string>();

        try
        {
            SqlCommand sqlCmd = new SqlCommand(procedureName.ToString(), sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(ValueOuterParam, SqlDbType.VarChar, 50);
            sqlCmd.Parameters[ValueOuterParam].Direction = ParameterDirection.Output;
            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    sqlCmd.Parameters.AddWithValue(parameter, parameters[parameter]);
                }
            }
            sqlCmd.Parameters.Add("@outer", SqlDbType.Int);
            sqlCmd.Parameters["@outer"].Direction = ParameterDirection.Output;
            sqlConn.Open();
            sqlCmd.ExecuteNonQuery();

            keyValue = new KeyValuePair<Int32, String>((Int32)(sqlCmd.Parameters["@outer"].Value), sqlCmd.Parameters[ValueOuterParam].Value.ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Print(
            string.Format("An exception occured when executing the {0} stored procedure. {1}",
            procedureName, ex.Message));
        }
        finally
        {
            if (sqlConn.State == ConnectionState.Open) sqlConn.Close();
            sqlConn.Dispose();
        }

        return keyValue;
    }
    #endregion

    #region Views
    public static DataTable ExecuteView(String View, String Fields, String Condition)
    {
        try
        {
            SqlConnection sqlConn = new SqlConnection(cnxString);

            SqlDataAdapter myAdapter = new SqlDataAdapter("Select " + Fields + " from " + View + " Where " + Condition, sqlConn);
            DataSet ds = new DataSet();
                myAdapter.SelectCommand.CommandTimeout = 120; // Set the timeout to 120 seconds

                myAdapter.Fill(ds);
            //ds.Tables[0].TableName = tableName;
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            string message = ex.Message;
            throw ex;
        }
    }
    #endregion
    #endregion
}
}