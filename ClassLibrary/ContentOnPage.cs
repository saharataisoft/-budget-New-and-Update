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
    public partial class ContentOnPage
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["BudgetWorldwide.ConnectionString"];
        public Guid ContentOID { get; set; }
        public string Culture { get; set; }
        public string CategoriesCode { get; set; }
        public string ContentValue { get; set; }

        public ContentOnPage()
        {
        }
        public ContentOnPage(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<ContentOnPage> SelectAll(string PrefixTable)
        {
            List<ContentOnPage> ContentOnPageList = null;
            try
            {
                string sSQL = "SELECT * FROM ##ContentOnPage";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ContentOnPageList = new List<ContentOnPage>();
                    ContentOnPage content = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        content = new ContentOnPage();
                        content.ContentOID = new Guid(ds.Tables[0].Rows[i]["ContentOID"].ToString());
                        content.Culture = ds.Tables[0].Rows[i]["Culture"].ToString();
                        content.CategoriesCode = ds.Tables[0].Rows[i]["ContentOnPage"].ToString();
                        content.ContentValue = ds.Tables[0].Rows[i]["ContentValue"].ToString();
                        ContentOnPageList.Add(content);
                    }
                }
            }
            catch (Exception ex) { }
            return ContentOnPageList;
        }
        public List<ContentOnPage> SelectByOID(string PrefixTable, Guid guidContentOID)
        {
            List<ContentOnPage> ContentOnPageList = null;
            try
            {
                string sSQL = "SELECT * FROM ##ContentOnPage WHERE ContentOID=@ContentOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ContentOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidContentOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ContentOnPageList = new List<ContentOnPage>();
                    ContentOnPage content = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        content = new ContentOnPage();
                        content.ContentOID = new Guid(ds.Tables[0].Rows[i]["ContentOID"].ToString());
                        content.Culture = ds.Tables[0].Rows[i]["Culture"].ToString();
                        content.CategoriesCode = ds.Tables[0].Rows[i]["ContentOnPage"].ToString();
                        content.ContentValue = ds.Tables[0].Rows[i]["ContentValue"].ToString();
                        ContentOnPageList.Add(content);
                    }
                }
            }
            catch (Exception ex) { }
            return ContentOnPageList;
        }
        public ContentOnPage getContentOnPage(string PrefixTable,string sOnPage,string sCulture)
        {
            ContentOnPage content = null;
            try
            {
                string sSQL = "SELECT * FROM ##ContentOnPage WHERE ContentOnPage=@ContentOnPage AND Culture=@Culture";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@ContentOnPage", SqlDbType.NVarChar);
                sqlParam[0].Value = sOnPage;
                sqlParam[1] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                sqlParam[1].Value = sCulture;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    content = new ContentOnPage();
                    content.ContentOID = new Guid(ds.Tables[0].Rows[0]["ContentOID"].ToString());
                    content.Culture = ds.Tables[0].Rows[0]["Culture"].ToString();
                    content.CategoriesCode = ds.Tables[0].Rows[0]["ContentOnPage"].ToString();
                    content.ContentValue = ds.Tables[0].Rows[0]["ContentValue"].ToString();
                }
            }
            catch (Exception ex) { }
            return content;
        }

        public bool update(string PrefixTable, List<ContentOnPage> contentList)
        {
            bool bResult = false;
            try
            {
                string sSQL = "UPDATE ##ContentOnPage SET ContentValue=@ContentValue WHERE ContentOID=@ContentOID AND Culture=@Culture";
                SqlParameter[] sqlParam;
                for (int i = 0; i < contentList.Count; i++)
                {
                    sqlParam = new SqlParameter[3];
                    sqlParam[0] = new SqlParameter("@ContentValue", SqlDbType.NVarChar);
                    sqlParam[0].Value = contentList[i].ContentValue;
                    sqlParam[1] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                    sqlParam[1].Value = contentList[i].Culture;
                    sqlParam[2] = new SqlParameter("@ContentOID", SqlDbType.UniqueIdentifier);
                    sqlParam[2].Value = contentList[i].ContentOID;

                    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
