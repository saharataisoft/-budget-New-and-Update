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
    public partial class iconAtHome
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["BudgetWorldwide.ConnectionString"];
        public Guid iconOID { get; set; }
        public int Seq { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string iconURL { get; set; }

        public iconAtHome()
        {
        }
        public iconAtHome(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<iconAtHome> SelectAll(string PrefixTable)
        {
            List<iconAtHome> iconAtHomeList = null;
            try
            {
                string sSQL = "SELECT * FROM ##iconAtHome";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    iconAtHomeList = new List<iconAtHome>();
                    iconAtHome icon = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        icon = new iconAtHome();
                        icon.iconOID = new Guid(ds.Tables[0].Rows[i]["iconOID"].ToString());
                        icon.Seq = Convert.ToInt32(ds.Tables[0].Rows[i]["Seq"].ToString());
                        icon.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        icon.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                        icon.ShortDes = ds.Tables[0].Rows[i]["ShortDes"].ToString();
                        icon.iconURL = ds.Tables[0].Rows[i]["iconURL"].ToString();
                        iconAtHomeList.Add(icon);
                    }
                }
            }
            catch (Exception ex) { }
            return iconAtHomeList;
        }
        public iconAtHome SelectByOID(string PrefixTable, Guid guidiconOID)
        {
            iconAtHome icon = null;
            try
            {
                string sSQL = "SELECT * FROM ##iconAtHome WHERE iconOID=@iconOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@iconOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidiconOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    icon = new iconAtHome();
                    icon.iconOID = new Guid(ds.Tables[0].Rows[0]["iconOID"].ToString());
                    icon.Seq = Convert.ToInt32(ds.Tables[0].Rows[0]["Seq"].ToString());
                    icon.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    icon.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                    icon.ShortDes = ds.Tables[0].Rows[0]["ShortDes"].ToString();
                    icon.iconURL = ds.Tables[0].Rows[0]["iconURL"].ToString();

                }
            }
            catch (Exception ex) { }
            return icon;
        }
        public bool add(string PrefixTable)
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO ##iconAtHome (iconOID,Seq,FileName,Title,ShortDes,iconURL) 
                            VALUES 
                            (@iconOID,@Seq,@FileName,@Title,@ShortDes,@iconURL)";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@iconOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.iconOID;
                sqlParam[1] = new SqlParameter("@Seq", SqlDbType.Int);
                sqlParam[1].Value = this.Seq;
                sqlParam[2] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[2].Value = this.FileName != null ? this.FileName : "";
                sqlParam[3] = new SqlParameter("@Title", SqlDbType.NVarChar);
                sqlParam[3].Value = this.Title != null ? this.Title : "";
                sqlParam[4] = new SqlParameter("@ShortDes", SqlDbType.NVarChar);
                sqlParam[4].Value = this.ShortDes != null ? this.ShortDes : "";
                sqlParam[5] = new SqlParameter("@iconURL", SqlDbType.NVarChar);
                sqlParam[5].Value = this.iconURL != null ? this.iconURL : "";

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool update(string PrefixTable)
        {
            bool bResult = false;
            try
            {
                string sSQL = "UPDATE ##iconAtHome SET Seq=@Seq,FileName=@FileName,Title=@Title,ShortDes=@ShortDes,iconURL=@iconURL WHERE iconOID=@iconOID";
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter("@iconOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.iconOID;
                sqlParam[1] = new SqlParameter("@Seq", SqlDbType.Int);
                sqlParam[1].Value = this.Seq;
                sqlParam[2] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[2].Value = this.FileName != null ? this.FileName : "";
                sqlParam[3] = new SqlParameter("@Title", SqlDbType.NVarChar);
                sqlParam[3].Value = this.Title != null ? this.Title : "";
                sqlParam[4] = new SqlParameter("@ShortDes", SqlDbType.NVarChar);
                sqlParam[4].Value = this.ShortDes != null ? this.ShortDes : "";
                sqlParam[5] = new SqlParameter("@iconURL", SqlDbType.NVarChar);
                sqlParam[5].Value = this.iconURL != null ? this.iconURL : "";

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
