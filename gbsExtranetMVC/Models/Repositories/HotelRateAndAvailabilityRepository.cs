using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class HotelRateAndAvailabilityRepository : BaseRepository
    {
        CommonRepository ObjCommon = new CommonRepository();

        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        DataTable Dates = new DataTable();
        DataTable HotelRooms = new DataTable();
        DataTable HotelAvailabilityTable = new DataTable();
        public DataTable GetDates(string OrderBy, DateTime startDate, DateTime endDate)
        {
            SQLCon.Open();
            CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            SqlCommand cmd = new SqlCommand("TB_SP_GetDates", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(Dates);
            SQLCon.Close();
            return Dates;

        }
        public List<HotelRateAndAvailabilityExt> GetDatesList()
        {
            //SQLCon.Open();
            //SqlCommand cmd = new SqlCommand("TB_SP_GetDates", SQLCon);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Culture", CultureValue);
            //cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            //cmd.Parameters.AddWithValue("@StartDate", startDate);
            //cmd.Parameters.AddWithValue("@EndDate", endDate);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            //SQLCon.Close();

            List<HotelRateAndAvailabilityExt> DateList = new List<HotelRateAndAvailabilityExt>();

            if (Dates.Rows.Count > 0)
            {
                foreach (DataRow dr in Dates.Rows)
                {
                    HotelRateAndAvailabilityExt DatesObj = new HotelRateAndAvailabilityExt();
                    DatesObj.DateID = Convert.ToInt32(dr["ID"]);
                    DatesObj.Date = Convert.ToDateTime(dr["Date"]);
                    DatesObj.DayID = Convert.ToInt32(dr["DayID"]);
                    DatesObj.Day = Convert.ToInt32(dr["Day"]);
                    DatesObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                    DatesObj.DayName = Convert.ToString(dr["DayName"]);
                    DatesObj.MonthID = Convert.ToInt32(dr["MonthID"]);
                    DatesObj.MonthName = Convert.ToString(dr["MonthName"]);
                    DatesObj.Year = Convert.ToInt32(dr["Year"]);
                    DateList.Add(DatesObj);
                }
            }
            return DateList;
        }

        public DataTable HotelAvailability(DateTime startDate, DateTime endDate, int HotelID)
        {
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAvailability", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(HotelAvailabilityTable);
            SQLCon.Close();
            return HotelAvailabilityTable;
        }
        public List<HotelRateAndAvailabilityExt> GetHotelAvailability()
        {
            //SQLCon.Open();
            //SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAvailability", SQLCon);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@StartDate", startDate);
            //cmd.Parameters.AddWithValue("@EndDate", endDate);
            //cmd.Parameters.AddWithValue("@HotelID", HotelID);
            //DataTable dt = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            //SQLCon.Close();

            List<HotelRateAndAvailabilityExt> HotelAvailabilityList = new List<HotelRateAndAvailabilityExt>();

            if (HotelAvailabilityTable.Rows.Count > 0)
            {
                foreach (DataRow dr in HotelAvailabilityTable.Rows)
                {
                    HotelRateAndAvailabilityExt HotelAvailabilityObj = new HotelRateAndAvailabilityExt();
                    HotelAvailabilityObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelAvailabilityObj.DateID = Convert.ToInt64(dr["DateID"]);
                    HotelAvailabilityObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                    HotelAvailabilityObj.HotelRoomID = Convert.ToInt32(dr["HotelRoomID"]);
                    HotelAvailabilityObj.AvailableRoomCount = Convert.ToInt32(dr["AvailableRoomCount"]);
                    HotelAvailabilityObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                    HotelAvailabilityObj.CloseToArrival = Convert.ToInt32(dr["CloseToArrival"]);
                    HotelAvailabilityObj.CloseToDeparture = Convert.ToInt32(dr["CloseToDeparture"]);
                    HotelAvailabilityObj.MinimumStay = Convert.ToInt32(dr["MinimumStay"]);
                    HotelAvailabilityObj.Closed = Convert.ToInt32(dr["Closed"]);
                    HotelAvailabilityObj.RoomRateMissing = Convert.ToInt32(dr["RoomRateMissing"]);

                    HotelAvailabilityList.Add(HotelAvailabilityObj);
                }
            }
            return HotelAvailabilityList;
        }

        public int CreateHotelRoomAvailability(string HotelRoomID, DateTime StartDate, DateTime EndDate, string RoomCount)
        {

            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_CreateHotelRoomAvailability", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@RoomCount", RoomCount);
            int i = Convert.ToInt32(cmd.ExecuteNonQuery());
            SQLCon.Close();
            return i;
        }

        public DataTable GetHotelRooms(string OrderBy, int HotelID)
        {
            SQLCon.Open();
            CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRooms", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", 1);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(HotelRooms);
            SQLCon.Close();
            return HotelRooms;
        }

        public List<HotelRateAndAvailabilityExt> GetHotelRoomsList()
        {
           
            List<HotelRateAndAvailabilityExt> HotelRoomsList = new List<HotelRateAndAvailabilityExt>();

            if (HotelRooms.Rows.Count > 0)
            {
                foreach (DataRow dr in HotelRooms.Rows)
                {
                    HotelRateAndAvailabilityExt HotelRoomsObj = new HotelRateAndAvailabilityExt();
                    HotelRoomsObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelRoomsObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    HotelRoomsObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelRoomsObj.RoomTypeName = Convert.ToString(dr["RoomTypeName"]);
                    HotelRoomsObj.RoomSize = Convert.ToInt32(dr["RoomSize"]);
                    HotelRoomsObj.MaxPeopleCount = Convert.ToInt32(dr["MaxPeopleCount"]);
                    HotelRoomsObj.IDWithMaxPeopleCount = Convert.ToString(dr["IDWithMaxPeopleCount"]);
                    HotelRoomsObj.MaxChildrenCount = Convert.ToInt32(dr["MaxChildrenCount"]);
                    HotelRoomsObj.BabyCotCount = Convert.ToInt32(dr["BabyCotCount"]);
                    HotelRoomsObj.ExtraBedCount = Convert.ToInt32(dr["ExtraBedCount"]);
                    HotelRoomsObj.SmokingTypeID = Convert.ToString(dr["SmokingTypeID"]);

                    HotelRoomsObj.SmokingTypeName = Convert.ToString(dr["SmokingTypeName"]);
                    HotelRoomsObj.ViewTypeID = Convert.ToString(dr["ViewTypeID"]);
                    HotelRoomsObj.ViewTypeName = Convert.ToString(dr["ViewTypeName"]);
                    HotelRoomsObj.IncludedInRoomTypeCaption = Convert.ToString(dr["IncludedInRoomTypeCaption"]);

                    Encryption64 objEncryptHotelRoomID = new Encryption64();
                    string EncryptHotelRoomID = Convert.ToString(dr["ID"]);
                    EncryptHotelRoomID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptHotelRoomID.Encrypt(EncryptHotelRoomID, "58421043")));
                    HotelRoomsObj.EncryptHotelRoomID = EncryptHotelRoomID;

                    HotelRoomsList.Add(HotelRoomsObj);
                }
            }
            return HotelRoomsList;
        }



        public bool CloseOpenHotelAvailability(DateTime StartDate, DateTime EndDate, bool Closed, int HotelID, string HotelRoomID = "")
        {
            bool status = false;

            //  int HotelRoomIDs = Convert.ToInt32(HotelRoomID);
            try
            {
                if (HotelRoomID != "")
                {
                    int HotelRoomIDs = Convert.ToInt32(HotelRoomID);
                    var hotelAvailability = from availability in db.TB_HotelAvailability
                                            join hotelRoom in db.TB_HotelRoom
                                            on availability.HotelRoomID equals hotelRoom.ID
                                            join date in db.TB_Date
                                            on availability.DateID equals date.ID
                                            where date.Date >= StartDate && date.Date <= EndDate && hotelRoom.HotelID == HotelID && availability.HotelRoomID == HotelRoomIDs
                                            select availability;

                    foreach (var items in hotelAvailability)
                    {

                        int hotelAvailabilityIDValue = Convert.ToInt32(items.ID);
                        // var ID = items.ID;
                        var obj = db.TB_HotelAvailability.Where(x => x.ID == hotelAvailabilityIDValue).FirstOrDefault();
                        //db.TB_HotelRoomAttribute.Remove(obj);
                        obj.Closed = Closed;

                    }
                    db.SaveChanges();
                    status = true;
                }
                else if (HotelRoomID == "")
                {

                    var hotelAvailability = from availability in db.TB_HotelAvailability
                                            join hotelRoom in db.TB_HotelRoom
                                            on availability.HotelRoomID equals hotelRoom.ID
                                            join date in db.TB_Date
                                            on availability.DateID equals date.ID
                                            where date.Date >= StartDate && date.Date <= EndDate && hotelRoom.HotelID == HotelID
                                            select availability;

                    foreach (var items in hotelAvailability)
                    {
                        //availability = availability_loopVariable;
                        //availability.availability.Closed = Closed;

                        int hotelAvailabilityIDValue = Convert.ToInt32(items.ID);
                        // var ID = items.ID;
                        var obj = db.TB_HotelAvailability.Where(x => x.ID == hotelAvailabilityIDValue).FirstOrDefault();
                        //db.TB_HotelRoomAttribute.Remove(obj);
                        obj.Closed = Closed;
                    }
                    db.SaveChanges();
                    status = true;
                }
            }

            catch (Exception ex)
            {

            }


            return status;
        }

        public void AddUserOperation(Controller ctrl, int OperationTypeID, string PartID = "", string RecordID = "", string UserSessionID = "")
        {
            bool status = false;
            try
            {
                BizTbl_UserOperation UserObjRate = new BizTbl_UserOperation();
                UserObjRate.UserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
                UserObjRate.Date = DateTime.Now;
                UserObjRate.OperationTypeID = 100;
                UserObjRate.IPAddress = ObjCommon.GetIPAddress();
                UserObjRate.PartID = 1;
                UserObjRate.RecordID = Convert.ToInt64(RecordID);
                UserObjRate.UserSessionID = Convert.ToInt64(UserSessionID);
                db.BizTbl_UserOperation.Add(UserObjRate);
                db.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {

            }
        }

        public static object CheckEmptyStringDBParameter(object Value, bool ReturnInteger = false, bool ReturnDate = false, bool ReturnDouble = false, bool ReturnDecimal = false, bool ReturnBoolean = false, bool ReturnLong = false)
        {

            if (Value == string.Empty)
            {
                return null;
            }

            if (ReturnInteger)
            {
                return Convert.ToInt32(Value);
            }

            //if (ReturnDate)
            //{

            //    return DateTime.ParseExact(Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            //}

            if (ReturnDouble)
            {
                return Convert.ToDouble(Value);
            }

            if (ReturnDecimal)
            {
                return Convert.ToDecimal(Value);
            }

            if (ReturnBoolean)
            {
                return Convert.ToBoolean(Value);
            }

            if (ReturnLong)
            {
                return Convert.ToInt64(Value);
            }

            return Value;

        }

        public class Encryption64
        {

            private byte[] key = { };
            //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            public string Encrypt(string stringToEncrypt, string sEncryptionKey)
            {
                try
                {
                    key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    Byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV),
                                                                      CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
    }


    public class HotelRateAndAvailabilityExt
    {
        public int ID { get; set; }
        public Int64 DateID { get; set; }
        public DateTime Date { get; set; }
        public int DayID { get; set; }
        public int Day { get; set; }
        public int WeekDay { get; set; }
        public string DayName { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int HotelRoomID { get; set; }
        public int HotelID { get; set; }
        public int AvailableRoomCount { get; set; }
        public int RoomCount { get; set; }
        public int CloseToArrival { get; set; }
        public int CloseToDeparture { get; set; }
        public int MinimumStay { get; set; }
        public int Closed { get; set; }
        public int RoomRateMissing { get; set; }
        public int RoomTypeID { get; set; }

        public string RoomTypeName { get; set; }
        public int RoomSize { get; set; }
        public int MaxPeopleCount { get; set; }
        public string IDWithMaxPeopleCount { get; set; }
        public int MaxChildrenCount { get; set; }

        public int BabyCotCount { get; set; }

        public int ExtraBedCount { get; set; }

        public string SmokingTypeID { get; set; }
        public string SmokingTypeName { get; set; }

        public string ViewTypeID { get; set; }
        public string ViewTypeName { get; set; }
        public string IncludedInRoomTypeCaption { get; set; }


        public string hotelRoomAvailabilityText { get; set; }

        public string lbtnAvailableRoomCount { get; set; }

        public string HotelAvailableStatus { get; set; }

        public string EncryptHotelRoomID { get; set; }
    }
}