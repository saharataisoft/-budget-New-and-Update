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
    public partial class VehiclesCategory
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public int Code { get; set; }
        public string Name { get; set; }
        public VehiclesCategory()
        {
        }
        public VehiclesCategory(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public VehiclesCategory SelectByCode(int Code)
        {
            VehiclesCategory Category = null;
            try
            {
                string sSQL = "SELECT * FROM VehiclesCategory WHERE Code=@Code";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Code", SqlDbType.NVarChar);
                sqlParam[0].Value = Code;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                  
                    Category = new VehiclesCategory();
                    Category.Code = Convert.ToInt32(ds.Tables[0].Rows[0]["Code"]);
                    Category.Name = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                  
                }
            }
            catch (Exception ex) { }
            return Category;
        }

        public List<VehiclesCategory> SelectAll()
        {
            List<VehiclesCategory> CategoryList = null;
            try
            {
                string sSQL = "SELECT * FROM VehiclesCategory";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CategoryList = new List<VehiclesCategory>();
                    VehiclesCategory Category = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Category = new VehiclesCategory();
                        Category.Code = Convert.ToInt32(ds.Tables[0].Rows[i]["Code"]);
                        Category.Name = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);

                        CategoryList.Add(Category);
                    }
                }
            }
            catch (Exception ex) { }
            return CategoryList;
        }

        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = "SELECT Count(*) FROM VehiclesCategory WHERE Code=@Code";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Code", SqlDbType.Int);
                sqlParam[0].Value = this.Code;
                int iRec = (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (iRec == 0)
                {
                    sSQL = @"INSERT INTO VehiclesCategory (Code,Name)
                VALUES
                (@Code,@Name)";
                    sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@Code",this.Code);
                    sqlParam[1] = new SqlParameter("@Name", this.Name);
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
                string sSQL = @"SELECT * FROM VehiclesCategory Where Name=@Name AND Code <> @Code";
                SqlParameter[] sqlParms = new SqlParameter[2];
                sqlParms[0] = new SqlParameter("@Code", SqlDbType.Int);
                sqlParms[0].Value = this.Code;
                sqlParms[1] = new SqlParameter("@Name", SqlDbType.NVarChar);
                sqlParms[1].Value = this.Name;
                DataSet ds = SqlHelper.ExecuteDataset(
              ConnectionString, CommandType.Text, sSQL, sqlParms);
                if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    sSQL = @"UPDATE VehiclesCategory SET Name=@Name Where Code=@Code";
                    sqlParms = new SqlParameter[2];
                    sqlParms[0] = new SqlParameter("@Name", SqlDbType.NVarChar);
                    sqlParms[0].Value = this.Name;
                    sqlParms[1] = new SqlParameter("@Code", SqlDbType.Int);
                    sqlParms[1].Value = this.Code;

                    SqlHelper.ExecuteNonQuery(
                      ConnectionString, CommandType.Text, sSQL, sqlParms);
                    bResult = true;
                }
            }
            catch (Exception ex) { }
            return bResult;
        }

        public bool delete(int Code)
        {
            bool bResult = false;
            try
            {
                string sSql = "DELETE FROM VehiclesCategory Where Code=@Code";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@Code", Code);

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
