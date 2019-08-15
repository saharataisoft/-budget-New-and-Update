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
    public partial class Budget_BannerConfig
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid BannerOID { get; set; }
        public string CategoriesCode { get; set; }
        public string CategoriesName { get; set; }
        public string BannerFileName { get; set; }
        public string BannerURL { get; set; }
        public string BannerTitle { get; set; }
        public string ContentOID { get; set; }
        public string ContentValue { get; set; }
        public Budget_BannerConfig()
        {
        }

        public Budget_BannerConfig(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<Budget_BannerConfig> SelectAll()
        {
            List<Budget_BannerConfig> BannerConfigList = null;
            try
            {
                string sSQL = @"SELECT [Categories_Banner].CategoriesCode,[Categories_Banner].CategoriesName
                        ,[BannerConfig].BannerOID,[BannerConfig].BannerFileName,[BannerConfig].BannerURL,[BannerConfig].BannerTitle
                        ,[ContentOnPage].* 
                        FROM [Categories_Banner] 
                        LEFT JOIN [BannerConfig] ON [Categories_Banner].CategoriesCode =[BannerConfig].CategoriesCode
                        LEFT JOIN [ContentOnPage] ON [BannerConfig].CategoriesCode = [ContentOnPage].ContentOnPage AND [ContentOnPage].Culture = 'en'";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BannerConfigList = new List<Budget_BannerConfig>();
                    Budget_BannerConfig banner = new Budget_BannerConfig();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        banner = new Budget_BannerConfig();
                        banner.CategoriesCode = ds.Tables[0].Rows[i]["CategoriesCode"].ToString();
                        banner.CategoriesName = ds.Tables[0].Rows[i]["CategoriesName"].ToString();
                        banner.BannerOID = (ds.Tables[0].Rows[i]["BannerOID"].Equals(DBNull.Value) == false) ? new Guid(ds.Tables[0].Rows[i]["BannerOID"].ToString()) : Guid.Empty;
                        banner.BannerFileName = (ds.Tables[0].Rows[i]["BannerFileName"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["BannerFileName"].ToString() : "";
                        banner.BannerURL = (ds.Tables[0].Rows[i]["BannerURL"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["BannerURL"].ToString() : "";
                        banner.BannerTitle = (ds.Tables[0].Rows[i]["BannerTitle"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["BannerTitle"].ToString() : "";
                        banner.ContentOID = (ds.Tables[0].Rows[i]["ContentOID"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["ContentOID"].ToString() : "";
                        banner.ContentValue = (ds.Tables[0].Rows[i]["ContentValue"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["ContentValue"].ToString() : "";
                        BannerConfigList.Add(banner);
                    }
                }
            }
            catch (Exception ex) { }

            return BannerConfigList;
        }

        public Budget_BannerConfig SelectByCategoriesCode(string CategoriesCode)
        {
            Budget_BannerConfig banner = null;
            try
            {
                string sSQL = @"SELECT [Categories_Banner].CategoriesCode,[Categories_Banner].CategoriesName
                        ,[BannerConfig].BannerOID,[BannerConfig].BannerFileName,[BannerConfig].BannerURL,[BannerConfig].BannerTitle
                        FROM [Categories_Banner]
                        LEFT JOIN [BannerConfig] ON [Categories_Banner].CategoriesCode =[BannerConfig].CategoriesCode WHERE [Categories_Banner].CategoriesCode=@CategoriesCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
                sqlParam[0].Value = CategoriesCode;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    banner = new Budget_BannerConfig();
                    banner.CategoriesCode = ds.Tables[0].Rows[0]["CategoriesCode"].ToString();
                    banner.CategoriesName = ds.Tables[0].Rows[0]["CategoriesName"].ToString();
                    banner.BannerOID = (ds.Tables[0].Rows[0]["BannerOID"].Equals(DBNull.Value) == false) ? new Guid(ds.Tables[0].Rows[0]["BannerOID"].ToString()) : Guid.Empty;
                    banner.BannerFileName = (ds.Tables[0].Rows[0]["BannerFileName"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["BannerFileName"].ToString() : "";
                    banner.BannerURL = (ds.Tables[0].Rows[0]["BannerURL"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["BannerURL"].ToString() : "";
                    banner.BannerTitle = (ds.Tables[0].Rows[0]["BannerTitle"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["BannerTitle"].ToString() : "";
                }
            }
            catch (Exception ex) { }
            return banner;
        }

        public bool chkDataAdd(string CategoriesCode)
        {
            string sSQL = "SELECT * FROM BannerConfig WHERE CategoriesCode=@CategoriesCode";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
            sqlParam[0].Value = CategoriesCode;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = "INSERT INTO BannerConfig (BannerOID,CategoriesCode,BannerFileName,BannerURL,BannerTitle) VALUES (@BannerOID,@CategoriesCode,@BannerFileName,@BannerURL,@BannerTitle)";
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@BannerOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.BannerOID;
                sqlParam[1] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
                sqlParam[1].Value = this.CategoriesCode;
                sqlParam[2] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[2].Value = this.BannerFileName != null ? this.BannerFileName : "";
                sqlParam[3] = new SqlParameter("@BannerURL", SqlDbType.NVarChar);
                sqlParam[3].Value = this.BannerURL != null ? this.BannerURL : "";
                sqlParam[4] = new SqlParameter("@BannerTitle", SqlDbType.NVarChar);
                sqlParam[4].Value = this.BannerTitle != null ? this.BannerTitle : "";


                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }

        public bool update()
        {
            bool bResult = false;
            try
            {
                string sSQL = "UPDATE BannerConfig SET BannerFileName=@BannerFileName,BannerURL=@BannerURL,BannerTitle=@BannerTitle WHERE BannerOID=@BannerOID";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[0].Value = this.BannerFileName != null ? this.BannerFileName : "";
                sqlParam[1] = new SqlParameter("@BannerURL", SqlDbType.NVarChar);
                sqlParam[1].Value = this.BannerURL != null ? this.BannerURL : "";
                sqlParam[2] = new SqlParameter("@BannerOID", SqlDbType.UniqueIdentifier);
                sqlParam[2].Value = this.BannerOID;
                sqlParam[3] = new SqlParameter("@BannerTitle", SqlDbType.NVarChar);
                sqlParam[3].Value = this.BannerTitle != null ? this.BannerTitle : "";

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
