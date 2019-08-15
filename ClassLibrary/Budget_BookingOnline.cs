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
    public partial class Budget_BookingOnline
    {
        private string ConnectionString = ConfigurationSettings.AppSettings["Budget.ConnectionString"];
        public Guid BookingOID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Flight { get; set; }
        public string Email { get; set; }
        public string ConfirmationID { get; set; }
        public double Price { get; set; }
        public string GroupType { get; set; }
        public string GroupValue { get; set; }
        public string RateQualifier { get; set; }
        public string CurrencyCode { get; set; }
        public string PickupLocation { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string ReturnLocation { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public string CarName { get; set; }
        public string Citizen { get; set; }
        public string VehType { get; set; }
        public string VehClass { get; set; }
        public int ChildSeatQty { get; set; }
        public double ChildSeatPrice { get; set; }
        public string BookingStatus { get; set; }
        public DateTime BookingDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public Budget_BookingOnline()
        {
        }

        public Budget_BookingOnline(string sConnectionString)
        {
            ConnectionString = sConnectionString;
        }
        public List<Budget_BookingOnline> SelectAll()
        {
            List<Budget_BookingOnline> BookingList = null;
            try
            {
                string sSQL = @"SELECT * FROM BookingOnline";

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BookingList = new List<Budget_BookingOnline>();
                    Budget_BookingOnline booking = new Budget_BookingOnline();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        booking = new Budget_BookingOnline();
                        booking.BookingOID = new Guid(ds.Tables[0].Rows[i]["BookingOID"].ToString());
                        booking.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                        booking.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();
                        booking.Contact = ds.Tables[0].Rows[i]["Contact"].ToString();
                        booking.Flight = ds.Tables[0].Rows[i]["Flight"].ToString();
                        booking.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        booking.ConfirmationID = ds.Tables[0].Rows[i]["ConfirmationID"].ToString();
                        booking.Price = Convert.ToDouble(ds.Tables[0].Rows[i]["Price"].ToString());
                        booking.GroupType = ds.Tables[0].Rows[i]["GroupType"].ToString();
                        booking.GroupValue = ds.Tables[0].Rows[i]["GroupValue"].ToString();
                        booking.RateQualifier = (ds.Tables[0].Rows[i]["RateQualifier"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["RateQualifier"].ToString() : "";
                        booking.CurrencyCode = ds.Tables[0].Rows[i]["CurrencyCode"].ToString();
                        booking.PickupLocation = (ds.Tables[0].Rows[i]["PickupLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["PickupLocation"].ToString() : "";
                        if(ds.Tables[0].Rows[i]["PickupDateTime"].Equals(DBNull.Value) == false)
                            booking.PickupDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["PickupDateTime"].ToString());
                        booking.ReturnLocation = (ds.Tables[0].Rows[i]["ReturnLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[i]["ReturnLocation"].ToString() : "";
                        if (ds.Tables[0].Rows[i]["ReturnDateTime"].Equals(DBNull.Value) == false)
                            booking.ReturnDateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["ReturnDateTime"].ToString());
                        booking.CarName = ds.Tables[0].Rows[i]["CarName"].ToString();
                        booking.Citizen = ds.Tables[0].Rows[i]["Citizen"].ToString();
                        booking.VehType = ds.Tables[0].Rows[i]["VehType"].ToString();
                        booking.VehClass = ds.Tables[0].Rows[i]["VehClass"].ToString();
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
        public Budget_BookingOnline SelectByOID(Guid BookingOID)
        {
            Budget_BookingOnline booking = null;
            try
            {
                string sSQL = @"SELECT * FROM BookingOnline WHERE BookingOID=@BookingOID";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@BookingOID", SqlDbType.UniqueIdentifier);
                sqlParam[0].Value = BookingOID;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    booking = new Budget_BookingOnline();

                    booking.BookingOID = new Guid(ds.Tables[0].Rows[0]["BookingOID"].ToString());
                    booking.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    booking.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    booking.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                    booking.Flight = ds.Tables[0].Rows[0]["Flight"].ToString();
                    booking.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    booking.ConfirmationID = ds.Tables[0].Rows[0]["ConfirmationID"].ToString();
                    booking.Price = Convert.ToDouble(ds.Tables[0].Rows[0]["Price"].ToString());
                    booking.GroupType = ds.Tables[0].Rows[0]["GroupType"].ToString();
                    booking.GroupValue = ds.Tables[0].Rows[0]["GroupValue"].ToString();
                    booking.RateQualifier = (ds.Tables[0].Rows[0]["RateQualifier"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["RateQualifier"].ToString() : "";
                    booking.CurrencyCode = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
                    booking.PickupLocation = (ds.Tables[0].Rows[0]["PickupLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["PickupLocation"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["PickupDateTime"].Equals(DBNull.Value) == false)
                        booking.PickupDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["PickupDateTime"].ToString());
                    booking.ReturnLocation = (ds.Tables[0].Rows[0]["ReturnLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["ReturnLocation"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["ReturnDateTime"].Equals(DBNull.Value) == false)
                        booking.ReturnDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReturnDateTime"].ToString());
                    booking.CarName = ds.Tables[0].Rows[0]["CarName"].ToString();
                    booking.Citizen = ds.Tables[0].Rows[0]["Citizen"].ToString();
                    booking.VehType = ds.Tables[0].Rows[0]["VehType"].ToString();
                    booking.VehClass = ds.Tables[0].Rows[0]["VehClass"].ToString();
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

        public Budget_BookingOnline SelectByConfirmationIDAndLastname(string ConfirmationID,string LastName)
        {
            Budget_BookingOnline booking = null;
            try
            {
                string sSQL = @"SELECT * FROM BookingOnline WHERE ConfirmationID=@ConfirmationID AND LastName=@LastName";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@ConfirmationID", SqlDbType.NVarChar);
                sqlParam[0].Value = ConfirmationID;
                sqlParam[1] = new SqlParameter("@LastName", SqlDbType.NVarChar);
                sqlParam[1].Value = LastName;
                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sSQL, sqlParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    booking = new Budget_BookingOnline();

                    booking.BookingOID = new Guid(ds.Tables[0].Rows[0]["BookingOID"].ToString());
                    booking.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    booking.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    booking.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                    booking.Flight = ds.Tables[0].Rows[0]["Flight"].ToString();
                    booking.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    booking.ConfirmationID = ds.Tables[0].Rows[0]["ConfirmationID"].ToString();
                    booking.Price = Convert.ToDouble(ds.Tables[0].Rows[0]["Price"].ToString());
                    booking.GroupType = ds.Tables[0].Rows[0]["GroupType"].ToString();
                    booking.GroupValue = ds.Tables[0].Rows[0]["GroupValue"].ToString();
                    booking.RateQualifier = (ds.Tables[0].Rows[0]["RateQualifier"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["RateQualifier"].ToString() : "";
                    booking.CurrencyCode = ds.Tables[0].Rows[0]["CurrencyCode"].ToString();
                    booking.PickupLocation = (ds.Tables[0].Rows[0]["PickupLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["PickupLocation"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["PickupDateTime"].Equals(DBNull.Value) == false)
                        booking.PickupDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["PickupDateTime"].ToString());
                    booking.ReturnLocation = (ds.Tables[0].Rows[0]["ReturnLocation"].Equals(DBNull.Value) == false) ? ds.Tables[0].Rows[0]["ReturnLocation"].ToString() : "";
                    if (ds.Tables[0].Rows[0]["ReturnDateTime"].Equals(DBNull.Value) == false)
                        booking.ReturnDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReturnDateTime"].ToString());
                    booking.CarName = ds.Tables[0].Rows[0]["CarName"].ToString();
                    booking.Citizen = ds.Tables[0].Rows[0]["Citizen"].ToString();
                    booking.VehType = ds.Tables[0].Rows[0]["VehType"].ToString();
                    booking.VehClass = ds.Tables[0].Rows[0]["VehClass"].ToString();
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
                string sSQL = @"INSERT INTO BookingOnline (BookingOID,FirstName,LastName,Contact,Flight,Email,ConfirmationID,
                    Price,GroupType,GroupValue,RateQualifier,CurrencyCode,
                    PickupLocation,PickupDateTime,ReturnLocation,ReturnDateTime,CarName,Citizen,
                    VehType,VehClass,ChildSeatQty,ChildSeatPrice,BookingStatus,BookingDateTime) 
                    VALUES
                    (@BookingOID, @FirstName, @LastName, @Contact, @Flight, @Email, @ConfirmationID,
                    @Price, @GroupType, @GroupValue, @RateQualifier, @CurrencyCode,
                    @PickupLocation, @PickupDateTime, @ReturnLocation, @ReturnDateTime, @CarName,@Citizen,
                    @VehType, @VehClass, @ChildSeatQty, @ChildSeatPrice, @BookingStatus, @BookingDateTime)";
                SqlParameter[] sqlParam = new SqlParameter[24];
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
                sqlParam[7] = new SqlParameter("@Price", SqlDbType.Money);
                sqlParam[7].Value = this.Price != null ? this.Price : 0;
                sqlParam[8] = new SqlParameter("@GroupType", SqlDbType.NVarChar);
                sqlParam[8].Value = this.GroupType != null ? this.GroupType : "";
                sqlParam[9] = new SqlParameter("@GroupValue", SqlDbType.NVarChar);
                sqlParam[9].Value = this.GroupValue != null ? this.GroupValue : "";
                sqlParam[10] = new SqlParameter("@RateQualifier", SqlDbType.NVarChar);
                sqlParam[10].Value = this.RateQualifier != null ? this.RateQualifier : "";
                sqlParam[11] = new SqlParameter("@CurrencyCode", SqlDbType.NVarChar);
                sqlParam[11].Value = this.CurrencyCode != null ? this.CurrencyCode : "";
                sqlParam[12] = new SqlParameter("@PickupLocation", SqlDbType.NVarChar);
                sqlParam[12].Value = this.PickupLocation != null ? this.PickupLocation : "";
                sqlParam[13] = new SqlParameter("@PickupDateTime", SqlDbType.DateTime);
                sqlParam[13].Value = this.PickupDateTime != null ? this.PickupDateTime : SqlDateTime.Null;
                sqlParam[14] = new SqlParameter("@ReturnLocation", SqlDbType.NVarChar);
                sqlParam[14].Value = this.ReturnLocation != null ? this.ReturnLocation : "";
                sqlParam[15] = new SqlParameter("@ReturnDateTime", SqlDbType.DateTime);
                sqlParam[15].Value = this.ReturnDateTime != null ? this.ReturnDateTime : SqlDateTime.Null;
                sqlParam[16] = new SqlParameter("@CarName", SqlDbType.NVarChar);
                sqlParam[16].Value = this.CarName != null ? this.CarName : "";
                sqlParam[17] = new SqlParameter("@Citizen", SqlDbType.NVarChar);
                sqlParam[17].Value = this.Citizen != null ? this.Citizen : "";
                sqlParam[18] = new SqlParameter("@VehType", SqlDbType.NVarChar);
                sqlParam[18].Value = this.VehType != null ? this.VehType : "";
                sqlParam[19] = new SqlParameter("@VehClass", SqlDbType.NVarChar);
                sqlParam[19].Value = this.VehClass != null ? this.VehClass : "";
                sqlParam[20] = new SqlParameter("@ChildSeatQty", SqlDbType.Int);
                sqlParam[20].Value = this.ChildSeatQty != null ? this.ChildSeatQty : 0;
                sqlParam[21] = new SqlParameter("@ChildSeatPrice", SqlDbType.Money);
                sqlParam[21].Value = this.ChildSeatPrice != null ? this.ChildSeatPrice : 0;
                sqlParam[22] = new SqlParameter("@BookingStatus", SqlDbType.NVarChar);
                sqlParam[22].Value = this.BookingStatus;
                sqlParam[23] = new SqlParameter("@BookingDateTime", SqlDbType.DateTime);
                sqlParam[23].Value = this.BookingDateTime != null ? this.BookingDateTime : DateTime.Now;

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
                    ConfirmationID=@ConfirmationID,Price= @Price,GroupType= @GroupType,GroupValue=@GroupValue,RateQualifier= @RateQualifier,CurrencyCode=@CurrencyCode,
                    PickupLocation= @PickupLocation,PickupDateTime=@PickupDateTime,ReturnLocation=@ReturnLocation,ReturnDateTime=@ReturnDateTime,CarName=@CarName,Citizen=@Citizen,
                    VehType= @VehType,VehClass=@VehClass,ChildSeatQty=@ChildSeatQty,ChildSeatPrice=@ChildSeatPrice,BookingStatus=@BookingStatus,ModifyDateTime=@ModifyDateTime
                    WHERE BookingOID=@BookingOID";
                SqlParameter[] sqlParam = new SqlParameter[24];
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
                sqlParam[7] = new SqlParameter("@Price", SqlDbType.Money);
                sqlParam[7].Value = this.Price != null ? this.Price : 0;
                sqlParam[8] = new SqlParameter("@GroupType", SqlDbType.NVarChar);
                sqlParam[8].Value = this.GroupType != null ? this.GroupType : "";
                sqlParam[9] = new SqlParameter("@GroupValue", SqlDbType.NVarChar);
                sqlParam[9].Value = this.GroupValue != null ? this.GroupValue : "";
                sqlParam[10] = new SqlParameter("@RateQualifier", SqlDbType.NVarChar);
                sqlParam[10].Value = this.RateQualifier != null ? this.RateQualifier : "";
                sqlParam[11] = new SqlParameter("@CurrencyCode", SqlDbType.NVarChar);
                sqlParam[11].Value = this.CurrencyCode != null ? this.CurrencyCode : "";
                sqlParam[12] = new SqlParameter("@PickupLocation", SqlDbType.NVarChar);
                sqlParam[12].Value = this.PickupLocation != null ? this.PickupLocation : "";
                sqlParam[13] = new SqlParameter("@PickupDateTime", SqlDbType.DateTime);
                sqlParam[13].Value = this.PickupDateTime != null ? this.PickupDateTime : SqlDateTime.Null;
                sqlParam[14] = new SqlParameter("@ReturnLocation", SqlDbType.NVarChar);
                sqlParam[14].Value = this.ReturnLocation != null ? this.ReturnLocation : "";
                sqlParam[15] = new SqlParameter("@ReturnDateTime", SqlDbType.DateTime);
                sqlParam[15].Value = this.ReturnDateTime != null ? this.ReturnDateTime : SqlDateTime.Null;
                sqlParam[16] = new SqlParameter("@CarName", SqlDbType.NVarChar);
                sqlParam[16].Value = this.CarName != null ? this.CarName : "";
                sqlParam[17] = new SqlParameter("@Citizen", SqlDbType.NVarChar);
                sqlParam[17].Value = this.Citizen != null ? this.Citizen : "";
                sqlParam[18] = new SqlParameter("@VehType", SqlDbType.NVarChar);
                sqlParam[18].Value = this.VehType != null ? this.VehType : "";
                sqlParam[19] = new SqlParameter("@VehClass", SqlDbType.NVarChar);
                sqlParam[19].Value = this.VehClass != null ? this.VehClass : "";
                sqlParam[20] = new SqlParameter("@ChildSeatQty", SqlDbType.Int);
                sqlParam[20].Value = this.ChildSeatQty != null ? this.ChildSeatQty : 0;
                sqlParam[21] = new SqlParameter("@ChildSeatPrice", SqlDbType.Money);
                sqlParam[21].Value = this.ChildSeatPrice != null ? this.ChildSeatPrice : 0;
                sqlParam[22] = new SqlParameter("@BookingStatus", SqlDbType.NVarChar);
                sqlParam[22].Value = this.BookingStatus != null ? this.BookingStatus : "";
                sqlParam[23] = new SqlParameter("@ModifyDateTime", SqlDbType.DateTime);
                sqlParam[23].Value = this.ModifyDateTime != null ? this.ModifyDateTime : DateTime.Now;

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sSQL, sqlParam);
                bResult = true;
            }
            catch (Exception ex) { }
            return bResult;
        }
    }
}
