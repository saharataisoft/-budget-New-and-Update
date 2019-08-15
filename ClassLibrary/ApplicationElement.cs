using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCBL
{
    public partial class ApplicationElement
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["BudgetWorldwide.ConnectionString"];
        public Guid ElementOID { get; set; }
        public string Key { get; set; }
        public string Culture { get; set; }
        public string Value { get; set; }
        public ApplicationElement()
        {
        }
        public ApplicationElement(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public string[] getAllLangCode(string PrefixTable)
        {
            string[] arr = null;
            try
            {
                string sSQL = "SELECT * FROM ##CL_ApplicationCulture";
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    arr = new string[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        arr[i] = ds.Tables[0].Rows[i]["Culture"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return arr;
        }
        public List<ApplicationElement> SelectAll(string PrefixTable)
        {
            List<ApplicationElement> allKey = null;
            try
            {
                string sSQL = "SELECT Culture.*,Element.ElementKey FROM ##CL_ApplicationElementCulture Culture Left Join ##CL_ApplicationElement Element on Element.ElementOID = Culture.ElementOID";
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oneRow;
                    allKey = new List<ApplicationElement>();
                    ApplicationElement elk = new ApplicationElement();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        elk = new ApplicationElement();
                        elk.ElementOID = new Guid(oneRow["ElementOID"].ToString());
                        elk.Key = oneRow["ElementKey"].ToString();
                        elk.Culture = oneRow["Culture"].ToString();
                        elk.Value = oneRow["PropertyValue"].ToString();
                        allKey.Add(elk);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return allKey;
        }
        public List<ApplicationElement> SelectByOID(string PrefixTable, Guid guidElementOID)
        {
            List<ApplicationElement> allKey = null;
            try
            {
                string sSQL = "SELECT Culture.*,Element.ElementKey FROM ##CL_ApplicationElementCulture Culture Left Join ##CL_ApplicationElement Element on Element.ElementOID = Culture.ElementOID WHERE Culture.ElementOID=@ElementOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ElementOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidElementOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oneRow;
                    allKey = new List<ApplicationElement>();
                    ApplicationElement elk = new ApplicationElement();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        elk = new ApplicationElement();
                        elk.ElementOID = new Guid(oneRow["ElementOID"].ToString());
                        elk.Key = oneRow["ElementKey"].ToString();
                        elk.Culture = oneRow["Culture"].ToString();
                        elk.Value = oneRow["PropertyValue"].ToString();
                        allKey.Add(elk);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return allKey;
        }
        public List<ApplicationElement> SelectByCulture(string PrefixTable, string Culture)
        {
            List<ApplicationElement> allKey = null;
            try
            {
                string sSQL = "SELECT Culture.*,Element.ElementKey FROM ##CL_ApplicationElementCulture Culture Left Join ##CL_ApplicationElement Element on Element.ElementOID = Culture.ElementOID WHERE Culture.Culture=@Culture";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                sqlParam[0].Value = Culture;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oneRow;
                    allKey = new List<ApplicationElement>();
                    ApplicationElement elk = new ApplicationElement();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        elk = new ApplicationElement();
                        elk.ElementOID = new Guid(oneRow["ElementOID"].ToString());
                        elk.Key = oneRow["ElementKey"].ToString();
                        elk.Culture = oneRow["Culture"].ToString();
                        elk.Value = oneRow["PropertyValue"].ToString();
                        allKey.Add(elk);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return allKey;
        }
        public bool add(string PrefixTable, List<ApplicationElement> listInsert)
        {
            bool bResult = false;
            if (listInsert == null || (listInsert != null && listInsert.Count == 0))
                return bResult;
            try
            {
                string sSQL = "SELECT Count(*) FROM ##CL_ApplicationElement WHERE ElementKey=@ElementKey";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ElementKey", SqlDbType.NVarChar);
                sqlParam[0].Value = listInsert[0].Key;
                int iRec = (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                if (iRec == 0)
                {
                    Guid guidElementOID = Guid.NewGuid();
                    sSQL = @"INSERT INTO ##CL_ApplicationElement (ElementOID,ElementKey)
                            VALUES
                            (@ElementOID,@ElementKey)";
                    sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@ElementOID", guidElementOID);
                    sqlParam[1] = new SqlParameter("@ElementKey", listInsert[0].Key);
                    iRec = SqlHelper.ExecuteNonQuery(
                           ConnectionString,
                           CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                    if (iRec > 0)
                    {
                        for (int i = 0; i < listInsert.Count; i++)
                        {
                            sSQL = @"INSERT INTO ##CL_ApplicationElementCulture (ElementOID,Culture,PropertyValue)
                            VALUES
                            (@ElementOID,@Culture,@PropertyValue)";
                            sqlParam = new SqlParameter[3];
                            sqlParam[0] = new SqlParameter("@ElementOID", guidElementOID);
                            sqlParam[1] = new SqlParameter("@Culture", listInsert[i].Culture);
                            sqlParam[2] = new SqlParameter("@PropertyValue", listInsert[i].Value);
                            SqlHelper.ExecuteNonQuery(
                                   ConnectionString,
                                   CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                        }
                    }
                    bResult = true;
                }
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool update(string PrefixTable, List<ApplicationElement> listUpdate)
        {
            bool bResult = false;
            if (listUpdate == null || (listUpdate != null && listUpdate.Count == 0))
                return bResult;
            try
            {
                string sSQL = "";
                SqlParameter[] sqlParam;
                for (int i = 0; i < listUpdate.Count; i++)
                {
                    sSQL = @"UPDATE ##CL_ApplicationElementCulture SET PropertyValue=@PropertyValue                            
                            WHERE ElementOID=@ElementOID AND Culture=@Culture";
                    sqlParam = new SqlParameter[3];
                    sqlParam[0] = new SqlParameter("@ElementOID", listUpdate[i].ElementOID);
                    sqlParam[1] = new SqlParameter("@Culture", listUpdate[i].Culture);
                    sqlParam[2] = new SqlParameter("@PropertyValue", listUpdate[i].Value);
                    SqlHelper.ExecuteNonQuery(
                           ConnectionString,
                           CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool delete(string PrefixTable, Guid _OID)
        {
            bool bResult = false;

            try
            {
                string sSQL = @"DELETE FROM ##CL_ApplicationElement Where ElementOID=@ElementOID;
                                DELETE FROM ##CL_ApplicationElementCulture Where ElementOID=@ElementOID;";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ElementOID", _OID);

                SqlHelper.ExecuteNonQuery(
                       ConnectionString,
                       CommandType.Text, sSQL.Replace("##", PrefixTable), param);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
