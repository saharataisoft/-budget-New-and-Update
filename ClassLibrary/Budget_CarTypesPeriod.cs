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
    public partial class Budget_CarTypesPeriod
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid RateOID { get; set; }
        public Guid CarTypeOID { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime FinishPeriod { get; set; }
        public double RatePerDay { get; set; }
        public Budget_CarTypesPeriod()
        {
        }
        public Budget_CarTypesPeriod(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_CarTypesPeriod> SelectAll()
        {
            List<Budget_CarTypesPeriod> PeriodList = null;
            try
            {
                string sSQL = "SELECT * FROM CarTypesPeriod";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    PeriodList = new List<Budget_CarTypesPeriod>();
                    Budget_CarTypesPeriod period = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        period = new Budget_CarTypesPeriod();
                        period.RateOID = new Guid(ds.Tables[0].Rows[i]["RateOID"].ToString());
                        period.CarTypeOID = new Guid(ds.Tables[0].Rows[i]["CarTypeOID"].ToString());
                        period.StartPeriod = Convert.ToDateTime(ds.Tables[0].Rows[i]["StartPeriod"].ToString());
                        period.FinishPeriod = Convert.ToDateTime(ds.Tables[0].Rows[i]["FinishPeriod"].ToString());
                        period.RatePerDay = ds.Tables[0].Rows[i]["RatePerDay"].Equals(DBNull.Value) == false ? Convert.ToDouble(ds.Tables[0].Rows[i]["RatePerDay"].ToString()) : 0;
                        PeriodList.Add(period);
                    }
                }
            }
            catch (Exception ex) { }
            return PeriodList;
        }
        public List<Budget_CarTypesPeriod> SelectAllByCarTypeOID(Guid CarTypeOID)
        {
            List<Budget_CarTypesPeriod> PeriodList = null;
            try
            {
                string sSQL = "SELECT * FROM CarTypesPeriod WHERE CarTypeOID=@CarTypeOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = CarTypeOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    PeriodList = new List<Budget_CarTypesPeriod>();
                    Budget_CarTypesPeriod period = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        period = new Budget_CarTypesPeriod();
                        period.RateOID = new Guid(ds.Tables[0].Rows[i]["RateOID"].ToString());
                        period.CarTypeOID = new Guid(ds.Tables[0].Rows[i]["CarTypeOID"].ToString());
                        period.StartPeriod = Convert.ToDateTime(ds.Tables[0].Rows[i]["StartPeriod"].ToString());
                        period.FinishPeriod = Convert.ToDateTime(ds.Tables[0].Rows[i]["FinishPeriod"].ToString());
                        period.RatePerDay = ds.Tables[0].Rows[i]["RatePerDay"].Equals(DBNull.Value) == false ? Convert.ToDouble(ds.Tables[0].Rows[i]["RatePerDay"].ToString()) : 0;
                        PeriodList.Add(period);
                    }
                }
            }
            catch (Exception ex) { }
            return PeriodList;
        }
        public Budget_CarTypesPeriod SelectByOID(Guid guidRateOID)
        {
            Budget_CarTypesPeriod period = null;
            try
            {
                string sSQL = "SELECT * FROM CarTypesPeriod WHERE RateOID=@RateOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@RateOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = guidRateOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    period = new Budget_CarTypesPeriod();
                    period.RateOID = new Guid(ds.Tables[0].Rows[0]["RateOID"].ToString());
                    period.CarTypeOID = new Guid(ds.Tables[0].Rows[0]["CarTypeOID"].ToString());
                    period.StartPeriod = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartPeriod"].ToString());
                    period.FinishPeriod = Convert.ToDateTime(ds.Tables[0].Rows[0]["FinishPeriod"].ToString());
                    period.RatePerDay = ds.Tables[0].Rows[0]["RatePerDay"].Equals(DBNull.Value) == false ? Convert.ToDouble(ds.Tables[0].Rows[0]["RatePerDay"].ToString()) : 0;
                }
            }
            catch (Exception ex) { }
            return period;
        }
        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO CarTypesPeriod 
                            (RateOID,CarTypeOID,StartPeriod,FinishPeriod,RatePerDay) 
                            VALUES 
                            (@RateOID,@CarTypeOID,@StartPeriod,@FinishPeriod,@RatePerDay)";
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@RateOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.RateOID;
                sqlParam[1] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[1].Value = this.CarTypeOID;
                sqlParam[2] = new SqlParameter("@StartPeriod", SqlDbType.DateTime);
                sqlParam[2].Value = this.StartPeriod;
                sqlParam[3] = new SqlParameter("@FinishPeriod", SqlDbType.DateTime);
                sqlParam[3].Value = this.FinishPeriod;
                sqlParam[4] = new SqlParameter("@RatePerDay", SqlDbType.Money);
                sqlParam[4].Value = this.RatePerDay != null ? this.RatePerDay : 0;

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
                string sSQL = @"UPDATE CarTypesPeriod SET 
                    StartPeriod=@StartPeriod,FinishPeriod=@FinishPeriod,RatePerDay=@RatePerDay
                    WHERE RateOID=@RateOID AND CarTypeOID=@CarTypeOID";
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter("@RateOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.RateOID;
                sqlParam[1] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[1].Value = this.CarTypeOID;
                sqlParam[2] = new SqlParameter("@StartPeriod", SqlDbType.DateTime);
                sqlParam[2].Value = this.StartPeriod;
                sqlParam[3] = new SqlParameter("@FinishPeriod", SqlDbType.DateTime);
                sqlParam[3].Value = this.FinishPeriod;
                sqlParam[4] = new SqlParameter("@RatePerDay", SqlDbType.Money);
                sqlParam[4].Value = this.RatePerDay != null ? this.RatePerDay : SqlDouble.Null;

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }

        public bool deleteByCarTypeOID(Guid CarTypeOID)
        {
            bool bResult = false;
            try
            {
                string sSQL = @"DELETE FROM CarTypesPeriod WHERE CarTypeOID=@CarTypeOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = CarTypeOID;
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
