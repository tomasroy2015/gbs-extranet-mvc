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
    public class TB_HotelPromotionFreebieRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelPromotionFreebieExt> ReadAll(int TableID)
        {
            List<TB_HotelPromotionFreebieExt> list = new List<TB_HotelPromotionFreebieExt>();

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
                    TB_HotelPromotionFreebieExt PageObj = new TB_HotelPromotionFreebieExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelPromotionID = Convert.ToInt32(dr["FK_HotelPromotionID_ID"]);
                    PageObj.FreebieID = Convert.ToInt32(dr["FK_FreebieID_ID"]);
                    PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                    PageObj.CreateDateTime =Convert.ToDateTime(dr["CreateDateTime"].ToString());
                    PageObj.CreateUser = dr["FK_CreateUserID_ID"].ToString();
                    PageObj.CreateUserID = Convert.ToInt32(dr["CreateUserID"]);
                    list.Add(PageObj);
                }
            }


            return list;
        }



        public bool Create(TB_HotelPromotionFreebieExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_HotelPromotionFreebie PageObj = new TB_HotelPromotionFreebie();
            PageObj.HotelPromotionID = model.HotelPromotionID;
            PageObj.FreebieID = model.FreebieID;
            PageObj.Active = model.Active;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.CreateDateTime = model.CreateDateTime;
            PageObj.CreateUserID = model.CreateUserID;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]) ;
            insertentity.TB_HotelPromotionFreebie.Add(PageObj);
            insertentity.SaveChanges();
            return status;
        }

        public bool Delete(TB_HotelPromotionFreebieExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelPromotionFreebie.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelPromotionFreebie.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Update(TB_HotelPromotionFreebieExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_HotelPromotionFreebie.Where(x => x.ID == model.ID).FirstOrDefault();
            PageObj.ID = model.ID;
            PageObj.HotelPromotionID = model.HotelPromotionID;
            PageObj.FreebieID = model.FreebieID;
            PageObj.Active = model.Active;
            PageObj.CreateDateTime = model.CreateDateTime;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.CreateUserID = model.CreateUserID;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
          
          
        }
    }
    public class TB_HotelPromotionFreebieExt
    {
        public int ID { get; set; }
        public int HotelPromotionID { get; set; }
   
        public bool Active { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDateTime { get; set; }
        //public string Description { get; set; }

        public int CreateUserID { get; set; }

        public string CreateUser { get; set; }

        public int FreebieID { get; set; }
    }
}