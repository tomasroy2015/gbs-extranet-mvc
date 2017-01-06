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
    public class TB_HotelRoomRepository:BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelRoomExt> ReadAll(int TableID)
        {
            List<TB_HotelRoomExt> list = new List<TB_HotelRoomExt>();

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
                    TB_HotelRoomExt PageObj = new TB_HotelRoomExt();
                    PageObj.ID = Convert.ToInt32(dr["FK_ID_ID"]);
                    PageObj.Hotel = dr["FK_HotelID_ID"].ToString();
                    PageObj.Description = dr["Description"].ToString();
                    PageObj.RoomType = dr["FK_RoomTypeID_ID"].ToString();
                    PageObj.RoomCount = dr["RoomCount"].ToString();
                    PageObj.RoomSize = dr["RoomSize"].ToString();
                    PageObj.MaxPeopleCount = dr["MaxPeopleCount"].ToString();
                    PageObj.MaxChildrenCount = dr["MaxChildrenCount"].ToString();
                    PageObj.BabyCotCount = dr["BabyCotCount"].ToString();
                    PageObj.ExtraBedCount = dr["ExtraBedCount"].ToString();
                    PageObj.SmokingType = dr["FK_SmokingTypeID_ID"].ToString();
                    PageObj.ViewType = dr["FK_ViewTypeID_ID"].ToString();
                    try
                    {
                        PageObj.Promotion = Convert.ToBoolean(dr["Promotion"].ToString());
                    }
                    catch   
                    {
                        PageObj.Promotion = false;
                    }
                   
                    PageObj.RelatedHotelRoomID = dr["FK_RelatedHotelRoomID_ID"].ToString();
                    PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());

                    PageObj.Sorts = dr["Sort"].ToString();
                    PageObj.SmokingTypeID = dr["SmokingTypeID"].ToString();
                    PageObj.ViewTypeID = dr["ViewTypeID"].ToString();
                    PageObj.RoomTypeID = dr["RoomTypeID"].ToString();
                    PageObj.HotelID = dr["HotelID"].ToString();
                    
                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(TB_HotelRoomExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_HotelRoom PageObj = new TB_HotelRoom();
            PageObj.HotelID = Convert.ToInt32(model.HotelID);
            PageObj.Description_en = model.Description;
            PageObj.RoomTypeID = Convert.ToInt32(model.RoomTypeID);
            PageObj.RoomCount = Convert.ToInt32(model.RoomCount);
            PageObj.RoomSize = Convert.ToInt32(model.RoomSize);
            PageObj.MaxChildrenCount = Convert.ToInt16(model.MaxChildrenCount);
            PageObj.MaxPeopleCount = Convert.ToInt16(model.MaxPeopleCount);
            PageObj.BabyCotCount = Convert.ToInt16(model.BabyCotCount);
            PageObj.ExtraBedCount = Convert.ToInt16(model.ExtraBedCount);
            if (model.ViewTypeID != null)
            {
                PageObj.ViewTypeID = Convert.ToInt32(model.ViewTypeID);
            }
            else
            {
                PageObj.ViewTypeID = null;
            }
            if (model.SmokingTypeID != null)
            {
                PageObj.SmokingTypeID = Convert.ToInt32(model.SmokingTypeID);
            }
            else
            {
                PageObj.SmokingTypeID = null;
            }
            PageObj.Promotion = Convert.ToBoolean(model.HotelPromotionID);
            PageObj.Active = Convert.ToBoolean(model.Active);
            if (model.RelatedHotelRoomID != null)
            {
                PageObj.RelatedHotelRoomID = Convert.ToInt32(model.RelatedHotelRoomID);
            }
            else
            {
                PageObj.RelatedHotelRoomID = null;
            }
            if (model.Sorts != null)
            {
                PageObj.Sort = Convert.ToInt32(model.Sorts);
            }
            else
            {
                PageObj.Sort = null;
            }
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            PageObj.CreateDateTime = DateTime.Now;
            PageObj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            insertentity.TB_HotelRoom.Add(PageObj);
            insertentity.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelRoomExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelRoom.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelRoom.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_HotelRoomExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_HotelRoom.Where(x => x.ID == model.ID).FirstOrDefault();
            PageObj.ID = model.ID;
            PageObj.HotelID = Convert.ToInt32(model.HotelID);
            PageObj.Description_en = model.Description;
            PageObj.RoomTypeID = Convert.ToInt32(model.RoomTypeID);
            PageObj.RoomCount = Convert.ToInt32(model.RoomCount);
            PageObj.RoomSize = Convert.ToInt32(model.RoomSize);
            PageObj.MaxChildrenCount =Convert.ToInt16(model.MaxChildrenCount);
            PageObj.MaxPeopleCount = Convert.ToInt16(model.MaxPeopleCount);
            PageObj.BabyCotCount = Convert.ToInt16(model.BabyCotCount);
            PageObj.ExtraBedCount = Convert.ToInt16(model.ExtraBedCount);

            if (model.ViewTypeID!=null)
            {
                PageObj.ViewTypeID = Convert.ToInt32(model.ViewTypeID);
            }
            else
            {
                PageObj.ViewTypeID = null;
            }
            if (model.SmokingTypeID != null)
            {
                PageObj.SmokingTypeID = Convert.ToInt32(model.SmokingTypeID);
            }
            else
            {
                PageObj.SmokingTypeID = null;
            }
            PageObj.Promotion =Convert.ToBoolean( model.HotelPromotionID);
            PageObj.Active = Convert.ToBoolean(model.Active);
            if (model.Sorts != null)
            {
                PageObj.Sort = Convert.ToInt32(model.Sorts);
            }
            else
            {
                PageObj.Sort = null;
            }
           
            PageObj.OpDateTime = DateTime.Now;
            if (model.RelatedHotelRoomID!=null)
            {
                PageObj.RelatedHotelRoomID = Convert.ToInt32(model.RelatedHotelRoomID);
            }
            else
            {
                PageObj.RelatedHotelRoomID = null;
            }
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
            PageObj.CreateDateTime = DateTime.Now;
            db.SaveChanges();
            return status;
        }

    }
    public class TB_HotelRoomExt
    {
        public int ID { get; set; }
        public string HotelPromotionID { get; set; }
        public string Hotel { get; set; }
        public string Description { get; set; }
        public string RoomType { get; set; }
        //[Required(ErrorMessage = "This field is required!")]
        //[Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public string RoomCount { get; set; }
        //[Required(ErrorMessage = "This field is required!")]
        //[Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public string RoomSize { get; set; }
        public string MaxPeopleCount { get; set; }
        //[Required(ErrorMessage = "This field is required!")]
        //[Range(0, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public string BabyCotCount { get; set; }
        //[Required(ErrorMessage = "This field is required!")]
        //[Range(0, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public string ExtraBedCount { get; set; }
        public string RelatedHotelRoomID { get; set; }
        public string SmokingType { get; set; }
        public string ViewType { get; set; }
        public bool Promotion { get; set; }
        public bool Active { get; set; }

        public string HotelID { get; set; }
        public string SmokingTypeID { get; set; }
        public string ViewTypeID { get; set; }
        public string RoomTypeID { get; set; }
        public string MaxChildrenCount { get; set; }
        public string Sorts { get; set; }
    }
}