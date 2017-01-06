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
    public class HotelRoomRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        DataTable dt = new DataTable();

        List<HotelRoomExt> ListOfBedTypes = new List<HotelRoomExt>();
        List<HotelRoomExt> RoomBedInfo = new List<HotelRoomExt>();

        List<HotelRoomExt> HotelRooms = new List<HotelRoomExt>();
        List<HotelRoomExt> AttributesListOfModel = new List<HotelRoomExt>();

        public List<HotelRoomExt> GetHotelRooms(string RoomID)
        {
           
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetHotelRoom_TB_HotelRoom_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@RoomID", RoomID);           
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<HotelRoomExt> ListOfModel = new List<HotelRoomExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //if (!ArrayID.Contains(Convert.ToInt32(dr["ID"])))
                    //{
                    HotelRoomExt HotelRoom = new HotelRoomExt();

                    HotelRoom.ID = Convert.ToInt32(dr["ID"]);
                    HotelRoom.HotelID = Convert.ToInt64(dr["HotelID"]);
                    HotelRoom.Description = dr["Description"].ToString();
                    HotelRoom.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelRoom.RoomCount = dr["RoomCount"].ToString();


                    HotelRoom.RoomSize = dr["RoomSize"].ToString();
                    HotelRoom.MaxPeopleCount = dr["MaxPeopleCount"].ToString();


                    HotelRoom.MaxChildrenCount = dr["MaxChildrenCount"].ToString();
                    HotelRoom.BabyCotCount = dr["BabyCotCount"].ToString();

                    HotelRoom.ExtraBedCount = dr["ExtraBedCount"].ToString();
                    HotelRoom.SmokingTypeID = dr["SmokingTypeID"].ToString();


                    HotelRoom.ViewTypeID = dr["ViewTypeID"].ToString();
                    HotelRoom.Promotion = dr["Promotion"].ToString();

                    HotelRoom.RelatedHotelRoomID = dr["RelatedHotelRoomID"].ToString();
                    HotelRoom.CreateDateTime = dr["CreateDateTime"].ToString();
                    HotelRoom.CreateUserID = dr["CreateUserID"].ToString();

                    HotelRoom.OpDateTime = dr["OpDateTime"].ToString();
                    HotelRoom.OpUserID = dr["OpUserID"].ToString();

                    HotelRoom.Language = dr["Language"].ToString();
                    string Language = dr["Language"].ToString();

                    if (Language == "English")
                    {
                        HotelRoom.LanguageID = 1;
                    }
                    else if (Language == "Deutsch")
                    {
                        HotelRoom.LanguageID = 3;
                    }
                    else if (Language == "Français")
                    {
                        HotelRoom.LanguageID = 4;
                    }
                    else if (Language == "العربية")
                    {
                        HotelRoom.LanguageID = 10;
                    }
                    else if (Language == "Русский")
                    {
                        HotelRoom.LanguageID = 6;
                    }
                    else if (Language == "Türkçe")
                    {
                        HotelRoom.LanguageID = 8;
                    }

                    ListOfModel.Add(HotelRoom);
                    //}

                }
            }
            return ListOfModel;
        }

        public List<HotelRoomExt> GetBedTypes()
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetBedType_TB_TypeBed_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            SQLCon.Close();

            ArrayList OptionNo = new ArrayList();
            for (int i = 1; i <= 3; i++)
            {
                OptionNo.Add(i);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < OptionNo.Count; i++)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HotelRoomExt obj = new HotelRoomExt();

                        string OptionNoValue = OptionNo[i].ToString();
                        obj.OptionNo = OptionNoValue;
                        obj.BedId = dr["ID"].ToString();
                        obj.BedName = dr["Name"].ToString();

                        ListOfBedTypes.Add(obj);
                    }
                }
                
            }

            return ListOfBedTypes;
        }
        public List<HotelRoomExt> GetHotelRoomBeds(Int64 HotelRoomID)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRoomBeds", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            SQLCon.Close();

            ArrayList OptionNo = new ArrayList();
            for (int i = 1; i <= 3; i++)
            {
                OptionNo.Add(i);
            }

            for (int i = 0; i < OptionNo.Count; i++)
            {
                foreach (var items in ListOfBedTypes)
                {
                    //int OptionNoInt = Convert.ToInt16(OptionNo[i]);
                    string OptionNoValue = OptionNo[i].ToString();
                    if (items.OptionNo == OptionNoValue)
                    {
                        HotelRoomExt BedTypes = new HotelRoomExt();
                        BedTypes.OptionNo = OptionNoValue;
                        BedTypes.BedId = items.BedId;
                        BedTypes.BedName = items.BedName;
                        if (dt.Rows.Count > 0)
                        {
                            DataRow[] optionRoomBedInfo = dt.Select("OptionNo=" + OptionNoValue);
                            if (optionRoomBedInfo.Length > 0)
                            {
                                DataRow[] roomBeds = optionRoomBedInfo.CopyToDataTable().Select("BedTypeID=" + items.BedId);
                                // DataRow[] roomBeds = optionRoomBedInfo.CopyToDataTable.Select("BedTypeID=" + bedTypeID);

                                if (roomBeds.Length > 0)
                                {
                                    BedTypes.BedCount = Convert.ToString(CheckNullDBValue(roomBeds[0]["Count"]));
                                }
                            }
                        }
                        RoomBedInfo.Add(BedTypes);
                    }
                }

            }

            return RoomBedInfo;
        }


        public List<HotelRoomExt> GetAttributeHeaders()
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributeHeaders", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PartID", 5);
            cmd.Parameters.AddWithValue("@AttributeTypeID", 1);
            cmd.Parameters.AddWithValue("@Active", 1);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort");

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            SQLCon.Close();

            List<HotelRoomExt> ListOfModel = new List<HotelRoomExt>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HotelRoomExt obj = new HotelRoomExt();

                    obj.AttributeHeaderId = dr["ID"].ToString();
                    obj.AttributeName = dr["Name"].ToString();
                    //  PropertyConditions += value;

                    //  obj.PropertyConditions = value;

                    ListOfModel.Add(obj);
                }
            }

            return ListOfModel;
        }

        public List<HotelRoomExt> GetAttributes(string AttributeHeaderId)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributes", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PartID", 5);
            cmd.Parameters.AddWithValue("@AttributeTypeID", 1);
            cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderId);
            cmd.Parameters.AddWithValue("@Active", 1);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Name");

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            SQLCon.Close();


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HotelRoomExt obj = new HotelRoomExt();

                    obj.AttributeId = Convert.ToInt32(dr["ID"].ToString());
                    obj.AttributeName = dr["Name"].ToString();
                    obj.AttributeHeaderId = dr["AttributeHeaderID"].ToString();
                    AttributesListOfModel.Add(obj);
                }
            }

            return AttributesListOfModel;
        }

        public List<HotelRoomExt> GetHotelRoomAttributes(Int64 HotelRoomID, int AttributeTypeID, string OrderBy)
        {
            PropertyRoomsExt obj = new PropertyRoomsExt();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRoomAttributes", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@AttributeTypeID", AttributeTypeID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            foreach (var items in AttributesListOfModel)
            {
                HotelRoomExt HotelRoom = new HotelRoomExt();
                HotelRoom.AttributeId = items.AttributeId;
                HotelRoom.AttributeName = items.AttributeName;
                HotelRoom.AttributeHeaderId = items.AttributeHeaderId;
                if (dt.Rows.Count > 0)
                {
                    DataRow[] hotelRoomAttribute = dt.Select("AttributeID=" + items.AttributeId);
                    HotelRoom.hasAttribute = (hotelRoomAttribute.Length > 0);
                    HotelRoom.charged = ((HotelRoom.hasAttribute) && (Convert.ToBoolean(hotelRoomAttribute[0]["charged"]))); 
                }
                HotelRooms.Add(HotelRoom);
            }
            return HotelRooms;

        }
        public string GetdescriptionbyCulture(string Culture, string HotelRoomID)
        {
            TB_HotelRoom hotelRooms = new TB_HotelRoom();
            string result="";
            //string HotelRoomID = "7225";
            //int HotelRoomIdValue = Convert.ToInt32(HotelRoomID);
            // result = db.TB_HotelRoom.Select("HotelRoomID=" + hotelRoomID + " AND DateID=" + dateID + "AND PricePolicyTypeID=" + PricePolicy)[0];
            try
            {
                int HotelRoomIdValue = Convert.ToInt32(HotelRoomID);
                hotelRooms = db.TB_HotelRoom.Where(x => x.ID == HotelRoomIdValue).FirstOrDefault();
                // hotelRooms."Description_" + Culture = result;
                if (Culture == "1")
                {
                    result = hotelRooms.Description_en;
                }
                else if (Culture == "3")
                {
                    result = hotelRooms.Description_de;
                }
                else if (Culture == "4")
                {
                    result = hotelRooms.Description_fr;
                }
                else if (Culture == "10")
                {
                    result = hotelRooms.Description_ar;
                }
                else if (Culture == "6")
                {
                    result = hotelRooms.Description_ru;
                }
                else if (Culture == "8")
                {
                    result = hotelRooms.Description_tr;
                }

            }
            catch
            {

            }
           
            return result;
        }
        public int SaveHotelRoom(string HotelRoomID, int HotelID, string RoomType, string RoomCount, string RoomSize, string RoomMaxPeopleCount, string RoomMaxChildrenCount, string BabyCotCount, string ExtraBedCount,
                string SmokingStatus, string ViewType, string Culture, string Description, Controller ctrl)
        {
            DBEntities insertentity = new DBEntities();
            TB_HotelRoom hotelRoom = new TB_HotelRoom();
            string culturecode = "";
            if (HotelRoomID != string.Empty)
            {
                int HotelRoomIdValue = Convert.ToInt32(HotelRoomID);
                hotelRoom = db.TB_HotelRoom.Where(x => x.ID == HotelRoomIdValue).FirstOrDefault();
              //  hotelRoom = db.TB_HotelRoom.Where(x => x.ID == Convert.ToInt32(HotelRoomID)).FirstOrDefault();
                // hotelRoom = (from room in DataContext.TB_HotelRoomswhere room.ID == HotelRoomID).Single;
            }
            hotelRoom.HotelID = Convert.ToInt32(HotelID);
            hotelRoom.RoomTypeID = Convert.ToInt32(RoomType);
            hotelRoom.RoomCount = Convert.ToInt32(RoomCount);
            hotelRoom.RoomSize = Convert.ToInt32(RoomSize);
            hotelRoom.MaxPeopleCount = Convert.ToInt16(RoomMaxPeopleCount);
            hotelRoom.MaxChildrenCount = Convert.ToInt16(RoomMaxChildrenCount);
            hotelRoom.BabyCotCount = Convert.ToInt16(CheckEmptyStringDBParameter(BabyCotCount, true));
            hotelRoom.ExtraBedCount = Convert.ToInt16(CheckEmptyStringDBParameter(ExtraBedCount, true));
            hotelRoom.ViewTypeID = (Nullable<int>)CheckEmptyStringDBParameter(ViewType, true);
            hotelRoom.SmokingTypeID = (Nullable<int>)CheckEmptyStringDBParameter(SmokingStatus, true);
            
            if (Description != string.Empty)
            {

             if(Culture == "1")
            {
                culturecode ="en";
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
                 SetColumn(hotelRoom, "Description_" + culturecode, Description);
            }
            hotelRoom.Active = true;
            hotelRoom.OpDateTime = DateTime.Now;
            hotelRoom.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            if (HotelRoomID == string.Empty)
            {
                hotelRoom.CreateDateTime = DateTime.Now;
                hotelRoom.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                db.TB_HotelRoom.Add(hotelRoom);
               // db.SaveChanges();
                // DataContext.TB_HotelRooms.InsertOnSubmit(hotelRoom);
            }
            db.SaveChanges();
            // DataContext.SubmitChanges();

            return hotelRoom.ID;
        }

        public int SaveHotelRoomBed(string HotelRoomBedID, string OptionNo, string HotelRoomID, string BedTypeID, string BedCount, Controller ctrl)
        {
            TB_HotelRoomBed hotelRoomBed = new TB_HotelRoomBed();

            if (HotelRoomBedID != string.Empty)
            {
                int HotelRoomBedIDValue = Convert.ToInt32(HotelRoomBedID);
                hotelRoomBed = db.TB_HotelRoomBed.Where(x => x.ID == HotelRoomBedIDValue).FirstOrDefault();
                //  hotelRoomBed = (from roomBed in DataContext.TB_HotelRoomBedswhere roomBed.ID == HotelRoomBedID).Single;
            }
            hotelRoomBed.OptionNo = Convert.ToInt32(OptionNo);
            hotelRoomBed.HotelRoomID = Convert.ToInt32(HotelRoomID);
            hotelRoomBed.BedTypeID = Convert.ToInt32(BedTypeID);
            hotelRoomBed.Count = Convert.ToInt32(BedCount);
            hotelRoomBed.OpDateTime = DateTime.Now.Date;
            hotelRoomBed.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            if (HotelRoomBedID == string.Empty)
            {

                db.TB_HotelRoomBed.Add(hotelRoomBed);
               // db.SaveChanges();
                // DataContext.TB_HotelRooms.InsertOnSubmit(hotelRoom);
            }
            db.SaveChanges();
            return hotelRoomBed.ID;
        }

        public bool DeleteHotelRoomBeds(string HotelRoomID)
        {
            bool status = true;
            int HotelRoomIDValue = Convert.ToInt32(HotelRoomID);
            try
            {
                var obj = db.TB_HotelRoomBed.Where(x => x.HotelRoomID == HotelRoomIDValue).ToList();
                foreach (var obj1 in obj)
                {
                    db.TB_HotelRoomBed.Remove(obj1);
                }
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }

            return status;
        }

        public  bool DeleteHotelRoomAttributes(string HotelRoomID, string AttributeTypeID = "")
        {
            bool status = false;
            int HotelRoomValue = Convert.ToInt32(HotelRoomID);
            int AttributeTypeIDValue = Convert.ToInt32(AttributeTypeID);
            try
            {
                var hotelRoomAttributes = from hotelRoomAttribute in db.TB_HotelRoomAttribute
                                          join attribute in db.TB_Attribute
                                          on hotelRoomAttribute.AttributeID equals attribute.ID
                                          where hotelRoomAttribute.HotelRoomID == HotelRoomValue && attribute.AttributeTypeID == AttributeTypeIDValue
                                          select new { hotelRoomAttribute.ID };

                foreach (var items in hotelRoomAttributes)
                {
                    int hotelRoomAttributeIDValue = Convert.ToInt32(items.ID);
                    // var ID = items.ID;

                    var obj = db.TB_HotelRoomAttribute.Where(x => x.ID == hotelRoomAttributeIDValue).FirstOrDefault();
                    db.TB_HotelRoomAttribute.Remove(obj);
                    status = true;
                }
                db.SaveChanges();  
            }
            catch(Exception ex)
            {

            }
                   
            return status;
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
            return i;
        }
        public bool SaveHotelRoomAttributes(string HotelRoomID, int Charged, int AttributeID,string UnitValue,string Charge,string CurrencyID,Controller ctrl)
        {
            bool status=false;
            if (HotelRoomID != string.Empty) {
	        TB_HotelRoomAttribute hotelRoomAttribute = new TB_HotelRoomAttribute();
	        hotelRoomAttribute.HotelRoomID = Convert.ToInt32(HotelRoomID);
	        hotelRoomAttribute.AttributeID = AttributeID;
            hotelRoomAttribute.Charged = Convert.ToBoolean(Charged);
            hotelRoomAttribute.UnitValue = (string)CheckEmptyStringDBParameter(UnitValue);
            hotelRoomAttribute.Charge = (Nullable<decimal>)CheckEmptyStringDBParameter(Charge, false, false, false, true);
	        hotelRoomAttribute.CurrencyID = (Nullable<int>)CheckEmptyStringDBParameter(CurrencyID, true);
	        hotelRoomAttribute.OpDateTime = DateTime.Now.Date;
	        hotelRoomAttribute.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_HotelRoomAttribute.Add(hotelRoomAttribute);
                db.SaveChanges();
	       // DataContext.TB_HotelRoomAttributes.InsertOnSubmit(hotelRoomAttribute);
                status=true;
            }
            return status;
        }

        public static object CheckNullDBValue(object Value)
        {

            if (Value == null || object.ReferenceEquals(Value, DBNull.Value))
            {
                return string.Empty;
            }

            return Value;

        }

        public void SetColumn(object DataObj, string ColumnName, object Value)
        {
            System.Reflection.PropertyInfo pi = DataObj.GetType().GetProperty(ColumnName);

            if ((pi != null))
            {
                pi.SetValue(DataObj, Value, null);
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

    }

    public class HotelRoomExt
    {
        
        public Int64 ID { get; set; }

        public Int64 HotelID { get; set; }

        public int RoomTypeID { get; set; }

        public string RoomCount { get; set; }

        public string RoomTypeName { get; set; }

        public string RoomSize { get; set; }

        public string MaxPeopleCount { get; set; }

        public string IDWithMaxPeopleCount { get; set; }

        public string MaxChildrenCount { get; set; }

        public string BabyCotCount { get; set; }

        public string ExtraBedCount { get; set; }

        public string SmokingTypeID { get; set; }     

        public string ViewTypeID { get; set; }

        public string Promotion { get; set; }
        public string RelatedHotelRoomID { get; set; }



        public string CreateDateTime { get; set; }

        public string CreateUserID { get; set; }

        public string OpDateTime { get; set; }

        public string OpUserID { get; set; }

        public string Language { get; set; }

        public string BedId { get; set; }

        public string BedName { get; set; }
        public string OptionNo { get; set; }

        public string BedTypeId { get; set; }

        public string BedCount { get; set; }

        public string AttributeHeaderId { get; set; }
        public string AttributeHeaderName { get; set; }

        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public bool AttributeIsSelected { get; set; }
        public List<PropertyRoomsExt> AttributeList { get; set; }
        public string EncryptRoomID { get; set; }

        public string Description { get; set; }

        public bool hasAttribute { get; set; }

        public bool charged { get; set; }

        public int LanguageID { get; set; }
    }
}