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
    public partial class Budget_Categories_Banner
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public string CategoriesCode { get; set; }
        public string CategoriesName { get; set; }
        public Budget_Categories_Banner()
        {
        }

        public Budget_Categories_Banner(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<Budget_Categories_Banner> SelectAll()
        {
            List<Budget_Categories_Banner> Categories_BannerList = null;
            try
            {
                string sSQL = "SELECT * FROM Categories_Banner";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Categories_BannerList = new List<Budget_Categories_Banner>();
                    Budget_Categories_Banner categories = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        categories = new Budget_Categories_Banner();
                        categories.CategoriesCode = ds.Tables[0].Rows[i]["CategoriesCode"].ToString();
                        categories.CategoriesName = ds.Tables[0].Rows[i]["CategoriesName"].ToString();
                        Categories_BannerList.Add(categories);
                    }
                }
            }
            catch (Exception ex) { }
            return Categories_BannerList;
        }

        public Budget_Categories_Banner SelectByCode(string CategoriesCode)
        {
            Budget_Categories_Banner categories = null;
            try
            {
                string sSQL = "SELECT * FROM Categories_Banner WHERE CategoriesCode=@CategoriesCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
                sqlParam[0].Value = CategoriesCode;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    categories = new Budget_Categories_Banner();
                    categories.CategoriesCode = Convert.ToString(ds.Tables[0].Rows[0]["CategoriesCode"]);
                    categories.CategoriesName = Convert.ToString(ds.Tables[0].Rows[0]["CategoriesName"]);

                }
            }
            catch (Exception ex) { }
            return categories;
        }

        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = "SELECT Count(*) FROM Categories_Banner WHERE CategoriesCode=@CategoriesCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
                sqlParam[0].Value = this.CategoriesCode;
                int iRec = (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (iRec == 0)
                {
                    sSQL = @"INSERT INTO Categories_Banner (CategoriesCode,CategoriesName)
                VALUES
                (@CategoriesCode,@CategoriesName)";
                    sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@CategoriesCode", this.CategoriesCode);
                    sqlParam[1] = new SqlParameter("@CategoriesName", this.CategoriesName);
                    SqlHelper.ExecuteNonQuery(ConnectionString,
                           CommandType.Text, sSQL, sqlParam);
                    bResult = true;
                }
            }
            catch (Exception ex) { }
            return bResult;
        }

        public bool update()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"SELECT * FROM Categories_Banner Where CategoriesName=@CategoriesName AND CategoriesCode <> @CategoriesCode";
                SqlParameter[] sqlParms = new SqlParameter[2];
                sqlParms[0] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
                sqlParms[0].Value = this.CategoriesCode;
                sqlParms[1] = new SqlParameter("@CategoriesName", SqlDbType.NVarChar);
                sqlParms[1].Value = this.CategoriesName;
                DataSet ds = SqlHelper.ExecuteDataset(
              ConnectionString, CommandType.Text, sSQL, sqlParms);
                if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    sSQL = @"UPDATE Categories_Banner SET CategoriesName=@CategoriesName Where CategoriesCode=@CategoriesCode";
                    sqlParms = new SqlParameter[2];
                    sqlParms[0] = new SqlParameter("@CategoriesName", SqlDbType.NVarChar);
                    sqlParms[0].Value = this.CategoriesName;
                    sqlParms[1] = new SqlParameter("@CategoriesCode", SqlDbType.NVarChar);
                    sqlParms[1].Value = this.CategoriesCode;

                    SqlHelper.ExecuteNonQuery(
                      ConnectionString, CommandType.Text, sSQL, sqlParms);
                    bResult = true;
                }
            }
            catch (Exception ex) { }
            return bResult;
        }

        public bool delete(string CategoriesCode)
        {
            bool bResult = false;
            try
            {
                string sSql = "DELETE FROM Categories_Banner Where CategoriesCode=@CategoriesCode";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@CategoriesCode", CategoriesCode);

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
