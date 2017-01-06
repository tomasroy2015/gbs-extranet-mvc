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
    public class TB_HotelPromotionRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelPromotionExt> ReadAll(int TableID)
        {
            List<TB_HotelPromotionExt> list = new List<TB_HotelPromotionExt>();

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
                    TB_HotelPromotionExt PageObj = new TB_HotelPromotionExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.Hotel = dr["FK_HotelID_ID"].ToString();
                    PageObj.Promotion = dr["FK_PromotionID_ID"].ToString();
                    PageObj.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    PageObj.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    PageObj.HasDiscount = Convert.ToBoolean(dr["HasDiscount"].ToString());
                    PageObj.DiscountPercentage = dr["DiscountPercentage"].ToString();
                    PageObj.AccommodationStartDate = Convert.ToDateTime(dr["AccommodationStartDate"].ToString());
                    PageObj.AccommodationEndDate = Convert.ToDateTime(dr["AccommodationEndDate"].ToString());
                    PageObj.DayID = dr["DayID"].ToString();
                    PageObj.DayCount = Convert.ToInt32(dr["DayCount"]);
                    if (dr["EarlyBookerMargin"].ToString()!="")
                    {
                        PageObj.EarlyBookerMargin = Convert.ToInt32(dr["EarlyBookerMargin"].ToString());
                    }
                    else
                    {
                        PageObj.EarlyBookerMargin = null;
                    }
                    if (dr["LastMinuteMargin"].ToString() != "")
                    {
                        PageObj.LastMinuteMargin = Convert.ToInt32(dr["LastMinuteMargin"].ToString());
                    }
                    else
                    {
                        PageObj.LastMinuteMargin = null;
                    }
                    if (dr["BookingDate"].ToString() != "")
                    {
                        PageObj.BookingDate = Convert.ToDateTime(dr["BookingDate"].ToString());
                    }
                    else
                    {
                        PageObj.BookingDate = null;
                    }
                   
                  
                    PageObj.PricePolicyID = dr["PricePolicyID"].ToString();
                    PageObj.SecretDeal = Convert.ToBoolean(dr["SecretDeal"].ToString());
                    PageObj.Region = dr["Region"].ToString();
                    PageObj.ValidForAllRoomTypes = Convert.ToBoolean(dr["ValidForAllRoomTypes"].ToString());
                    PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                    PageObj.HotelID = Convert.ToInt32(dr["HotelID"].ToString());
                    PageObj.PromotionID = Convert.ToInt32(dr["PromotionID"].ToString());
                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(TB_HotelPromotionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_HotelPromotion PageObj = new TB_HotelPromotion();
            //  PageObj.ID = model.ID;
            PageObj.HotelID = model.HotelID;
            PageObj.PromotionID = model.PromotionID;
            PageObj.StartDate = model.StartDate;
            PageObj.EndDate = model.EndDate;
            PageObj.HasDiscount = model.HasDiscount;
            PageObj.DiscountPercentage = Convert.ToInt32(model.DiscountPercentage);
            PageObj.AccommodationEndDate = model.AccommodationEndDate;
            PageObj.AccommodationStartDate = model.AccommodationStartDate;
            PageObj.DayID = Convert.ToString(model.DayID);
            PageObj.DayCount = model.DayCount;
            PageObj.EarlyBookerMargin = model.EarlyBookerMargin;
            PageObj.LastMinuteMargin = model.LastMinuteMargin;
            PageObj.BookingDate = model.BookingDate;
            PageObj.PricePolicyID = Convert.ToString(model.PricePolicyID);
            PageObj.SecretDeal = model.SecretDeal;
            PageObj.Region = model.Region;
            PageObj.ValidForAllRoomTypes = model.ValidForAllRoomTypes;
            PageObj.Active = model.Active;
            PageObj.SecretDeal = model.SecretDeal;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            PageObj.CreateDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            insertentity.TB_HotelPromotion.Add(PageObj);
            insertentity.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelPromotionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelPromotion.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelPromotion.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_HotelPromotionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_HotelPromotion.Where(x => x.ID == model.ID).FirstOrDefault();
            PageObj.ID = model.ID;
            PageObj.HotelID = model.HotelID;
            PageObj.PromotionID = model.PromotionID;
            PageObj.StartDate = model.StartDate;
            PageObj.EndDate = model.EndDate;
            PageObj.HasDiscount = model.HasDiscount;
            PageObj.DiscountPercentage = Convert.ToInt32(model.DiscountPercentage);
            PageObj.AccommodationEndDate = model.AccommodationEndDate;
            PageObj.AccommodationStartDate = model.AccommodationStartDate;
            PageObj.DayID = Convert.ToString(model.DayID);
            PageObj.DayCount = model.DayCount;
            PageObj.EarlyBookerMargin = model.EarlyBookerMargin;
            PageObj.LastMinuteMargin = model.LastMinuteMargin;
            PageObj.BookingDate = model.BookingDate;
            PageObj.PricePolicyID = Convert.ToString(model.PricePolicyID);
            PageObj.SecretDeal = model.SecretDeal;
            PageObj.Region = model.Region;
            PageObj.ValidForAllRoomTypes = model.ValidForAllRoomTypes;
            PageObj.Active = model.Active;
            PageObj.SecretDeal = model.SecretDeal;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            db.SaveChanges();
            return status;
        }

    }
    public class TB_HotelPromotionExt
    {
        public int ID { get; set; }
        public bool Active { get; set; }

        public string Hotel { get; set; }

        public string Promotion { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public bool HasDiscount { get; set; }

        public string DiscountPercentage { get; set; }
          [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AccommodationStartDate { get; set; }
          [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AccommodationEndDate { get; set; }

        public string DayID { get; set; }

        public int DayCount { get; set; }

        public int? EarlyBookerMargin { get; set; }

        public int? LastMinuteMargin { get; set; }
          [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BookingDate { get; set; }

        public string PricePolicyID { get; set; }

        public bool SecretDeal { get; set; }

        public string Region { get; set; }

        public bool ValidForAllRoomTypes { get; set; }

        public int HotelID { get; set; }

        public int PromotionID { get; set; }
    }
}