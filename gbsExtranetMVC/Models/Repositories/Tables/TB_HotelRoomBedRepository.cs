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
    public class TB_HotelRoomBedRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelRoomBedExt> ReadAll(int TableID)
        {
            List<TB_HotelRoomBedExt> list = new List<TB_HotelRoomBedExt>();

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
                    TB_HotelRoomBedExt PageObj = new TB_HotelRoomBedExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.OptionNo = Convert.ToInt32(dr["OptionNo"].ToString());
                    PageObj.HotelRoomID = Convert.ToInt32(dr["FK_HotelRoomID_ID"]);
                    PageObj.BedType = dr["FK_BedTypeID_ID"].ToString();
                    PageObj.Count = Convert.ToInt32(dr["Count"]);
                    PageObj.BedTypeID = Convert.ToInt32(dr["BedTypeID"].ToString());
                    list.Add(PageObj);
                }
            }


            return list;
        }

        public bool Create(TB_HotelRoomBedExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_HotelRoomBed PageObj = new TB_HotelRoomBed();
            PageObj.OptionNo = model.OptionNo;
            PageObj.HotelRoomID = model.HotelRoomID;
            PageObj.BedTypeID = model.BedTypeID;
            PageObj.Count = model.Count;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.TB_HotelRoomBed.Add(PageObj);
            insertentity.SaveChanges();
            return status;
        }

        public bool Delete(TB_HotelRoomBedExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelRoomBed.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelRoomBed.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_HotelRoomBedExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_HotelRoomBed.Where(x => x.ID == model.ID).FirstOrDefault();
            PageObj.ID = model.ID;
            PageObj.OptionNo= model.OptionNo;
            PageObj.HotelRoomID = model.HotelRoomID;
            PageObj.BedTypeID = model.BedTypeID;
            PageObj.Count = model.Count;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

    }
    public class TB_HotelRoomBedExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int OptionNo { get; set; }
         [Required(ErrorMessage = "This field is required!")]
        public int HotelRoomID { get; set; }
         [Required(ErrorMessage = "This field is required!")]
        public int BedTypeID { get; set; }
    
        public string BedType { get; set; }
        public string Attribute { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int Count { get; set; }
        public string CurrencyID { get; set; }
      

    }
}