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
    public partial class Budget_BookingOffline
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid BookingOID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Flight { get; set; }
        public string Email { get; set; }
        public string ConfirmationID { get; set; }
        public Guid CarTypeOID { get; set; }
        public Guid RateOID { get; set; }
        public double Price { get; set; }
        public string PickupLocation { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string ReturnLocation { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public string CarName { get; set; }

        public string GroupCode { get; set; }
        public string SIPPCode { get; set; }
        public int ChildSeatQty { get; set; }
        public double ChildSeatPrice { get; set; }
        public string BookingStatus { get; set; }
        public DateTime BookingDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }

        public Budget_BookingOffline()
        {
        }

        public Budget_BookingOffline(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_BookingOffline> SelectAll()
        {
            List<Budget_BookingOffline> BookingList = null;
            try
            {
                string sSQL = @"SELECT * FROM BookingOffline";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BookingList = new List<Budget_BookingOffline>();
                    Budget_BookingOffline booking = new Budget_BookingOffline();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        booking = new Budget_BookingOffline();
                        booking.BookingOID = new Guid(ds.Tables[0].Rows[i]["BookingOID"].ToString());
                        booking.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                        booking.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();
                        booking.Contact = ds.Tables[0].Rows[i]["Contact"].ToString();
                        booking.Flight = ds.Tables[0].Rows[i]["Flight"].ToString();
                        booking.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        booking.ConfirmationID = ds.Tables[0].Rows[i]["ConfirmationID"].ToString();
                        booking.CarTypeOID = new Guid(ds.Tables[0].Rows[i]["CarTypeOID"].ToString());
                        booking.RateOID = new Guid(ds.Tables[0].Rows[i]["RateOID"].ToString());
                        booking.Price = Convert.ToDouble(ds.Tables[0].Rows[i]["Price"].ToString());                      
                        booking.PickupLocation = (ds.Tables[0].Rows[i]["PickupLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["PickupLocation"].ToString() : "";
                        if (ds.Tables[0].Rows[i]["PickupDateTime"].Equals(DBNull.Value) == false)
                            booking.PickupDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["PickupDateTime"].ToString());
                        booking.ReturnLocation = (ds.Tables[0].Rows[i]["ReturnLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["ReturnLocation"].ToString() : "";
                        if (ds.Tables[0].Rows[i]["ReturnDateTime"].Equals(DBNull.Value) == false)
                            booking.ReturnDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["ReturnDateTime"].ToString());
                        booking.CarName = ds.Tables[0].Rows[i]["CarName"].ToString();
                        booking.GroupCode = ds.Tables[0].Rows[i]["GroupCode"].ToString();
                        booking.SIPPCode = ds.Tables[0].Rows[i]["SIPPCode"].ToString();
                        booking.ChildSeatQty = int.Parse(ds.Tables[0].Rows[i]["ChildSeatQty"].ToString());
                        booking.ChildSeatPrice = (ds.Tables[0].Rows[i]["ChildSeatPrice"].Equals(DBNull.Value) == false) ? Convert.ToDouble(ds.Tables[0].Rows[i]["ChildSeatPrice"].ToString()) : 0;
                        booking.BookingStatus = (ds.Tables[0].Rows[i]["BookingStatus"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["BookingStatus"].ToString() : "";
                        if (ds.Tables[0].Rows[i]["BookingDateTime"].Equals(DBNull.Value) == false)
                            booking.BookingDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["BookingDateTime"].ToString());
                        if (ds.Tables[0].Rows[i]["ModifyDateTime"].Equals(DBNull.Value) == false)
                            booking.ModifyDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["ModifyDateTime"].ToString());
                        BookingList.Add(booking);
                    }
                }
            }
            catch (Exception ex) { }

            return BookingList;
        }
        public Budget_BookingOffline SelectByOID(Guid BookingOID)
        {
            Budget_BookingOffline booking = null;
            try
            {
                string sSQL = @"SELECT * FROM BookingOffline WHERE BookingOID=@BookingOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@BookingOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = BookingOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    booking = new Budget_BookingOffline();

                    booking.BookingOID = new Guid(ds.Tables[0].Rows[0]["BookingOID"].ToString());
                    booking.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    booking.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    booking.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                    booking.Flight = ds.Tables[0].Rows[0]["Flight"].ToString();
                    booking.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    booking.ConfirmationID = ds.Tables[0].Rows[0]["ConfirmationID"].ToString();
                    booking.CarTypeOID = new Guid(ds.Tables[0].Rows[0]["CarTypeOID"].ToString());
                    booking.RateOID = new Guid(ds.Tables[0].Rows[0]["RateOID"].ToString());
                    booking.Price = Convert.ToDouble(ds.Tables[0].Rows[0]["Price"].ToString());                   
                    booking.PickupLocation = (ds.Tables[0].Rows[0]["PickupLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["PickupLocation"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["PickupDateTime"].Equals(DBNull.Value) == false)
                        booking.PickupDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["PickupDateTime"].ToString());
                    booking.ReturnLocation = (ds.Tables[0].Rows[0]["ReturnLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["ReturnLocation"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["ReturnDateTime"].Equals(DBNull.Value) == false)
                        booking.ReturnDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReturnDateTime"].ToString());
                    booking.CarName = ds.Tables[0].Rows[0]["CarName"].ToString();
                    booking.GroupCode = ds.Tables[0].Rows[0]["GroupCode"].ToString();
                    booking.SIPPCode = ds.Tables[0].Rows[0]["SIPPCode"].ToString();
                    booking.ChildSeatQty = int.Parse(ds.Tables[0].Rows[0]["ChildSeatQty"].ToString());
                    booking.ChildSeatPrice = (ds.Tables[0].Rows[0]["ChildSeatPrice"].Equals(DBNull.Value) == false) ? Convert.ToDouble(ds.Tables[0].Rows[0]["ChildSeatPrice"].ToString()) : 0;
                    booking.BookingStatus = (ds.Tables[0].Rows[0]["BookingStatus"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["BookingStatus"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["BookingDateTime"].Equals(DBNull.Value) == false)
                        booking.BookingDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["BookingDateTime"].ToString());
                    if (ds.Tables[0].Rows[0]["ModifyDateTime"].Equals(DBNull.Value) == false)
                        booking.ModifyDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ModifyDateTime"].ToString());

                }
            }
            catch (Exception ex) { }

            return booking;
        }

        public Budget_BookingOffline SelectByConfirmationIDAndLastname(string ConfirmationID, string LastName)
        {
            Budget_BookingOffline booking = null;
            try
            {
                string sSQL = @"SELECT * FROM BookingOffline WHERE ConfirmationID=@ConfirmationID AND LastName=@LastName";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@ConfirmationID", SqlDbType.NVarChar);
                sqlParam[0].Value = ConfirmationID;
                sqlParam[1] = new SqlParameter("@LastName", SqlDbType.NVarChar);
                sqlParam[1].Value = LastName;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    booking = new Budget_BookingOffline();

                    booking.BookingOID = new Guid(ds.Tables[0].Rows[0]["BookingOID"].ToString());
                    booking.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    booking.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    booking.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                    booking.Flight = ds.Tables[0].Rows[0]["Flight"].ToString();
                    booking.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    booking.ConfirmationID = ds.Tables[0].Rows[0]["ConfirmationID"].ToString();
                    booking.CarTypeOID = new Guid(ds.Tables[0].Rows[0]["CarTypeOID"].ToString());
                    booking.RateOID = new Guid(ds.Tables[0].Rows[0]["RateOID"].ToString());
                    booking.Price = Convert.ToDouble(ds.Tables[0].Rows[0]["Price"].ToString());                   
                    booking.PickupLocation = (ds.Tables[0].Rows[0]["PickupLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["PickupLocation"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["PickupDateTime"].Equals(DBNull.Value) == false)
                        booking.PickupDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["PickupDateTime"].ToString());
                    booking.ReturnLocation = (ds.Tables[0].Rows[0]["ReturnLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["ReturnLocation"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["ReturnDateTime"].Equals(DBNull.Value) == false)
                        booking.ReturnDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReturnDateTime"].ToString());
                    booking.CarName = ds.Tables[0].Rows[0]["CarName"].ToString();
                    booking.GroupCode = ds.Tables[0].Rows[0]["GroupCode"].ToString();
                    booking.SIPPCode = ds.Tables[0].Rows[0]["SIPPCode"].ToString();
                    booking.ChildSeatQty = int.Parse(ds.Tables[0].Rows[0]["ChildSeatQty"].ToString());
                    booking.ChildSeatPrice = (ds.Tables[0].Rows[0]["ChildSeatPrice"].Equals(DBNull.Value) == false) ? Convert.ToDouble(ds.Tables[0].Rows[0]["ChildSeatPrice"].ToString()) : 0;
                    booking.BookingStatus = (ds.Tables[0].Rows[0]["BookingStatus"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["BookingStatus"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["BookingDateTime"].Equals(DBNull.Value) == false)
                        booking.BookingDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["BookingDateTime"].ToString());
                    if (ds.Tables[0].Rows[0]["ModifyDateTime"].Equals(DBNull.Value) == false)
                        booking.ModifyDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ModifyDateTime"].ToString());

                }
            }
            catch (Exception ex) { }

            return booking;
        }

        public bool add()
        {
            bool bResult = false;
            try
            {
                string sSQL = @"INSERT INTO BookingOffline (BookingOID,FirstName,LastName,Contact,Flight,Email,ConfirmationID,
                    CarTypeOID,RateOID,Price,PickupLocation,PickupDateTime,ReturnLocation,ReturnDateTime,CarName,GroupCode,
                    SIPPCode,ChildSeatQty,ChildSeatPrice,BookingStatus,BookingDateTime) 
                    VALUES
                    (@BookingOID, @FirstName, @LastName, @Contact, @Flight, @Email, @ConfirmationID,
                    @CarTypeOID,@RateOID,@Price,@PickupLocation, @PickupDateTime, @ReturnLocation, @ReturnDateTime, @CarName,@GroupCode,
                    @SIPPCode, @ChildSeatQty, @ChildSeatPrice, @BookingStatus, @BookingDateTime)";
                SqlParameter[] sqlParam = new SqlParameter[21];
                sqlParam[0] = new SqlParameter("@BookingOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.BookingOID;
                sqlParam[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                sqlParam[1].Value = this.FirstName;
                sqlParam[2] = new SqlParameter("@LastName", SqlDbType.NVarChar);
                sqlParam[2].Value = this.LastName != null ? this.LastName : "";
                sqlParam[3] = new SqlParameter("@Contact", SqlDbType.NVarChar);
                sqlParam[3].Value = this.Contact != null ? this.Contact : "";
                sqlParam[4] = new SqlParameter("@Flight", SqlDbType.NVarChar);
                sqlParam[4].Value = this.Flight != null ? this.Flight : "";
                sqlParam[5] = new SqlParameter("@Email", SqlDbType.NVarChar);
                sqlParam[5].Value = this.Email;
                sqlParam[6] = new SqlParameter("@ConfirmationID", SqlDbType.NVarChar);
                sqlParam[6].Value = this.ConfirmationID != null ? this.ConfirmationID : "";
                sqlParam[7] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[7].Value = this.CarTypeOID;
                sqlParam[8] = new SqlParameter("@RateOID", SqlDbType.UniqueIdentifier);
                sqlParam[8].Value = this.RateOID;
                sqlParam[9] = new SqlParameter("@Price", SqlDbType.Money);
                sqlParam[9].Value = this.Price != null ? this.Price : 0;
                sqlParam[10] = new SqlParameter("@PickupLocation", SqlDbType.NVarChar);
                sqlParam[10].Value = this.PickupLocation != null ? this.PickupLocation : "";
                sqlParam[11] = new SqlParameter("@PickupDateTime", SqlDbType.DateTime);
                sqlParam[11].Value = this.PickupDateTime != null ? this.PickupDateTime : SqlDateTime.Null;
                sqlParam[12] = new SqlParameter("@ReturnLocation", SqlDbType.NVarChar);
                sqlParam[12].Value = this.ReturnLocation != null ? this.ReturnLocation : "";
                sqlParam[13] = new SqlParameter("@ReturnDateTime", SqlDbType.DateTime);
                sqlParam[13].Value = this.ReturnDateTime != null ? this.ReturnDateTime : SqlDateTime.Null;
                sqlParam[14] = new SqlParameter("@CarName", SqlDbType.NVarChar);
                sqlParam[14].Value = this.CarName != null ? this.CarName : "";
                sqlParam[15] = new SqlParameter("@GroupCode", SqlDbType.NVarChar);
                sqlParam[15].Value = this.GroupCode != null ? this.GroupCode : "";
                sqlParam[16] = new SqlParameter("@SIPPCode", SqlDbType.NVarChar);
                sqlParam[16].Value = this.SIPPCode != null ? this.SIPPCode : "";
                sqlParam[17] = new SqlParameter("@ChildSeatQty", SqlDbType.Int);
                sqlParam[17].Value = this.ChildSeatQty != null ? this.ChildSeatQty : 0;
                sqlParam[18] = new SqlParameter("@ChildSeatPrice", SqlDbType.Money);
                sqlParam[18].Value = this.ChildSeatPrice != null ? this.ChildSeatPrice : 0;
                sqlParam[19] = new SqlParameter("@BookingStatus", SqlDbType.NVarChar);
                sqlParam[19].Value = this.BookingStatus;
                sqlParam[20] = new SqlParameter("@BookingDateTime", SqlDbType.DateTime);
                sqlParam[20].Value = this.BookingDateTime != null ? this.BookingDateTime : DateTime.Now;

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
                string sSQL = @"UPDATE BookingOnline SET FirstName=@FirstName,LastName=@LastName,Contact=@Contact,Flight=@Flight,Email= @Email,
                    ConfirmationID=@ConfirmationID,CarTypeOID=@CarTypeOID,RateOID=@RateOID,Price= @Price,
                    PickupLocation= @PickupLocation,PickupDateTime=@PickupDateTime,ReturnLocation=@ReturnLocation,ReturnDateTime=@ReturnDateTime,CarName=@CarName,
                    GroupCode=@GroupCode,SIPPCode=@SIPPCode,ChildSeatQty=@ChildSeatQty,ChildSeatPrice=@ChildSeatPrice,BookingStatus=@BookingStatus,ModifyDateTime=@ModifyDateTime
                    WHERE BookingOID=@BookingOID";
                SqlParameter[] sqlParam = new SqlParameter[21];
                sqlParam[0] = new SqlParameter("@BookingOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = this.BookingOID;
                sqlParam[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                sqlParam[1].Value = this.FirstName;
                sqlParam[2] = new SqlParameter("@LastName", SqlDbType.NVarChar);
                sqlParam[2].Value = this.LastName != null ? this.LastName : "";
                sqlParam[3] = new SqlParameter("@Contact", SqlDbType.NVarChar);
                sqlParam[3].Value = this.Contact != null ? this.Contact : "";
                sqlParam[4] = new SqlParameter("@Flight", SqlDbType.NVarChar);
                sqlParam[4].Value = this.Flight != null ? this.Flight : "";
                sqlParam[5] = new SqlParameter("@Email", SqlDbType.NVarChar);
                sqlParam[5].Value = this.Email;
                sqlParam[6] = new SqlParameter("@ConfirmationID", SqlDbType.NVarChar);
                sqlParam[6].Value = this.ConfirmationID != null ? this.ConfirmationID : "";
                sqlParam[7] = new SqlParameter("@CarTypeOID", SqlDbType.UniqueIdentifier);
                sqlParam[7].Value = this.CarTypeOID;
                sqlParam[8] = new SqlParameter("@RateOID", SqlDbType.UniqueIdentifier);
                sqlParam[8].Value = this.RateOID;
                sqlParam[9] = new SqlParameter("@Price", SqlDbType.Money);
                sqlParam[9].Value = this.Price != null ? this.Price : 0;               
                sqlParam[10] = new SqlParameter("@PickupLocation", SqlDbType.NVarChar);
                sqlParam[10].Value = this.PickupLocation != null ? this.PickupLocation : "";
                sqlParam[11] = new SqlParameter("@PickupDateTime", SqlDbType.DateTime);
                sqlParam[11].Value = this.PickupDateTime != null ? this.PickupDateTime : SqlDateTime.Null;
                sqlParam[12] = new SqlParameter("@ReturnLocation", SqlDbType.NVarChar);
                sqlParam[12].Value = this.ReturnLocation != null ? this.ReturnLocation : "";
                sqlParam[13] = new SqlParameter("@ReturnDateTime", SqlDbType.DateTime);
                sqlParam[13].Value = this.ReturnDateTime != null ? this.ReturnDateTime : SqlDateTime.Null;
                sqlParam[14] = new SqlParameter("@CarName", SqlDbType.NVarChar);
                sqlParam[14].Value = this.CarName != null ? this.CarName : "";
                sqlParam[15] = new SqlParameter("@GroupCode", SqlDbType.NVarChar);
                sqlParam[15].Value = this.GroupCode != null ? this.GroupCode : "";
                sqlParam[16] = new SqlParameter("@SIPPCode", SqlDbType.NVarChar);
                sqlParam[16].Value = this.SIPPCode != null ? this.SIPPCode : "";               
                sqlParam[17] = new SqlParameter("@ChildSeatQty", SqlDbType.Int);
                sqlParam[17].Value = this.ChildSeatQty != null ? this.ChildSeatQty : 0;
                sqlParam[18] = new SqlParameter("@ChildSeatPrice", SqlDbType.Money);
                sqlParam[18].Value = this.ChildSeatPrice != null ? this.ChildSeatPrice : 0;
                sqlParam[19] = new SqlParameter("@BookingStatus", SqlDbType.NVarChar);
                sqlParam[19].Value = this.BookingStatus != null ? this.BookingStatus : "";
                sqlParam[20] = new SqlParameter("@ModifyDateTime", SqlDbType.DateTime);
                sqlParam[20].Value = this.ModifyDateTime != null ? this.ModifyDateTime : DateTime.Now;

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
