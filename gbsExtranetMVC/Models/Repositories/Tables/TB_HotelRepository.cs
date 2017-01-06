using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_HotelExt> ReadAll(int TableID)
        {
            List<TB_HotelExt> list = new List<TB_HotelExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTable_BizTbl_Table_Sp", SQLCon);
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
                    TB_HotelExt model = new TB_HotelExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Firm = dr["FK_FirmID"].ToString();
                    model.HotelType = dr["FK_HotelTypeID"].ToString();
                    model.HotelClass = dr["FK_HotelClassID"].ToString();
                    model.HotelChain = dr["FK_HotelChainID"].ToString();
                    model.HotelAccommodationType = dr["FK_HotelAccommodationTypeID"].ToString();
                    model.Country = dr["FK_CountryID"].ToString();
                    model.City = dr["FK_CityID"].ToString();
                    model.MainRegion = dr["FK_MainRegionID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.Addres = dr["Address"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.Fax = dr["Fax"].ToString();
                    model.PostCode = dr["PostCode"].ToString();
                    model.RoomCount = dr["RoomCount"].ToString();
                    model.WebAddress = dr["WebAddress"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.CheckinStartTime = dr["CheckinStart"].ToString();
                    model.CheckinEndTime = dr["CheckinEnd"].ToString();
                    model.CheckoutStartTime = dr["CheckoutStart"].ToString();
                    model.CheckoutEndTime = dr["CheckoutEnd"].ToString();
                    model.FloorCount = dr["FloorCount"].ToString();
                    model.BuiltYear = dr["BuiltYear"].ToString();
                    model.RenovationYear = dr["RenovationYear"].ToString();
                    model.HitCount = dr["HitCount"].ToString();
                    model.Sort = dr["Sort"].ToString();
                    model.Status = dr["FK_StatusID"].ToString();
                    model.Preferred = dr["IsPreferred"].ToString();
                    model.MainPageDisplay = dr["IsMainPageDisplay"].ToString();
                    model.Currency = dr["FK_CurrencyID"].ToString();
                    model.Latitude = dr["Latitude"].ToString();
                    model.Longitude = dr["Longitude"].ToString();
                    model.MapZoomIndex = dr["MapZoomIndex"].ToString();
                    model.ClosestAirport = dr["FK_ClosestAirportID"].ToString();
                    model.ClosestAirportDistance = dr["ClosestAirportDistance"].ToString();
                    model.ShowOffline = dr["ShowOffline"].ToString();
                    model.CreditCardNotRequired = dr["CreditCardNotRequired"].ToString();
                    model.Culture = dr["FK_CultureID"].ToString();
                    model.RoutingName = dr["RoutingName"].ToString();
                    model.ChannelManager = dr["FK_ChannelManagerID"].ToString();
                    model.Active = dr["Active"].ToString();
                    model.IPAddress = dr["IPAddress"].ToString();                  
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_HotelExt
    {
        public int ID { get; set; }
        public string Firm { get; set; }
        public string HotelType { get; set; }
        public string HotelClass { get; set; }
        public string HotelChain { get; set; }
        public string HotelAccommodationType { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string MainRegion { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PostCode { get; set; }
        public string RoomCount { get; set; }
        public string WebAddress { get; set; }
        public string Email { get; set; }
        public string CheckinStartTime { get; set; }
        public string CheckinEndTime { get; set; }
        public string CheckoutStartTime { get; set; }
        public string CheckoutEndTime { get; set; }
        public string FloorCount { get; set; }
        public string BuiltYear { get; set; }
        public string RenovationYear { get; set; }
        public string HitCount { get; set; }
        public string Sort { get; set; }
        public string Status { get; set; }
        public string Preferred { get; set; }
        public string MainPageDisplay	 { get; set; }
        public string Currency { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MapZoomIndex { get; set; }
        public string ClosestAirport { get; set; }
        public string ClosestAirportDistance { get; set; }
        public string ShowOffline { get; set; }
        public string CreditCardNotRequired	 { get; set; }
        public string Culture { get; set; }
        public string RoutingName { get; set; }
        public string ChannelManager { get; set; }
        public string Active { get; set; }
        public string IPAddress { get; set; }
    }
}