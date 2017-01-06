using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class HotelSearchParameterRepository
    {
        public List<HotelSearchParameterExt> GetHotelSearchParameterDetails()
        {
            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Culture");
            dt.Columns.Add("UserCountry");
            dt.Columns.Add("Country");
            dt.Columns.Add("City");
            dt.Columns.Add("RegionID");
            dt.Columns.Add("RegionNames");
            dt.Columns.Add("CheckInDate");
            dt.Columns.Add("CheckOutDate");
            dt.Columns.Add("RoomCount");
            dt.Columns.Add("GuestCount");
            dt.Columns.Add("LowerUSDPrice");
            dt.Columns.Add("UpperUSDPrice");
            dt.Columns.Add("Date");
            var result = entity.Database.SqlQuery<GetHotelSearchParameter_Result>("B_Ex_GetHotelSearchParameter_TB_HotelSearchParameter_SP").ToList();
            List<HotelSearchParameterExt> ListOfModel = new List<HotelSearchParameterExt>();
            foreach (GetHotelSearchParameter_Result Val in result)
            {
                dt.Rows.Add(Val.ID,Val.Culture,Val.FK_UserCountryID_ID,Val.FK_CountryID_ID,Val.FK_CityID_ID,Val.RegionIDs,Val.RegionNames,Val.CheckInDate,Val.CheckOutDate,Val.RoomCount,Val.GuestCount,Val.LowerUSDPrice,Val.UpperUSDPrice,Val.Date);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HotelSearchParameterExt ParamObj = new HotelSearchParameterExt();
                    ParamObj.ID = Convert.ToInt32(dr["ID"]);
                    ParamObj.Culture = dr["Culture"].ToString();
                    ParamObj.UserCountry = dr["UserCountry"].ToString();
                    ParamObj.Country = dr["Country"].ToString();
                    ParamObj.City = dr["City"].ToString();
                    ParamObj.RegionID = dr["RegionID"].ToString();
                    ParamObj.RegionNames = dr["RegionNames"].ToString();
                    ParamObj.RoomCount = dr["RoomCount"].ToString();
                    ParamObj.GuestCount = dr["GuestCount"].ToString();
                    ParamObj.LowerUSDPrice = dr["LowerUSDPrice"].ToString();
                    ParamObj.UpperUSDPrice = dr["UpperUSDPrice"].ToString();
                    ParamObj.Date = Convert.ToDateTime(dr["Date"]);
                    ParamObj.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                    ParamObj.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);

                    ListOfModel.Add(ParamObj);
                }
            }

            return ListOfModel;
        }
    }

    public class HotelSearchParameterExt
    {
        public int ID { get; set; }
        public string Culture { get; set; }
        public string UserCountry { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string RegionID { get; set; }
        public string RegionNames { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomCount { get; set; }
        public string GuestCount { get; set; } 
        public string LowerUSDPrice { get; set; }
        public string UpperUSDPrice { get; set; }
        public DateTime Date { get; set; }

    }
}