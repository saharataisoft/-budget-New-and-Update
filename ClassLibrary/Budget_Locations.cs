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
    public partial class Budget_Locations
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid LocationOID { get; set; }
        public string LocationCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ProvinceCode { get; set; }
        public string mfOpen { get; set; }
        public string mfClose { get; set; }
        public string satOpen { get; set; }
        public string satClose { get; set; }
        public string sunOpen { get; set; }
        public string sunClose { get; set; }
        public string FileName { get; set; }
        public List<Detais> LocationDetais { get; set; }
        public Budget_Locations()
        {
        }
        public Budget_Locations(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_Locations> SelectAll()
        {
            List<Budget_Locations> LocationsList = null;
            try
            {
                string sSQL = "SELECT * FROM Locations";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    LocationsList = new List<Budget_Locations>();
                    Budget_Locations location = null;
                    List<Detais> detailList = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        location = new Budget_Locations();
                        location.LocationOID = new Guid(ds.Tables[0].Rows[i]["LocationOID"].ToString());
                        location.LocationCode = ds.Tables[0].Rows[i]["LocationCode"].ToString();
                        location.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                        location.Fax = ds.Tables[0].Rows[i]["Fax"].ToString();
                        location.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        location.Latitude = ds.Tables[0].Rows[i]["Latitude"].ToString();
                        location.Longitude = ds.Tables[0].Rows[i]["Longitude"].ToString();
                        location.ProvinceCode = ds.Tables[0].Rows[i]["ProvinceCode"].ToString();
                        location.mfOpen = ds.Tables[0].Rows[i]["mfOpen"].ToString();
                        location.mfClose = ds.Tables[0].Rows[i]["mfClose"].ToString();
                        location.satOpen = ds.Tables[0].Rows[i]["satOpen"].ToString();
                        location.satClose = ds.Tables[0].Rows[i]["satClose"].ToString();
                        location.sunOpen = ds.Tables[0].Rows[i]["sunOpen"].ToString();
                        location.sunClose = ds.Tables[0].Rows[i]["sunClose"].ToString();
                        location.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        detailList = getDetails(location.LocationOID);
                        if (detailList != null)
                        {
                            location.LocationDetais = new List<Detais>();
                            location.LocationDetais = detailList;
                        }


                        LocationsList.Add(location);
                    }
                }
            }
            catch (Exception ex) { }
            return LocationsList;
        }
        public List<Budget_Locations> SelectByCulture(string Culture)
        {
            List<Budget_Locations> locationList = null;
            try
            {
                string sSQL = "SELECT * FROM Locations";
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    locationList = new List<Budget_Locations>();
                    List<Detais> detailList = null;
                    Budget_Locations location = new Budget_Locations();
                    Detais detail = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        location = new Budget_Locations();
                        location.LocationOID = new Guid(ds.Tables[0].Rows[i]["LocationOID"].ToString());
                        location.LocationCode = ds.Tables[0].Rows[i]["LocationCode"].ToString();
                        location.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                        location.Fax = ds.Tables[0].Rows[i]["Fax"].ToString();
                        location.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        location.Latitude = ds.Tables[0].Rows[i]["Latitude"].ToString();
                        location.Longitude = ds.Tables[0].Rows[i]["Longitude"].ToString();
                        location.ProvinceCode = ds.Tables[0].Rows[i]["ProvinceCode"].ToString();
                        location.mfOpen = ds.Tables[0].Rows[i]["mfOpen"].ToString();
                        location.mfClose = ds.Tables[0].Rows[i]["mfClose"].ToString();
                        location.satOpen = ds.Tables[0].Rows[i]["satOpen"].ToString();
                        location.satClose = ds.Tables[0].Rows[i]["satClose"].ToString();
                        location.sunOpen = ds.Tables[0].Rows[i]["sunOpen"].ToString();
                        location.sunClose = ds.Tables[0].Rows[i]["sunClose"].ToString();
                        location.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        detail = getDetails(location.LocationOID).Find(x => x.Culture == Culture);
                        if (detail != null)
                        {
                            location.LocationDetais = new List<Detais>();
                            detailList = new List<Detais>();
                            detailList.Add(detail);
                            location.LocationDetais = detailList;
                        }
                        locationList.Add(location);
                    }
                }
            }
            catch (Exception ex) { }
            return locationList;
        }
        public List<Budget_Locations> SelectByProvince(string ProvinceCode)
        {
            List<Budget_Locations> locationList = null;
            try
            {
                string sSQL = "SELECT * FROM Locations WHERE ProvinceCode=@ProvinceCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@ProvinceCode", SqlDbType.NVarChar);
                sqlParam[0].Value = ProvinceCode;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    locationList = new List<Budget_Locations>();
                    List<Detais> detailList = null;
                    Budget_Locations location = new Budget_Locations();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        location = new Budget_Locations();
                        location.LocationOID = new Guid(ds.Tables[0].Rows[i]["LocationOID"].ToString());
                        location.LocationCode = ds.Tables[0].Rows[i]["LocationCode"].ToString();
                        location.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                        location.Fax = ds.Tables[0].Rows[i]["Fax"].ToString();
                        location.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        location.Latitude = ds.Tables[0].Rows[i]["Latitude"].ToString();
                        location.Longitude = ds.Tables[0].Rows[i]["Longitude"].ToString();
                        location.ProvinceCode = ds.Tables[0].Rows[i]["ProvinceCode"].ToString();
                        location.mfOpen = ds.Tables[0].Rows[i]["mfOpen"].ToString();
                        location.mfClose = ds.Tables[0].Rows[i]["mfClose"].ToString();
                        location.satOpen = ds.Tables[0].Rows[i]["satOpen"].ToString();
                        location.satClose = ds.Tables[0].Rows[i]["satClose"].ToString();
                        location.sunOpen = ds.Tables[0].Rows[i]["sunOpen"].ToString();
                        location.sunClose = ds.Tables[0].Rows[i]["sunClose"].ToString();
                        location.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        detailList = getDetails(location.LocationOID);
                        if (detailList != null)
                        {
                            location.LocationDetais = detailList;
                        }
                        locationList.Add(location);
                    }
                }
            }
            catch (Exception ex) { }
            return locationList;
        }
        private List<Detais> getDetails(Guid guidLocationOID)
        {
            List<Detais> DetaisList = null;
            try
            {
                string sSQL = "SELECT * FROM LocationDetails WHERE LocationOID=@LocationOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@LocationOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidLocationOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DetaisList = new List<Detais>();
                    Detais detail = new Detais();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        detail = new Detais();
                        detail.Culture = ds.Tables[0].Rows[i]["Culture"].ToString();
                        detail.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                        detail.LocationName = ds.Tables[0].Rows[i]["LocationName"].ToString();
                        detail.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                        DetaisList.Add(detail);
                    }
                }
            }
            catch (Exception ex) { }
            return DetaisList;
        }
        public Budget_Locations SelectByOID(Guid guidLocationOID)
        {
            Budget_Locations location = null;
            try
            {
                string sSQL = "SELECT * FROM Locations WHERE LocationOID=@LocationOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@LocationOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidLocationOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Detais> detailList = null;
                    location = new Budget_Locations();
                    location.LocationOID = new Guid(ds.Tables[0].Rows[0]["LocationOID"].ToString());
                    location.LocationCode = ds.Tables[0].Rows[0]["LocationCode"].ToString();
                    location.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    location.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                    location.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    location.Latitude = ds.Tables[0].Rows[0]["Latitude"].ToString();
                    location.Longitude = ds.Tables[0].Rows[0]["Longitude"].ToString();
                    location.ProvinceCode = ds.Tables[0].Rows[0]["ProvinceCode"].ToString();
                    location.mfOpen = ds.Tables[0].Rows[0]["mfOpen"].ToString();
                    location.mfClose = ds.Tables[0].Rows[0]["mfClose"].ToString();
                    location.satOpen = ds.Tables[0].Rows[0]["satOpen"].ToString();
                    location.satClose = ds.Tables[0].Rows[0]["satClose"].ToString();
                    location.sunOpen = ds.Tables[0].Rows[0]["sunOpen"].ToString();
                    location.sunClose = ds.Tables[0].Rows[0]["sunClose"].ToString();
                    location.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    detailList = getDetails(location.LocationOID);
                    if (detailList != null)
                    {
                        location.LocationDetais = new List<Detais>();
                        location.LocationDetais = detailList;
                    }
                }
            }
            catch (Exception ex) { }
            return location;
        }
                
        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO Locations (LocationOID,LocationCode,Phone,Fax,Email,Latitude,Longitude,ProvinceCode,mfOpen,mfClose,satOpen,satClose,sunOpen,sunClose,FileName)
                                VALUES
                                (@LocationOID,@LocationCode,@Phone,@Fax,@Email,@Latitude,@Longitude,@ProvinceCode,@mfOpen,@mfClose,@satOpen,@satClose,@sunOpen,@sunClose,@FileName)";
                SqlParameter[] sqlParam = new SqlParameter[15];
                sqlParam[0] = new SqlParameter("@LocationOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.LocationOID;
                sqlParam[1] = new SqlParameter("@LocationCode", SqlDbType.NVarChar);
                sqlParam[1].Value = this.LocationCode;
                sqlParam[2] = new SqlParameter("@Phone", SqlDbType.NVarChar);
                sqlParam[2].Value = this.Phone;
                sqlParam[3] = new SqlParameter("@Fax", SqlDbType.NVarChar);
                sqlParam[3].Value = this.Fax;
                sqlParam[4] = new SqlParameter("@Email", SqlDbType.NVarChar);
                sqlParam[4].Value = this.Email;
                sqlParam[5] = new SqlParameter("@Latitude", SqlDbType.NVarChar);
                sqlParam[5].Value = this.Latitude;
                sqlParam[6] = new SqlParameter("@Longitude", SqlDbType.NVarChar);
                sqlParam[6].Value = this.Longitude;
                sqlParam[7] = new SqlParameter("@ProvinceCode", SqlDbType.NVarChar);
                sqlParam[7].Value = this.ProvinceCode;
                sqlParam[8] = new SqlParameter("@mfOpen", SqlDbType.NVarChar);
                sqlParam[8].Value = this.mfOpen;
                sqlParam[9] = new SqlParameter("@mfClose", SqlDbType.NVarChar);
                sqlParam[9].Value = this.mfClose;
                sqlParam[10] = new SqlParameter("@satOpen", SqlDbType.NVarChar);
                sqlParam[10].Value = this.satOpen;
                sqlParam[11] = new SqlParameter("@satClose", SqlDbType.NVarChar);
                sqlParam[11].Value = this.satClose;
                sqlParam[12] = new SqlParameter("@sunOpen", SqlDbType.NVarChar);
                sqlParam[12].Value = this.sunOpen;
                sqlParam[13] = new SqlParameter("@sunClose", SqlDbType.NVarChar);
                sqlParam[13].Value = this.sunClose;
                sqlParam[14] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[14].Value = this.FileName;

                int iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (iRec > 0)
                {
                    sSQL = @"INSERT INTO LocationDetails (LocationOID,Culture,LocationName,Address,Description) VALUES (@LocationOID,@Culture,@LocationName,@Address,@Description)";
                    foreach(Detais detais in this.LocationDetais)
                    {
                        sqlParam = new SqlParameter[5];
                        sqlParam[0] = new SqlParameter("@LocationOID", SqlDbType.UniqueIdentifier);
                        sqlParam[0].Value = this.LocationOID;
                        sqlParam[1] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                        sqlParam[1].Value = detais.Culture;
                        sqlParam[2] = new SqlParameter("@LocationName", SqlDbType.NVarChar);
                        sqlParam[2].Value = detais.LocationName;
                        sqlParam[3] = new SqlParameter("@Address", SqlDbType.NVarChar);
                        sqlParam[3].Value = detais.Address;
                        sqlParam[4] = new SqlParameter("@Description", SqlDbType.NVarChar);
                        sqlParam[4].Value = detais.Description;
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
                string sSQL = @"UPDATE Locations SET Phone=@Phone,Fax=@Fax,Email=@Email,Latitude=@Latitude,Longitude=@Longitude,ProvinceCode=@ProvinceCode
                                ,mfOpen=@mfOpen,mfClose=@mfClose,satOpen=@satOpen,satClose=@satClose,sunOpen=@sunOpen,sunClose=@sunClose,LocationCode=@LocationCode,FileName=@FileName
                                WHERE LocationOID=@LocationOID";
                SqlParameter[] sqlParam = new SqlParameter[15];
                sqlParam[0] = new SqlParameter("@LocationOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.LocationOID;
                sqlParam[1] = new SqlParameter("@Phone", SqlDbType.NVarChar);
                sqlParam[1].Value = this.Phone;
                sqlParam[2] = new SqlParameter("@Fax", SqlDbType.NVarChar);
                sqlParam[2].Value = this.Fax;
                sqlParam[3] = new SqlParameter("@Email", SqlDbType.NVarChar);
                sqlParam[3].Value = this.Email;
                sqlParam[4] = new SqlParameter("@Latitude", SqlDbType.NVarChar);
                sqlParam[4].Value = this.Latitude;
                sqlParam[5] = new SqlParameter("@Longitude", SqlDbType.NVarChar);
                sqlParam[5].Value = this.Longitude;
                sqlParam[6] = new SqlParameter("@ProvinceCode", SqlDbType.NVarChar);
                sqlParam[6].Value = this.ProvinceCode;
                sqlParam[7] = new SqlParameter("@mfOpen", SqlDbType.NVarChar);
                sqlParam[7].Value = this.mfOpen;
                sqlParam[8] = new SqlParameter("@mfClose", SqlDbType.NVarChar);
                sqlParam[8].Value = this.mfClose;
                sqlParam[9] = new SqlParameter("@satOpen", SqlDbType.NVarChar);
                sqlParam[9].Value = this.satOpen;
                sqlParam[10] = new SqlParameter("@satClose", SqlDbType.NVarChar);
                sqlParam[10].Value = this.satClose;
                sqlParam[11] = new SqlParameter("@sunOpen", SqlDbType.NVarChar);
                sqlParam[11].Value = this.sunOpen;
                sqlParam[12] = new SqlParameter("@sunClose", SqlDbType.NVarChar);
                sqlParam[12].Value = this.sunClose;
                sqlParam[13] = new SqlParameter("@LocationCode", SqlDbType.NVarChar);
                sqlParam[13].Value = this.LocationCode;
                sqlParam[14] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[14].Value = this.FileName;
                int iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (iRec > 0)
                {
                    sSQL = @"UPDATE LocationDetails SET LocationName=@LocationName,Address=@Address,Description=@Description WHERE  LocationOID=@LocationOID AND Culture=@Culture";
                    foreach (Detais detais in this.LocationDetais)
                    {
                        sqlParam = new SqlParameter[5];
                        sqlParam[0] = new SqlParameter("@LocationOID", SqlDbType.UniqueIdentifier);
                        sqlParam[0].Value = this.LocationOID;
                        sqlParam[1] = new SqlParameter("@Culture", SqlDbType.NVarChar);
                        sqlParam[1].Value = detais.Culture;
                        sqlParam[2] = new SqlParameter("@LocationName", SqlDbType.NVarChar);
                        sqlParam[2].Value = detais.LocationName;
                        sqlParam[3] = new SqlParameter("@Address", SqlDbType.NVarChar);
                        sqlParam[3].Value = detais.Address;
                        sqlParam[4] = new SqlParameter("@Description", SqlDbType.NVarChar);
                        sqlParam[4].Value = detais.Description;
                        iRec = (int)SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                    }
                }
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
        public bool delete(Guid _OID)
        {
            bool bResult = false;

            try
            {
                string sSql = @"DELETE FROM Locations Where LocationOID=@LocationOID;
                                DELETE FROM LocationDetails Where LocationOID=@LocationOID;";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@LocationOID", _OID);

                SqlHelper.ExecuteNonQuery(
                       ConnectionString,
                       CommandType.Text, sSql, param);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
    public class Detais
    {
        public string Culture { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
