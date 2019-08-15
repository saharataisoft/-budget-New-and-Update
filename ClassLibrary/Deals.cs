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
    public partial class Deals
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["BudgetWorldwide.ConnectionString"];
        public Guid DealOID { get; set; }
        public bool Enable { get; set; }
        public int Seq { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string TextInButton { get; set; }
        public string ButtonURL { get; set; }
        public string Descriptions { get; set; }
        public string BannerFileName { get; set; }

        public Deals()
        {
        }
        public Deals(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<Deals> SelectAll(string PrefixTable)
        {
            List<Deals> DealsList = null;
            try
            {
                string sSQL = "SELECT * FROM ##Deals ORDER BY Seq";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DealsList = new List<Deals>();
                    Deals deal = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        deal = new Deals();
                        deal.DealOID = new Guid(ds.Tables[0].Rows[i]["DealOID"].ToString());
                        deal.Enable = Convert.ToBoolean(ds.Tables[0].Rows[i]["Enable"].ToString());
                        deal.Seq = Convert.ToInt32(ds.Tables[0].Rows[i]["Seq"].ToString());
                        deal.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        deal.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                        deal.ShortDes = ds.Tables[0].Rows[i]["ShortDes"].ToString();
                        deal.TextInButton = ds.Tables[0].Rows[i]["TextInButton"].ToString();
                        deal.ButtonURL = ds.Tables[0].Rows[i]["ButtonURL"].ToString();
                        deal.Descriptions = ds.Tables[0].Rows[i]["Descriptions"].ToString();
                        deal.BannerFileName = ds.Tables[0].Rows[i]["BannerFileName"].ToString();
                        DealsList.Add(deal);
                    }
                }
            }
            catch (Exception ex) { }
            return DealsList;
        }
        public Deals SelectByOID(string PrefixTable, Guid guidDealOID)
        {
            Deals deal = null;
            try
            {
                string sSQL = "SELECT * FROM ##Deals WHERE DealOID=@DealOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@DealOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidDealOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    deal = new Deals();
                    deal.DealOID = new Guid(ds.Tables[0].Rows[0]["DealOID"].ToString());
                    deal.Enable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Enable"].ToString());
                    deal.Seq = Convert.ToInt32(ds.Tables[0].Rows[0]["Seq"].ToString());
                    deal.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    deal.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                    deal.ShortDes = ds.Tables[0].Rows[0]["ShortDes"].ToString();
                    deal.TextInButton = ds.Tables[0].Rows[0]["TextInButton"].ToString();
                    deal.ButtonURL = ds.Tables[0].Rows[0]["ButtonURL"].ToString();
                    deal.Descriptions = ds.Tables[0].Rows[0]["Descriptions"].ToString();
                    deal.BannerFileName = ds.Tables[0].Rows[0]["BannerFileName"].ToString();

                }
            }
            catch (Exception ex) { }
            return deal;
        }
        public bool add(string PrefixTable)
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO ##Deals (DealOID,Enable,Seq,FileName,Title,ShortDes,TextInButton,ButtonURL,Descriptions,BannerFileName) 
                            VALUES 
                            (@DealOID,@Enable,@Seq,@FileName,@Title,@ShortDes,@TextInButton,@ButtonURL,@Descriptions,@BannerFileName)";
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@DealOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.DealOID;
                sqlParam[1] = new SqlParameter("@Enable", SqlDbType.Bit);
                sqlParam[1].Value = this.Enable;
                sqlParam[2] = new SqlParameter("@Seq", SqlDbType.Int);
                sqlParam[2].Value = this.Seq;
                sqlParam[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[3].Value = this.FileName != null ? this.FileName : "";
                sqlParam[4] = new SqlParameter("@Title", SqlDbType.NVarChar);
                sqlParam[4].Value = this.Title;
                sqlParam[5] = new SqlParameter("@ShortDes", SqlDbType.NVarChar);
                sqlParam[5].Value = this.ShortDes;
                sqlParam[6] = new SqlParameter("@TextInButton", SqlDbType.NVarChar);
                sqlParam[6].Value = this.TextInButton;
                sqlParam[7] = new SqlParameter("@ButtonURL", SqlDbType.NVarChar);
                sqlParam[7].Value = this.ButtonURL != null ? this.ButtonURL : "";
                sqlParam[8] = new SqlParameter("@Descriptions", SqlDbType.NVarChar);
                sqlParam[8].Value = this.Descriptions != null ? this.Descriptions : "";
                sqlParam[9] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[9].Value = this.BannerFileName != null ? this.BannerFileName : "";

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
                string sSQL = "UPDATE ##Deals SET Enable=@Enable,Seq=@Seq,FileName=@FileName,Title=@Title,ShortDes=@ShortDes,TextInButton=@TextInButton,ButtonURL=@ButtonURL,Descriptions=@Descriptions,BannerFileName=@BannerFileName WHERE DealOID=@DealOID";
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@DealOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.DealOID;
                sqlParam[1] = new SqlParameter("@Enable", SqlDbType.Bit);
                sqlParam[1].Value = this.Enable;
                sqlParam[2] = new SqlParameter("@Seq", SqlDbType.Int);
                sqlParam[2].Value = this.Seq;
                sqlParam[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[3].Value = this.FileName != null ? this.FileName : "";
                sqlParam[4] = new SqlParameter("@Title", SqlDbType.NVarChar);
                sqlParam[4].Value = this.Title;
                sqlParam[5] = new SqlParameter("@ShortDes", SqlDbType.NVarChar);
                sqlParam[5].Value = this.ShortDes;
                sqlParam[6] = new SqlParameter("@TextInButton", SqlDbType.NVarChar);
                sqlParam[6].Value = this.TextInButton;
                sqlParam[7] = new SqlParameter("@ButtonURL", SqlDbType.NVarChar);
                sqlParam[7].Value = this.ButtonURL != null ? this.ButtonURL : "";
                sqlParam[8] = new SqlParameter("@Descriptions", SqlDbType.NVarChar);
                sqlParam[8].Value = this.Descriptions != null ? this.Descriptions : "";
                sqlParam[9] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[9].Value = this.BannerFileName != null ? this.BannerFileName : "";

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL.Replace("##", PrefixTable), sqlParam);
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
