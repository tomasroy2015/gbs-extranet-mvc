using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelRateRepository:BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelRateExt> ReadAll(int TableID)
        {
            List<TB_HotelRateExt> list = new List<TB_HotelRateExt>();

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
                    TB_HotelRateExt PageObj = new TB_HotelRateExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelRoomID = Convert.ToInt32(dr["FK_HotelRoomID_ID"].ToString());
                    PageObj.Date =Convert.ToDateTime( dr["FK_DateID_ID"].ToString());
                    PageObj.PricePolicy = dr["FK_PricePolicyTypeID_ID"].ToString();
                    PageObj.HotelAccommodation = dr["FK_HotelAccommodationTypeID_ID"].ToString();
                    PageObj.SinglePrice = Convert.ToDouble(dr["SinglePrice"].ToString());
                    PageObj.DoublePrice = Convert.ToDouble(dr["DoublePrice"].ToString());
                    PageObj.RoomPrice = Convert.ToDouble(dr["RoomPrice"].ToString());
                    PageObj.Currency = dr["FK_CurrencyID_ID"].ToString();
                    PageObj.DateID = Convert.ToInt32(dr["DateID"]);
                    PageObj.PricePolicyTypeID = Convert.ToInt32(dr["PricePolicyTypeID"].ToString());
                    PageObj.AccommodationID = Convert.ToInt32(dr["AccommodationID"].ToString());
                    PageObj.CurrencyID = Convert.ToInt32(dr["CurrencyID"].ToString());

                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(TB_HotelRateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_HotelRate PageObj = new TB_HotelRate();
          //  PageObj.ID = model.ID;
            PageObj.PricePolicyTypeID = model.PricePolicyTypeID;
            PageObj.HotelAccommodationTypeID = model.AccommodationID;
            PageObj.CurrencyID = model.CurrencyID;
            PageObj.DateID = model.DateID;
            PageObj.HotelRoomID = model.HotelRoomID;
            PageObj.SinglePrice = Convert.ToDecimal(model.SinglePrice);
            PageObj.DoublePrice = Convert.ToDecimal(model.DoublePrice);
            PageObj.RoomPrice = Convert.ToDecimal(model.RoomPrice);
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.TB_HotelRate.Add(PageObj);
            insertentity.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelRateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelRate.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelRate.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_HotelRateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_HotelRate.Where(x => x.ID == model.ID).FirstOrDefault();
            PageObj.PricePolicyTypeID= model.PricePolicyTypeID;
            PageObj.HotelAccommodationTypeID = model.AccommodationID;
            PageObj.CurrencyID = model.CurrencyID;
            PageObj.DateID = model.DateID;
            PageObj.HotelRoomID = model.HotelRoomID;
            PageObj.SinglePrice =Convert.ToDecimal( model.SinglePrice);
            PageObj.DoublePrice = Convert.ToDecimal(model.DoublePrice);
            PageObj.RoomPrice = Convert.ToDecimal(model.RoomPrice);
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

    }   
    public class TB_HotelRateExt
    {
        public int ID { get; set; }
        public int HotelRoomID { get; set; }
        public string Freebie { get; set; }
        public string HotelAccommodation { get; set; }
        public string PricePolicy { get; set; }
        public double SinglePrice { get; set; }
        public double DoublePrice { get; set; }
        public double RoomPrice { get; set; }
        public bool Active { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public int DateID { get; set; }
        public int PricePolicyTypeID { get; set; }
        public int AccommodationID { get; set; }
        public int CurrencyID { get; set; }
    }
}