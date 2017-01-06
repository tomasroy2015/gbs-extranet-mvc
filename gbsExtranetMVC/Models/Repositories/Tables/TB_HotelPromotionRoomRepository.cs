using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelPromotionRoomRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelPromotionRoomExt> ReadAll(int TableID)
        {
            List<TB_HotelPromotionRoomExt> list = new List<TB_HotelPromotionRoomExt>();

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
                    TB_HotelPromotionRoomExt PageObj = new TB_HotelPromotionRoomExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelPromotionID = Convert.ToInt32(dr["FK_HotelPromotionID_ID"].ToString());
                    PageObj.HotelRoomID = Convert.ToInt32(dr["FK_HotelRoomID_ID"].ToString());
                    PageObj.Active =Convert.ToBoolean(dr["Active"].ToString());
                   
                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(TB_HotelPromotionRoomExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_HotelPromotionRoom PageObj = new TB_HotelPromotionRoom();
           // PageObj.ID = model.ID;
            PageObj.HotelPromotionID = model.HotelPromotionID;
            PageObj.HotelRoomID = model.HotelRoomID;
            PageObj.CreateDateTime = DateTime.Now;
            PageObj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            PageObj.Active =model.Active;
            insertentity.TB_HotelPromotionRoom.Add(PageObj);
            insertentity.SaveChanges();
           return status;
        }
        public bool Delete(TB_HotelPromotionRoomExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelPromotionRoom.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelPromotionRoom.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_HotelPromotionRoomExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_HotelPromotionRoom.Where(x => x.ID == model.ID).FirstOrDefault();
            PageObj.ID = model.ID;
            PageObj.HotelPromotionID = model.HotelPromotionID;
            PageObj.HotelRoomID = model.HotelRoomID;
            PageObj.Active = model.Active;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

    }
    public class TB_HotelPromotionRoomExt
    {
        public int ID { get; set; }
        public int HotelPromotionID { get; set; }
        public int HotelRoomID { get; set; }
        public bool Active { get; set; }
     
    }
}