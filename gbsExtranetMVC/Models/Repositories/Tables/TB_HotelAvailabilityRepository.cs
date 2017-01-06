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
    public class TB_HotelAvailabilityRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelAvailabilityExt> ReadAll(int TableID)
        {
            List<TB_HotelAvailabilityExt> list = new List<TB_HotelAvailabilityExt>();
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
                    TB_HotelAvailabilityExt model = new TB_HotelAvailabilityExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelRoomID = Convert.ToInt32(dr["HotelRoomID"]);
                    model.HotelRoom = dr["FK_HotelRoomID_ID"].ToString();
                    model.DateID = Convert.ToInt32(dr["DateID"]);
                    model.Date = Convert.ToDateTime(dr["FK_DateID_ID"]);
                    model.RoomCount = Convert.ToInt32(dr["RoomCount"]);
                    model.CTA = Convert.ToBoolean(dr["CloseToArrival"]);
                    model.CTD = Convert.ToBoolean(dr["CloseToDeparture"]);
                    model.MinimumStay = Convert.ToInt32(dr["MinimumStay"]);
                    model.Closed = Convert.ToBoolean(dr["Closed"]);
                    
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_HotelAvailabilityExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_HotelAvailability obj = new TB_HotelAvailability();
            obj.ID = model.ID;
            obj.HotelRoomID = model.HotelRoomID;
            obj.DateID = model.DateID;
            obj.RoomCount =model.RoomCount;
            obj.CloseToArrival = model.CTA;
            obj.CloseToDeparture = model.CTD;
            obj.MinimumStay = model.MinimumStay;
            obj.Closed = model.Closed;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = 0;

            db.TB_HotelAvailability.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_HotelAvailabilityExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelAvailability.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelAvailability.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_HotelAvailabilityExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelAvailability.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.ID = model.ID;
            obj.HotelRoomID = model.HotelRoomID;
            obj.DateID = model.DateID;
            obj.RoomCount = model.RoomCount;
            obj.CloseToArrival = model.CTA;
            obj.CloseToDeparture = model.CTD;
            obj.MinimumStay = model.MinimumStay;
            obj.Closed = model.Closed;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = 0;
            db.SaveChanges();

            return status;
        }


    }

    public class TB_HotelAvailabilityExt
    {
        public int ID { get; set; }
        public int HotelRoomID { get; set; }
        public string HotelRoom { get; set; }
        public int DateID { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "This field is required!")]

        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int RoomCount { get; set; }
        public bool CTA { get; set; }
        public bool CTD { get; set; }
        public int MinimumStay { get; set; }
        public bool Closed { get; set; }
       
    }
}