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
    public partial class Budget_VehicleRange
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid VehicleRangeOID { get; set; }
        public bool Enable { get; set; }
        public string FileName { get; set; }
        public string VehicleRangeURL { get; set; }

        public Budget_VehicleRange()
        {
        }
        public Budget_VehicleRange(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }

        public List<Budget_VehicleRange> SelectAll()
        {
            List<Budget_VehicleRange> VehicleRangeList = null;
            try
            {
                string sSQL = "SELECT * FROM VehicleRange";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    VehicleRangeList = new List<Budget_VehicleRange>();
                    Budget_VehicleRange vehicleRange = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        vehicleRange = new Budget_VehicleRange();
                        vehicleRange.VehicleRangeOID = new Guid(ds.Tables[0].Rows[i]["VehicleRangeOID"].ToString());
                        vehicleRange.Enable = Convert.ToBoolean(ds.Tables[0].Rows[i]["Enable"].ToString());
                        vehicleRange.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        vehicleRange.VehicleRangeURL = ds.Tables[0].Rows[i]["VehicleRangeURL"].ToString();
                        VehicleRangeList.Add(vehicleRange);
                    }
                }
            }
            catch (Exception ex) { }
            return VehicleRangeList;
        }
        public Budget_VehicleRange SelectByOID(Guid guidVehicleRangeOID)
        {
            Budget_VehicleRange vehicleRange = null;
            try
            {
                string sSQL = "SELECT * FROM VehicleRange WHERE VehicleRangeOID=@VehicleRangeOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@VehicleRangeOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidVehicleRangeOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    vehicleRange = new Budget_VehicleRange();
                    vehicleRange.VehicleRangeOID = new Guid(ds.Tables[0].Rows[0]["VehicleRangeOID"].ToString());
                    vehicleRange.Enable = Convert.ToBoolean(ds.Tables[0].Rows[0]["Enable"].ToString());
                    vehicleRange.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    vehicleRange.VehicleRangeURL = ds.Tables[0].Rows[0]["VehicleRangeURL"].ToString();

                }
            }
            catch (Exception ex) { }
            return vehicleRange;
        }
        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO VehicleRange (VehicleRangeOID,Enable,FileName,VehicleRangeURL) 
                            VALUES 
                            (@VehicleRangeOID,@Enable,@FileName,@VehicleRangeURL)";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@VehicleRangeOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.VehicleRangeOID;
                sqlParam[1] = new SqlParameter("@Enable", SqlDbType.Bit);
                sqlParam[1].Value = this.Enable;
                sqlParam[2] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[2].Value = this.FileName != null ? this.FileName : "";
                sqlParam[3] = new SqlParameter("@VehicleRangeURL", SqlDbType.NVarChar);
                sqlParam[3].Value = this.VehicleRangeURL != null ? this.VehicleRangeURL : "";

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
                string sSQL = "UPDATE VehicleRange SET Enable=@Enable,FileName=@FileName,VehicleRangeURL=@VehicleRangeURL WHERE VehicleRangeOID=@VehicleRangeOID";
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter("@VehicleRangeOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.VehicleRangeOID;
                sqlParam[1] = new SqlParameter("@Enable", SqlDbType.Bit);
                sqlParam[1].Value = this.Enable;
                sqlParam[2] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[2].Value = this.FileName != null ? this.FileName : "";
                sqlParam[3] = new SqlParameter("@VehicleRangeURL", SqlDbType.NVarChar);
                sqlParam[3].Value = this.VehicleRangeURL != null ? this.VehicleRangeURL : "";

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
