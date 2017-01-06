using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelRoomAttributeRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_HotelRoomAttributeExt> ReadAll(int TableID)
        {
            List<TB_HotelRoomAttributeExt> list = new List<TB_HotelRoomAttributeExt>();

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
                    TB_HotelRoomAttributeExt PageObj = new TB_HotelRoomAttributeExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelRoomID = Convert.ToInt32(dr["FK_HotelRoomID_ID"].ToString());
                    PageObj.Attribute = dr["FK_AttributeID_ID"].ToString();
                    PageObj.Charged = Convert.ToBoolean(dr["Charged"]);
                    PageObj.Unitvalue = dr["UnitValue"].ToString();
                    PageObj.Charge = dr["Charge"].ToString();
                    PageObj.CurrencyID = dr["CurrencyID"].ToString();
                    PageObj.AttributeID = Convert.ToInt32(dr["AttributeID"].ToString());
                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(TB_HotelRoomAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_HotelRoomAttribute PageObj = new TB_HotelRoomAttribute();
            PageObj.HotelRoomID = model.HotelRoomID;
            PageObj.Charged = Convert.ToBoolean(model.Charged);
            PageObj.AttributeID = model.AttributeID;
            PageObj.Charge = Convert.ToDecimal(model.Charge);
            PageObj.UnitValue = model.Unitvalue;
            PageObj.CurrencyID = Convert.ToInt32(model.CurrencyID);
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.TB_HotelRoomAttribute.Add(PageObj);
            insertentity.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelRoomAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelRoomAttribute.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelRoomAttribute.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_HotelRoomAttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_HotelRoomAttribute.Where(x => x.ID == model.ID).FirstOrDefault();
            PageObj.ID = model.ID;
            PageObj.HotelRoomID = model.HotelRoomID;
            PageObj.Charged = Convert.ToBoolean(model.Charged);
            PageObj.AttributeID = model.AttributeID;
            PageObj.Charge = Convert.ToDecimal(model.Charge);
            PageObj.UnitValue = model.Unitvalue;
            PageObj.CurrencyID = Convert.ToInt32(model.CurrencyID);
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

    }
    public class TB_HotelRoomAttributeExt
    {
        public int ID { get; set; }
        public int HotelRoomID { get; set; }
        public int AttributeID { get; set; }
        public bool Charged { get; set; }
        public string Unitvalue { get; set; }
        public string Attribute { get; set; }
        public string Charge { get; set; }
        public string CurrencyID { get; set; }
       // public int HotelRoomID { get; set; }
     
    }
}