using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelCreditCardRepository : BaseRepository
    {

        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelCreditCardExt> ReadAll(int TableID)
        {
            List<TB_HotelCreditCardExt> list = new List<TB_HotelCreditCardExt>();

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
                    TB_HotelCreditCardExt model = new TB_HotelCreditCardExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelID = Convert.ToInt32(dr["HotelID"]);
                    model.Hotel = dr["FK_HotelID"].ToString();
                    model.CreditCardTypeID = Convert.ToInt32(dr["CreditCardTypeID"]);  
                    model.CreditCardType = dr["FK_CreditCardTypeID"].ToString();                  
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(TB_HotelCreditCardExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelCreditCard.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.CreditCardTypeID = Convert.ToInt32(model.CreditCardTypeID);          
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelCreditCardExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelCreditCard.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelCreditCard.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(TB_HotelCreditCardExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_HotelCreditCard obj = new TB_HotelCreditCard();
           // obj.ID = model.ID;
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.CreditCardTypeID = Convert.ToInt32(model.CreditCardTypeID);              
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_HotelCreditCard.Add(obj);
            db.SaveChanges();
            int id = obj.ID;
            return status;
        }
    }

    public class TB_HotelCreditCardExt
    {
        public int ID { get; set; }

        public int HotelID { get; set; } 
        public string Hotel { get; set; }

        public int CreditCardTypeID { get; set; }
        public string CreditCardType { get; set; }
    }
}