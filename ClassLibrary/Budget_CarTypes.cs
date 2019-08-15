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
    public partial class Budget_CarTypes
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid CarTypeOID { get; set; }
        public string VehiclesClassCode { get; set; }
        public string VehiclesCategoryCode { get; set; }
        public string GroupCode { get; set; }
        public string SIPPCode { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public int NoOfPax { get; set; }
        public int NoOfLargeBag { get; set; }
        public int NoOfMediumBag { get; set; }
        public int NoOfSmallBag { get; set; }
        public int Doors { get; set; }
        public bool isSedan { get; set; }
        public float Litre { get; set; }
        public int Cylindar { get; set; }
        public int AirBag { get; set; }
        public bool isAutomatic { get; set; }
        public bool isAirCondition { get; set; }
        public bool isPowerSteering { get; set; }
        public bool isABS { get; set; }
        public bool isCDPlayer { get; set; }
        public bool isReversingCamera { get; set; }
        public bool isBluetoothAUXandUSB { get; set; }        
        public string Others { get; set; }
        public Budget_CarTypesPeriod period { get; set; }

        public Budget_CarTypes()
        {
        }
        public Budget_CarTypes(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_CarTypes> SelectAll()
        {
            List<Budget_CarTypes> CarTypeList = null;
            try
            {
                string sSQL = "SELECT * FROM CarTypes";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CarTypeList = new List<Budget_CarTypes>();
                    Budget_CarTypes carType = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        carType = new Budget_CarTypes();
                        carType.CarTypeOID = new Guid(ds.Tables[0].Rows[i]["CarTypeOID"].ToString());
                        carType.VehiclesClassCode = ds.Tables[0].Rows[i]["VehiclesClassCode"].ToString();
                        carType.VehiclesCategoryCode = ds.Tables[0].Rows[i]["VehiclesCategoryCode"].ToString();
                        carType.GroupCode = ds.Tables[0].Rows[i]["GroupCode"].ToString();
                        carType.SIPPCode = ds.Tables[0].Rows[i]["SIPPCode"].ToString();
                        carType.Type = ds.Tables[0].Rows[i]["Type"].ToString();
                        carType.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                        carType.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                        carType.NoOfPax = int.Parse(ds.Tables[0].Rows[i]["NoOfPax"].ToString());
                        carType.NoOfLargeBag = int.Parse(ds.Tables[0].Rows[i]["NoOfLargeBag"].ToString());
                        carType.NoOfMediumBag = int.Parse(ds.Tables[0].Rows[i]["NoOfMediumBag"].ToString());
                        carType.NoOfSmallBag = int.Parse(ds.Tables[0].Rows[i]["NoOfSmallBag"].ToString());
                        carType.Doors = int.Parse(ds.Tables[0].Rows[i]["Doors"].ToString());
                        carType.isSedan = Convert.ToBoolean(ds.Tables[0].Rows[i]["isSedan"].ToString());
                        carType.Litre = float.Parse(ds.Tables[0].Rows[i]["Litre"].ToString());
                        carType.Cylindar = int.Parse(ds.Tables[0].Rows[i]["Cylindar"].ToString());
                        carType.AirBag = int.Parse(ds.Tables[0].Rows[i]["AirBag"].ToString());
                        carType.isAutomatic = Convert.ToBoolean(ds.Tables[0].Rows[i]["isAutomatic"].ToString());
                        carType.isAirCondition = Convert.ToBoolean(ds.Tables[0].Rows[i]["isAirCondition"].ToString());
                        carType.isPowerSteering = Convert.ToBoolean(ds.Tables[0].Rows[i]["isPowerSteering"].ToString());
                        carType.isABS = Convert.ToBoolean(ds.Tables[0].Rows[i]["isABS"].ToString());
                        carType.isCDPlayer = Convert.ToBoolean(ds.Tables[0].Rows[i]["isCDPlayer"].ToString());
                        carType.isReversingCamera = Convert.ToBoolean(ds.Tables[0].Rows[i]["isReversingCamera"].ToString());
                        carType.isBluetoothAUXandUSB = Convert.ToBoolean(ds.Tables[0].Rows[i]["isBluetoothAUXandUSB"].ToString());
                        carType.Others = ds.Tables[0].Rows[i]["Others"].ToString();
                        CarTypeList.Add(carType);
                    }
                }
            }
            catch (Exception ex) { }
            return CarTypeList;
        }

        public Budget_CarTypes SelectByOID(Guid guidCarTypeOID)
        {
            Budget_CarTypes carType = null;
            try
            {
                string sSQL = "SELECT * FROM CarTypes WHERE CarTypeOID=@CarTypeOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidCarTypeOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    carType = new Budget_CarTypes();
                    carType.CarTypeOID = new Guid(ds.Tables[0].Rows[0]["CarTypeOID"].ToString());
                    carType.VehiclesClassCode = ds.Tables[0].Rows[0]["VehiclesClassCode"].ToString();
                    carType.VehiclesCategoryCode = ds.Tables[0].Rows[0]["VehiclesCategoryCode"].ToString();
                    carType.GroupCode = ds.Tables[0].Rows[0]["GroupCode"].ToString();
                    carType.SIPPCode = ds.Tables[0].Rows[0]["SIPPCode"].ToString();
                    carType.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                    carType.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    carType.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    carType.NoOfPax = int.Parse(ds.Tables[0].Rows[0]["NoOfPax"].ToString());
                    carType.NoOfLargeBag = int.Parse(ds.Tables[0].Rows[0]["NoOfLargeBag"].ToString());
                    carType.NoOfMediumBag = int.Parse(ds.Tables[0].Rows[0]["NoOfMediumBag"].ToString());
                    carType.NoOfSmallBag = int.Parse(ds.Tables[0].Rows[0]["NoOfSmallBag"].ToString());
                    carType.Doors = int.Parse(ds.Tables[0].Rows[0]["Doors"].ToString());
                    carType.isSedan = Convert.ToBoolean(ds.Tables[0].Rows[0]["isSedan"].ToString());
                    carType.Litre = float.Parse(ds.Tables[0].Rows[0]["Litre"].ToString());
                    carType.Cylindar = int.Parse(ds.Tables[0].Rows[0]["Cylindar"].ToString());
                    carType.AirBag = int.Parse(ds.Tables[0].Rows[0]["AirBag"].ToString());
                    carType.isAutomatic = Convert.ToBoolean(ds.Tables[0].Rows[0]["isAutomatic"].ToString());
                    carType.isAirCondition = Convert.ToBoolean(ds.Tables[0].Rows[0]["isAirCondition"].ToString());
                    carType.isPowerSteering = Convert.ToBoolean(ds.Tables[0].Rows[0]["isPowerSteering"].ToString());
                    carType.isABS = Convert.ToBoolean(ds.Tables[0].Rows[0]["isABS"].ToString());
                    carType.isCDPlayer = Convert.ToBoolean(ds.Tables[0].Rows[0]["isCDPlayer"].ToString());
                    carType.isReversingCamera = Convert.ToBoolean(ds.Tables[0].Rows[0]["isReversingCamera"].ToString());
                    carType.isBluetoothAUXandUSB = Convert.ToBoolean(ds.Tables[0].Rows[0]["isBluetoothAUXandUSB"].ToString());
                    carType.Others = ds.Tables[0].Rows[0]["Others"].ToString();
                }
            }
            catch (Exception ex) { }
            return carType;
        }

        public Budget_CarTypes SelectBySIPPCode(string SIPPCode)
        {
            Budget_CarTypes carType = null;
            try
            {
                string sSQL = "SELECT * FROM CarTypes WHERE SIPPCode=@SIPPCode";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@SIPPCode", SqlDbType.NVarChar);
                sqlParam[0].Value = SIPPCode;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    carType = new Budget_CarTypes();
                    carType.CarTypeOID = new Guid(ds.Tables[0].Rows[0]["CarTypeOID"].ToString());
                    carType.VehiclesClassCode = ds.Tables[0].Rows[0]["VehiclesClassCode"].ToString();
                    carType.VehiclesCategoryCode = ds.Tables[0].Rows[0]["VehiclesCategoryCode"].ToString();
                    carType.GroupCode = ds.Tables[0].Rows[0]["GroupCode"].ToString();
                    carType.SIPPCode = ds.Tables[0].Rows[0]["SIPPCode"].ToString();
                    carType.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                    carType.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    carType.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    carType.NoOfPax = int.Parse(ds.Tables[0].Rows[0]["NoOfPax"].ToString());
                    carType.NoOfLargeBag = int.Parse(ds.Tables[0].Rows[0]["NoOfLargeBag"].ToString());
                    carType.NoOfMediumBag = int.Parse(ds.Tables[0].Rows[0]["NoOfMediumBag"].ToString());
                    carType.NoOfSmallBag = int.Parse(ds.Tables[0].Rows[0]["NoOfSmallBag"].ToString());
                    carType.Doors = int.Parse(ds.Tables[0].Rows[0]["Doors"].ToString());
                    carType.isSedan = Convert.ToBoolean(ds.Tables[0].Rows[0]["isSedan"].ToString());
                    carType.Litre = float.Parse(ds.Tables[0].Rows[0]["Litre"].ToString());
                    carType.Cylindar = int.Parse(ds.Tables[0].Rows[0]["Cylindar"].ToString());
                    carType.AirBag = int.Parse(ds.Tables[0].Rows[0]["AirBag"].ToString());
                    carType.isAutomatic = Convert.ToBoolean(ds.Tables[0].Rows[0]["isAutomatic"].ToString());
                    carType.isAirCondition = Convert.ToBoolean(ds.Tables[0].Rows[0]["isAirCondition"].ToString());
                    carType.isPowerSteering = Convert.ToBoolean(ds.Tables[0].Rows[0]["isPowerSteering"].ToString());
                    carType.isABS = Convert.ToBoolean(ds.Tables[0].Rows[0]["isABS"].ToString());
                    carType.isCDPlayer = Convert.ToBoolean(ds.Tables[0].Rows[0]["isCDPlayer"].ToString());
                    carType.isReversingCamera = Convert.ToBoolean(ds.Tables[0].Rows[0]["isReversingCamera"].ToString());
                    carType.isBluetoothAUXandUSB = Convert.ToBoolean(ds.Tables[0].Rows[0]["isBluetoothAUXandUSB"].ToString());
                    carType.Others = ds.Tables[0].Rows[0]["Others"].ToString();
                }
            }
            catch (Exception ex) { }
            return carType;
        }

        public List<Budget_CarTypes> SelectAllByPeriod(DateTime PickupDateTime)
        {
            List<Budget_CarTypes> CarTypeList = null;
            try
            {
                string sSQL = @"SELECT CarTypes.*, CarTypesPeriod.RateOID,CarTypesPeriod.StartPeriod,CarTypesPeriod.FinishPeriod,CarTypesPeriod.RatePerDay
                                FROM CarTypes LEFT JOIN CarTypesPeriod ON CarTypes.CarTypeOID=CarTypesPeriod.CarTypeOID
                                WHERE CONVERT(nvarchar,CarTypesPeriod.StartPeriod,102) <= @PickupDateTime AND CONVERT(nvarchar,CarTypesPeriod.FinishPeriod,102) >= @PickupDateTime";

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@PickupDateTime", SqlDbType.NVarChar);
                sqlParam[0].Value = PickupDateTime.ToString("yyyy.MM.dd");
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CarTypeList = new List<Budget_CarTypes>();
                    Budget_CarTypes carType = null;
                    Budget_CarTypesPeriod period = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["RatePerDay"].Equals(DBNull.Value) == false)
                        {
                            carType = new Budget_CarTypes();
                            carType.CarTypeOID = new Guid(ds.Tables[0].Rows[i]["CarTypeOID"].ToString());
                            carType.VehiclesClassCode = ds.Tables[0].Rows[i]["VehiclesClassCode"].ToString();
                            carType.VehiclesCategoryCode = ds.Tables[0].Rows[i]["VehiclesCategoryCode"].ToString();
                            carType.GroupCode = ds.Tables[0].Rows[i]["GroupCode"].ToString();
                            carType.SIPPCode = ds.Tables[0].Rows[i]["SIPPCode"].ToString();
                            carType.Type = ds.Tables[0].Rows[i]["Type"].ToString();
                            carType.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                            carType.FileName = ds.Tables[0].Rows[i]["FileName"].ToString();
                            carType.NoOfPax = int.Parse(ds.Tables[0].Rows[i]["NoOfPax"].ToString());
                            carType.NoOfLargeBag = int.Parse(ds.Tables[0].Rows[i]["NoOfLargeBag"].ToString());
                            carType.NoOfMediumBag = int.Parse(ds.Tables[0].Rows[i]["NoOfMediumBag"].ToString());
                            carType.NoOfSmallBag = int.Parse(ds.Tables[0].Rows[i]["NoOfSmallBag"].ToString());
                            carType.Doors = int.Parse(ds.Tables[0].Rows[i]["Doors"].ToString());
                            carType.isSedan = Convert.ToBoolean(ds.Tables[0].Rows[i]["isSedan"].ToString());
                            carType.Litre = float.Parse(ds.Tables[0].Rows[i]["Litre"].ToString());
                            carType.Cylindar = int.Parse(ds.Tables[0].Rows[i]["Cylindar"].ToString());
                            carType.AirBag = int.Parse(ds.Tables[0].Rows[i]["AirBag"].ToString());
                            carType.isAutomatic = Convert.ToBoolean(ds.Tables[0].Rows[i]["isAutomatic"].ToString());
                            carType.isAirCondition = Convert.ToBoolean(ds.Tables[0].Rows[i]["isAirCondition"].ToString());
                            carType.isPowerSteering = Convert.ToBoolean(ds.Tables[0].Rows[i]["isPowerSteering"].ToString());
                            carType.isABS = Convert.ToBoolean(ds.Tables[0].Rows[i]["isABS"].ToString());
                            carType.isCDPlayer = Convert.ToBoolean(ds.Tables[0].Rows[i]["isCDPlayer"].ToString());
                            carType.isReversingCamera = Convert.ToBoolean(ds.Tables[0].Rows[i]["isReversingCamera"].ToString());
                            carType.isBluetoothAUXandUSB = Convert.ToBoolean(ds.Tables[0].Rows[i]["isBluetoothAUXandUSB"].ToString());
                            carType.Others = ds.Tables[0].Rows[i]["Others"].ToString();
                            period = new Budget_CarTypesPeriod();
                            period.RateOID = new Guid(ds.Tables[0].Rows[i]["RateOID"].ToString());
                            period.CarTypeOID = new Guid(ds.Tables[0].Rows[i]["CarTypeOID"].ToString());
                            period.StartPeriod = Convert.ToDateTime(ds.Tables[0].Rows[i]["StartPeriod"].ToString());
                            period.FinishPeriod = Convert.ToDateTime(ds.Tables[0].Rows[i]["FinishPeriod"].ToString());
                            period.RatePerDay = ds.Tables[0].Rows[i]["RatePerDay"].Equals(DBNull.Value) == false ? Convert.ToDouble(ds.Tables[0].Rows[i]["RatePerDay"].ToString()) : 0;
                            carType.period = period;
                            CarTypeList.Add(carType);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return CarTypeList;
        }

        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO CarTypes (CarTypeOID,GroupCode,SIPPCode,Type,Name,NoOfPax,NoOfLargeBag,NoOfSmallBag,Doors,Litre,Cylindar,AirBag
                            ,isAutomatic,isAirCondition,isPowerSteering,isABS,isCDPlayer,isReversingCamera,isBluetoothAUXandUSB,Others
                            ,FileName,NoOfMediumBag,VehiclesClassCode,VehiclesCategoryCode,isSedan) 
                            VALUES 
                            (@CarTypeOID,@GroupCode,@SIPPCode,@Type,@Name,@NoOfPax,@NoOfLargeBag,@NoOfSmallBag,@Doors,@Litre,@Cylindar,@AirBag
                            ,@isAutomatic,@isAirCondition,@isPowerSteering,@isABS,@isCDPlayer,@isReversingCamera,@isBluetoothAUXandUSB,@Others
                            ,@FileName,@NoOfMediumBag,@VehiclesClassCode,@VehiclesCategoryCode,@isSedan)";
                SqlParameter[] sqlParam = new SqlParameter[25];
                sqlParam[0] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.CarTypeOID;
                sqlParam[1] = new SqlParameter("@GroupCode", SqlDbType.NVarChar);
                sqlParam[1].Value = this.GroupCode;
                sqlParam[2] = new SqlParameter("@SIPPCode", SqlDbType.NVarChar);
                sqlParam[2].Value = this.SIPPCode;
                sqlParam[3] = new SqlParameter("@Type", SqlDbType.NVarChar);
                sqlParam[3].Value = this.Type != null ? this.Type : SqlString.Null;
                sqlParam[4] = new SqlParameter("@Name", SqlDbType.NVarChar);
                sqlParam[4].Value = this.Name != null ? this.Name : SqlString.Null;

                sqlParam[5] = new SqlParameter("@NoOfPax", SqlDbType.Int);
                sqlParam[5].Value = this.NoOfPax;
                sqlParam[6] = new SqlParameter("@NoOfLargeBag", SqlDbType.Int);
                sqlParam[6].Value = this.NoOfLargeBag;               
                sqlParam[7] = new SqlParameter("@NoOfSmallBag", SqlDbType.Int);
                sqlParam[7].Value = this.NoOfSmallBag;
                sqlParam[8] = new SqlParameter("@Doors", SqlDbType.Int);
                sqlParam[8].Value = this.Doors;
                sqlParam[9] = new SqlParameter("@Litre", SqlDbType.Float);
                sqlParam[9].Value = this.Litre;
                sqlParam[10] = new SqlParameter("@Cylindar", SqlDbType.Int);
                sqlParam[10].Value = this.Cylindar;
                sqlParam[11] = new SqlParameter("@AirBag", SqlDbType.Int);
                sqlParam[11].Value = this.AirBag;

                sqlParam[12] = new SqlParameter("@isAutomatic", SqlDbType.Bit);
                sqlParam[12].Value = this.isAutomatic;
                sqlParam[13] = new SqlParameter("@isAirCondition", SqlDbType.Bit);
                sqlParam[13].Value = this.isAirCondition;
                sqlParam[14] = new SqlParameter("@isPowerSteering", SqlDbType.Bit);
                sqlParam[14].Value = this.isPowerSteering;
                sqlParam[15] = new SqlParameter("@isABS", SqlDbType.Bit);
                sqlParam[15].Value = this.isABS;
                sqlParam[16] = new SqlParameter("@isCDPlayer", SqlDbType.Bit);
                sqlParam[16].Value = this.isCDPlayer;
                sqlParam[17] = new SqlParameter("@isReversingCamera", SqlDbType.Bit);
                sqlParam[17].Value = this.isReversingCamera;
                sqlParam[18] = new SqlParameter("@isBluetoothAUXandUSB", SqlDbType.Bit);
                sqlParam[18].Value = this.isBluetoothAUXandUSB;

                sqlParam[19] = new SqlParameter("@Others", SqlDbType.NVarChar);
                sqlParam[19].Value = this.Others != null ? this.Others : SqlString.Null;
                sqlParam[20] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[20].Value = this.FileName != null ? this.FileName : SqlString.Null;
                sqlParam[21] = new SqlParameter("@NoOfMediumBag", SqlDbType.Int);
                sqlParam[21].Value = this.NoOfMediumBag;
                sqlParam[22] = new SqlParameter("@VehiclesClassCode", SqlDbType.NVarChar);
                sqlParam[22].Value = this.VehiclesClassCode != null ? this.VehiclesClassCode : SqlString.Null;
                sqlParam[23] = new SqlParameter("@VehiclesCategoryCode", SqlDbType.NVarChar);
                sqlParam[23].Value = this.VehiclesCategoryCode != null ? this.VehiclesCategoryCode : SqlString.Null;
                sqlParam[24] = new SqlParameter("@isSedan", SqlDbType.Bit);
                sqlParam[24].Value = this.isSedan;

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
                string sSQL = @"UPDATE CarTypes SET GroupCode=@GroupCode,SIPPCode=@SIPPCode,Type=@Type,Name=@Name,NoOfPax=@NoOfPax,NoOfLargeBag=@NoOfLargeBag,NoOfSmallBag=@NoOfSmallBag,Doors=@Doors,Litre=@Litre,Cylindar=@Cylindar,AirBag=@AirBag
                            ,isAutomatic=@isAutomatic,isAirCondition=@isAirCondition,isPowerSteering=@isPowerSteering,isABS=@isABS,isCDPlayer=@isCDPlayer,isReversingCamera=@isReversingCamera,isBluetoothAUXandUSB=@isBluetoothAUXandUSB,Others=@Others
                            ,FileName=@FileName,NoOfMediumBag=@NoOfMediumBag,VehiclesClassCode=@VehiclesClassCode,VehiclesCategoryCode=@VehiclesCategoryCode,isSedan=@isSedan
                            WHERE CarTypeOID=@CarTypeOID";
                SqlParameter[] sqlParam = new SqlParameter[25];
                sqlParam[0] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.CarTypeOID;
                sqlParam[1] = new SqlParameter("@GroupCode", SqlDbType.NVarChar);
                sqlParam[1].Value = this.GroupCode;
                sqlParam[2] = new SqlParameter("@SIPPCode", SqlDbType.NVarChar);
                sqlParam[2].Value = this.SIPPCode;
                sqlParam[3] = new SqlParameter("@Type", SqlDbType.NVarChar);
                sqlParam[3].Value = this.Type != null ? this.Type : SqlString.Null;
                sqlParam[4] = new SqlParameter("@Name", SqlDbType.NVarChar);
                sqlParam[4].Value = this.Name != null ? this.Name : SqlString.Null;

                sqlParam[5] = new SqlParameter("@NoOfPax", SqlDbType.Int);
                sqlParam[5].Value = this.NoOfPax;
                sqlParam[6] = new SqlParameter("@NoOfLargeBag", SqlDbType.Int);
                sqlParam[6].Value = this.NoOfLargeBag;
                sqlParam[7] = new SqlParameter("@NoOfSmallBag", SqlDbType.Int);
                sqlParam[7].Value = this.NoOfSmallBag;
                sqlParam[8] = new SqlParameter("@Doors", SqlDbType.Int);
                sqlParam[8].Value = this.Doors;
                sqlParam[9] = new SqlParameter("@Litre", SqlDbType.Float);
                sqlParam[9].Value = this.Litre;
                sqlParam[10] = new SqlParameter("@Cylindar", SqlDbType.Int);
                sqlParam[10].Value = this.Cylindar;
                sqlParam[11] = new SqlParameter("@AirBag", SqlDbType.Int);
                sqlParam[11].Value = this.AirBag;

                sqlParam[12] = new SqlParameter("@isAutomatic", SqlDbType.Bit);
                sqlParam[12].Value = this.isAutomatic;
                sqlParam[13] = new SqlParameter("@isAirCondition", SqlDbType.Bit);
                sqlParam[13].Value = this.isAirCondition;
                sqlParam[14] = new SqlParameter("@isPowerSteering", SqlDbType.Bit);
                sqlParam[14].Value = this.isPowerSteering;
                sqlParam[15] = new SqlParameter("@isABS", SqlDbType.Bit);
                sqlParam[15].Value = this.isABS;
                sqlParam[16] = new SqlParameter("@isCDPlayer", SqlDbType.Bit);
                sqlParam[16].Value = this.isCDPlayer;
                sqlParam[17] = new SqlParameter("@isReversingCamera", SqlDbType.Bit);
                sqlParam[17].Value = this.isReversingCamera;
                sqlParam[18] = new SqlParameter("@isBluetoothAUXandUSB", SqlDbType.Bit);
                sqlParam[18].Value = this.isBluetoothAUXandUSB;

                sqlParam[19] = new SqlParameter("@Others", SqlDbType.NVarChar);
                sqlParam[19].Value = this.Others != null ? this.Others : SqlString.Null;
                sqlParam[20] = new SqlParameter("@FileName", SqlDbType.NVarChar);
                sqlParam[20].Value = this.FileName != null ? this.FileName : SqlString.Null;
                sqlParam[21] = new SqlParameter("@NoOfMediumBag", SqlDbType.Int);
                sqlParam[21].Value = this.NoOfMediumBag;
                sqlParam[22] = new SqlParameter("@VehiclesClassCode", SqlDbType.NVarChar);
                sqlParam[22].Value = this.VehiclesClassCode != null ? this.VehiclesClassCode : SqlString.Null;
                sqlParam[23] = new SqlParameter("@VehiclesCategoryCode", SqlDbType.NVarChar);
                sqlParam[23].Value = this.VehiclesCategoryCode != null ? this.VehiclesCategoryCode : SqlString.Null;
                sqlParam[24] = new SqlParameter("@isSedan", SqlDbType.Bit);
                sqlParam[24].Value = this.isSedan;

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
