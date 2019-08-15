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
    public partial class Budget_TravelBlog
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid TravelBlogOID { get; set; }
        public bool Enable { get; set; }
        public int Seq { get; set; }
        public string FileName { get; set; }
        public string BannerFileName { get; set; }
        public string Title { get; set; }
        public string ShortDes { get; set; }
        public string Descriptions { get; set; }
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
        public string Latitude_From { get; set; }
        public string Latitude_To { get; set; }
        public string Longitude_From { get; set; }
        public string Longitude_To { get; set; }

        public Budget_TravelBlog()
        {
        }
        public Budget_TravelBlog(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<Budget_TravelBlog> SelectAll()
        {
            List<Budget_TravelBlog> TravelBlogList = null;
            try
            {
                string sSQL = "SELECT * FROM TravelBlog";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TravelBlogList = new List<Budget_TravelBlog>();
                    Budget_TravelBlog blog = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        blog = new Budget_TravelBlog();
                        blog.TravelBlogOID = new Guid(ds.Tables[0].Rows[i]["TravelBlogOID"].ToString());
                        blog.Enable = Convert.ToBoolean(ds.Tables[0].Rows[i]["Enable"].ToString());
                        blog.Seq = Convert.ToInt32(ds.Tables[0].Rows[i]["Seq"].ToString());
                        blog.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        blog.BannerFileName = ds.Tables[0].Rows[i]["BannerFileName"].ToString();
                        blog.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                        blog.ShortDes = ds.Tables[0].Rows[i]["ShortDes"].ToString();
                        blog.Descriptions = ds.Tables[0].Rows[i]["Descriptions"].ToString();
                        blog.imageFileName1 = ds.Tables[0].Rows[i]["imageFileName1"].ToString();
                        blog.imageFileName2 = ds.Tables[0].Rows[i]["imageFileName2"].ToString();
                        blog.imageFileName3 = ds.Tables[0].Rows[i]["imageFileName3"].ToString();
                        blog.imageFileName4 = ds.Tables[0].Rows[i]["imageFileName4"].ToString();
                        blog.imageFileName5 = ds.Tables[0].Rows[i]["imageFileName5"].ToString();
                        blog.imageFileName6 = ds.Tables[0].Rows[i]["imageFileName6"].ToString();
                        blog.imageFileName7 = ds.Tables[0].Rows[i]["imageFileName7"].ToString();
                        blog.imageFileName8 = ds.Tables[0].Rows[i]["imageFileName8"].ToString();
                        blog.imageFileName9 = ds.Tables[0].Rows[i]["imageFileName9"].ToString();
                        blog.imageFileName10 = ds.Tables[0].Rows[i]["imageFileName10"].ToString();
                        blog.imageFileName11 = ds.Tables[0].Rows[i]["imageFileName11"].ToString();
                        blog.imageFileName12 = ds.Tables[0].Rows[i]["imageFileName12"].ToString();
                        blog.imageFileName13 = ds.Tables[0].Rows[i]["imageFileName13"].ToString();
                        blog.imageFileName14 = ds.Tables[0].Rows[i]["imageFileName14"].ToString();
                        blog.imageFileName15 = ds.Tables[0].Rows[i]["imageFileName15"].ToString();
                        blog.Latitude_From = ds.Tables[0].Rows[i]["Latitude_From"].ToString();
                        blog.Latitude_To = ds.Tables[0].Rows[i]["Latitude_To"].ToString();
                        blog.Longitude_From = ds.Tables[0].Rows[i]["Longitude_From"].ToString();
                        blog.Longitude_To = ds.Tables[0].Rows[i]["Longitude_To"].ToString();
                        TravelBlogList.Add(blog);
                    }
                }
            }
            catch (Exception ex) { }
            return TravelBlogList;
        }
        public Budget_TravelBlog SelectByOID(Guid guidTravelBlogOID)
        {
            Budget_TravelBlog blog = null;
            try
            {
                string sSQL = "SELECT * FROM TravelBlog WHERE TravelBlogOID=@TravelBlogOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@TravelBlogOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidTravelBlogOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    blog = new Budget_TravelBlog();
                    blog.TravelBlogOID = new Guid(ds.Tables[0].Rows[0]["TravelBlogOID"].ToString());
                    blog.Enable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Enable"].ToString());
                    blog.Seq = Convert.ToInt32(ds.Tables[0].Rows[0]["Seq"].ToString());
                    blog.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    blog.BannerFileName = ds.Tables[0].Rows[0]["BannerFileName"].ToString();
                    blog.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                    blog.ShortDes = ds.Tables[0].Rows[0]["ShortDes"].ToString();
                    blog.Descriptions = ds.Tables[0].Rows[0]["Descriptions"].ToString();
                    blog.imageFileName1 = ds.Tables[0].Rows[0]["imageFileName1"].ToString();
                    blog.imageFileName2 = ds.Tables[0].Rows[0]["imageFileName2"].ToString();
                    blog.imageFileName3 = ds.Tables[0].Rows[0]["imageFileName3"].ToString();
                    blog.imageFileName4 = ds.Tables[0].Rows[0]["imageFileName4"].ToString();
                    blog.imageFileName5 = ds.Tables[0].Rows[0]["imageFileName5"].ToString();
                    blog.imageFileName6 = ds.Tables[0].Rows[0]["imageFileName6"].ToString();
                    blog.imageFileName7 = ds.Tables[0].Rows[0]["imageFileName7"].ToString();
                    blog.imageFileName8 = ds.Tables[0].Rows[0]["imageFileName8"].ToString();
                    blog.imageFileName9 = ds.Tables[0].Rows[0]["imageFileName9"].ToString();
                    blog.imageFileName10 = ds.Tables[0].Rows[0]["imageFileName10"].ToString();
                    blog.imageFileName11 = ds.Tables[0].Rows[0]["imageFileName11"].ToString();
                    blog.imageFileName12 = ds.Tables[0].Rows[0]["imageFileName12"].ToString();
                    blog.imageFileName13 = ds.Tables[0].Rows[0]["imageFileName13"].ToString();
                    blog.imageFileName14 = ds.Tables[0].Rows[0]["imageFileName14"].ToString();
                    blog.imageFileName15 = ds.Tables[0].Rows[0]["imageFileName15"].ToString();
                    blog.Latitude_From = ds.Tables[0].Rows[0]["Latitude_From"].ToString();
                    blog.Latitude_To = ds.Tables[0].Rows[0]["Latitude_To"].ToString();
                    blog.Longitude_From = ds.Tables[0].Rows[0]["Longitude_From"].ToString();
                    blog.Longitude_To = ds.Tables[0].Rows[0]["Longitude_To"].ToString();

                }
            }
            catch (Exception ex) { }
            return blog;
        }
        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO TravelBlog 
                            (TravelBlogOID,Enable,Seq,FileName,BannerFileName,Title,ShortDes,Descriptions
                            ,imageFileName1,imageFileName2,imageFileName3,imageFileName4,imageFileName5,imageFileName6,imageFileName7,imageFileName8
                            ,imageFileName9,imageFileName10,imageFileName11,imageFileName12,imageFileName13,imageFileName14,imageFileName15
                            ,Latitude_From,Latitude_To,Longitude_From,Longitude_To) 
                            VALUES 
                            (@TravelBlogOID,@Enable,@Seq,@FileName,@BannerFileName,@Title,@ShortDes,@Descriptions
                            ,@imageFileName1,@imageFileName2,@imageFileName3,@imageFileName4,@imageFileName5,@imageFileName6,@imageFileName7,@imageFileName8
                            ,@imageFileName9,@imageFileName10,@imageFileName11,@imageFileName12,@imageFileName13,@imageFileName14,@imageFileName15
                            ,@Latitude_From,@Latitude_To,@Longitude_From,@Longitude_To)";
                SqlParameter[] sqlParam = new SqlParameter[27];
                sqlParam[0] = new SqlParameter("@TravelBlogOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.TravelBlogOID;
                sqlParam[1] = new SqlParameter("@Enable", SqlDbType.Bit);
                sqlParam[1].Value = this.Enable;
                sqlParam[2] = new SqlParameter("@Seq", SqlDbType.Int);
                sqlParam[2].Value = this.Seq;
                sqlParam[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[3].Value = this.FileName != null ? this.FileName : SqlString.Null;
                sqlParam[4] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[4].Value = this.BannerFileName != null ? this.BannerFileName : SqlString.Null;
                sqlParam[5] = new SqlParameter("@Title", SqlDbType.NVarChar);
                sqlParam[5].Value = this.Title;
                sqlParam[6] = new SqlParameter("@ShortDes", SqlDbType.NVarChar);
                sqlParam[6].Value = this.ShortDes;
                sqlParam[7] = new SqlParameter("@Descriptions", SqlDbType.NVarChar);
                sqlParam[7].Value = this.Descriptions;
                sqlParam[8] = new SqlParameter("@imageFileName1", SqlDbType.NVarChar);
                sqlParam[8].Value = this.imageFileName1 != null ? this.imageFileName1 : SqlString.Null;
                sqlParam[9] = new SqlParameter("@imageFileName2", SqlDbType.NVarChar);
                sqlParam[9].Value = this.imageFileName2 != null ? this.imageFileName2 : SqlString.Null;
                sqlParam[10] = new SqlParameter("@imageFileName3", SqlDbType.NVarChar);
                sqlParam[10].Value = this.imageFileName3 != null ? this.imageFileName3 : SqlString.Null;
                sqlParam[11] = new SqlParameter("@imageFileName4", SqlDbType.NVarChar);
                sqlParam[11].Value = this.imageFileName4 != null ? this.imageFileName4 : SqlString.Null;
                sqlParam[12] = new SqlParameter("@imageFileName5", SqlDbType.NVarChar);
                sqlParam[12].Value = this.imageFileName5 != null ? this.imageFileName5 : SqlString.Null;
                sqlParam[13] = new SqlParameter("@imageFileName6", SqlDbType.NVarChar);
                sqlParam[13].Value = this.imageFileName6 != null ? this.imageFileName6 : SqlString.Null;
                sqlParam[14] = new SqlParameter("@imageFileName7", SqlDbType.NVarChar);
                sqlParam[14].Value = this.imageFileName7 != null ? this.imageFileName7 : SqlString.Null;
                sqlParam[15] = new SqlParameter("@imageFileName8", SqlDbType.NVarChar);
                sqlParam[15].Value = this.imageFileName8 != null ? this.imageFileName8 : SqlString.Null;
                sqlParam[16] = new SqlParameter("@imageFileName9", SqlDbType.NVarChar);
                sqlParam[16].Value = this.imageFileName9 != null ? this.imageFileName9 : SqlString.Null;
                sqlParam[17] = new SqlParameter("@imageFileName10", SqlDbType.NVarChar);
                sqlParam[17].Value = this.imageFileName10 != null ? this.imageFileName10 : SqlString.Null;
                sqlParam[18] = new SqlParameter("@imageFileName11", SqlDbType.NVarChar);
                sqlParam[18].Value = this.imageFileName11 != null ? this.imageFileName11 : SqlString.Null;
                sqlParam[19] = new SqlParameter("@imageFileName12", SqlDbType.NVarChar);
                sqlParam[19].Value = this.imageFileName12 != null ? this.imageFileName12 : SqlString.Null;
                sqlParam[20] = new SqlParameter("@imageFileName13", SqlDbType.NVarChar);
                sqlParam[20].Value = this.imageFileName13 != null ? this.imageFileName13 : SqlString.Null;
                sqlParam[21] = new SqlParameter("@imageFileName14", SqlDbType.NVarChar);
                sqlParam[21].Value = this.imageFileName14 != null ? this.imageFileName14 : SqlString.Null;
                sqlParam[22] = new SqlParameter("@imageFileName15", SqlDbType.NVarChar);
                sqlParam[22].Value = this.imageFileName15 != null ? this.imageFileName15 : SqlString.Null;
                sqlParam[23] = new SqlParameter("@Latitude_From", SqlDbType.NVarChar);
                sqlParam[23].Value = this.Latitude_From != null ? this.Latitude_From : SqlString.Null;
                sqlParam[24] = new SqlParameter("@Latitude_To", SqlDbType.NVarChar);
                sqlParam[24].Value = this.Latitude_To != null ? this.Latitude_To : SqlString.Null;
                sqlParam[25] = new SqlParameter("@Longitude_From", SqlDbType.NVarChar);
                sqlParam[25].Value = this.Longitude_From != null ? this.Longitude_From : SqlString.Null;
                sqlParam[26] = new SqlParameter("@Longitude_To", SqlDbType.NVarChar);
                sqlParam[26].Value = this.Longitude_To != null ? this.Longitude_To : SqlString.Null;

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
                string sSQL = @"UPDATE TravelBlog SET 
                    Enable=@Enable,Seq=@Seq,FileName=@FileName,BannerFileName=@BannerFileName,Title=@Title,ShortDes=@ShortDes,Descriptions=@Descriptions
                    ,imageFileName1=@imageFileName1,imageFileName2=@imageFileName2,imageFileName3=@imageFileName3,imageFileName4=@imageFileName4
                    ,imageFileName5=@imageFileName5,imageFileName6=@imageFileName6,imageFileName7=@imageFileName7,imageFileName8=@imageFileName8
                    ,imageFileName9=@imageFileName9,imageFileName10=@imageFileName10,imageFileName11=@imageFileName11,imageFileName12=@imageFileName12
                    ,imageFileName13=@imageFileName13,imageFileName14=@imageFileName14,imageFileName15=@imageFileName15
                    ,Latitude_From=@Latitude_From,Latitude_To=@Latitude_To,Longitude_From=@Longitude_From,Longitude_To=@Longitude_To
                    WHERE TravelBlogOID=@TravelBlogOID";
                SqlParameter[] sqlParam = new SqlParameter[27];
                sqlParam[0] = new SqlParameter("@TravelBlogOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.TravelBlogOID;
                sqlParam[1] = new SqlParameter("@Enable", SqlDbType.Bit);
                sqlParam[1].Value = this.Enable;
                sqlParam[2] = new SqlParameter("@Seq", SqlDbType.Int);
                sqlParam[2].Value = this.Seq;
                sqlParam[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[3].Value = this.FileName != null ? this.FileName : SqlString.Null;
                sqlParam[4] = new SqlParameter("@BannerFileName", SqlDbType.NVarChar);
                sqlParam[4].Value = this.BannerFileName != null ? this.BannerFileName : SqlString.Null;
                sqlParam[5] = new SqlParameter("@Title", SqlDbType.NVarChar);
                sqlParam[5].Value = this.Title;
                sqlParam[6] = new SqlParameter("@ShortDes", SqlDbType.NVarChar);
                sqlParam[6].Value = this.ShortDes;
                sqlParam[7] = new SqlParameter("@Descriptions", SqlDbType.NVarChar);
                sqlParam[7].Value = this.Descriptions;
                sqlParam[8] = new SqlParameter("@imageFileName1", SqlDbType.NVarChar);
                sqlParam[8].Value = this.imageFileName1 != null ? this.imageFileName1 : SqlString.Null;
                sqlParam[9] = new SqlParameter("@imageFileName2", SqlDbType.NVarChar);
                sqlParam[9].Value = this.imageFileName2 != null ? this.imageFileName2 : SqlString.Null;
                sqlParam[10] = new SqlParameter("@imageFileName3", SqlDbType.NVarChar);
                sqlParam[10].Value = this.imageFileName3 != null ? this.imageFileName3 : SqlString.Null;
                sqlParam[11] = new SqlParameter("@imageFileName4", SqlDbType.NVarChar);
                sqlParam[11].Value = this.imageFileName4 != null ? this.imageFileName4 : SqlString.Null;
                sqlParam[12] = new SqlParameter("@imageFileName5", SqlDbType.NVarChar);
                sqlParam[12].Value = this.imageFileName5 != null ? this.imageFileName5 : SqlString.Null;
                sqlParam[13] = new SqlParameter("@imageFileName6", SqlDbType.NVarChar);
                sqlParam[13].Value = this.imageFileName6 != null ? this.imageFileName6 : SqlString.Null;
                sqlParam[14] = new SqlParameter("@imageFileName7", SqlDbType.NVarChar);
                sqlParam[14].Value = this.imageFileName7 != null ? this.imageFileName7 : SqlString.Null;
                sqlParam[15] = new SqlParameter("@imageFileName8", SqlDbType.NVarChar);
                sqlParam[15].Value = this.imageFileName8 != null ? this.imageFileName8 : SqlString.Null;
                sqlParam[16] = new SqlParameter("@imageFileName9", SqlDbType.NVarChar);
                sqlParam[16].Value = this.imageFileName9 != null ? this.imageFileName9 : SqlString.Null;
                sqlParam[17] = new SqlParameter("@imageFileName10", SqlDbType.NVarChar);
                sqlParam[17].Value = this.imageFileName10 != null ? this.imageFileName10 : SqlString.Null;
                sqlParam[18] = new SqlParameter("@imageFileName11", SqlDbType.NVarChar);
                sqlParam[18].Value = this.imageFileName11 != null ? this.imageFileName11 : SqlString.Null;
                sqlParam[19] = new SqlParameter("@imageFileName12", SqlDbType.NVarChar);
                sqlParam[19].Value = this.imageFileName12 != null ? this.imageFileName12 : SqlString.Null;
                sqlParam[20] = new SqlParameter("@imageFileName13", SqlDbType.NVarChar);
                sqlParam[20].Value = this.imageFileName13 != null ? this.imageFileName13 : SqlString.Null;
                sqlParam[21] = new SqlParameter("@imageFileName14", SqlDbType.NVarChar);
                sqlParam[21].Value = this.imageFileName14 != null ? this.imageFileName14 : SqlString.Null;
                sqlParam[22] = new SqlParameter("@imageFileName15", SqlDbType.NVarChar);
                sqlParam[22].Value = this.imageFileName15 != null ? this.imageFileName15 : SqlString.Null;
                sqlParam[23] = new SqlParameter("@Latitude_From", SqlDbType.NVarChar);
                sqlParam[23].Value = this.Latitude_From != null ? this.Latitude_From : SqlString.Null;
                sqlParam[24] = new SqlParameter("@Latitude_To", SqlDbType.NVarChar);
                sqlParam[24].Value = this.Latitude_To != null ? this.Latitude_To : SqlString.Null;
                sqlParam[25] = new SqlParameter("@Longitude_From", SqlDbType.NVarChar);
                sqlParam[25].Value = this.Longitude_From != null ? this.Longitude_From : SqlString.Null;
                sqlParam[26] = new SqlParameter("@Longitude_To", SqlDbType.NVarChar);
                sqlParam[26].Value = this.Longitude_To != null ? this.Longitude_To : SqlString.Null;

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
