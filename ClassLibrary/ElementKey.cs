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
    public partial class ElementKey
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid ElementOID { get; set; }
        public string Key { get; set; }
        public string Culture { get; set; }
        public string Value { get; set; }
        public ElementKey()
        {
        }
        public ElementKey(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public string [] getAllLangCode()
        {
            string[] arr=null;
            try
            {
                string sSQL = "SELECT * FROM CL_ApplicationCulture";
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    arr = new string[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        arr[i] = ds.Tables[0].Rows[i]["Culture"].ToString();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return arr;                 
        }
        public List<ElementKey> SelectAll()
        {
            List<ElementKey> allKey = null;
            try
            {
                string sSQL = "SELECT Culture.*,Element.ElementKey FROM CL_ApplicationElementCulture Culture Left Join CL_ApplicationElement Element on Element.ElementOID = Culture.ElementOID";
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oneRow;
                    allKey = new List<ElementKey>();
                    ElementKey elk = new ElementKey();
                    for (int i =0;i< ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        elk = new ElementKey();
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
        public List<ElementKey> SelectByOID(Guid guidElementOID)
        {
            List<ElementKey> allKey = null;
            try
            {
                string sSQL = "SELECT Culture.*,Element.ElementKey FROM CL_ApplicationElementCulture Culture Left Join CL_ApplicationElement Element on Element.ElementOID = Culture.ElementOID WHERE Culture.ElementOID=@ElementOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ElementOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidElementOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oneRow;
                    allKey = new List<ElementKey>();
                    ElementKey elk = new ElementKey();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        elk = new ElementKey();
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
        public List<ElementKey> SelectByCulture(string Culture)
        {
            List<ElementKey> allKey = null;
            try
            {
                string sSQL = "SELECT Culture.*,Element.ElementKey FROM CL_ApplicationElementCulture Culture Left Join CL_ApplicationElement Element on Element.ElementOID = Culture.ElementOID WHERE Culture.Culture=@Culture";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                sqlParam[0].Value = Culture;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oneRow;
                    allKey = new List<ElementKey>();
                    ElementKey elk = new ElementKey();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        elk = new ElementKey();
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
        public bool add(List<ElementKey> listInsert)
        {
            bool bResult = false;
            if (listInsert == null || (listInsert != null && listInsert.Count == 0))
                return bResult;
            try
            {
                string sSQL = "SELECT Count(*) FROM CL_ApplicationElement WHERE ElementKey=@ElementKey";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ElementKey", SqlDbType.NVarChar);
                sqlParam[0].Value = listInsert[0].Key;
                int iRec = (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (iRec == 0)
                {
                    Guid guidElementOID =Guid.NewGuid();
                    sSQL = @"INSERT INTO CL_ApplicationElement (ElementOID,ElementKey)
                            VALUES
                            (@ElementOID,@ElementKey)";
                    sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@ElementOID", guidElementOID);
                    sqlParam[1] = new SqlParameter("@ElementKey", listInsert[0].Key);
                    iRec = SqlHelper.ExecuteNonQuery(
                           ConnectionString,
                           CommandType.Text, sSQL, sqlParam);
                    if (iRec > 0)
                    {
                        for (int i = 0; i < listInsert.Count; i++)
                        {
                            sSQL = @"INSERT INTO CL_ApplicationElementCulture (ElementOID,Culture,PropertyValue)
                            VALUES
                            (@ElementOID,@Culture,@PropertyValue)";
                            sqlParam = new SqlParameter[3];
                            sqlParam[0] = new SqlParameter("@ElementOID", guidElementOID);
                            sqlParam[1] = new SqlParameter("@Culture", listInsert[i].Culture);
                            sqlParam[2] = new SqlParameter("@PropertyValue", listInsert[i].Value);
                            SqlHelper.ExecuteNonQuery(
                                   ConnectionString,
                                   CommandType.Text, sSQL, sqlParam);
                        }
                    }
                    bResult = true;
                }
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool update(List<ElementKey> listUpdate)
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
                    sSQL = @"UPDATE CL_ApplicationElementCulture SET PropertyValue=@PropertyValue                            
                            WHERE ElementOID=@ElementOID AND Culture=@Culture";
                    sqlParam = new SqlParameter[3];
                    sqlParam[0] = new SqlParameter("@ElementOID", listUpdate[i].ElementOID);
                    sqlParam[1] = new SqlParameter("@Culture", listUpdate[i].Culture);
                    sqlParam[2] = new SqlParameter("@PropertyValue", listUpdate[i].Value);
                    SqlHelper.ExecuteNonQuery(
                           ConnectionString,
                           CommandType.Text, sSQL, sqlParam);
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool delete(Guid _OID)
        {
            bool bResult = false;
           
            try
            {
                string sSql = @"DELETE FROM CL_ApplicationElement Where ElementOID=@ElementOID;
                                DELETE FROM CL_ApplicationElementCulture Where ElementOID=@ElementOID;";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ElementOID", _OID);

                SqlHelper.ExecuteNonQuery(
                       ConnectionString,
                       CommandType.Text, sSql, param);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
