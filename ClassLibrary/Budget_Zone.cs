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
    public partial class Budget_Zone
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public string ZoneCode { get; set; }
        public string Culture { get; set; }
        public string ZoneName { get; set; }
        public string FileName { get; set; }
        public Budget_Zone()
        {
        }
        public Budget_Zone(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_Zone> SelectAll()
        {
            List<Budget_Zone> ZoneList = null;
            try
            {
                string sSQL = "SELECT * FROM Zone";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ZoneList = new List<Budget_Zone>();
                    Budget_Zone zone = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        zone = new Budget_Zone();
                        zone.ZoneCode = ds.Tables[0].Rows[i]["ZoneCode"].ToString();
                        zone.Culture = ds.Tables[0].Rows[i]["Culture"].ToString();
                        zone.ZoneName = ds.Tables[0].Rows[i]["ZoneName"].ToString();
                        zone.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();

                        ZoneList.Add(zone);
                    }
                }
            }
            catch (Exception ex) { }
            return ZoneList;
        }
        public List<Budget_Zone> SelectByCode(string Code)
        {
            List<Budget_Zone> allZone = null;
            try
            {
                string sSQL = "SELECT * FROM Zone WHERE ZoneCode=@ZoneCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ZoneCode", SqlDbType.NVarChar);
                sqlParam[0].Value = Code;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    allZone = new List<Budget_Zone>();
                    DataRow oneRow;
                    Budget_Zone zone = new Budget_Zone();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        zone = new Budget_Zone();
                        zone.ZoneCode = oneRow["ZoneCode"].ToString();
                        zone.Culture = oneRow["Culture"].ToString();
                        zone.ZoneName = oneRow["ZoneName"].ToString();
                        zone.FileName = oneRow["FileName"].ToString();
                        allZone.Add(zone);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return allZone;
        }
        public List<Budget_Zone> SelectByCulture(string Culture)
        {
            List<Budget_Zone> allZone = null;
            try
            {
                string sSQL = "SELECT * FROM Zone WHERE Culture=@Culture";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                sqlParam[0].Value = Culture;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oneRow;
                    allZone = new List<Budget_Zone>();
                    Budget_Zone zone = new Budget_Zone();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        zone = new Budget_Zone();
                        zone.ZoneCode = oneRow["ZoneCode"].ToString();
                        zone.Culture = oneRow["Culture"].ToString();
                        zone.ZoneName = oneRow["ZoneName"].ToString();
                        zone.FileName = oneRow["FileName"].ToString();
                        allZone.Add(zone);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return allZone;
        }
        public bool add(List<Budget_Zone> listInsert)
        {
            bool bResult = false;
            if (listInsert == null || (listInsert != null && listInsert.Count == 0))
                return bResult;
            try
            {
                SqlParameter[] sqlParam;
                string sSQL = "";
                for (int i = 0; i < listInsert.Count; i++)
                {
                    sSQL = "SELECT Count(*) FROM Zone WHERE ZoneCode=@ZoneCode and Culture=@Culture";
                    sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@ZoneCode", SqlDbType.NVarChar);
                    sqlParam[0].Value = listInsert[i].ZoneCode;
                    sqlParam[1] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                    sqlParam[1].Value = listInsert[i].Culture;
                    int iRec = (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sSQL, sqlParam);
                    if (iRec == 0)
                    {
                        sSQL = @"INSERT INTO Zone (ZoneCode,Culture,ZoneName,FileName)
                            VALUES
                            (@ZoneCode,@Culture,@ZoneName,@FileName)";
                    }
                    else
                    {
                        sSQL = @"UPDATE Zone SET ZoneName=@ZoneName,FileName=@FileName
                           WHERE ZoneCode=@ZoneCode AND Culture=@Culture";
                    }
                    sqlParam = new SqlParameter[4];
                    sqlParam[0] = new SqlParameter("@ZoneCode", listInsert[i].ZoneCode);
                    sqlParam[1] = new SqlParameter("@Culture", listInsert[i].Culture);
                    sqlParam[2] = new SqlParameter("@ZoneName", listInsert[i].ZoneName);
                    sqlParam[3] = new SqlParameter("@FileName", listInsert[i].FileName);
                    //sqlParam[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                    //sqlParam[3].Value = this.FileName != null ? this.FileName : "";
                    iRec = SqlHelper.ExecuteNonQuery(
                           ConnectionString,
                           CommandType.Text, sSQL, sqlParam);
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool update(List<Budget_Zone> listUpdate)
        {
            bool bResult = false;
            if (listUpdate == null || (listUpdate != null && listUpdate.Count == 0))
                return bResult;
            try
            {
                string sSQL = "";
                SqlParameter[] sqlParam;
                for (int i = 0; i < listUpdate.Count; i++)
                {
                    sSQL = @"UPDATE Zone SET ZoneName=@ZoneName,FileName=@FileName
                           WHERE ZoneCode=@ZoneCode AND Culture=@Culture";
                    sqlParam = new SqlParameter[4];
                    sqlParam[0] = new SqlParameter("@ZoneCode", listUpdate[i].ZoneCode);
                    sqlParam[1] = new SqlParameter("@Culture", listUpdate[i].Culture);
                    sqlParam[2] = new SqlParameter("@ZoneName", listUpdate[i].ZoneName);
                    sqlParam[3] = new SqlParameter("@FileName", listUpdate[i].FileName);
                    //sqlParam[3] = new SqlParameter("@FileName", listUpdate[1].FileName);
                    //sqlParam[3] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                    //sqlParam[3].Value = this.FileName != null ? this.FileName : "";
                    SqlHelper.ExecuteNonQuery(
                           ConnectionString,
                           CommandType.Text, sSQL, sqlParam);
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool delete(string ZoneCode)
        {
            bool bResult = false;

            try
            {
                string sSql = @"DELETE FROM Zone Where ZoneCode=@ZoneCode;";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ZoneCode", ZoneCode);

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
