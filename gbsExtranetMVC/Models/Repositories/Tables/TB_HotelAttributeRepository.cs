using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelAttributeRepository : BaseRepository
    {

        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelAttributeExt> ReadAll(int TableID)
        {
            List<TB_HotelAttributeExt> list = new List<TB_HotelAttributeExt>();

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
                    TB_HotelAttributeExt model = new TB_HotelAttributeExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelID = dr["HotelID"].ToString();
                    model.Hotel = dr["FK_HotelID"].ToString();
                    model.AttributeID = dr["AttributeID"].ToString();
                    model.Attribute = dr["FK_AttributeID"].ToString();
                    model.Charged = Convert.ToBoolean(dr["Charged"]);
                    model.UnitID = dr["UnitID"].ToString();
                    model.Unit = dr["FK_UnitID"].ToString();
                    model.UnitValue = dr["UnitValue"].ToString();
                    model.Charge = dr["Charge"].ToString();
                    model.CurrencyID = dr["CurrencyID"].ToString();
                    model.Currency = dr["FK_CurrencyID"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime( dr["EndDate"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);                     
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(TB_HotelAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelAttribute.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.HotelID = Convert.ToInt32(model.HotelID);        
            obj.AttributeID =Convert.ToInt32( model.AttributeID);
            obj.Charged = model.Charged;
            obj.UnitID = Convert.ToInt32( model.UnitID);
            obj.UnitValue = model.UnitValue;
            obj.Charge = Convert.ToDecimal(model.Charge);
            obj.CurrencyID = Convert.ToInt32( model.CurrencyID);
            obj.StartDate = Convert.ToDateTime( model.StartDate);
            obj.EndDate = Convert.ToDateTime( model.EndDate);
            obj.Active = Convert.ToBoolean(model.Active);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelAttribute.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelAttribute.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(TB_HotelAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_HotelAttribute obj = new TB_HotelAttribute();
            obj.ID = model.ID;
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.AttributeID = Convert.ToInt32(model.AttributeID);
            obj.Charged =  Convert.ToBoolean(model.Charged);
            obj.UnitID = Convert.ToInt32(model.UnitID);
            obj.UnitValue = model.UnitValue;
            obj.Charge = Convert.ToDecimal(model.Charge);
            obj.CurrencyID = Convert.ToInt32(model.CurrencyID);
            obj.StartDate =model.StartDate;
            obj.EndDate = model.EndDate;
            obj.Active = Convert.ToBoolean(model.Active);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_HotelAttribute.Add(obj);
            db.SaveChanges();
            int id = obj.ID;
            return status;
        }
    }

    public class TB_HotelAttributeExt
    {

        
        public int ID { get; set; }

         [Required(ErrorMessage = "Please select from list!")]
        public string HotelID { get; set; }

       
        public string Hotel { get; set; }

         [Required(ErrorMessage = "Please select from list!")]
        public string AttributeID { get; set; }

       
        public string Attribute { get; set; }
        public bool Charged { get; set; }
        public string UnitID { get; set; }
        public string Unit { get; set; }
        public string UnitValue { get; set; }
        public string Charge { get; set; }

        public string CurrencyID { get; set; }
        public string Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }
}