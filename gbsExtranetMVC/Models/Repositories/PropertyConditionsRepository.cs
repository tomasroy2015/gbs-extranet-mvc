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
    public class PropertyConditionsRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        List<PropertyConditionsExt> HotelconditionsObj = new List<PropertyConditionsExt>();
        List<PropertyConditionsExt> AttributesListOfModel = new List<PropertyConditionsExt>();
        

           public DataTable GetAttributeHeaders()
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetAttributeHeaders", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort");
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            SQLCon.Close();

           

            return dt;
        }
           public DataTable GetAttributes()
           {
               // string PropertyConditions = "";
               DataTable dt = new DataTable();
               SQLCon.Open();
               SqlCommand cmd = new SqlCommand("TB_SP_GetAttributes", SQLCon);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@PartID", 1);
               cmd.Parameters.AddWithValue("@AttributeTypeID", 2);
               // cmd.Parameters.AddWithValue("@AttributeHeaderID", AttributeHeaderId);
               cmd.Parameters.AddWithValue("@Active", true);
               cmd.Parameters.AddWithValue("@Culture", CultureValue);
               cmd.Parameters.AddWithValue("@OrderBy", "ID");

               SqlDataAdapter sda = new SqlDataAdapter(cmd);

               sda.Fill(dt);
               SQLCon.Close();



               return dt;
           }
           public DataTable HotelPropertyConditions(int HotelID)
           {
               DataTable dt = new DataTable();
               SQLCon.Open();
               SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAttributes", SQLCon);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Active", true);
               cmd.Parameters.AddWithValue("@HotelID", HotelID);
               cmd.Parameters.AddWithValue("@Culture", CultureValue);
               cmd.Parameters.AddWithValue("@OrderBy", "AttributeID");
               cmd.Parameters.AddWithValue("@AttributeTypeID", 2);
               SqlDataAdapter sda = new SqlDataAdapter(cmd);

               sda.Fill(dt);
               SQLCon.Close();
              
               return dt;

           }
        public int GetcurrencyID(int HotelID)
        {
            int status=0;
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("B_Ex_GetCurrencyIDByHotel_TB_Hotel_Sp", SQLCon);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@HotelId", HotelID);
               status = Convert.ToInt32(cmd.ExecuteScalar());
               SQLCon.Close();
               return status;
        }


        public bool DeleteHotelAttributes(string HotelID, string AttributeTypeID )
        {

            DateTime Date1 = DateTime.Now.Date;
            int HotelIDValue = Convert.ToInt32(HotelID);
            int AttributeTypeIDvalue = Convert.ToInt32(AttributeTypeID);
            var hotelAvailability = from hotelAttribute in db.TB_HotelAttribute
                                    join Attributes in db.TB_Attribute
                                    on hotelAttribute.AttributeID  equals Attributes.ID
                                    where Attributes.AttributeTypeID == 2 && hotelAttribute.HotelID == HotelIDValue
                                    && Date1 >= hotelAttribute.StartDate && Date1 <= hotelAttribute.EndDate
                                    select hotelAttribute;

            foreach (var items in hotelAvailability)
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


        public int InsertPropertyConditions(int HotelID, string attributeID, string UnitID, string CurrencyID, string Prices, string Charge, string Unitvalues, Controller ctrl)
        {
            int i = 1;
            int Charged = Convert.ToInt32(Charge);
            double PriceCharge = 0;
            if (Prices != "")
            {
                PriceCharge = Convert.ToDouble(Prices);
            }

            if (Convert.ToString(HotelID) != string.Empty && HotelID != 0)
            {
                TB_HotelAttribute hotelAttribute = new TB_HotelAttribute();
                hotelAttribute.HotelID = HotelID;
                hotelAttribute.AttributeID = Convert.ToInt32(attributeID);
                hotelAttribute.Charged = Convert.ToBoolean(Charged);
                //hotelAttribute.UnitID = (Nullable<int>)CheckEmptyStringDBParameter(UnitID,true);
                Nullable<int> UnitIDValue = (Nullable<int>)CheckEmptyStringDBParameter(UnitID, true);
                if (UnitIDValue == 0)
                {
                    hotelAttribute.UnitID = null;
                }
                else
                {
                    hotelAttribute.UnitID = UnitIDValue;
                }

                hotelAttribute.UnitValue = (string)CheckEmptyStringDBParameter(Unitvalues);
                if (PriceCharge > 0)
                {
                    hotelAttribute.Charge = Convert.ToDecimal(Prices);
                }
                hotelAttribute.CurrencyID = (Nullable<int>)CheckEmptyStringDBParameter(CurrencyID, true);
                hotelAttribute.StartDate = DateTime.Now.Date;

                hotelAttribute.EndDate = Convert.ToDateTime("2100-12-31", new CultureInfo("en-US", false));
                hotelAttribute.Active = true;
                hotelAttribute.OpDateTime = DateTime.Now.Date;
                hotelAttribute.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

                db.TB_HotelAttribute.Add(hotelAttribute);
                db.SaveChanges();

            }
            //SQLCon.Open();

            
            //SqlCommand cmd = new SqlCommand("B_Ex_InsertHotelAttribute_TB_HotelAttribute_SP", SQLCon);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@HotelID", HotelID);
            //cmd.Parameters.AddWithValue("@AttributeID", Convert.ToInt32(attributeID));
            //cmd.Parameters.AddWithValue("@Charged", Convert.ToInt32(Charge));
            //if (UnitID == "" || UnitID == "0")
            //{

            //    cmd.Parameters.AddWithValue("@UnitID", null);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@UnitID", Convert.ToInt32(UnitID));
            //}

            //if (Unitvalues == "" || Unitvalues == "0")
            //{

            //    cmd.Parameters.AddWithValue("@UnitValue", null);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@UnitValue", Unitvalues);
            //}
            //if (Prices == "" || Prices == "0")
            //{

            //    cmd.Parameters.AddWithValue("@Charge", null);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@Charge", Convert.ToDecimal(Prices));
            //}

            //cmd.Parameters.AddWithValue("@OpUserID", 0);
            //if (CurrencyID == "" || CurrencyID == "0")
            //{

            //    cmd.Parameters.AddWithValue("@CurrencyID", null);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@CurrencyID ", Convert.ToInt32(CurrencyID));
            //}
            ////DateTime StartDate = DateTime.ParseExact(DateTime.Now.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ////DateTime dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ////cmd.Parameters.AddWithValue("@StartDate", DateTime.Now.Date);
            ////cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime("2100-12-31"));
            //i = cmd.ExecuteNonQuery();
            //SQLCon.Close();
            return i;

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
    public class PropertyConditionSaveModel
    {
        public string AttributeID{ get; set; }
        public string UnitID { get; set; }
        public string CurrencyID{ get; set; }
        public string Price { get; set; }
        public string Charged { get; set; }
        public string Unitvalue { get; set; }
    }
        public class PropertyConditionsExt
        {
            public int HotelID { get; set; }

            public string AttributeHeaderName { get; set; }

            public int ID { get; set; }

            public bool Chargeable { get; set; }
            public int AttributeTypeID { get; set; }
            public string AttributeName { get; set; }

            public string Accessibility { get; set; }

            public string CityTax { get; set; }

            public string Breakfast { get; set; }

            public string Children { get; set; }

            public string DomesticAnimal { get; set; }

            public string Parking { get; set; }

            public string ExtraBed { get; set; }

            public string Internet { get; set; }

            public string BabyCot { get; set; }

            public string Activity { get; set; }

            public string Service { get; set; }

            public string General { get; set; }

            public string GeneralHeaderName { get; set; }

            public string CityTaxHeaderName { get; set; }

            public string BreakfastHeaderName { get; set; }

            public string ChildrenName { get; set; }

            public string ChildrenHeaderName { get; set; }

            public string DomesticAnimalName { get; set; }

            public string ParkingHeaderName { get; set; }

            public string InternetHeaderName { get; set; }

            public string ActivityHeaderName { get; set; }

            public string ServiceHeaderName { get; set; }

            public string BabyCotHeaderName { get; set; }

            public string ExtraBedHeaderName { get; set; }

            public int GeneralID { get; set; }

            public int ServiceID { get; set; }

            public int ActivityID { get; set; }

            public int BabyCotID { get; set; }

            public int ExtraBedID { get; set; }

            public int InternetID { get; set; }

            public int ParkingID { get; set; }

            public int DomesticAnimalID { get; set; }

            public int ChildrenID { get; set; }

            public int BreakfastID { get; set; }

            public int CityTaxID { get; set; }

            public string AttributeHeaderId { get; set; }

            public int AttributeId { get; set; }

            public bool hasAttribute { get; set; }
            public bool Charged { get; set; }

            public string UnitName { get; set; }

            public string UnitID { get; set; }

            public string HotelUnitID { get; set; }

            public string HotelUnitName { get; set; }

            public string CurrencyID { get; set; }

            public string Chargedvalue { get; set; }

            public bool charge { get; set; }
            public string design { get; set; }

            public string Currency { get; set; }

            public string AttributeHeaderCode { get; set; }

            public string checkedradio { get; set; }

            public string UnitValue { get; set; }
        }
    
}