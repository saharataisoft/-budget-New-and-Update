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
    public partial class Budget_NewsAndUpdate
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid NewsOID { get; set; }
        public bool Enable { get; set; }
        public int Seq { get; set; }
        public string FileName { get; set; }
        public string BannerFileName { get; set; }
        //public string Title { get; set; }
        //public string ShortDes { get; set; }
        //public string Descriptions { get; set; }
        public string imageFileName1 { get; set; }
        public string imageFileName2 { get; set; }
        public string imageFileName3 { get; set; }
        public string imageFileName4 { get; set; }
        public string imageFileName5 { get; set; }
        public string imageFileName6 { get; set; }
        public string imageFileName7 { get; set; }
        public string imageFileName8 { get; set; }
        public string imageFileName9 { get; set; }
        public string imageFileName10 { get; set; }
        public string imageFileName11 { get; set; }
        public string imageFileName12 { get; set; }
        public string imageFileName13 { get; set; }
        public string imageFileName14 { get; set; }
        public string imageFileName15 { get; set; }
        public DateTime StartDate { get; set; }
        public List<Details> NewsAndUpdateDetails { get; set; }

        public Budget_NewsAndUpdate()
        {
        }
        public Budget_NewsAndUpdate(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<Budget_NewsAndUpdate> SelectAll()
        {
            List<Budget_NewsAndUpdate> NewsAndUpdateList = null;
            try
            {
                string sSQL = "SELECT * FROM NewsAndUpdate Order By Seq";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    NewsAndUpdateList = new List<Budget_NewsAndUpdate>();
                    Budget_NewsAndUpdate news = null;
                    List<Details> detailList = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        news = new Budget_NewsAndUpdate();
                        news.NewsOID = new Guid(ds.Tables[0].Rows[i]["NewsOID"].ToString());
                        news.Enable = Convert.ToBoolean(ds.Tables[0].Rows[i]["Enable"].ToString());
                        news.Seq = Convert.ToInt32(ds.Tables[0].Rows[i]["Seq"].ToString());
                        news.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        news.BannerFileName = ds.Tables[0].Rows[i]["BannerFileName"].ToString();
                        //news.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                        //news.ShortDes = ds.Tables[0].Rows[i]["ShortDes"].ToString();
                        //news.Descriptions = ds.Tables[0].Rows[i]["Descriptions"].ToString();
                        news.imageFileName1 = ds.Tables[0].Rows[i]["imageFileName1"].ToString();
                        news.imageFileName2 = ds.Tables[0].Rows[i]["imageFileName2"].ToString();
                        news.imageFileName3 = ds.Tables[0].Rows[i]["imageFileName3"].ToString();
                        news.imageFileName4 = ds.Tables[0].Rows[i]["imageFileName4"].ToString();
                        news.imageFileName5 = ds.Tables[0].Rows[i]["imageFileName5"].ToString();
                        news.imageFileName6 = ds.Tables[0].Rows[i]["imageFileName6"].ToString();
                        news.imageFileName7 = ds.Tables[0].Rows[i]["imageFileName7"].ToString();
                        news.imageFileName8 = ds.Tables[0].Rows[i]["imageFileName8"].ToString();
                        news.imageFileName9 = ds.Tables[0].Rows[i]["imageFileName9"].ToString();
                        news.imageFileName10 = ds.Tables[0].Rows[i]["imageFileName10"].ToString();
                        news.imageFileName11 = ds.Tables[0].Rows[i]["imageFileName11"].ToString();
                        news.imageFileName12 = ds.Tables[0].Rows[i]["imageFileName12"].ToString();
                        news.imageFileName13 = ds.Tables[0].Rows[i]["imageFileName13"].ToString();
                        news.imageFileName14 = ds.Tables[0].Rows[i]["imageFileName14"].ToString();
                        news.imageFileName15 = ds.Tables[0].Rows[i]["imageFileName15"].ToString();
                        if (ds.Tables[0].Rows[i]["StartDate"].Equals(DBNull.Value) == false)
                            news.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["StartDate"]);
                        detailList = getDetails(news.NewsOID);
                        if (detailList != null)
                        {
                            news.NewsAndUpdateDetails = new List<Details>();
                            news.NewsAndUpdateDetails = detailList;
                        }


                        NewsAndUpdateList.Add(news);
                    }
                }
            }
            catch (Exception ex) { }
            return NewsAndUpdateList;
        }


        public List<Budget_NewsAndUpdate> SelectByCulture(string Culture)
        {
            List<Budget_NewsAndUpdate> NewsAndUpdateList = null;
            try
            {
                string sSQL = "SELECT * FROM NewsAndUpdate";
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    NewsAndUpdateList = new List<Budget_NewsAndUpdate>();
                    List<Details> detailList = null;
                    Budget_NewsAndUpdate news = new Budget_NewsAndUpdate();
                    Details detail = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        news = new Budget_NewsAndUpdate();
                        news.NewsOID = new Guid(ds.Tables[0].Rows[i]["NewsOID"].ToString());
                        news.Enable = Convert.ToBoolean(ds.Tables[0].Rows[i]["Enable"].ToString());
                        news.Seq = Convert.ToInt32(ds.Tables[0].Rows[i]["Seq"].ToString());
                        news.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        news.BannerFileName = ds.Tables[0].Rows[i]["BannerFileName"].ToString();
                        //news.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                        //news.ShortDes = ds.Tables[0].Rows[i]["ShortDes"].ToString();
                        //news.Descriptions = ds.Tables[0].Rows[i]["Descriptions"].ToString();
                        news.imageFileName1 = ds.Tables[0].Rows[i]["imageFileName1"].ToString();
                        news.imageFileName2 = ds.Tables[0].Rows[i]["imageFileName2"].ToString();
                        news.imageFileName3 = ds.Tables[0].Rows[i]["imageFileName3"].ToString();
                        news.imageFileName4 = ds.Tables[0].Rows[i]["imageFileName4"].ToString();
                        news.imageFileName5 = ds.Tables[0].Rows[i]["imageFileName5"].ToString();
                        news.imageFileName6 = ds.Tables[0].Rows[i]["imageFileName6"].ToString();
                        news.imageFileName7 = ds.Tables[0].Rows[i]["imageFileName7"].ToString();
                        news.imageFileName8 = ds.Tables[0].Rows[i]["imageFileName8"].ToString();
                        news.imageFileName9 = ds.Tables[0].Rows[i]["imageFileName9"].ToString();
                        news.imageFileName10 = ds.Tables[0].Rows[i]["imageFileName10"].ToString();
                        news.imageFileName11 = ds.Tables[0].Rows[i]["imageFileName11"].ToString();
                        news.imageFileName12 = ds.Tables[0].Rows[i]["imageFileName12"].ToString();
                        news.imageFileName13 = ds.Tables[0].Rows[i]["imageFileName13"].ToString();
                        news.imageFileName14 = ds.Tables[0].Rows[i]["imageFileName14"].ToString();
                        news.imageFileName15 = ds.Tables[0].Rows[i]["imageFileName15"].ToString();
                        if (ds.Tables[0].Rows[i]["StartDate"].Equals(DBNull.Value) == false)
                            news.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["StartDate"]);
                        //detail = getDetails(news.NewsOID).Find(x => x.Culture == Culture);
                        /*ตรงนี*/
                        detailList = getDetails(news.NewsOID);
                        detail = detailList.Find(x => x.Culture == Culture);
                        if (detail == null)
                            detail = detailList.Find(x => x.Culture == "en"); /*ถึงตรงนี้*/
                        if (detail != null)
                        {
                            news.NewsAndUpdateDetails = new List<Details>();
                            detailList = new List<Details>();
                            detailList.Add(detail);
                            news.NewsAndUpdateDetails = detailList;
                        }
                        NewsAndUpdateList.Add(news);
                    }
                }
            }
            catch (Exception ex) { }
            return NewsAndUpdateList;
        }

        private List<Details> getDetails(Guid guidNewsOID)
        {
            List<Details> DetailsList = null;
            try
            {
                string sSQL = "SELECT * FROM NewsAndUpdateDetails WHERE NewsOID=@NewsOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@NewsOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidNewsOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DetailsList = new List<Details>();
                    Details details = new Details();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        details = new Details();
                        details.Culture = ds.Tables[0].Rows[i]["Culture"].ToString();
                        details.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                        details.ShortDes = ds.Tables[0].Rows[i]["ShortDes"].ToString();
                        details.Descriptions = ds.Tables[0].Rows[i]["Descriptions"].ToString();
                        DetailsList.Add(details);
                    }
                }
            }
            catch (Exception ex) { }
            return DetailsList;
        }

        public Budget_NewsAndUpdate SelectByOID(Guid guidNewsOID)
        {
            Budget_NewsAndUpdate news = null;
            try
            {
                string sSQL = "SELECT * FROM NewsAndUpdate WHERE NewsOID=@NewsOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@NewsOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidNewsOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Details> detailList = null;
                    news = new Budget_NewsAndUpdate();
                    news.NewsOID = new Guid(ds.Tables[0].Rows[0]["NewsOID"].ToString());
                    news.Enable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Enable"].ToString());
                    news.Seq = Convert.ToInt32(ds.Tables[0].Rows[0]["Seq"].ToString());
                    news.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    news.BannerFileName = ds.Tables[0].Rows[0]["BannerFileName"].ToString();
                    //news.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                    //news.ShortDes = ds.Tables[0].Rows[0]["ShortDes"].ToString();
                    //news.Descriptions = ds.Tables[0].Rows[0]["Descriptions"].ToString();
                    news.imageFileName1 = ds.Tables[0].Rows[0]["imageFileName1"].ToString();
                    news.imageFileName2 = ds.Tables[0].Rows[0]["imageFileName2"].ToString();
                    news.imageFileName3 = ds.Tables[0].Rows[0]["imageFileName3"].ToString();
                    news.imageFileName4 = ds.Tables[0].Rows[0]["imageFileName4"].ToString();
                    news.imageFileName5 = ds.Tables[0].Rows[0]["imageFileName5"].ToString();
                    news.imageFileName6 = ds.Tables[0].Rows[0]["imageFileName6"].ToString();
                    news.imageFileName7 = ds.Tables[0].Rows[0]["imageFileName7"].ToString();
                    news.imageFileName8 = ds.Tables[0].Rows[0]["imageFileName8"].ToString();
                    news.imageFileName9 = ds.Tables[0].Rows[0]["imageFileName9"].ToString();
                    news.imageFileName10 = ds.Tables[0].Rows[0]["imageFileName10"].ToString();
                    news.imageFileName11 = ds.Tables[0].Rows[0]["imageFileName11"].ToString();
                    news.imageFileName12 = ds.Tables[0].Rows[0]["imageFileName12"].ToString();
                    news.imageFileName13 = ds.Tables[0].Rows[0]["imageFileName13"].ToString();
                    news.imageFileName14 = ds.Tables[0].Rows[0]["imageFileName14"].ToString();
                    news.imageFileName15 = ds.Tables[0].Rows[0]["imageFileName15"].ToString();
                    if (ds.Tables[0].Rows[0]["StartDate"].Equals(DBNull.Value) == false)
                        news.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]);
                    detailList = getDetails(news.NewsOID);
                    if (detailList != null)
                    {
                        news.NewsAndUpdateDetails = new List<Details>();
                        news.NewsAndUpdateDetails = detailList;
                    }
                }
            }
            catch (Exception ex) { }
            return news;
        }

public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO NewsAndUpdate 
                            (NewsOID,Enable,Seq,FileName,BannerFileName
                            ,imageFileName1,imageFileName2,imageFileName3,imageFileName4,imageFileName5,imageFileName6,imageFileName7,imageFileName8
                            ,imageFileName9,imageFileName10,imageFileName11,imageFileName12,imageFileName13,imageFileName14,imageFileName15,StartDate) 
                            VALUES 
                            (@NewsOID,@Enable,@Seq,@FileName,@BannerFileName
                            ,@imageFileName1,@imageFileName2,@imageFileName3,@imageFileName4,@imageFileName5,@imageFileName6,@imageFileName7,@imageFileName8
                            ,@imageFileName9,@imageFileName10,@imageFileName11,@imageFileName12,@imageFileName13,@imageFileName14,@imageFileName15,@StartDate)";
                SqlParameter[] sqlParam = new SqlParameter[21];
                sqlParam[0] = new SqlParameter("@NewsOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.NewsOID;
                sqlParam[1] = new SqlParameter("@Enable", SqlDbType.Bit);
                sqlParam[1].Value = this.Enable;
                sqlParam[2] = new SqlParameter("@Seq", SqlDbType.Int);
                sqlParam[2].Value = this.Seq;
                sqlParam[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[3].Value = this.FileName != null ? this.FileName : SqlString.Null;
                sqlParam[4] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[4].Value = this.BannerFileName != null ? this.BannerFileName : SqlString.Null;
                sqlParam[5] = new SqlParameter("@imageFileName1", SqlDbType.NVarChar);
                sqlParam[5].Value = this.imageFileName1 != null ? this.imageFileName1 : SqlString.Null;
                sqlParam[6] = new SqlParameter("@imageFileName2", SqlDbType.NVarChar);
                sqlParam[6].Value = this.imageFileName2 != null ? this.imageFileName2 : SqlString.Null;
                sqlParam[7] = new SqlParameter("@imageFileName3", SqlDbType.NVarChar);
                sqlParam[7].Value = this.imageFileName3 != null ? this.imageFileName3 : SqlString.Null;
                sqlParam[8] = new SqlParameter("@imageFileName4", SqlDbType.NVarChar);
                sqlParam[8].Value = this.imageFileName4 != null ? this.imageFileName4 : SqlString.Null;
                sqlParam[9] = new SqlParameter("@imageFileName5", SqlDbType.NVarChar);
                sqlParam[9].Value = this.imageFileName5 != null ? this.imageFileName5 : SqlString.Null;
                sqlParam[10] = new SqlParameter("@imageFileName6", SqlDbType.NVarChar);
                sqlParam[10].Value = this.imageFileName6 != null ? this.imageFileName6 : SqlString.Null;
                sqlParam[11] = new SqlParameter("@imageFileName7", SqlDbType.NVarChar);
                sqlParam[11].Value = this.imageFileName7 != null ? this.imageFileName7 : SqlString.Null;
                sqlParam[12] = new SqlParameter("@imageFileName8", SqlDbType.NVarChar);
                sqlParam[12].Value = this.imageFileName8 != null ? this.imageFileName8 : SqlString.Null;
                sqlParam[13] = new SqlParameter("@imageFileName9", SqlDbType.NVarChar);
                sqlParam[13].Value = this.imageFileName9 != null ? this.imageFileName9 : SqlString.Null;
                sqlParam[14] = new SqlParameter("@imageFileName10", SqlDbType.NVarChar);
                sqlParam[14].Value = this.imageFileName10 != null ? this.imageFileName10 : SqlString.Null;
                sqlParam[15] = new SqlParameter("@imageFileName11", SqlDbType.NVarChar);
                sqlParam[15].Value = this.imageFileName11 != null ? this.imageFileName11 : SqlString.Null;
                sqlParam[16] = new SqlParameter("@imageFileName12", SqlDbType.NVarChar);
                sqlParam[16].Value = this.imageFileName12 != null ? this.imageFileName12 : SqlString.Null;
                sqlParam[17] = new SqlParameter("@imageFileName13", SqlDbType.NVarChar);
                sqlParam[17].Value = this.imageFileName13 != null ? this.imageFileName13 : SqlString.Null;
                sqlParam[18] = new SqlParameter("@imageFileName14", SqlDbType.NVarChar);
                sqlParam[18].Value = this.imageFileName14 != null ? this.imageFileName14 : SqlString.Null;
                sqlParam[19] = new SqlParameter("@imageFileName15", SqlDbType.NVarChar);
                sqlParam[19].Value = this.imageFileName15 != null ? this.imageFileName15 : SqlString.Null;
                sqlParam[20] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                sqlParam[20].Value = this.StartDate;


                int iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (iRec > 0)
                {
                    sSQL = @"INSERT INTO NewsAndUpdateDetails (NewsOID,Culture,Title,ShortDes,Descriptions) VALUES (@NewsOID,@Culture,@Title,@ShortDes,@Descriptions)";
                    foreach (Details details in this.NewsAndUpdateDetails)
                    {
                        sqlParam = new SqlParameter[5];
                        sqlParam[0] = new SqlParameter("@NewsOID", SqlDbType.UniqueIdentifier);
                        sqlParam[0].Value = this.NewsOID;
                        sqlParam[1] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                        sqlParam[1].Value = details.Culture;
                        sqlParam[2] = new SqlParameter("@Title", SqlDbType.NVarChar);
                        sqlParam[2].Value = details.Title;
                        sqlParam[3] = new SqlParameter("@ShortDes", SqlDbType.NVarChar);
                        sqlParam[3].Value = details.ShortDes;
                        sqlParam[4] = new SqlParameter("@Descriptions", SqlDbType.NVarChar);
                        sqlParam[4].Value = details.Descriptions;
                        iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                    }
                }
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
                string sSQL = @"UPDATE NewsAndUpdate SET 
                    Enable=@Enable,Seq=@Seq,FileName=@FileName,BannerFileName=@BannerFileName
                    ,imageFileName1=@imageFileName1,imageFileName2=@imageFileName2,imageFileName3=@imageFileName3,imageFileName4=@imageFileName4
                    ,imageFileName5=@imageFileName5,imageFileName6=@imageFileName6,imageFileName7=@imageFileName7,imageFileName8=@imageFileName8
                    ,imageFileName9=@imageFileName9,imageFileName10=@imageFileName10,imageFileName11=@imageFileName11,imageFileName12=@imageFileName12
                    ,imageFileName13=@imageFileName13,imageFileName14=@imageFileName14,imageFileName15=@imageFileName15,StartDate=@StartDate
                    WHERE NewsOID=@NewsOID";
                SqlParameter[] sqlParam = new SqlParameter[21];
                sqlParam[0] = new SqlParameter("@NewsOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.NewsOID;
                sqlParam[1] = new SqlParameter("@Enable", SqlDbType.Bit);
                sqlParam[1].Value = this.Enable;
                sqlParam[2] = new SqlParameter("@Seq", SqlDbType.Int);
                sqlParam[2].Value = this.Seq;
                sqlParam[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[3].Value = this.FileName != null ? this.FileName : SqlString.Null;
                sqlParam[4] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[4].Value = this.BannerFileName != null ? this.BannerFileName : SqlString.Null;
                sqlParam[5] = new SqlParameter("@imageFileName1", SqlDbType.NVarChar);
                sqlParam[5].Value = this.imageFileName1 != null ? this.imageFileName1 : SqlString.Null;
                sqlParam[6] = new SqlParameter("@imageFileName2", SqlDbType.NVarChar);
                sqlParam[6].Value = this.imageFileName2 != null ? this.imageFileName2 : SqlString.Null;
                sqlParam[7] = new SqlParameter("@imageFileName3", SqlDbType.NVarChar);
                sqlParam[7].Value = this.imageFileName3 != null ? this.imageFileName3 : SqlString.Null;
                sqlParam[8] = new SqlParameter("@imageFileName4", SqlDbType.NVarChar);
                sqlParam[8].Value = this.imageFileName4 != null ? this.imageFileName4 : SqlString.Null;
                sqlParam[9] = new SqlParameter("@imageFileName5", SqlDbType.NVarChar);
                sqlParam[9].Value = this.imageFileName5 != null ? this.imageFileName5 : SqlString.Null;
                sqlParam[10] = new SqlParameter("@imageFileName6", SqlDbType.NVarChar);
                sqlParam[10].Value = this.imageFileName6 != null ? this.imageFileName6 : SqlString.Null;
                sqlParam[11] = new SqlParameter("@imageFileName7", SqlDbType.NVarChar);
                sqlParam[11].Value = this.imageFileName7 != null ? this.imageFileName7 : SqlString.Null;
                sqlParam[12] = new SqlParameter("@imageFileName8", SqlDbType.NVarChar);
                sqlParam[12].Value = this.imageFileName8 != null ? this.imageFileName8 : SqlString.Null;
                sqlParam[13] = new SqlParameter("@imageFileName9", SqlDbType.NVarChar);
                sqlParam[13].Value = this.imageFileName9 != null ? this.imageFileName9 : SqlString.Null;
                sqlParam[14] = new SqlParameter("@imageFileName10", SqlDbType.NVarChar);
                sqlParam[14].Value = this.imageFileName10 != null ? this.imageFileName10 : SqlString.Null;
                sqlParam[15] = new SqlParameter("@imageFileName11", SqlDbType.NVarChar);
                sqlParam[15].Value = this.imageFileName11 != null ? this.imageFileName11 : SqlString.Null;
                sqlParam[16] = new SqlParameter("@imageFileName12", SqlDbType.NVarChar);
                sqlParam[16].Value = this.imageFileName12 != null ? this.imageFileName12 : SqlString.Null;
                sqlParam[17] = new SqlParameter("@imageFileName13", SqlDbType.NVarChar);
                sqlParam[17].Value = this.imageFileName13 != null ? this.imageFileName13 : SqlString.Null;
                sqlParam[18] = new SqlParameter("@imageFileName14", SqlDbType.NVarChar);
                sqlParam[18].Value = this.imageFileName14 != null ? this.imageFileName14 : SqlString.Null;
                sqlParam[19] = new SqlParameter("@imageFileName15", SqlDbType.NVarChar);
                sqlParam[19].Value = this.imageFileName15 != null ? this.imageFileName15 : SqlString.Null;
                sqlParam[20] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                sqlParam[20].Value = this.StartDate;

                int iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (iRec > 0)
                {
                    sSQL = @"UPDATE NewsAndUpdateDetails SET Title=@Title,ShortDes=@ShortDes,Descriptions=@Descriptions WHERE  NewsOID=@NewsOID AND Culture=@Culture";
                    foreach (Details details in this.NewsAndUpdateDetails)
                    {
                        sqlParam = new SqlParameter[5];
                        sqlParam[0] = new SqlParameter("@NewsOID", SqlDbType.UniqueIdentifier);
                        sqlParam[0].Value = this.NewsOID;
                        sqlParam[1] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                        sqlParam[1].Value = details.Culture;
                        sqlParam[2] = new SqlParameter("@Title", SqlDbType.NVarChar);
                        sqlParam[2].Value = details.Title;
                        sqlParam[3] = new SqlParameter("@ShortDes", SqlDbType.NVarChar);
                        sqlParam[3].Value = details.ShortDes;
                        sqlParam[4] = new SqlParameter("@Descriptions", SqlDbType.NVarChar);
                        sqlParam[4].Value = details.Descriptions;
                        iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                    }
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
    public class Details
        {
            public string Culture { get; set; }
            public string Title { get; set; }
            public string ShortDes { get; set; }
            public string Descriptions { get; set; }
        }
    }

