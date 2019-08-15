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
    public partial class Budget_Country
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid CountryOID { get; set; }
        public string FileName { get; set; }
        public Budget_Country()
        {
        }
        public Budget_Country(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_Country> SelectAll()
        {
            List<Budget_Country> countryList = null;
            try
            {
                string sSQL = "SELECT * FROM Country";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    countryList = new List<Budget_Country>();
                    Budget_Country country = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        country = new Budget_Country();
                        country.CountryOID = new Guid(ds.Tables[0].Rows[i]["CountryOID"].ToString());
                        country.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();

                        countryList.Add(country);
                    }
                }
            }
            catch (Exception ex) { }
            return countryList;
        }
        //public List<Budget_Zone> SelectByCode(string Code)
        //{
        //    List<Budget_Zone> allZone = null;
        //    try
        //    {
        //        string sSQL = "SELECT * FROM Zone WHERE ZoneCode=@ZoneCode";
        //        SqlParameter[] sqlParam = new SqlParameter[1];
        //        sqlParam[0] = new SqlParameter("@ZoneCode", SqlDbType.NVarChar);
        //        sqlParam[0].Value = Code;
        //        DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            allZone = new List<Budget_Zone>();
        //            DataRow oneRow;
        //            Budget_Zone zone = new Budget_Zone();
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                oneRow = ds.Tables[0].Rows[i];
        //                zone = new Budget_Zone();
        //                zone.ZoneCode = oneRow["ZoneCode"].ToString();
        //                zone.Culture = oneRow["Culture"].ToString();
        //                zone.ZoneName = oneRow["ZoneName"].ToString();
        //                zone.FileName = oneRow["FileName"].ToString();
        //                allZone.Add(zone);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return allZone;
        //}
        //public List<Budget_Zone> SelectByCulture(string Culture)
        //{
        //    List<Budget_Zone> allZone = null;
        //    try
        //    {
        //        string sSQL = "SELECT * FROM Zone WHERE Culture=@Culture";
        //        SqlParameter[] sqlParam = new SqlParameter[1];
        //        sqlParam[0] = new SqlParameter("@Culture", SqlDbType.NVarChar);
        //        sqlParam[0].Value = Culture;
        //        DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataRow oneRow;
        //            allZone = new List<Budget_Zone>();
        //            Budget_Zone zone = new Budget_Zone();
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                oneRow = ds.Tables[0].Rows[i];
        //                zone = new Budget_Zone();
        //                zone.ZoneCode = oneRow["ZoneCode"].ToString();
        //                zone.Culture = oneRow["Culture"].ToString();
        //                zone.ZoneName = oneRow["ZoneName"].ToString();
        //                zone.FileName = oneRow["FileName"].ToString();
        //                allZone.Add(zone);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return allZone;
        //}

       
        public Budget_Country SelectByOID(Guid guidCountryOID)
        {
            Budget_Country country = null;
            try
            {
                string sSQL = "SELECT * FROM Country WHERE CountryOID=@CountryOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@CountryOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidCountryOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    country = new Budget_Country();
                    country.CountryOID = new Guid(ds.Tables[0].Rows[0]["CountryOID"].ToString());
                    country.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                }
            }
            catch (Exception ex) { }
            return country;
        }
        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO Country 
                            (CountryOID,FileName) 
                            VALUES 
                            (@CountryOID,@FileName)";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@CountryOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.CountryOID;
                sqlParam[1] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[1].Value = this.FileName != null ? this.FileName : SqlString.Null;
                int iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
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
                string sSQL = @"UPDATE Country SET 
                    FileName=@FileName WHERE CountryOID=@CountryOID";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@CountryOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.CountryOID;
                sqlParam[1] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[1].Value = this.FileName != null ? this.FileName : SqlString.Null;
                int iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
