//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Data;
//using System.Data.SqlClient;
//using System.Globalization;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_TourAttributeRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_TourAttributeExt> ReadAll(int TableID)
        {
            List<TB_TourAttributeExt> list = new List<TB_TourAttributeExt>();

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
                    TB_TourAttributeExt model = new TB_TourAttributeExt();

                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Tour = dr["FK_TourID_ID"].ToString();
                    model.Attribute = dr["FK_AttributeID_ID"].ToString();
                    model.Charged = Convert.ToBoolean(dr["Charged"].ToString());
                    model.UnitValue = dr["UnitValue"].ToString();

                    //model.Charge = Convert.ToDecimal(dr["Charge"].ToString());
                    //model.CurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());


                    model.Charge = (Nullable<decimal>)CheckEmptyStringDBParameter(dr["Charge"].ToString(), false, false, false, true);
                    model.CurrencyID = (Nullable<int>)CheckEmptyStringDBParameter(dr["CurrencyID"].ToString(), true);

                    model.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    model.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.TourID = Convert.ToInt32(dr["TourID"]);
                    model.AttributeID = Convert.ToInt32(dr["AttributeID"]);

                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(TB_TourAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_TourAttribute.Where(x => x.ID == model.ID).FirstOrDefault();

            obj.ID = model.ID;
            obj.TourID = model.TourID;
            obj.AttributeID = model.AttributeID;
            obj.Charged = model.Charged;
            obj.UnitValue = model.UnitValue;
            obj.Charge = model.Charge;
            obj.CurrencyID = model.CurrencyID;
            obj.StartDate = Convert.ToDateTime(model.StartDate);
            obj.EndDate = Convert.ToDateTime(model.EndDate);            
            obj.Active = model.Active;     
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_TourAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_TourAttribute.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_TourAttribute.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(TB_TourAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_TourAttribute obj = new TB_TourAttribute();

            obj.TourID = model.TourID;
            obj.AttributeID = model.AttributeID;
            obj.Charged = model.Charged;
            obj.UnitValue = model.UnitValue;
            obj.Charge = model.Charge;
            obj.CurrencyID = model.CurrencyID;
            obj.StartDate = Convert.ToDateTime(model.StartDate);
            obj.EndDate = Convert.ToDateTime(model.EndDate);
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_TourAttribute.Add(obj);
            db.SaveChanges();
            int id = obj.ID;

            return status;
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

    public class TB_TourAttributeExt
    {
        public int ID { get; set; }
        public string Tour { get; set; }
        public string Attribute { get; set; }
        public bool Charged { get; set; }
        public string UnitValue { get; set; }
        public Nullable<decimal> Charge { get; set; }
        public string Sorts { get; set; }
        public Nullable<int> CurrencyID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public Boolean Active { get; set; }
        public int AttributeID { get; set; }
        public int TourID { get; set; }
    }

}