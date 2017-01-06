using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_HotelHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelHistoryExt> list = new List<TB_HotelHistoryExt>();

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
                    TB_HotelHistoryExt model = new TB_HotelHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelID = Convert.ToInt32(dr["HotelID"]);
                    model.Firm = dr["FK_FirmID"].ToString();
                    model.Room = dr["FK_HotelTypeID"].ToString();
                    model.HotelClass = dr["FK_HotelClassID"].ToString();
                    model.HotelChain = dr["FK_HotelChainID_ID"].ToString();
                    model.HotelAccommodationType = dr["FK_HotelAccommodationTypeID_ID"].ToString();
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
                    model.CreateDate = Convert.ToDateTime(dr["CreateDateTime"]);
                    model.CreateUser = dr["CreateUserID"].ToString();
                    model.OperationDate = dr["OpDateTime"].ToString();
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"].ToString());
                    model.LogUser = dr["FK_LogUserID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_HotelHistoryExt
    {
        public int ID { get; set; }
        public int HotelID { get; set; }
        public string Firm { get; set; }
        public string Room { get; set; }  
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
        public string Currency { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MapZoomIndex { get; set; }
        public string ClosestAirport { get; set; }
        public string ClosestAirportDistance { get; set; }
        public string ShowOffline { get; set; }
        public string CreditCardNotRequired { get; set; }
        public string Culture { get; set; }
        public string RoutingName { get; set; }
        public string ChannelManager { get; set; }
        public string Active { get; set; }
        public string IPAddress { get; set; }
        public DateTime CreateDate { get; set; } 
        public string CreateUser { get; set; }
        public string OperationDate { get; set; } 
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}