using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCBL
{
    public partial class BannerConfig
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["BudgetWorldwide.ConnectionString"];
        public Guid BannerOID { get; set; }
        public string CategoriesCode { get; set; }
        public string CategoriesName { get; set; }
        public string BannerFileName { get; set; }
        public string BannerURL { get; set; }
        public string ContentOID { get; set; }
        public string ContentValue { get; set; }
        public BannerConfig()
        {
        }

        public BannerConfig(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<BannerConfig> SelectAll(string PrefixTable)
        {
            List<BannerConfig> BannerConfigList = null;
            try
            {
                string sSQL = @"SELECT [##Categories_Banner].CategoriesCode,[##Categories_Banner].CategoriesName
                        ,[##BannerConfig].BannerOID,[##BannerConfig].BannerFileName,[##BannerConfig].BannerURL
                        ,[##ContentOnPage].* 
                        FROM [##Categories_Banner] 
                        LEFT JOIN [##BannerConfig] ON [##Categories_Banner].CategoriesCode =[##BannerConfig].CategoriesCode
                        LEFT JOIN [##ContentOnPage] ON [##BannerConfig].CategoriesCode = [##ContentOnPage].ContentOnPage AND [##ContentOnPage].Culture = 'en'";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BannerConfigList = new List<BannerConfig>();
                    BannerConfig banner = new BannerConfig();
                    for (int i=0;i< ds.Tables[0].Rows.Count; i++)
                    {
                        banner = new BannerConfig();
                        banner.CategoriesCode = ds.Tables[0].Rows[i]["CategoriesCode"].ToString();
                        banner.CategoriesName = ds.Tables[0].Rows[i]["CategoriesName"].ToString();
                        banner.BannerOID = (ds.Tables[0].Rows[i]["BannerOID"].Equals(DBNull.Value) == false) ? new Guid(ds.Tables[0].Rows[i]["BannerOID"].ToString()) : Guid.Empty;
                        banner.BannerFileName = (ds.Tables[0].Rows[i]["BannerFileName"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["BannerFileName"].ToString():"";
                        banner.BannerURL = (ds.Tables[0].Rows[i]["BannerURL"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["BannerURL"].ToString() : "";
                        banner.ContentOID = (ds.Tables[0].Rows[i]["ContentOID"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["ContentOID"].ToString() : "";
                        banner.ContentValue = (ds.Tables[0].Rows[i]["ContentValue"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["ContentValue"].ToString() : "";
                        BannerConfigList.Add(banner);
                    }
                }
            }
            catch (Exception ex) { }

            return BannerConfigList;
        }

        public BannerConfig SelectByCategoriesCode(string PrefixTable, string CategoriesCode)
        {
            BannerConfig banner = null;
            try
            {
                string sSQL = @"SELECT [##Categories_Banner].CategoriesCode,[##Categories_Banner].CategoriesName
                        ,[##BannerConfig].BannerOID,[##BannerConfig].BannerFileName,[##BannerConfig].BannerURL
                        FROM [##Categories_Banner]
                        LEFT JOIN [##BannerConfig] ON [##Categories_Banner].CategoriesCode =[##BannerConfig].CategoriesCode WHERE [##Categories_Banner].CategoriesCode=@CategoriesCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
                sqlParam[0].Value = CategoriesCode;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    banner = new BannerConfig();
                    banner.CategoriesCode = ds.Tables[0].Rows[0]["CategoriesCode"].ToString();
                    banner.CategoriesName = ds.Tables[0].Rows[0]["CategoriesName"].ToString();
                    banner.BannerOID = (ds.Tables[0].Rows[0]["BannerOID"].Equals(DBNull.Value) == false) ? new Guid(ds.Tables[0].Rows[0]["BannerOID"].ToString()) : Guid.Empty;
                    banner.BannerFileName = (ds.Tables[0].Rows[0]["BannerFileName"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["BannerFileName"].ToString() : "";
                    banner.BannerURL = (ds.Tables[0].Rows[0]["BannerURL"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["BannerURL"].ToString() : "";
                }
            }
            catch (Exception ex) { }
            return banner;
        }

        public bool chkDataAdd(string PrefixTable,string CategoriesCode)
        {
            string sSQL = "SELECT * FROM ##BannerConfig WHERE CategoriesCode=@CategoriesCode";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
            sqlParam[0].Value = CategoriesCode;
            DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool add(string PrefixTable)
        {
            bool bResult = false;
            try
            {
                string sSQL = "INSERT INTO ##BannerConfig (BannerOID,CategoriesCode,BannerFileName,BannerURL) VALUES (@BannerOID,@CategoriesCode,@BannerFileName,@BannerURL)";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@BannerOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.BannerOID;
                sqlParam[1] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
                sqlParam[1].Value = this.CategoriesCode;
                sqlParam[2] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[2].Value = this.BannerFileName!=null? this.BannerFileName:"";
                sqlParam[3] = new SqlParameter("@BannerURL", SqlDbType.NVarChar);
                sqlParam[3].Value = this.BannerURL != null ? this.BannerURL : "";


                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }

        public bool update(string PrefixTable)
        {
            bool bResult = false;
            try
            {
                string sSQL = "UPDATE ##BannerConfig SET BannerFileName=@BannerFileName,BannerURL=@BannerURL WHERE BannerOID=@BannerOID";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[0].Value = this.BannerFileName != null ? this.BannerFileName : "";
                sqlParam[1] = new SqlParameter("@BannerURL", SqlDbType.NVarChar);
                sqlParam[1].Value = this.BannerURL != null ? this.BannerURL : "";
                sqlParam[2] = new SqlParameter("@BannerOID", SqlDbType.UniqueIdentifier);
                sqlParam[2].Value = this.BannerOID;

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
