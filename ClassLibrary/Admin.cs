using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlTypes;

namespace BudgetCBL
{
    public partial class Admin
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid AdminOID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public Admin()
        {
        }

        public Admin(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public Admin SelectByLogin(string sUserName, string sPassword)
        {
            Admin admin = null;
            try
            {
                string sSQL = "SELECT * FROM AdminConfig WHERE UserName=@UserName AND Password=@Password  AND Enable = 1 ";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@UserName", SqlDbType.NVarChar);
                sqlParam[0].Value = sUserName;
                sqlParam[1] = new SqlParameter("@Password", SqlDbType.NVarChar);
                sqlParam[1].Value = Crypto.EncryptText(sPassword);

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    admin = new Admin();
                    admin.AdminOID = new Guid(ds.Tables[0].Rows[0]["AdminOID"].ToString());
                    admin.UserName = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                    admin.Password = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                    admin.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                    admin.Role = Convert.ToInt32(ds.Tables[0].Rows[0]["Role"]);
                    admin.Enable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Enable"]);
                    admin.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateDate"]);
                    admin.LastLoginDate = DateTime.Now;// Convert.ToDateTime(ds.Tables[0].Rows[0]["LastLoginDate"]);
                    this.AdminOID = admin.AdminOID;
                    this.LastLoginDate = admin.LastLoginDate;
                    updateLastLogin();
                }
            }
            catch (Exception ex) { }
            return admin;
        }

        public Admin SelectByOID(Guid guidAdminOId)
        {

            Admin admin = null;
            try
            {
                string sSQL = "SELECT * FROM AdminConfig WHERE AdminOID=@AdminOID  AND Enable = 1 ";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@AdminOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidAdminOId;

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    admin = new Admin();
                    admin.AdminOID = new Guid(ds.Tables[0].Rows[0]["AdminOID"].ToString());
                    admin.UserName = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                    admin.Password = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                    admin.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                    admin.Role = Convert.ToInt32(ds.Tables[0].Rows[0]["Role"]);
                    admin.Enable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Enable"]);
                    admin.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateDate"]);
                    admin.LastLoginDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["LastLoginDate"]);
                }
            }
            catch (Exception ex) { }
            return admin;
        }

        public List<Admin> SelectAll()
        {
            List<Admin> AdminList = null;
            try
            {
                string sSQL = "SELECT * FROM AdminConfig";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AdminList = new List<Admin>();
                    Admin admin = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        admin = new Admin();
                        admin.AdminOID = new Guid(ds.Tables[0].Rows[i]["AdminOID"].ToString());
                        admin.UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                        admin.Password = Convert.ToString(ds.Tables[0].Rows[i]["Password"]);
                        admin.Email = Convert.ToString(ds.Tables[0].Rows[i]["Email"]);
                        admin.Role = Convert.ToInt32(ds.Tables[0].Rows[i]["Role"]);
                        admin.Enable = Convert.ToBoolean(ds.Tables[0].Rows[i]["Enable"]);
                        admin.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreateDate"]);
                        if (ds.Tables[0].Rows[i]["LastLoginDate"].Equals(DBNull.Value) == false)
                            admin.LastLoginDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["LastLoginDate"]);
                        else
                            admin.LastLoginDate = null;
                        AdminList.Add(admin);
                    }
                }
            }
            catch (Exception ex) { }
            return AdminList;
        }

        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = "SELECT Count(*) FROM AdminConfig WHERE UserName=@UserName";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@UserName", SqlDbType.NVarChar);
                sqlParam[0].Value = this.UserName;
                int iRec = (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (iRec == 0)
                {
                    sSQL = @"INSERT INTO AdminConfig (AdminOID,UserName,Password,Enable,Email,Role,CreateDate,LastLoginDate)
                VALUES
                (@AdminOID,@UserName,@Password,@Enable,@Email,@Role,@CreateDate,@LastLoginDate)";
                    sqlParam = new SqlParameter[8];
                    sqlParam[0] = new SqlParameter("@AdminOID", Guid.NewGuid());
                    sqlParam[1] = new SqlParameter("@UserName", this.UserName);
                    sqlParam[2] = new SqlParameter("@Password", Crypto.EncryptText(this.Password));
                    sqlParam[3] = new SqlParameter("@Enable", this.Enable);
                    sqlParam[4] = new SqlParameter("@Email", this.Email);
                    sqlParam[5] = new SqlParameter("@Role", this.Role);
                    sqlParam[6] = new SqlParameter("@CreateDate", DateTime.Now);
                    sqlParam[7] = new SqlParameter("@LastLoginDate", SqlDateTime.Null);
                    SqlHelper.ExecuteNonQuery(
                           ConnectionString,
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
                string sSQL = @"SELECT * FROM AdminConfig Where Username=@Username AND AdminOID <> @AdminOID";
                SqlParameter[] sqlParms = new SqlParameter[2];
                sqlParms[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
                sqlParms[0].Value = this.UserName;
                sqlParms[1] = new SqlParameter("@AdminOID", SqlDbType.UniqueIdentifier);
                sqlParms[1].Value = this.AdminOID;
                DataSet ds = SqlHelper.ExecuteDataset(
              ConnectionString , CommandType.Text, sSQL, sqlParms);
                if (ds != null && ds.Tables[0].Rows.Count == 0)
                {
                    sSQL = @"UPDATE AdminConfig SET Username=@Username,Password=@Password,Enable=@Enable,Email=@Email,Role=@Role  Where AdminOID=@AdminOID";
                    sqlParms = new SqlParameter[6];
                    sqlParms[0] = new SqlParameter("@AdminOID", SqlDbType.UniqueIdentifier);
                    sqlParms[0].Value = this.AdminOID;
                    sqlParms[1] = new SqlParameter("@Username", SqlDbType.NVarChar);
                    sqlParms[1].Value = this.UserName;
                    sqlParms[2] = new SqlParameter("@Password", SqlDbType.NVarChar);
                    sqlParms[2].Value = Crypto.EncryptText(this.Password);
                    sqlParms[3] = new SqlParameter("@Enable", SqlDbType.NVarChar);
                    sqlParms[3].Value = this.Enable;
                    sqlParms[4] = new SqlParameter("@Email", SqlDbType.NVarChar);
                    sqlParms[4].Value = this.Email;
                    sqlParms[5] = new SqlParameter("@Role", SqlDbType.NVarChar);
                    sqlParms[5].Value = this.Role;

                    SqlHelper.ExecuteNonQuery(
                      ConnectionString,CommandType.Text, sSQL, sqlParms);
                    bResult = true;
                }
            }
            catch (Exception ex) { }
            return bResult;
        }

        public bool delete(Guid _OID)
        {
            bool bResult = false;
            try
            {
                string sSql = "DELETE FROM AdminConfig Where AdminOID=@AdminOID";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@AdminOID", _OID);

                SqlHelper.ExecuteNonQuery(
                       ConnectionString,
                       CommandType.Text, sSql, param);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }

        private bool updateLastLogin()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"UPDATE AdminConfig SET LastLoginDate=@LastLoginDate  Where AdminOID=@AdminOID";
                SqlParameter[] sqlParms = new SqlParameter[2];
                sqlParms[0] = new SqlParameter("@AdminOID", SqlDbType.UniqueIdentifier);
                sqlParms[0].Value = this.AdminOID;
                sqlParms[1] = new SqlParameter("@LastLoginDate", SqlDbType.DateTime);
                sqlParms[1].Value = this.LastLoginDate;
                SqlHelper.ExecuteNonQuery(
                      ConnectionString, CommandType.Text, sSQL, sqlParms);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
