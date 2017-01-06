using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelMinumumAccommodationRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelMinumumAccommodationExt> ReadAll(int TableID)
        {
            List<TB_HotelMinumumAccommodationExt> list = new List<TB_HotelMinumumAccommodationExt>();

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
                    TB_HotelMinumumAccommodationExt model = new TB_HotelMinumumAccommodationExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelID = dr["HotelID"].ToString();
                    model.Hotel = dr["FK_HotelID"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.MinDayCount = Convert.ToInt16(dr["MinDayCount"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(TB_HotelMinumumAccommodationExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelMinumumAccommodation.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.StartDate = Convert.ToDateTime(model.StartDate);
            obj.EndDate = Convert.ToDateTime(model.EndDate);
            obj.MinDayCount = Convert.ToInt16(model.MinDayCount);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelMinumumAccommodationExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelMinumumAccommodation.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelMinumumAccommodation.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(TB_HotelMinumumAccommodationExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_HotelMinumumAccommodation obj = new TB_HotelMinumumAccommodation();
          //  obj.ID = model.ID;
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.StartDate = Convert.ToDateTime(model.StartDate);
            obj.EndDate = Convert.ToDateTime(model.EndDate);
            obj.MinDayCount = Convert.ToInt16(model.MinDayCount);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_HotelMinumumAccommodation.Add(obj);
            db.SaveChanges();
            int id = obj.ID;
            return status;
        }
    }
    public class TB_HotelMinumumAccommodationExt
    {
        public int ID { get; set; }

        public string HotelID { get; set; }
        public string Hotel { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int MinDayCount { get; set; }
    }
}