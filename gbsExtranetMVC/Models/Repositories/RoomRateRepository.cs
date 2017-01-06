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
    public class RoomRateRepository : BaseRepository
    {
        CommonRepository ObjCommon = new CommonRepository();

        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        DataTable HotelRooms = new DataTable();
        DataTable Dates = new DataTable();

        List<RoomRateExt> DateList = new List<RoomRateExt>();
        
        public DataTable GetDates(string OrderBy, string startDate, string endDate)
        {

            CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            DateTime dtStart = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtEnd = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetDates", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(Dates);
            SQLCon.Close();
            return Dates;
        }

        public List<RoomRateExt> GetDatesList()
        {
            if (Dates.Rows.Count > 0)
            {
                foreach (DataRow dr in Dates.Rows)
                {
                    RoomRateExt DatesObj = new RoomRateExt();
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
        public void CreateHotelRoomRate(string HotelRoomID, string StartDate, string EndDate, string HotelAccommodationTypeID, string PricePolicyTypeID)
        {
            DateTime dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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

        public DataTable GetHotelRate(int HotelID, string StartDate, string EndDate, string HotelAccommodationTypeID, string PricePolicyTypeID)
        {
            DateTime dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
           
            return dt;
        }

        public DataTable GetHotelCancelPolicy(int HotelID)
        {
            SQLCon.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelCancelPolicy", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            return dt;
        }


        public void SaveHotelRate(int HotelID, string HotelRoomID, string DateIDArray, string PricePolicy, string AccommodationType, string SinglePriceArray, string DoublePriceArray, string RoomPriceArray, int MaxPeopleCount, long UserSessionID, DateTime Date, Controller ctrl)
        {

            string[] DateID = DateIDArray.Split(',');
            string[] SinglePrice = SinglePriceArray.Split(',');
            string[] DoublePrice = DoublePriceArray.Split(',');
            string[] RoomPrice = RoomPriceArray.Split(',');

            for (int i = 0; i < DateID.Length; i++)
            {
                //  string HotelRoomID = RoomID[i];                

                int dateId = Convert.ToInt32(DateID[i]);
                int RoomID = Convert.ToInt32(HotelRoomID);
                int PricePolicyTypeID = Convert.ToInt32(PricePolicy);
                int HotelAccommodationTypeID = Convert.ToInt32(AccommodationType);

                var HotelRate = db.TB_HotelRate.Where(x => x.DateID == dateId && x.HotelRoomID == RoomID && x.PricePolicyTypeID == PricePolicyTypeID && x.HotelAccommodationTypeID == HotelAccommodationTypeID).FirstOrDefault();

                if (SinglePrice[i] != "")
                {
                    HotelRate.SinglePrice = Convert.ToDecimal(SinglePrice[i]);
                }
                if (MaxPeopleCount > 1)
                {
                    if (DoublePrice[i] != "")
                    {
                        HotelRate.DoublePrice = Convert.ToDecimal(DoublePrice[i]);
                    }
                }
                if (MaxPeopleCount > 2)
                {
                    if (RoomPrice[i] != "")
                    {
                        HotelRate.RoomPrice = Convert.ToDecimal(RoomPrice[i]);
                    }
                }

                HotelRate.OpDateTime = Date;
                HotelRate.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                db.SaveChanges();
                int id = HotelRate.ID;
            }

            BizTbl_UserOperation UserObjRate = new BizTbl_UserOperation();
            UserObjRate.UserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            UserObjRate.Date = DateTime.Now;
            UserObjRate.OperationTypeID = 100;
            UserObjRate.IPAddress = ObjCommon.GetIPAddress();
            UserObjRate.PartID = 1;
            UserObjRate.RecordID = HotelID;
            UserObjRate.UserSessionID = UserSessionID;
            db.BizTbl_UserOperation.Add(UserObjRate);
            db.SaveChanges();

        }

        public void SaveHotelRoomRate(int HotelID, string HotelRoomID, string StartDate, string Enddate, string PricePolicy, string AccommodationType, double singlePrice, double doublePrice, double roomPrice, long UserSessionID, DateTime Date, Controller ctrl)
        {

            int HotelRoomIDValue = Convert.ToInt32(HotelRoomID);
            int PricePolicyTypeID = Convert.ToInt32(PricePolicy);
            int HotelAccommodationTypeID = Convert.ToInt32(AccommodationType);

            DateTime DStartdate = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime DEndDate = DateTime.ParseExact(Enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            SQLCon.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("B_GetRoomRate_TB_HotelRate_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Startdate", DStartdate);
            cmd.Parameters.AddWithValue("@EndDate", DEndDate);
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@PricePolicyTypeID", PricePolicyTypeID);
            cmd.Parameters.AddWithValue("@HotelAccommodationTypeID", HotelAccommodationTypeID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            //var hotelRoomRate = from roomRate in db.TB_HotelRate
            //                    join date in db.TB_Date
            //                    on roomRate.DateID equals date.ID
            //                    where roomRate.HotelRoomID == HotelRoomIDValue && roomRate.PricePolicyTypeID == PricePolicyTypeID && roomRate.HotelAccommodationTypeID == HotelAccommodationTypeID && date.Date >= DStartdate && date.Date <= DEndDate
            //                    select new { HotelRoomID = roomRate.HotelRoomID, SinglePrice = roomRate.SinglePrice, DoublePrice = roomRate.DoublePrice, RoomPrice = roomRate.RoomPrice };
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int HotelRoom = Convert.ToInt32(dr["HotelRoomID"]);
                        int PricePolicyType = Convert.ToInt32(dr["PricePolicyTypeID"]);
                        int HotelAccommodationType = Convert.ToInt32(dr["HotelAccommodationTypeID"]);
                        int IDDate = Convert.ToInt32(dr["ID"]);

                        var obj = db.TB_HotelRate.Where(x => x.HotelRoomID == HotelRoom && x.PricePolicyTypeID == PricePolicyType && x.HotelAccommodationTypeID == HotelAccommodationType && x.DateID == IDDate).FirstOrDefault();

                        if (singlePrice > 0)
                        {
                            obj.SinglePrice = Convert.ToDecimal(singlePrice);
                        }
                        if (doublePrice > 0)
                        {
                            obj.DoublePrice = Convert.ToDecimal(doublePrice);
                        }
                        if (roomPrice > 0)
                        {
                            obj.RoomPrice = Convert.ToDecimal(roomPrice);
                        }
                        obj.OpDateTime = Date;
                        obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                        int ID = obj.ID;
                        db.SaveChanges();
                    }
                }
            }
            BizTbl_UserOperation UserObjRate = new BizTbl_UserOperation();
            UserObjRate.UserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            UserObjRate.Date = DateTime.Now;
            UserObjRate.OperationTypeID = 100;
            UserObjRate.IPAddress = ObjCommon.GetIPAddress();
            UserObjRate.PartID = 1;
            UserObjRate.RecordID = HotelID;
            UserObjRate.UserSessionID = UserSessionID;
            db.BizTbl_UserOperation.Add(UserObjRate);
            db.SaveChanges();

        }

       public List<RoomRateExt> Getdates1(string Mode, string StartDate, string EndDate, DataTable RateTable, string RoomID, DataTable HotelRooms)
       {
            DateTime dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetDates]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Date");
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            // cmd.Parameters.AddWithValue("@WeekDayIDs", ReplaceComma(WeekDay));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<RoomRateExt> ListOfModel = new List<RoomRateExt>();
            RoomRateExt HotelObj = new RoomRateExt();
            string Roomname = "";
            if (Mode == "1")
            {


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (DataRow Room in HotelRooms.Rows)
                        {
                           
                            HotelObj.DayID = Convert.ToInt32(dr["DayID"]);
                            HotelObj.Day = Convert.ToInt32(dr["Day"]);
                            HotelObj.WeekDay = Convert.ToInt32(dr["WeekDay"]);
                            HotelObj.MonthID = Convert.ToInt32(dr["MonthID"]);
                            HotelObj.Year = Convert.ToInt32(dr["Year"]);
                            HotelObj.DateID = Convert.ToInt64(dr["ID"].ToString());
                            string DateID = dr["ID"].ToString();
                            HotelObj.Date = Convert.ToDateTime(dr["Date"].ToString());
                            HotelObj.DayName = dr["DayName"].ToString();
                            HotelObj.MonthName = dr["MonthName"].ToString();
                            string hotelRoomID = Room["ID"].ToString();
                            HotelObj.RoomTypeID = Convert.ToInt32(Room["RoomTypeID"]);
                            HotelObj.RoomTypeName = Convert.ToString(Room["RoomTypeName"]);
                            HotelObj.HotelRoomID = Convert.ToInt32(Room["ID"]);
                            HotelObj.MaxPeopleCount = Convert.ToInt32(Room["MaxPeopleCount"]);
                            if (RateTable.Rows.Count > 0)
                            {
                                foreach (DataRow drRate in RateTable.Rows)
                                {

                                    if (DateID == drRate["DateID"].ToString() && hotelRoomID == Convert.ToString(drRate["HotelRoomID"]) || hotelRoomID == RoomID)
                                    
                                           {
                                              HotelObj.txtSinglePrice = true;
                                           }
                                          else
                                           {
                                             HotelObj.txtSinglePrice = false;
                                           }
                                          if (HotelObj.txtSinglePrice == true)
                                           {
                                            HotelObj.txtDoublePrice = true;
                                           }
                                         else
                                          {
                                           HotelObj.txtDoublePrice = false;
                                           }
                                            if (HotelObj.txtSinglePrice == true)
                                            {
                                            HotelObj.txtRoomPrice = true;
                                           }
                                             else
                                            {
                                             HotelObj.txtRoomPrice = false;
                                           }
                                        
                                       
                                    }
                                }
                            }

                            ListOfModel.Add(HotelObj);
                        }
                    }
                }
            
            else if (Mode == "2")
            {
                foreach (DataRow Room in HotelRooms.Rows)
                {
                   // RoomRateExt HotelObj = new RoomRateExt();
                    string HotelRoomID = Room["ID"].ToString();
                    HotelObj.RoomTypeName = Convert.ToString(Room["RoomTypeName"]);

                    HotelObj.HotelRoomID = Convert.ToInt32(Room["ID"]);
                    HotelObj.MaxPeopleCount = Convert.ToInt32(Room["MaxPeopleCount"]);

                    if (HotelRoomID == Convert.ToString(RoomID) || Convert.ToString(RoomID) == string.Empty)
                    {
                        HotelObj.txtSinglePrice = true;
                    }
                    if (HotelObj.txtSinglePrice == true && HotelObj.MaxPeopleCount > 1)
                    {
                        HotelObj.txtDoublePrice = true;
                    }
                    if (HotelObj.txtSinglePrice == true && HotelObj.MaxPeopleCount > 2)
                    {
                        HotelObj.txtRoomPrice = true;
                    }

                    ListOfModel.Add(HotelObj);
                }
            }

            return ListOfModel;
        }

        //public DataTable GetHotelRooms(string OrderBy, int HotelID)
        //{
        //    SQLCon.Open();
        //    SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRooms", SQLCon);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Culture", CultureValue);
        //    cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
        //    cmd.Parameters.AddWithValue("@HotelID", HotelID);
        //    cmd.Parameters.AddWithValue("@Active", 1);

        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    sda.Fill(HotelRooms);
        //    SQLCon.Close();
        //    return HotelRooms;
        //}

        //public List<RoomRateExt> GetRoomHeaderList()
        //{

        //    List<RoomRateExt> HotelRoomsList = new List<RoomRateExt>();

        //    if (HotelRooms.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in HotelRooms.Rows)
        //        {
        //            RoomRateExt HotelRoomsObj = new RoomRateExt();
        //            HotelRoomsObj.ID = Convert.ToInt32(dr["ID"]);
        //            HotelRoomsObj.HotelID = Convert.ToInt32(dr["HotelID"]);
        //            HotelRoomsObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
        //            HotelRoomsObj.RoomTypeName = Convert.ToString(dr["RoomTypeName"]);
        //            HotelRoomsObj.RoomSize = Convert.ToInt32(dr["RoomSize"]);
        //            HotelRoomsObj.MaxPeopleCount = Convert.ToInt32(dr["MaxPeopleCount"]);
        //            HotelRoomsObj.IDWithMaxPeopleCount = Convert.ToString(dr["IDWithMaxPeopleCount"]);
        //            HotelRoomsObj.MaxChildrenCount = Convert.ToInt32(dr["MaxChildrenCount"]);
        //            HotelRoomsObj.BabyCotCount = Convert.ToInt32(dr["BabyCotCount"]);
        //            HotelRoomsObj.ExtraBedCount = Convert.ToInt32(dr["ExtraBedCount"]);
        //            HotelRoomsObj.SmokingTypeID = Convert.ToString(dr["SmokingTypeID"]);

        //            HotelRoomsObj.SmokingTypeName = Convert.ToString(dr["SmokingTypeName"]);
        //            HotelRoomsObj.ViewTypeID = Convert.ToString(dr["ViewTypeID"]);
        //            HotelRoomsObj.ViewTypeName = Convert.ToString(dr["ViewTypeName"]);
        //            HotelRoomsObj.IncludedInRoomTypeCaption = Convert.ToString(dr["IncludedInRoomTypeCaption"]);

        //            HotelRoomsList.Add(HotelRoomsObj);
        //        }
        //    }
        //    return HotelRoomsList;
        //}

    }
    public class RoomRateExt
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

        public string SinglePrice { get; set; }

        public string DoublePrice { get; set; }

        public string RoomPrice { get; set; }

        public bool txtSinglePrice { get; set; }

        public bool txtDoublePrice { get; set; }

        public bool txtRoomPrice { get; set; }

    }
}