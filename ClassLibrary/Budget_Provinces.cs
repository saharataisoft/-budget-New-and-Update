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
    public partial class Budget_Provinces
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public string ProvinceCode { get; set; }
        public string Culture { get; set; }
        public string ProvinceName { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public Budget_Provinces()
        {
        }
        public Budget_Provinces(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_Provinces> SelectAll()
        {
            List<Budget_Provinces> ProvincesList = null;
            try
            {
                string sSQL = "SELECT * FROM Provinces";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ProvincesList = new List<Budget_Provinces>();
                    Budget_Provinces province = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        province = new Budget_Provinces();
                        province.ProvinceCode = ds.Tables[0].Rows[i]["ProvinceCode"].ToString();
                        province.Culture = ds.Tables[0].Rows[i]["Culture"].ToString();
                        province.ProvinceName = ds.Tables[0].Rows[i]["ProvinceName"].ToString();
                        province.ZoneCode = ds.Tables[0].Rows[i]["ZoneCode"].ToString();
                        province.ZoneName = getZoneName(province.ZoneCode, province.Culture);
                        ProvincesList.Add(province);
                    }
                }
            }
            catch (Exception ex) { }
            return ProvincesList;
        }

        private string getZoneName(string ZoneCode,string Culture)
        {
            Budget_Zone zone = new Budget_Zone();
            List< Budget_Zone> zoneList = zone.SelectByCode(ZoneCode);
            zone = zoneList.Find(x => x.Culture == Culture);
            return zone.ZoneName;
        }
        public List<Budget_Provinces> SelectByCode(string Code)
        {
            List<Budget_Provinces> allProvince = null;
            try
            {
                string sSQL = "SELECT * FROM Provinces WHERE ProvinceCode=@ProvinceCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ProvinceCode", SqlDbType.NVarChar);
                sqlParam[0].Value = Code;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    allProvince = new List<Budget_Provinces>();
                    DataRow oneRow;
                    Budget_Provinces province = new Budget_Provinces();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        province = new Budget_Provinces();
                        province.ProvinceCode = oneRow["ProvinceCode"].ToString();
                        province.Culture = oneRow["Culture"].ToString();
                        province.ProvinceName = oneRow["ProvinceName"].ToString();
                        province.ZoneCode = oneRow["ZoneCode"].ToString();
                        province.ZoneName = getZoneName(province.ZoneCode, province.Culture);
                        allProvince.Add(province);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return allProvince;
        }
        public List<Budget_Provinces> SelectByCulture(string Culture)
        {
            List<Budget_Provinces> allProvinces = null;
            try
            {
                string sSQL = "SELECT * FROM Provinces WHERE Culture=@Culture";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                sqlParam[0].Value = Culture;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oneRow;
                    allProvinces = new List<Budget_Provinces>();
                    Budget_Provinces province = new Budget_Provinces();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        province = new Budget_Provinces();
                        province.ProvinceCode = oneRow["ProvinceCode"].ToString();
                        province.Culture = oneRow["Culture"].ToString();
                        province.ProvinceName = oneRow["ProvinceName"].ToString();
                        province.ZoneCode = oneRow["ZoneCode"].ToString();
                        province.ZoneName = getZoneName(province.ZoneCode, province.Culture);
                        allProvinces.Add(province);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return allProvinces;
        }
        public List<Budget_Provinces> SelectByZone(string ZoneCode)
        {
            List<Budget_Provinces> allProvincesInZone = null;
            try
            {
                string sSQL = "SELECT * FROM Provinces WHERE ZoneCode=@ZoneCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ZoneCode", SqlDbType.NVarChar);
                sqlParam[0].Value = ZoneCode;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    allProvincesInZone = new List<Budget_Provinces>();
                    DataRow oneRow;
                    Budget_Provinces province = new Budget_Provinces();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        oneRow = ds.Tables[0].Rows[i];
                        province = new Budget_Provinces();
                        province.ProvinceCode = oneRow["ProvinceCode"].ToString();
                        province.Culture = oneRow["Culture"].ToString();
                        province.ProvinceName = oneRow["ProvinceName"].ToString();
                        province.ZoneCode = oneRow["ZoneCode"].ToString();
                        province.ZoneName = getZoneName(province.ZoneCode, province.Culture);
                        allProvincesInZone.Add(province);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return allProvincesInZone;
        }
        public bool add(List<Budget_Provinces> listInsert)
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
                    sSQL = "SELECT Count(*) FROM Provinces WHERE ProvinceCode=@ProvinceCode and Culture=@Culture";
                    sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter("@ProvinceCode", SqlDbType.NVarChar);
                    sqlParam[0].Value = listInsert[i].ProvinceCode;
                    sqlParam[1] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                    sqlParam[1].Value = listInsert[i].Culture;
                    int iRec = (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sSQL, sqlParam);
                    if (iRec == 0)
                    {
                        sSQL = @"INSERT INTO Provinces (ProvinceCode,Culture,ProvinceName,ZoneCode)
                            VALUES
                            (@ProvinceCode,@Culture,@ProvinceName,@ZoneCode)";
                    }
                    else
                    {
                        sSQL = @"UPDATE Provinces SET ZoneName=@ZoneName
                           WHERE ProvinceCode=@ProvinceCode AND Culture=@Culture";
                    }
                    sqlParam = new SqlParameter[4];
                    sqlParam[0] = new SqlParameter("@ProvinceCode", listInsert[i].ProvinceCode);
                    sqlParam[1] = new SqlParameter("@Culture", listInsert[i].Culture);
                    sqlParam[2] = new SqlParameter("@ProvinceName", listInsert[i].ProvinceName);
                    sqlParam[3] = new SqlParameter("@ZoneCode", listInsert[i].ZoneCode);
                    iRec = SqlHelper.ExecuteNonQuery(
                           ConnectionString,
                           CommandType.Text, sSQL, sqlParam);
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool update(List<Budget_Provinces> listUpdate)
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
                    sSQL = @"UPDATE Provinces SET ProvinceName=@ProvinceName
                           WHERE ProvinceCode=@ProvinceCode AND Culture=@Culture";
                    sqlParam = new SqlParameter[3];
                    sqlParam[0] = new SqlParameter("@ProvinceCode", listUpdate[i].ProvinceCode);
                    sqlParam[1] = new SqlParameter("@Culture", listUpdate[i].Culture);
                    sqlParam[2] = new SqlParameter("@ProvinceName", listUpdate[i].ProvinceName);
                    SqlHelper.ExecuteNonQuery(
                           ConnectionString,
                           CommandType.Text, sSQL, sqlParam);
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool delete(string ProvinceCode)
        {
            bool bResult = false;

            try
            {
                string sSql = @"DELETE FROM Provinces Where ProvinceCode=@ProvinceCode;";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ProvinceCode", ProvinceCode);

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
