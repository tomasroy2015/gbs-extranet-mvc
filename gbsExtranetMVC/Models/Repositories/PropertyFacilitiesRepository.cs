using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class PropertyFacilitiesExt
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Sort { get; set; }
        public bool Active { get; set; }
        public string Activity { get; set; }
        public string Service { get; set; }
        public string General { get; set; }
        public string AttributeHeaderName { get; set; }
        public int AttributeID { get; set; }
        public Int64 HotelID { get; set; }
        public string AttributeName { get; set; }
        public bool? Charged { get; set; }
        public bool? Charged1 { get; set; }
        public bool hasAttribute { get; set; }
        public bool Chargable { get; set; }
    }
    public class PropertyFacilitiesHeader
    {
        public int ID { get; set; }
        public string AttributeHeaderCode { get; set; } 
        public string AttributeHeaderName { get; set; }
        public List<PropertyFacilitiesExt> PropertyFacilitiesItems { get; set; }
    }
    public class PropertyFacilitiesRepository:BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


        public List<PropertyFacilitiesExt> GetHotelAttributes(int AttributeHeaderID, int HotelID)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributes", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", 1);
            cmd.Parameters.AddWithValue("@PartID", 1);
            cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderID);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            SQLCon.Close();

            List<PropertyFacilitiesExt> AttributesListOfModel = new List<PropertyFacilitiesExt>();
            List<PropertyFacilitiesExt> AttributesListOfModel1 = new List<PropertyFacilitiesExt>();
            bool charged1 = false;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyFacilitiesExt obj = new PropertyFacilitiesExt();

                    obj.AttributeID = Convert.ToInt32(dr["ID"].ToString());
                    obj.AttributeName = dr["Name"].ToString();
                    obj.Chargable = Convert.ToBoolean(dr["Chargeable"]);
                    //charged1 = Convert.ToBoolean(dr["Chargeable"]);
                    //  PropertyConditions += value;

                    //  obj.PropertyConditions = value;

                    AttributesListOfModel.Add(obj);
                }
            }

            DataTable SelectedRoomAttribute = HotelRoomAttributes(AttributeHeaderID, HotelID);
            foreach (var items in AttributesListOfModel)
            {
                PropertyFacilitiesExt HotelRoom = new PropertyFacilitiesExt();
                if (SelectedRoomAttribute.Rows.Count > 0)
                {
                    //DataRow hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" & items.AttributeId);
                    DataRow[] hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" + items.AttributeID);
                    HotelRoom.hasAttribute = Convert.ToBoolean(hotelRoomAttribute.Length > 0);

                    HotelRoom.Charged = ((HotelRoom.hasAttribute) && (Convert.ToBoolean(hotelRoomAttribute[0]["charged"])));

                    if ((items.Chargable == true) && (HotelRoom.Charged == true))
                    {
                        HotelRoom.Charged = true;
                    }
                    else if ((items.Chargable == true) && (HotelRoom.Charged == false))
                    {
                        HotelRoom.Charged = false;
                    }
                    else 
                    {
                        HotelRoom.Charged = null;
                    }
                    HotelRoom.AttributeID = items.AttributeID;
                    HotelRoom.AttributeName = items.AttributeName;

                    AttributesListOfModel1.Add(HotelRoom);
                }
                else
                {
                    DataRow[] hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" + items.AttributeID);
                    HotelRoom.hasAttribute = Convert.ToBoolean(hotelRoomAttribute.Length > 0);

                    HotelRoom.Charged = ((HotelRoom.hasAttribute) && (Convert.ToBoolean(hotelRoomAttribute[0]["charged"])));

                    if ((items.Chargable == true) && (HotelRoom.Charged == true))
                    {
                        HotelRoom.Charged = true;
                    }
                    else if ((items.Chargable == true) && (HotelRoom.Charged == false))
                    {
                        HotelRoom.Charged = false;
                    }
                    else
                    {
                        HotelRoom.Charged = null;
                    }
                    HotelRoom.AttributeID = items.AttributeID;
                    HotelRoom.AttributeName = items.AttributeName;

                    AttributesListOfModel1.Add(HotelRoom);
                }
            }

            return AttributesListOfModel1;
        }

       // public List<PropertyFacilitiesExt> GetHotelAttributes(int AttributeHeaderID)
       //{
       //    // string PropertyConditions = "";
       //    DataTable dt = new DataTable();
       //    SQLCon.Open();
       //    SqlCommand cmd = new SqlCommand("TB_SP_GetAttributes", SQLCon);
       //    cmd.CommandType = CommandType.StoredProcedure;
       //    cmd.Parameters.AddWithValue("@Active", 1);
       //    cmd.Parameters.AddWithValue("@PartID", 1);
       //    cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderID);
       //    cmd.Parameters.AddWithValue("@Culture", CultureValue);
       //    cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
       //    //SqlDataAdapter sda = new SqlDataAdapter(cmd);

       //    SqlDataAdapter sda = new SqlDataAdapter(cmd);

       //    sda.Fill(dt);
       //    SQLCon.Close();

       //    List<PropertyFacilitiesExt> AttributesListOfModel = new List<PropertyFacilitiesExt>();
       //    List<PropertyFacilitiesExt> AttributesListOfModel1 = new List<PropertyFacilitiesExt>();
       //    bool charged1 = false;
       //    if (dt.Rows.Count > 0)
       //    {
       //        foreach (DataRow dr in dt.Rows)
       //        {
       //            PropertyFacilitiesExt obj = new PropertyFacilitiesExt();

       //            obj.AttributeID = Convert.ToInt32(dr["ID"].ToString());
       //            obj.AttributeName = dr["Name"].ToString();
       //            obj.Chargable = Convert.ToBoolean(dr["Chargeable"]);
       //            //charged1 = Convert.ToBoolean(dr["Chargeable"]);
       //            //  PropertyConditions += value;

       //            //  obj.PropertyConditions = value;

       //            AttributesListOfModel.Add(obj);
       //        }
       //    }

       //    DataTable SelectedRoomAttribute = HotelRoomAttributes(AttributeHeaderID);
       //    foreach (var items in AttributesListOfModel)
       //    {
       //        PropertyFacilitiesExt HotelRoom = new PropertyFacilitiesExt();
       //        if (SelectedRoomAttribute.Rows.Count > 0)
       //        {
       //            //DataRow hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" & items.AttributeId);
       //            DataRow[] hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" + items.AttributeID);
       //            HotelRoom.hasAttribute = Convert.ToBoolean (hotelRoomAttribute.Length > 0);
                  
       //            HotelRoom.Chargable = Convert.ToBoolean(items.Chargable);
       //            if (items.Charged == true) {

       //                HotelRoom.Charged = ((Convert.ToBoolean(HotelRoom.hasAttribute)) && (Convert.ToBoolean(hotelRoomAttribute[0]["charged"])));
       //                HotelRoom.Charged = true;
       //            }
       //            else if (HotelRoom.Chargable == true)
       //            {
       //                HotelRoom.Charged = false;
       //            }
       //            else 
       //            {
       //                HotelRoom.Charged1 = null;
       //            }
                   
       //            HotelRoom.AttributeID = items.AttributeID;
       //            HotelRoom.AttributeName = items.AttributeName;
                        
       //            AttributesListOfModel1.Add(HotelRoom);
       //        }
       //        else
       //        {
       //            DataRow[] hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" + items.AttributeID);
       //            HotelRoom.Chargable = Convert.ToBoolean(items.Chargable);
       //            if ((HotelRoom.Chargable == true) && (items.Charged == true))
       //            {
       //                HotelRoom.Charged = ((Convert.ToBoolean(HotelRoom.hasAttribute)) && (Convert.ToBoolean(hotelRoomAttribute[0]["charged"])));
       //                HotelRoom.Charged = true;
       //            }
       //            else if (HotelRoom.Chargable == true)
       //            {
       //                HotelRoom.Charged = false;
       //            }
       //            else if ((HotelRoom.Chargable == false) && (items.Charged == false))
       //            {
       //                HotelRoom.Charged1 = null;
       //            }
       //            //DataRow[] hotelRoomAttribute = SelectedRoomAttribute.Select("AttributeID=" + items.AttributeID);
       //            //HotelRoom.Charged = charged1;
       //            HotelRoom.AttributeID = items.AttributeID;
       //            HotelRoom.AttributeName = items.AttributeName;

       //            AttributesListOfModel1.Add(HotelRoom);
       //        }
       //    }

       //    return AttributesListOfModel1;
       //}

        public DataTable HotelRoomAttributes(int AttributeHeaderID, int HotelID)
       {
           PropertyRoomsExt obj = new PropertyRoomsExt();
           DataTable dt = new DataTable();
           SQLCon.Open();
           SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAttributes", SQLCon);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@HotelID", HotelID);
           cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderID);
           cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
           cmd.Parameters.AddWithValue("@Culture", CultureValue);
           cmd.Parameters.AddWithValue("@Active", 1);
           SqlDataAdapter sda = new SqlDataAdapter(cmd);
           sda.Fill(dt);
           return dt;
       }
        #region old code
        //public List<PropertyFacilitiesExt> GetPropertyFacilitiesHeader()
        //{
        //    // string PropertyConditions = "";
        //    DataTable dt = new DataTable();
        //    SQLCon.Open();
        //    SqlCommand cmd = new SqlCommand("TB_SP_GetAttributeHeaders", SQLCon);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Culture", CultureValue);
        //    cmd.Parameters.AddWithValue("@AttributeTypeID", 1);
        //    cmd.Parameters.AddWithValue("@Active", 1);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);

        //    sda.Fill(dt);
        //    SQLCon.Close();

        //    List<PropertyFacilitiesExt> ListOfModel = new List<PropertyFacilitiesExt>();

        //    if (dt.Rows.Count > 0)
        //    {
        //        // var Count = 1;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            PropertyFacilitiesExt HotelconditionsObj = new PropertyFacilitiesExt();

        //            #region old code
        //            //if (Count == 1)
        //            //{
        //            //    HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);
        //            //    HotelconditionsObj.General = dr["Name"].ToString();
        //            //}
        //            //if (Count == 2)
        //            //{
        //            //    HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);

        //            //    HotelconditionsObj.Service = dr["Name"].ToString();
        //            //}
        //            //if (Count == 3)
        //            //{
        //            //    HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);

        //            //    HotelconditionsObj.Activity = dr["Name"].ToString();
        //            //}
        //            #endregion
        //            #region new code by tomas 10-April-2016
        //            HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);
        //            HotelconditionsObj.AttributeHeaderName = dr["Name"].ToString();
        //            #endregion
        //            ListOfModel.Add(HotelconditionsObj);
        //            //Count++;
        //        }

        //    }
        //    return ListOfModel;
        //}
        #endregion
        #region new code
        public List<PropertyFacilitiesHeader> GetPropertyFacilitiesHeader(int hotelID)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributeHeaders", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@AttributeTypeID", 1);
            cmd.Parameters.AddWithValue("@Active", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            SQLCon.Close();

            List<PropertyFacilitiesHeader> propertyFacilitiesHeaders = new List<PropertyFacilitiesHeader>();

            if (dt.Rows.Count > 0)
            {
                // var Count = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyFacilitiesHeader propertyHeader = new PropertyFacilitiesHeader();

                    #region old code
                    //if (Count == 1)
                    //{
                    //    HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);
                    //    HotelconditionsObj.General = dr["Name"].ToString();
                    //}
                    //if (Count == 2)
                    //{
                    //    HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);

                    //    HotelconditionsObj.Service = dr["Name"].ToString();
                    //}
                    //if (Count == 3)
                    //{
                    //    HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);

                    //    HotelconditionsObj.Activity = dr["Name"].ToString();
                    //}
                    #endregion
                    #region new code by tomas 10-April-2016
                    propertyHeader.ID = Convert.ToInt32(dr["ID"]);
                    propertyHeader.AttributeHeaderName = dr["Name"].ToString();
                    #endregion
                    propertyFacilitiesHeaders.Add(propertyHeader);
                    //Count++;
                }

            }
            return propertyFacilitiesHeaders;
        }
        #endregion
        public bool DeleteHotelAttributes(string HotelID, string AttributeTypeID)
        {

            DateTime Date1 = DateTime.Now.Date;
            int HotelIDValue = Convert.ToInt32(HotelID);
            int AttributeTypeIDvalue = Convert.ToInt32(AttributeTypeID);
            var hotelAttributesAvailability = from hotelAttribute in db.TB_HotelAttribute
                                    join Attributes in db.TB_Attribute
                                    on hotelAttribute.AttributeID equals Attributes.ID
                                    where Attributes.AttributeTypeID == AttributeTypeIDvalue && hotelAttribute.HotelID == HotelIDValue
                                    && Date1 >= hotelAttribute.StartDate && Date1 <= hotelAttribute.EndDate
                                    select hotelAttribute;

            foreach (var items in hotelAttributesAvailability)
            {

                int hotelAvailabilityIDValue = Convert.ToInt32(items.ID);
                // var ID = items.ID; Date1 >= hotelAttribute.StartDate && Date1 <= hotelAttribute.EndDate && 
                var obj = db.TB_HotelAttribute.Where(x => x.ID == hotelAvailabilityIDValue).FirstOrDefault();
                obj.EndDate = DateTime.Now.Date;
                obj.Active = false;
            }
            db.SaveChanges();
            return true;

        }
        public int SavePropertyFacility(int HotelID, string HotelAttributes,int Charged,Controller ctrl)
        {
            int status = 1;
            TB_HotelAttribute hotelAttribute = new TB_HotelAttribute();
            hotelAttribute.HotelID = HotelID;
            hotelAttribute.AttributeID = Convert.ToInt32(HotelAttributes);
            hotelAttribute.Charged = Convert.ToBoolean(Charged);
            hotelAttribute.StartDate = DateTime.Now.Date;
            hotelAttribute.EndDate = Convert.ToDateTime("2100-12-31", new CultureInfo("en-US", false));
            hotelAttribute.Active = true;
            hotelAttribute.OpDateTime = DateTime.Now.Date;
            hotelAttribute.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.TB_HotelAttribute.Add(hotelAttribute);
            db.SaveChanges();
            return status;
        }

        //public int SavePropertyFacility(int HotelID, string HotelAttributes)
        //{
        //    SQLCon.Open();
        //    int status = 0;
        //    //int charge;
        //    SqlCommand cmd = new SqlCommand("B_Ex_SaveHotelAttribute_TB_HotelAttribute_SP", SQLCon);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@HotelID", HotelID);
        //    cmd.Parameters.AddWithValue("@AttributeID", HotelAttributes);
        //    //cmd.Parameters.AddWithValue("@AttributeID1", HotelAttributes1);
        //    //cmd.Parameters.AddWithValue("@AttributeID2", HotelAttributes2);
        //    cmd.Parameters.AddWithValue("@Charged", 0);
        //    // cmd.Parameters.AddWithValue("@UnitID", null);
        //    // cmd.Parameters.AddWithValue("@UnitValue", null);
        //    // cmd.Parameters.AddWithValue("@Charge", null);
        //    //cmd.Parameters.AddWithValue("@CurrencyID", null);
        //    //if (Prices > 0)
        //    //{
        //    //    cmd.Parameters.AddWithValue("@Charge", Prices);
        //    //   // hotelAttribute.Charge = Charge
        //    //}
        //    // cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
        //    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
        //    // cmd.Parameters.AddWithValue("@Active", true);
        //    cmd.Parameters.AddWithValue("@OpDateTime", DateTime.Now);
        //    cmd.Parameters.AddWithValue("@OpUserID", 0);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    status = Convert.ToInt32(cmd.ExecuteNonQuery());
        //    SQLCon.Close();
        //    return status;
        //}

        //public List<PropertyFacilitiesExt> GetPropertyFacilities(int AttributeHeaderID1)
        //{

        //    DataTable dt = new DataTable();
        //    SQLCon.Open();
        //    SqlCommand cmd = new SqlCommand("TB_SP_GetAttributes", SQLCon);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Active", 1);
        //    cmd.Parameters.AddWithValue("@PartID", 1);
        //    cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderID1);
        //    cmd.Parameters.AddWithValue("@Culture", CultureValue);
        //    cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);

        //    sda.Fill(dt);
        //    SQLCon.Close();

        //    List<PropertyFacilitiesExt> ListOfModel = new List<PropertyFacilitiesExt>();
        //    ArrayList value = new ArrayList();
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            PropertyFacilitiesExt HotelconditionsObj = new PropertyFacilitiesExt();

        //            var AttributeHeaderID = Convert.ToInt32(dr["AttributeHeaderID"]);
        //            if (AttributeHeaderID == 1)
        //            {

        //                HotelconditionsObj.AttributeHeaderName = dr["AttributeHeaderName"].ToString();
        //                HotelconditionsObj.AttributeID = Convert.ToInt32(dr["ID"]);
        //                //HotelconditionsObj.HotelID = Convert.ToInt32(dr["HotelID"]);
        //                HotelconditionsObj.AttributeName = dr["Name"].ToString();
        //                HotelconditionsObj.Charged = Convert.ToBoolean(dr["Chargeable"]);
        //            }
        //            if (AttributeHeaderID == 2)
        //            {
        //                HotelconditionsObj.AttributeHeaderName = dr["AttributeHeaderName"].ToString();
        //                HotelconditionsObj.AttributeID = Convert.ToInt32(dr["ID"]);
        //                //HotelconditionsObj.HotelID = Convert.ToInt32(dr["HotelID"]);
        //                HotelconditionsObj.AttributeName = dr["Name"].ToString();
        //                HotelconditionsObj.Charged = Convert.ToBoolean(dr["Chargeable"]);
        //            }
        //            if (AttributeHeaderID == 3)
        //            {

        //                HotelconditionsObj.AttributeHeaderName = dr["AttributeHeaderName"].ToString();
        //                HotelconditionsObj.AttributeID = Convert.ToInt32(dr["ID"]);
        //               // HotelconditionsObj.HotelID = Convert.ToInt32(dr["HotelID"]);
        //                HotelconditionsObj.AttributeName = dr["Name"].ToString();
        //                HotelconditionsObj.Charged = Convert.ToBoolean(dr["Chargeable"]);
        //            }
                   
        //            ListOfModel.Add(HotelconditionsObj);
        //        }
        //    }
        //    return ListOfModel;
        //}

        //public List<PropertyFacilitiesExt> GetHotelAttributes(int AttributeHeaderID1)
        //{
        //    // string PropertyConditions = "";
        //    DataTable dt = new DataTable();
        //    SQLCon.Open();
        //    SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAttributes", SQLCon);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@HotelID", 100001);
        //    cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderID1);
        //    cmd.Parameters.AddWithValue("@Culture", CultureValue);
        //    cmd.Parameters.AddWithValue("@Active", 1);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);

        //    sda.Fill(dt);
        //    SQLCon.Close();

        //    List<PropertyFacilitiesExt> ListOfModel = new List<PropertyFacilitiesExt>();

        //    if (dt.Rows.Count > 0)
        //    {
        //        //var Count = 1;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            PropertyFacilitiesExt HotelconditionsObj = new PropertyFacilitiesExt();
        //            var AttributeHeaderID = Convert.ToInt32(dr["AttributeHeaderID"]);
        //            if (AttributeHeaderID == 1)
        //            {
        //                HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);
        //                HotelconditionsObj.AttributeName = dr["AttributeName"].ToString();
        //                HotelconditionsObj.Charged = Convert.ToBoolean(dr["Charged"]);
        //            }
        //            if (AttributeHeaderID == 2)
        //            {
        //                HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);

        //                HotelconditionsObj.AttributeName = dr["AttributeName"].ToString();
        //                HotelconditionsObj.Charged = Convert.ToBoolean(dr["Charged"]);
        //            }
        //            if (AttributeHeaderID == 3)
        //            {
        //                HotelconditionsObj.ID = Convert.ToInt32(dr["ID"]);

        //                HotelconditionsObj.AttributeName = dr["AttributeName"].ToString();
        //                HotelconditionsObj.Charged = Convert.ToBoolean(dr["Charged"]);
        //            }
        //            ListOfModel.Add(HotelconditionsObj);
        //           // Count++;
        //        }

        //    }
        //    return ListOfModel;
        //}
    }
}