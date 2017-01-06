using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelSearchParameterRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelSearchParameterExt> ReadAll(int TableID)
        {
            List<TB_HotelSearchParameterExt> list = new List<TB_HotelSearchParameterExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTableNew_BizTbl_Table_Sp", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TableID", TableID);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TB_HotelSearchParameterExt PageObj = new TB_HotelSearchParameterExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.Culture = dr["Culture"].ToString();
                    PageObj.UserCountry = dr["FK_UserCountryID_ID"].ToString();
                    PageObj.CountryID = dr["FK_CountryID_ID"].ToString();
                    PageObj.City = dr["FK_CityID_ID"].ToString();
                    PageObj.RegionIDs = dr["RegionIDs"].ToString();
                    PageObj.RegionNames = dr["RegionNames"].ToString();
                    PageObj.CheckInDate = dr["CheckInDate"].ToString();
                    PageObj.CheckOutDate = dr["CheckOutDate"].ToString();
                    PageObj.RoomCount = dr["RoomCount"].ToString();
                    PageObj.GuestCount = dr["GuestCount"].ToString();
                    PageObj.LowerUSDPrice = dr["LowerUSDPrice"].ToString();
                    PageObj.UpperUSDPrice = dr["UpperUSDPrice"].ToString();
                    PageObj.Date = dr["Date"].ToString();
                 
                    list.Add(PageObj);
                }
            }


            return list;
        }


    }
    public class TB_HotelSearchParameterExt
    {
        public int ID { get; set; }
        public string RoomCount { get; set; }
        public string Culture { get; set; }
        public string UserCountry { get; set; }
        public string CountryID { get; set; }
        public string City { get; set; }
        public string RegionIDs { get; set; }
        public string RegionNames { get; set; }
        public string CheckOutDate { get; set; }
        public string CheckInDate { get; set; }
        public string GuestCount { get; set; }
        public string LowerUSDPrice { get; set; }
        public string UpperUSDPrice { get; set; }
        public string Date { get; set; }
    }

}