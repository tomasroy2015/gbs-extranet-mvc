using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class PropertyOperationsRepository : BaseRepository
    {
        public string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        CommonRepository ObjCommon = new CommonRepository();
        public List<HotelExt> GetHotels()
          {
        //    DBEntities entity = new DBEntities();
        //    var Culture = new SqlParameter("@Culture", CultureValue);
        //    var OrderBy = new SqlParameter("@OrderBy", "Name ASC,ID ASC");
        //    var PagingSize = new SqlParameter("@PagingSize", int.MaxValue);
        //    var PageIndex = new SqlParameter("@PageIndex", 1);
        //    var result = entity.Database.SqlQuery<GetHotels_Result5>("B_Ex_GetHotels_TB_Hotel_SP @Culture,@OrderBy,@PagingSize,@PageIndex", Culture, OrderBy, PagingSize, PageIndex).ToList();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("ID");
        //    dt.Columns.Add("Name");
        //    dt.Columns.Add("FirmName");
        //    dt.Columns.Add("HotelTypeName");
        //    dt.Columns.Add("HotelClassName");
        //    dt.Columns.Add("HotelAccommodationTypeName");
        //    dt.Columns.Add("HotelChainName");
        //    dt.Columns.Add("CountryName");
        //    dt.Columns.Add("RegionName");
        //    dt.Columns.Add("CityName");
        //    dt.Columns.Add("MainRegionName");
        //    dt.Columns.Add("Address");
        //    dt.Columns.Add("PostCode");
        //    dt.Columns.Add("Phone");
        //    dt.Columns.Add("Fax");
        //    dt.Columns.Add("Email");
        //    dt.Columns.Add("WebAddress");
        //    dt.Columns.Add("StatusName");
        //    dt.Columns.Add("Active");
        //    dt.Columns.Add("CreateDateTime");
        //    dt.Columns.Add("OpDateTime");
        //    dt.Columns.Add("FirmID");
        //    dt.Columns.Add("CountryID");
        //    dt.Columns.Add("RegionID");
        //    dt.Columns.Add("CityID");
        //    dt.Columns.Add("CurrencyID");
        //    dt.Columns.Add("RoutingName");
        //    dt.Columns.Add("HotelAccommodationTypeID");
        //    dt.Columns.Add("AvailabilityRateUpdate");
        //    dt.Columns.Add("Latitude");
        //    dt.Columns.Add("Longitude");
        //    dt.Columns.Add("ParentRegionID");
        //    foreach (GetHotels_Result5 dr in result)
        //    {
        //        dt.Rows.Add(dr.ID, dr.Name, dr.FirmName, dr.HotelTypeName, dr.HotelClassName, dr.HotelAccommodationTypeName, dr.HotelChainName,
        //            dr.CountryName, dr.RegionName, dr.CityName, dr.MainRegionName, dr.Address, dr.PostCode, dr.Phone,dr.Fax,dr.Email,
        //            dr.WebAddress,dr.StatusName,dr.Active,dr.CreateDateTime,dr.OpDateTime,dr.FirmID,dr.CountryID,dr.RegionID,dr.CityID,dr.CurrencyID,
        //            dr.RoutingName,dr.HotelAccommodationTypeID,dr.AvailabilityRateUpdate,dr.Latitude,dr.Longitude,dr.ParentRegionID);
        //    }

              DataTable dt = new DataTable();
              SQLCon.Open();
              SqlCommand cmd = new SqlCommand("B_Ex_GetHotels_TB_Hotel_SP", SQLCon);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@Culture", CultureValue);
              cmd.Parameters.AddWithValue("@OrderBy", "Name ASC,ID ASC");
              cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
              cmd.Parameters.AddWithValue("@PageIndex", 1);
              SqlDataAdapter sda = new SqlDataAdapter(cmd);
              sda.Fill(dt);
              SQLCon.Close();

            List<HotelExt> ListOfModel = new List<HotelExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HotelExt HotelObj = new HotelExt();
                    gbsExtranetMVC.Models.Repositories.FirmRequestsRepository.Encryption64 objEncryptreservation = new gbsExtranetMVC.Models.Repositories.FirmRequestsRepository.Encryption64();
                    HotelObj.EncryptHotelID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(dr["ID"].ToString(), "58421043")));

                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.Name = dr["Name"].ToString();
                    HotelObj.FirmName = dr["FirmName"].ToString();
                    HotelObj.HotelTypeName = dr["HotelTypeName"].ToString();
                    HotelObj.HotelClassName = dr["HotelClassName"].ToString();
                    HotelObj.HotelAccommodationTypeName = dr["HotelAccommodationTypeName"].ToString();
                    HotelObj.HotelChainName = dr["HotelChainName"].ToString();
                    HotelObj.CountryName = dr["CountryName"].ToString();
                    HotelObj.CityName = dr["CityName"].ToString();
                    HotelObj.RegionName = dr["RegionName"].ToString();
                    HotelObj.MainRegionName = dr["MainRegionName"].ToString();
                    HotelObj.MainRegionID = dr["MainRegionID"].ToString();
                    HotelObj.Address = dr["Address"].ToString();
                    HotelObj.PostCode = dr["PostCode"].ToString();
                    HotelObj.Phone = dr["Phone"].ToString();
                    HotelObj.Fax = dr["Fax"].ToString();
                    HotelObj.Email = dr["Email"].ToString();
                    HotelObj.WebAddress = dr["WebAddress"].ToString();
                    HotelObj.StatusName = dr["StatusName"].ToString();
                    int active = Convert.ToInt32(dr["Active"]);
                    HotelObj.CultureID = Convert.ToInt32(dr["CultureID"]);
                    HotelObj.Active = Convert.ToBoolean(active);
                    HotelObj.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    HotelObj.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    HotelObj.FirmID = dr["FirmID"].ToString();
                    HotelObj.CountryID = dr["CountryID"].ToString();
                    HotelObj.RegionID = dr["RegionID"].ToString();
                    HotelObj.CityID = dr["CityID"].ToString();
                    HotelObj.CurrencyID = dr["CurrencyID"].ToString();
                    HotelObj.RoutingName = dr["RoutingName"].ToString();
                    HotelObj.HotelAccommodationTypeID = dr["HotelAccommodationTypeID"].ToString();
                    //string test = dr["AvailabilityRateUpdate"].ToString();
                    if (dr["AvailabilityRateUpdate"].ToString() != "")
                    {
                        HotelObj.AvailabilityRateUpdate = Convert.ToBoolean(Convert.ToInt32(dr["AvailabilityRateUpdate"]));
                    }
                    else
                    {
                        HotelObj.AvailabilityRateUpdate = false;
                    }
                    HotelObj.Latitude = dr["Latitude"].ToString();
                    HotelObj.Longitude = dr["Longitude"].ToString();
                    HotelObj.ParentRegionID = dr["ParentRegionID"].ToString();
                    HotelObj.HotelTypeID = dr["HotelTypeID"].ToString();
                    HotelObj.HotelClassID = dr["HotelClassID"].ToString();
                    HotelObj.CityTaxApplied = dr["CityTaxApplied"].ToString();
                    HotelObj.ClosestAirportID = dr["ClosestAirportID"].ToString();
                    HotelObj.CountryCode = dr["CountryCode"].ToString();
                    HotelObj.VAT = dr["VAT"].ToString();
                    HotelObj.ClosestAirportName = dr["ClosestAirportName"].ToString();
                    HotelObj.ClosestAirportDistance = dr["ClosestAirportDistance"].ToString().Replace(',','.');
                    HotelObj.Description = dr["Description"].ToString();
                    HotelObj.RoomCount = dr["RoomCount"].ToString();
                    HotelObj.CheckinStart = dr["CheckinStart"].ToString();
                    HotelObj.CheckinEnd = dr["CheckinEnd"].ToString();
                    HotelObj.RenovationYear = dr["RenovationYear"].ToString();
                    HotelObj.CheckoutStart = dr["CheckoutStart"].ToString();
                    HotelObj.CheckoutEnd = dr["CheckoutEnd"].ToString();
                    HotelObj.FloorCount = dr["FloorCount"].ToString();
                    HotelObj.BuiltYear = dr["BuiltYear"].ToString();
                    HotelObj.HitCount = dr["HitCount"].ToString();
                    HotelObj.Sort = dr["Sort"].ToString();
                    HotelObj.MapZoomIndex = dr["MapZoomIndex"].ToString();
                    HotelObj.StatusID = dr["StatusID"].ToString();
                    if (dr["IsPreferred"].ToString() != "")
                    {
                        HotelObj.IsPreferred = Convert.ToBoolean(Convert.ToInt32(dr["IsPreferred"]));
                    }
                    else
                    {
                        HotelObj.IsPreferred = false;
                    }
                    if (dr["IsSecret"].ToString() != "")
                    {
                        HotelObj.IsSecret = Convert.ToBoolean(Convert.ToInt32(dr["IsSecret"]));
                    }
                    else
                    {
                        HotelObj.IsSecret = false;
                    }
                    // HotelObj.IsPreferred = dr["IsPreferred"].ToString();
                    HotelObj.ShowOffline = dr["ShowOffline"].ToString();
                    HotelObj.ChannelManagerID = dr["ChannelManagerID"].ToString();
                    HotelObj.CreditCardNotRequired = dr["CreditCardNotRequired"].ToString();
                    HotelObj.IPAddress = dr["IPAddress"].ToString();
                    HotelObj.HotelChainID = dr["HotelChainID"].ToString();
                    HotelObj.CurrencyName = dr["CurrencyName"].ToString();
                    
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
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
        public string GetCity(string Region)
        {
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetCityID_TB_Region_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Regionid", Region);
            string CityID = Convert.ToString(cmd.ExecuteScalar());
            SQLCon.Close();
            return CityID;
        }
        public string GetdescriptionbyCulture(string Culture, string HotelID)
        {
           // TB_HotelRoom hotelRooms = new TB_HotelRoom();
            TB_Hotel hotel = new TB_Hotel();
            string result = "";
            if(HotelID!="")
            {
                int HotelIDValue = Convert.ToInt32(HotelID);
                hotel = db.TB_Hotel.Where(x => x.ID == HotelIDValue).FirstOrDefault();

                if (Culture == "1")
                {
                    result = hotel.Description_en;
                }
                else if (Culture == "3")
                {
                    result = hotel.Description_de;
                }
                else if (Culture == "4")
                {
                    result = hotel.Description_fr;
                }
                else if (Culture == "10")
                {
                    result = hotel.Description_ar;
                }
                else if (Culture == "6")
                {
                    result = hotel.Description_ru;
                }
                else if (Culture == "8")
                {
                    result = hotel.Description_tr;
                }

            }
           
            return result;
        }
        public string InsertPropertyOperations(string HotelName, string Latitude, string Longitude, string Country, string Region, string MainRegion, string HotelAddress,
               string HotelPostCode, string HotelPhone, string HotelFax, string Firm, string HotelType, string HotelClass, string HotelChain, string AccommodationType,
               string RoomCount, string FloorCount, string BuiltYear, string RenovationYearYear, string WebAddress, string HotelEmail, string CheckinStart, string CheckinEnd,
               string CheckoutStart, string CheckoutEnd, string Culture, string Currency, string Description, string Sorts, string MapZoomIndex, string ClosestAirport,
               string ClosestAirportDistance, string ChannelManager, string AvailabilityRateUpdate, string Status, string IsSecret, string IsPreferred, string IsActive,
               string NotificationCulture, string RoutingName, string SelectedCards, string SelectedRegions, Controller ctrl)
        {
            string culturecode = "";
            string CityID = GetCity(Region);
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[B_Ex_Insert_TB_Hotel_SP]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CityID", ObjCommon.CheckEmptyStringDBParameter(CityID, false, false, false, false, false, true));
           // cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@HotelName", HotelName);
            cmd.Parameters.AddWithValue("@Latitude", Convert.ToDecimal(Latitude));
            cmd.Parameters.AddWithValue("@Longitude", Convert.ToDecimal(Longitude));
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@Region", Region);
            cmd.Parameters.AddWithValue("@MainRegion", ObjCommon.CheckEmptyStringDBParameter(MainRegion, false, false, false, false, false, true));
            cmd.Parameters.AddWithValue("@HotelAddress", HotelAddress);
            cmd.Parameters.AddWithValue("@HotelPostCode", HotelPostCode);
            cmd.Parameters.AddWithValue("@HotelPhone", HotelPhone);
            cmd.Parameters.AddWithValue("@Firm", ObjCommon.CheckEmptyStringDBParameter(Firm, true));
            cmd.Parameters.AddWithValue("@HotelFax", HotelFax);
            cmd.Parameters.AddWithValue("@HotelType", HotelType);
            cmd.Parameters.AddWithValue("@HotelClass", HotelClass);
            cmd.Parameters.AddWithValue("@HotelChain", ObjCommon.CheckEmptyStringDBParameter(HotelChain, true));
            cmd.Parameters.AddWithValue("@AccommodationType", AccommodationType);
            cmd.Parameters.AddWithValue("@RoomCount", RoomCount);
            cmd.Parameters.AddWithValue("@FloorCount", ObjCommon.CheckEmptyStringDBParameter(FloorCount, true));
            cmd.Parameters.AddWithValue("@BuiltYear", ObjCommon.CheckEmptyStringDBParameter(BuiltYear, true));
            cmd.Parameters.AddWithValue("@RenovationYearYear", ObjCommon.CheckEmptyStringDBParameter(RenovationYearYear, true));
            cmd.Parameters.AddWithValue("@WebAddress", ObjCommon.CheckEmptyStringDBParameter(WebAddress));
            cmd.Parameters.AddWithValue("@HotelEmail", HotelEmail);
            cmd.Parameters.AddWithValue("@CheckinStart", ObjCommon.CheckEmptyStringDBParameter(CheckinStart));
            cmd.Parameters.AddWithValue("@CheckinEnd", ObjCommon.CheckEmptyStringDBParameter(CheckinEnd));
            cmd.Parameters.AddWithValue("@CheckoutStart",ObjCommon.CheckEmptyStringDBParameter(CheckoutStart));
            cmd.Parameters.AddWithValue("@CheckoutEnd", ObjCommon.CheckEmptyStringDBParameter(CheckoutEnd));
            cmd.Parameters.AddWithValue("@Culture", NotificationCulture);
            cmd.Parameters.AddWithValue("@Currency", Currency);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Sorts",ObjCommon.CheckEmptyStringDBParameter(Sorts, true));
            cmd.Parameters.AddWithValue("@MapZoomIndex",ObjCommon.CheckEmptyStringDBParameter(MapZoomIndex, true));
            cmd.Parameters.AddWithValue("@ClosestAirport", ClosestAirport);
            cmd.Parameters.AddWithValue("@ClosestAirportDistance", ClosestAirportDistance);
            cmd.Parameters.AddWithValue("@ChannelManager", ObjCommon.CheckEmptyStringDBParameter(ChannelManager, true));
            cmd.Parameters.AddWithValue("@AvailabilityRateUpdate", AvailabilityRateUpdate);
            if (Status != "")
            {
                cmd.Parameters.AddWithValue("@Status", Status);
            }
            cmd.Parameters.AddWithValue("@IsSecret", IsSecret);
            cmd.Parameters.AddWithValue("@IsPreferred", IsPreferred);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@NotificationCulture", NotificationCulture);

            if (RoutingName != "")
            {
                cmd.Parameters.AddWithValue("@RoutingName", RoutingName);
            }
            cmd.Parameters.AddWithValue("@OperatedUserID", Convert.ToInt64(ctrl.Session["UserID"]));
            if (SelectedCards != "")
            {
                cmd.Parameters.AddWithValue("@SelectedCards", SelectedCards);
            }
            if (SelectedRegions != "")
            {
                cmd.Parameters.AddWithValue("@SelectedRegions", SelectedRegions);
            }
         
            cmd.Parameters.AddWithValue("@IPaddress", ObjCommon.GetIPAddress());
            string i = Convert.ToString(cmd.ExecuteScalar());
            gbsExtranetMVC.Models.Repositories.FirmRequestsRepository.Encryption64 objEncryptreservation = new gbsExtranetMVC.Models.Repositories.FirmRequestsRepository.Encryption64();
            string EncryptHotelID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(i.ToString(), "58421043")));

            SQLCon.Close();
            int hotelidd = Convert.ToInt32(i);
            var DepObj = db.TB_Hotel.Where(x => x.ID == hotelidd).FirstOrDefault();

            if (Culture == "1")
            {
                culturecode = "en";
            }
            else if (Culture == "3")
            {
                culturecode = "de";
            }
            else if (Culture == "4")
            {
                culturecode = "fr";

            }
            else if (Culture == "10")
            {
                culturecode = "ar";
            }
            else if (Culture == "6")
            {
                culturecode = "ru";

            }
            else if (Culture == "8")
            {
                culturecode = "tr";
            }
            if (Description!="")
            {
                switch (culturecode)
                {
                    case "en":

                        DepObj.Description_en = Description;
                        break;
                    case "tr":

                        DepObj.Description_tr = Description;
                        break;
                    case "de":

                        DepObj.Description_de = Description;
                        break;
                    case "es":

                        DepObj.Description_es = Description;
                        break;
                    case "fr":

                        DepObj.Description_fr = Description;
                        break;
                    case "ru":

                        DepObj.Description_ru = Description;
                        break;
                    case "it":

                        DepObj.Description_it = Description;
                        break;
                    case "ar":

                        DepObj.Description_ar = Description;
                        break;
                    case "ja":

                        DepObj.Description_ja = Description;
                        break;
                    case "pt":

                        DepObj.Description_pt = Description;
                        break;
                    case "zh":

                        DepObj.Description_zh = Description;
                        break;
                    default:
                        break;
                }

                db.SaveChanges();
            }
            
            return EncryptHotelID;
        }

        public int UpdatePropertyOperations(string HotelID,string HotelName, string Latitude, string Longitude, string Country, string Region, string MainRegion, string HotelAddress,
                string HotelPostCode, string HotelPhone, string HotelFax, string Firm, string HotelType, string HotelClass, string HotelChain, string AccommodationType,
                string RoomCount, string FloorCount, string BuiltYear, string RenovationYearYear, string WebAddress, string HotelEmail, string CheckinStart, string CheckinEnd,
                string CheckoutStart, string CheckoutEnd, string Culture, string Currency, string Description, string Sorts, string MapZoomIndex, string ClosestAirport,
                string ClosestAirportDistance, string ChannelManager, string AvailabilityRateUpdate, string Status, string IsSecret, string IsPreferred, string IsActive,
                string NotificationCulture, string RoutingName, string SelectedCards, string SelectedRegions, Controller ctrl)
        {
            string culturecode = "";
            string CityID = GetCity(Region);
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_Update_TB_Hotel_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CityID", ObjCommon.CheckEmptyStringDBParameter(CityID, false, false, false, false, false, true));
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@HotelName", HotelName);
            cmd.Parameters.AddWithValue("@Latitude", Convert.ToDecimal(Latitude));
            cmd.Parameters.AddWithValue("@Longitude", Convert.ToDecimal(Longitude));
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@Region", Region);
            cmd.Parameters.AddWithValue("@MainRegion", ObjCommon.CheckEmptyStringDBParameter(MainRegion, false, false, false, false, false, true));
            cmd.Parameters.AddWithValue("@HotelAddress", HotelAddress);
            cmd.Parameters.AddWithValue("@HotelPostCode", HotelPostCode);
            cmd.Parameters.AddWithValue("@HotelPhone", HotelPhone);
            cmd.Parameters.AddWithValue("@Firm", ObjCommon.CheckEmptyStringDBParameter(Firm, true));
            cmd.Parameters.AddWithValue("@HotelFax", HotelFax);
            cmd.Parameters.AddWithValue("@HotelType", HotelType);
            cmd.Parameters.AddWithValue("@HotelClass", HotelClass);
            cmd.Parameters.AddWithValue("@HotelChain", ObjCommon.CheckEmptyStringDBParameter(HotelChain, true));
            cmd.Parameters.AddWithValue("@AccommodationType", AccommodationType);
            cmd.Parameters.AddWithValue("@RoomCount", RoomCount);
            cmd.Parameters.AddWithValue("@FloorCount", ObjCommon.CheckEmptyStringDBParameter(FloorCount, true));
            cmd.Parameters.AddWithValue("@BuiltYear", ObjCommon.CheckEmptyStringDBParameter(BuiltYear, true));
            cmd.Parameters.AddWithValue("@RenovationYearYear", ObjCommon.CheckEmptyStringDBParameter(RenovationYearYear, true));
            cmd.Parameters.AddWithValue("@WebAddress", ObjCommon.CheckEmptyStringDBParameter(WebAddress));
            cmd.Parameters.AddWithValue("@HotelEmail", HotelEmail);
            cmd.Parameters.AddWithValue("@CheckinStart", ObjCommon.CheckEmptyStringDBParameter(CheckinStart));
            cmd.Parameters.AddWithValue("@CheckinEnd", ObjCommon.CheckEmptyStringDBParameter(CheckinEnd));
            cmd.Parameters.AddWithValue("@CheckoutStart", ObjCommon.CheckEmptyStringDBParameter(CheckoutStart));
            cmd.Parameters.AddWithValue("@CheckoutEnd", ObjCommon.CheckEmptyStringDBParameter(CheckoutEnd));
            cmd.Parameters.AddWithValue("@Culture", NotificationCulture);
            cmd.Parameters.AddWithValue("@Currency", Currency);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Sorts", ObjCommon.CheckEmptyStringDBParameter(Sorts, true));
            cmd.Parameters.AddWithValue("@MapZoomIndex", ObjCommon.CheckEmptyStringDBParameter(MapZoomIndex, true));
            cmd.Parameters.AddWithValue("@ClosestAirport", ClosestAirport);
            cmd.Parameters.AddWithValue("@ClosestAirportDistance", ClosestAirportDistance);
            cmd.Parameters.AddWithValue("@ChannelManager", ObjCommon.CheckEmptyStringDBParameter(ChannelManager, true));
            cmd.Parameters.AddWithValue("@AvailabilityRateUpdate", AvailabilityRateUpdate);
            if (Status != "")
            {
                cmd.Parameters.AddWithValue("@Status", Status);
            }
            cmd.Parameters.AddWithValue("@IsSecret", IsSecret);
            cmd.Parameters.AddWithValue("@IsPreferred", IsPreferred);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@NotificationCulture", NotificationCulture);

            if (RoutingName != "")
            {
                cmd.Parameters.AddWithValue("@RoutingName", RoutingName);
            }
            cmd.Parameters.AddWithValue("@OperatedUserID", Convert.ToInt64(ctrl.Session["UserID"]));
            if (SelectedCards != "")
            {
                cmd.Parameters.AddWithValue("@SelectedCards", SelectedCards);
            }
            if (SelectedRegions != "")
            {
                cmd.Parameters.AddWithValue("@SelectedRegions", SelectedRegions);
            }
            cmd.Parameters.AddWithValue("@IPaddress", ObjCommon.GetIPAddress());
            int i = cmd.ExecuteNonQuery();
            SQLCon.Close();
            int hotelidd = Convert.ToInt32(HotelID);
            var DepObj = db.TB_Hotel.Where(x => x.ID == hotelidd).FirstOrDefault();

            if (Culture == "1")
            {
                culturecode = "en";
            }
            else if (Culture == "3")
            {
                culturecode = "de";
            }
            else if (Culture == "4")
            {
                culturecode = "fr";

            }
            else if (Culture == "10")
            {
                culturecode = "ar";
            }
            else if (Culture == "6")
            {
                culturecode = "ru";

            }
            else if (Culture == "8")
            {
                culturecode = "tr";
            }
            if (Description != "")
            {
                switch (culturecode)
                {
                    case "en":

                        DepObj.Description_en = Description;
                        break;
                    case "tr":

                        DepObj.Description_tr = Description;
                        break;
                    case "de":

                        DepObj.Description_de = Description;
                        break;
                    case "es":

                        DepObj.Description_es = Description;
                        break;
                    case "fr":

                        DepObj.Description_fr = Description;
                        break;
                    case "ru":

                        DepObj.Description_ru = Description;
                        break;
                    case "it":

                        DepObj.Description_it = Description;
                        break;
                    case "ar":

                        DepObj.Description_ar = Description;
                        break;
                    case "ja":

                        DepObj.Description_ja = Description;
                        break;
                    case "pt":

                        DepObj.Description_pt = Description;
                        break;
                    case "zh":

                        DepObj.Description_zh = Description;
                        break;
                    default:
                        break;
                }

                db.SaveChanges();
            }
            return i;

//            DBEntities UpdateUserOperationsobj = new DBEntities();
//            var HotelIDParameter = new SqlParameter("@HotelID", HotelID);
//            var HotelNameParameter = new SqlParameter("@HotelName", HotelName);
//            var LatitudeParameter = new SqlParameter("@Latitude", Latitude);
//            var LongitudeParameter = new SqlParameter("@Longitude", Longitude);
//            var CountryParameter = new SqlParameter("@Country", Country);
//            var RegionParameter = new SqlParameter("@Region", Region);
//            var MainRegionParameter = new SqlParameter("@MainRegion", MainRegion);
//            var HotelAddressParameter = new SqlParameter("@HotelAddress", HotelAddress);
//            var HotelPostCodeParameter = new SqlParameter("@HotelPostCode", HotelPostCode);
//            var HotelPhoneParameter = new SqlParameter("@HotelPhone", HotelPhone);
//            var FirmParameter = new SqlParameter("@Firm", Firm);
//            var HotelFaxParameter = new SqlParameter("@HotelFax", HotelFax);
//            var HotelTypeParameter = new SqlParameter("@HotelType", HotelType);
//            var HotelClassParameter = new SqlParameter("@HotelClass", HotelClass);
//            var HotelChainParameter = new SqlParameter("@HotelChain", HotelChain);

//            var AccommodationTypeParameter = new SqlParameter("@AccommodationType", AccommodationType);
//            var RoomCountParameter = new SqlParameter("@RoomCount", RoomCount);
//            var FloorCountParameter = new SqlParameter("@FloorCount", FloorCount);
//            var BuiltYearParameter = new SqlParameter("@BuiltYear", BuiltYear);
//            var RenovationYearYearParameter = new SqlParameter("@RenovationYearYear", RenovationYearYear);
//            var WebAddressParameter = new SqlParameter("@WebAddress", WebAddress);
//            var HotelEmailParameter = new SqlParameter("@HotelEmail", HotelEmail);
//            var CheckinStartParameter = new SqlParameter("@CheckinStart", CheckinStart);
//            var CheckinEndParameter = new SqlParameter("@CheckinEnd", CheckinEnd);
//            var CheckoutStartParameter = new SqlParameter("@CheckoutStart", CheckoutStart);
//            var CheckoutEndParameter = new SqlParameter("@CheckoutEnd", CheckoutEnd);
//            var CultureParameter = new SqlParameter("@Culture", Culture);
//            var CurrencyParameter = new SqlParameter("@Currency", Currency);
//            var DescriptionParameter = new SqlParameter("@Description", Description);
//            var SortsParameter = new SqlParameter("@Sorts", Sorts);
//            var MapZoomIndexParameter = new SqlParameter("@MapZoomIndex", MapZoomIndex);

//            var ClosestAirportParameter = new SqlParameter("@ClosestAirport", ClosestAirport);
//            var ClosestAirportDistanceParameter = new SqlParameter("@ClosestAirportDistance", ClosestAirportDistance);
//            var ChannelManagerParameter = new SqlParameter("@ChannelManager", ChannelManager);
//            var AvailabilityRateUpdateParameter = new SqlParameter("@AvailabilityRateUpdate", AvailabilityRateUpdate);
//            var StatusParameter = new SqlParameter("@Status", Status);
//            var IsSecretParameter= new SqlParameter("@IsSecret", IsSecret);
//            var IsPreferredParameter = new SqlParameter("@IsPreferred", IsPreferred);
//            var IsActiveParameter = new SqlParameter("@IsActive", IsActive);
//            var NotificationCultureParameter = new SqlParameter("@NotificationCulture", NotificationCulture);
//            var RoutingNameParameter = new SqlParameter("@RoutingName", RoutingName);
//           // Int64 iny = Convert.ToInt64(Session["UserID"]);
//            var OperatedUserIDParameter = new SqlParameter("@OperatedUserID", "0");
//            int i = UpdateUserOperationsobj.Database.ExecuteSqlCommand("B_Ex_Update_TB_Hotel_SP @HotelID,@HotelName,@Latitude,@Longitude,@Country,@Region,@MainRegion,@HotelAddress,@HotelPostCode,@HotelPhone,@HotelFax ,@Firm,@HotelType,@HotelClass,@HotelChain,@AccommodationType,@RoomCount,@FloorCount,@BuiltYear,@RenovationYearYear,@WebAddress,@HotelEmail,@CheckinStart,@CheckinEnd,@CheckoutStart,@CheckoutEnd,@Culture,@Currency,@Description,@Sorts,@MapZoomIndex,@ClosestAirport,@ClosestAirportDistance,@ChannelManager,@AvailabilityRateUpdate,@Status,@IsSecret,@IsPreferred,@IsActive,@NotificationCulture,@RoutingName,@OperatedUserID",
//HotelIDParameter, HotelNameParameter, LatitudeParameter, LongitudeParameter, CountryParameter, RegionParameter, MainRegionParameter, HotelAddressParameter, HotelPostCodeParameter, HotelPhoneParameter,HotelFaxParameter, FirmParameter, HotelTypeParameter, HotelClassParameter, HotelChainParameter, AccommodationTypeParameter, RoomCountParameter, FloorCountParameter, BuiltYearParameter, RenovationYearYearParameter, WebAddressParameter, HotelEmailParameter, CheckinStartParameter, CheckinEndParameter, CheckoutStartParameter, CheckoutEndParameter, CultureParameter, CurrencyParameter, DescriptionParameter, SortsParameter, MapZoomIndexParameter, ClosestAirportParameter, ClosestAirportDistanceParameter, ChannelManagerParameter, AvailabilityRateUpdateParameter, StatusParameter, IsSecretParameter, IsPreferredParameter, IsActiveParameter, NotificationCultureParameter, RoutingNameParameter, OperatedUserIDParameter);
           // return i;
        }
        public List<FirmOperationsExt> GetFirmOperations(string FirmID)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            List<FirmOperationsExt> list = new List<FirmOperationsExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetFirmByFirmID_TB_Firm_SP", SQLCon);
            cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            cmd.Parameters.AddWithValue("@FirmID", FirmID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            // return dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FirmOperationsExt FirmObj = new FirmOperationsExt();
                    FirmObj.ID = Convert.ToInt32(dr["ID"]);
                    FirmObj.Firm = dr["Name"].ToString();
                    FirmObj.CountryID = dr["CountryID"].ToString();
                    FirmObj.Country = dr["Country"].ToString();
                    FirmObj.RegionID = dr["RegionID"].ToString();
                    FirmObj.Region = dr["Region"].ToString();
                    FirmObj.City = dr["City"].ToString();
                    FirmObj.Address = dr["Address"].ToString();
                    FirmObj.Postal_code = dr["PostCode"].ToString();
                    FirmObj.Phone = dr["Phone"].ToString();
                    FirmObj.Fax = dr["Fax"].ToString();
                    FirmObj.Email = dr["Email"].ToString();
                    FirmObj.Tax_Office = dr["TaxOffice"].ToString();
                    FirmObj.Tax_ID = dr["TaxID"].ToString();
                    FirmObj.Executive_TitleID = dr["ExecutiveTitleID"].ToString();
                    FirmObj.Executive_Title = dr["ExecutiveTitle"].ToString();
                    FirmObj.Executive_Name = dr["ExecutiveName"].ToString();
                    FirmObj.Executive_Surname = dr["ExecutiveSurname"].ToString();
                    FirmObj.Executive_Position = dr["ExecutivePosition"].ToString();
                    FirmObj.Executive_Phone = dr["ExecutivePhone"].ToString();
                    FirmObj.Executive_Email = dr["ExecutiveMail"].ToString();
                    FirmObj.StatusID = dr["StatusID"].ToString();
                    FirmObj.Status = dr["Status"].ToString();
                    FirmObj.Active = Convert.ToBoolean(dr["Active"]);
                    FirmObj.IPaddress = dr["IPAddress"].ToString();
                    FirmObj.Created_Date = dr["CreatedDate"].ToString();
                    FirmObj.Updated_Date = dr["UpdatedDate"].ToString();
                    list.Add(FirmObj);
                }
            }
            return list;
        }

        public int Updatefirm(int FirmID, Controller ctrl)
        {

            int status = 1;

            var obj = db.TB_Firm.Where(x => x.ID == FirmID).FirstOrDefault();
            obj.Active = true;
            obj.StatusID = 2;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

        public int DeleteHotel(int HotelID)
        {
            int status = 1;

            using (DBEntities DE = new DBEntities())
            {
                var HotelTable = DE.TB_Hotel.Where(x => x.ID == HotelID).FirstOrDefault();
                DE.TB_Hotel.Remove(HotelTable);
                DE.SaveChanges();
            }
            return status;

        }
        public List<FirmOperationsExt> GetUserinfo(string username)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string FirmID = "";
            List<FirmOperationsExt> list = new List<FirmOperationsExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetUserInfoByEmail_Sp", SQLCon);
            cmd.Parameters.AddWithValue("@Email", username);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            // return dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FirmOperationsExt FirmObj = new FirmOperationsExt();
                    FirmObj.ID = Convert.ToInt32(dr["ID"]);
                    FirmObj.FirmID = dr["FirmID"].ToString();
                    FirmObj.Name = dr["Name"].ToString();
                    FirmObj.Surname = dr["Surname"].ToString();
                    FirmObj.UserName = dr["UserName"].ToString();

                    list.Add(FirmObj);
                }
            }
            if (FirmID != "")
            {

            }
            return list;
        }
    }
    public class HotelExt
    {
        public int ID { get; set; }
        public string EncryptHotelID { get; set; }
        public string Name { get; set; }
        public string FirmName { get; set; }
        public string HotelTypeName { get; set; }
        public string HotelClassName { get; set; }
        public string HotelAccommodationTypeName { get; set; }
        public string HotelChainName { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
        public string MainRegionName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebAddress { get; set; }
        public string StatusName { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime OpDateTime { get; set; }
        public string FirmID { get; set; }
        public string CountryID { get; set; }
        public string RegionID { get; set; }
        public string CityID { get; set; }
        public string CurrencyID { get; set; }
        public string RoutingName { get; set; }
        public string HotelAccommodationTypeID { get; set; }
        public bool AvailabilityRateUpdate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ParentRegionID { get; set; }
        public int CultureID { get; set; }
        public string HotelTypeID { get; set; }
        public string HotelClassID { get; set; }
        public string HotelChainID { get; set; }
        public string MainRegionID { get; set; }
        public string CityTaxApplied { get; set; }
        public string ClosestAirportID { get; set; }
        public string CountryCode { get; set; }
        public string VAT { get; set; }
        public string ClosestAirportName { get; set; }
        public string ClosestAirportNameWithParentNameAndCode { get; set; }
        public string ClosestAirportDistance { get; set; }
        public string Description { get; set; }
        public string RoomCount { get; set; }
        public string CheckinStart { get; set; }
        public string CheckinEnd { get; set; }
        public string CheckoutStart { get; set; }
        public string CheckoutEnd { get; set; }
        public string FloorCount { get; set; }
        public string BuiltYear { get; set; }
        public string RenovationYear { get; set; }
        public string HitCount { get; set; }
        public string Sort { get; set; }
        public string MapZoomIndex { get; set; }
        public string StatusID { get; set; }
        public bool IsSecret { get; set; }
        public bool IsPreferred { get; set; }
        public string ShowOffline { get; set; }
        public string ChannelManagerID { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }
        public string CreditCardNotRequired { get; set; }
        public string IPAddress { get; set; }
       
    }
}