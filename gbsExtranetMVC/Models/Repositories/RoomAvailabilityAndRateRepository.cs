using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;
using System.Globalization;
using Business;

namespace gbsExtranetMVC.Models.Repositories
{
    public class RoomAvailabilityAndRateRepository : BaseRepository
    {
        CommonRepository ObjCommon = new CommonRepository();
            
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public string ReplaceComma(string text)
        {
            string value = "";
            if (text.Contains(','))
            {
                value = text.Replace(",", ";");
                value = value + ";";
            }
            else
            {
                value = text;
            }
            return value;
        }
        public void CreateHotelRoomAvailability(string HotelRoomID, string StartDate, string EndDate, RoomAvailabilityAndRateRepositoryExt RoomCount)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_CreateHotelRoomAvailability]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            cmd.Parameters.AddWithValue("@RoomCount", RoomCount.RoomCount);
            cmd.ExecuteNonQuery();
                 SQLCon.Close();
        }
        public void CreateHotelRoomRate(string HotelRoomID, string StartDate, string EndDate, string HotelAccommodationTypeID, string PricePolicyTypeID)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_CreateHotelRoomRate]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            cmd.Parameters.AddWithValue("@HotelAccommodationTypeID", HotelAccommodationTypeID);
            cmd.Parameters.AddWithValue("@PricePolicyTypeID", PricePolicyTypeID);
            cmd.ExecuteNonQuery();
            SQLCon.Close();
        }

        public List<RoomAvailabilityAndRateRepositoryExt> GetHotelRooms(int HotelID)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<RoomAvailabilityAndRateRepositoryExt> ListOfModel = new List<RoomAvailabilityAndRateRepositoryExt>();
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //if (Convert.ToInt32(dr["ID"]) == RoomID)
                    //{
                    RoomAvailabilityAndRateRepositoryExt HotelObj = new RoomAvailabilityAndRateRepositoryExt();
                    HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelObj.MaxPeopleCount = Convert.ToInt32(dr["MaxPeopleCount"]);
                    HotelObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                    HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    ListOfModel.Add(HotelObj);
                    //}
                }
            }
            return ListOfModel;
        }
        public List<RoomAvailabilityAndRateRepositoryExt> Getdates(string StartDate, string EndDate, string WeekDay, DataTable AvailabilityTable, DataTable RateTable, int RoomID, int MaximamPeopleCount)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetDates]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "Date");
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            cmd.Parameters.AddWithValue("@WeekDayIDs", ReplaceComma(WeekDay));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<RoomAvailabilityAndRateRepositoryExt> ListOfModel = new List<RoomAvailabilityAndRateRepositoryExt>();
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    RoomAvailabilityAndRateRepositoryExt HotelObj = new RoomAvailabilityAndRateRepositoryExt();
                    HotelObj.DayID = Convert.ToInt32(dr["DayID"]);
                    HotelObj.Day = Convert.ToInt32(dr["Day"]);
                    HotelObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                    HotelObj.MonthID = Convert.ToInt32(dr["MonthID"]);
                    HotelObj.Year = Convert.ToInt32(dr["Year"]);
                    HotelObj.DateID = dr["ID"].ToString();
                    string DateID = dr["ID"].ToString();
                    HotelObj.Date = dr["Date"].ToString();
                    HotelObj.DayName = dr["DayName"].ToString();
                    HotelObj.MonthName = dr["MonthName"].ToString();
                    HotelObj.MaxPeopleCount = MaximamPeopleCount;
                    if (RateTable.Rows.Count > 0)
                    {
                        foreach (DataRow drRate in RateTable.Rows)
                        {
                            if (DateID == drRate["DateID"].ToString() && RoomID == Convert.ToInt32(drRate["HotelRoomID"]))
                            {
                                HotelObj.SinglePrice = Convert.ToDecimal(drRate["SinglePrice"]);
                                HotelObj.DoublePrice = Convert.ToDecimal(drRate["DoublePrice"]);
                                HotelObj.RoomPrice = Convert.ToDecimal(drRate["RoomPrice"]);
                                int AvailableRoomCount = Convert.ToInt32(drRate["AvailableRoomCount"]);
                                bool Closed = Convert.ToBoolean(drRate["Closed"]);
                                bool RoomRateMissing = Convert.ToBoolean(drRate["RoomRateMissing"]);
                                if (Closed)
                                {
                                    HotelObj.CssClass = "Closed";
                                }
                                else if (AvailableRoomCount == 0)
                                {
                                    HotelObj.CssClass = "Full";
                                }
                                else if (RoomRateMissing)
                                {
                                    HotelObj.CssClass = "RateMissing";
                                }
                                else
                                {
                                    HotelObj.CssClass = "";
                                }
                            }
                        }
                    }
                    if (AvailabilityTable.Rows.Count > 0)
                    {
                        foreach (DataRow drAvail in AvailabilityTable.Rows)
                        {
                            if (DateID == drAvail["DateID"].ToString() && RoomID == Convert.ToInt32(drAvail["HotelRoomID"]))
                            {
                                HotelObj.AvailableRoomCount = Convert.ToInt32(drAvail["AvailableRoomCount"]);
                               
                                HotelObj.MinimumStay = Convert.ToInt32(drAvail["MinimumStay"]);
                                HotelObj.RoomRateMissing = Convert.ToBoolean(drAvail["RoomRateMissing"]);
                                HotelObj.CloseToArrival = Convert.ToBoolean(drAvail["CloseToArrival"]);
                                HotelObj.CloseToDeparture = Convert.ToBoolean(drAvail["CloseToDeparture"]);
                                HotelObj.Closed = Convert.ToBoolean(drAvail["Closed"]);

                                
                            }
                        }
                    }
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }

        public bool SaveRoomAvailabilityAndRate(int AccommodationType, int PricePolicy, int RoomID, int MaximamPeopleCount, string DateIDArray, string SinglePriceNameArray, string DoublePriceNameArray, string RoomPriceNameArray,
            string AvailableRoomCountNameArray, string MinimumStayNameArray, string CloseToArrivalArray, string CloseToDepartureArray, string ClosedArray,long UserSessionID, Controller ctrl)
       
        {

            bool status = true;
              string [] DateID= DateIDArray.Split(',');
              string [] SinglePrice=SinglePriceNameArray.Split(',');
              string [] DoublePrice=DoublePriceNameArray.Split(',');
              string [] RoomPrice=RoomPriceNameArray.Split(',');
              string [] AvailableRoomCount=AvailableRoomCountNameArray.Split(',');
              string [] MinimumStay=MinimumStayNameArray.Split(',');
              string [] CloseToArrival=CloseToArrivalArray.Split(',');
              string [] CloseToDeparture=CloseToDepartureArray.Split(',');
              string [] Closed=ClosedArray.Split(',');
            //using (DBEntities DE = new DBEntities())
            //{
            for(int i=0;i<DateID.Length;i++)
            {
                int dateId=Convert.ToInt32(DateID[i]);
                var AvailabilityTable = db.TB_HotelAvailability.Where(x => x.DateID == dateId && x.HotelRoomID == RoomID).FirstOrDefault();
                if (AvailableRoomCount[i] != "")
                {
                    AvailabilityTable.RoomCount = Convert.ToInt32(AvailableRoomCount[i]);
                }
                AvailabilityTable.CloseToArrival = Convert.ToBoolean(Convert.ToInt32(CloseToArrival[i]));
                AvailabilityTable.CloseToDeparture = Convert.ToBoolean(Convert.ToInt32(CloseToDeparture[i]));
                AvailabilityTable.Closed = Convert.ToBoolean(Convert.ToInt32(Closed[i]));
                if(MinimumStay[i]=="")
                {
                    AvailabilityTable.MinimumStay = 1;
                }
                else
                {
                    AvailabilityTable.MinimumStay = Convert.ToInt32(MinimumStay[i]);
                }
                AvailabilityTable.OpDateTime = DateTime.Now;
                AvailabilityTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

                var RateTable = db.TB_HotelRate.Where(x => x.DateID == dateId && x.HotelRoomID == RoomID && x.PricePolicyTypeID == PricePolicy && x.HotelAccommodationTypeID == AccommodationType).FirstOrDefault();
                if (SinglePrice[i] != "")
                {
                    RateTable.SinglePrice = Convert.ToDecimal(SinglePrice[i]);
                }
                if (MaximamPeopleCount > 1)
                {
                    if (DoublePrice[i] != "")
                    {
                        RateTable.DoublePrice = Convert.ToDecimal(DoublePrice[i]);
                    }
                }
                if (MaximamPeopleCount>2)
                {
                    if (RoomPrice[i] != "")
                    {
                        RateTable.RoomPrice = Convert.ToDecimal(RoomPrice[i]);
                    }
                }
                RateTable.OpDateTime = DateTime.Now;
                RateTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                db.SaveChanges();

               
            }
            BizTbl_UserOperation UserObjAvail = new BizTbl_UserOperation();
            UserObjAvail.UserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            UserObjAvail.Date = DateTime.Now;
            UserObjAvail.OperationTypeID = 101;
            UserObjAvail.IPAddress = ObjCommon.GetIPAddress();
            UserObjAvail.PartID = 5;
            UserObjAvail.RecordID = RoomID;
            UserObjAvail.UserSessionID = UserSessionID;
            db.BizTbl_UserOperation.Add(UserObjAvail);
            db.SaveChanges();

            BizTbl_UserOperation UserObjRate = new BizTbl_UserOperation();
            UserObjRate.UserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            UserObjRate.Date = DateTime.Now;
            UserObjRate.OperationTypeID = 100;
            UserObjRate.IPAddress = ObjCommon.GetIPAddress();
            UserObjRate.PartID = 5;
            UserObjRate.RecordID = RoomID;
            UserObjRate.UserSessionID = UserSessionID;
            db.BizTbl_UserOperation.Add(UserObjRate);
            db.SaveChanges();  
            //}
               return status;
        }
        public DataTable GetHotelAvailability(int HotelID, string StartDate, string EndDate)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelAvailability]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            //cmd.Parameters.AddWithValue("@HotelRoomID", "RoomID");
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            //List<RoomAvailabilityAndRateRepositoryExt> ListOfModel = new List<RoomAvailabilityAndRateRepositoryExt>();
            //string Roomname = "";

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        RoomAvailabilityAndRateRepositoryExt HotelObj = new RoomAvailabilityAndRateRepositoryExt();
            //        HotelObj.HotelAvailabityID = Convert.ToInt32(dr["ID"]);
            //        HotelObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
            //        HotelObj.RoomID = Convert.ToInt32(dr["HotelRoomID"]);
            //        HotelObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
            //        HotelObj.DateID = dr["DateID"].ToString();
            //        HotelObj.Date = dr["Date"].ToString();
            //        HotelObj.AvailableRoomCount = Convert.ToInt32(dr["AvailableRoomCount"]);
            //        HotelObj.MinimumStay = Convert.ToInt32(dr["MinimumStay"]);
            //        HotelObj.RoomRateMissing = Convert.ToBoolean(dr["RoomRateMissing"]);
            //        HotelObj.CloseToArrival = Convert.ToBoolean(dr["CloseToArrival"]);
            //        HotelObj.CloseToDeparture = Convert.ToBoolean(dr["CloseToDeparture"]);
            //        HotelObj.Closed = Convert.ToBoolean(dr["Closed"]);
                    
            //        ListOfModel.Add(HotelObj);
            //    }
            // }
            return dt;
        }

        public DataTable GetHotelRate(int HotelID, string StartDate, string EndDate, string HotelAccommodationTypeID, string PricePolicyTypeID)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now.AddDays(30);
            if (StartDate.Contains('.'))
            {
                dtStart = DateTime.ParseExact(StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (EndDate.Contains('.'))
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRate]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            cmd.Parameters.AddWithValue("@HotelAccommodationTypeID", HotelAccommodationTypeID);
            cmd.Parameters.AddWithValue("@PricePolicyTypeID", PricePolicyTypeID);
            //cmd.Parameters.AddWithValue("@HotelRoomID", "RoomID");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            //List<RoomAvailabilityAndRateRepositoryExt> ListOfModel = new List<RoomAvailabilityAndRateRepositoryExt>();
            //string Roomname = "";

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        RoomAvailabilityAndRateRepositoryExt HotelObj = new RoomAvailabilityAndRateRepositoryExt();
            //        HotelObj.HotelRateID = Convert.ToInt32(dr["ID"]);
            //        HotelObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
            //        HotelObj.DateID = dr["DateID"].ToString();
            //        HotelObj.Date = dr["Date"].ToString();
            //        HotelObj.RoomID = Convert.ToInt32(dr["HotelRoomID"]);
            //        HotelObj.AvailableRoomCount = Convert.ToInt32(dr["AvailableRoomCount"]);
            //        HotelObj.RoomCount = Convert.ToInt32(dr["RoomCount"]);
            //        HotelObj.SinglePrice = Convert.ToDecimal(dr["SinglePrice"]);
            //        HotelObj.DoublePrice = Convert.ToDecimal(dr["DoublePrice"]);
            //        HotelObj.RoomPrice = Convert.ToDecimal(dr["RoomPrice"]);
            //        HotelObj.RoomRateMissing = Convert.ToBoolean(dr["RoomRateMissing"]);
            //        HotelObj.Closed = Convert.ToBoolean(dr["Closed"]);
            //        ListOfModel.Add(HotelObj);
            //    }
            //}
            return dt;
        }
    }


    public class RoomAvailabilityAndRateRepositoryExt
    {
        public int RoomID { get; set; }
        public int RoomTypeID { get; set; }
        public int MaxPeopleCount { get; set; }
        public int RoomCount { get; set; }
        public string RoomTypeName { get; set; }

        public string CssClass { get; set; }

        //For Date
        public string DateID { get; set; }
        public string Date { get; set; }
        public int DayID { get; set; }
        public int Day { get; set; }
        public int WeekDay { get; set; }
        public string DayName { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }

        //For HotelAvailability
        public int HotelAvailabityID { get; set; }
        public int AvailableRoomCount { get; set; }
        public int MinimumStay { get; set; }
        public bool RoomRateMissing { get; set; }
        public bool CloseToArrival { get; set; }
        public bool CloseToDeparture { get; set; }
        public bool Closed { get; set; }
        public int HotelRateID { get; set; }

        public int PricePolicyTypeID { get; set; }
        public decimal SinglePrice { get; set; }
        public decimal DoublePrice { get; set; }
        public decimal RoomPrice { get; set; }
    }
}