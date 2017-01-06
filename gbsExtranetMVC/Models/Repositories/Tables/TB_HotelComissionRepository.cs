using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelComissionRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelComissionExt> ReadAll(int TableID)
        {
            List<TB_HotelComissionExt> list = new List<TB_HotelComissionExt>();

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
                    TB_HotelComissionExt model = new TB_HotelComissionExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Hotel = dr["FK_HotelID"].ToString();
                    model.HotelID = Convert.ToInt32(dr["HotelID"]);
                    
                    model.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    model.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    model.Comission = Convert.ToInt32(dr["Comission"]);                 
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(TB_HotelComissionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelComission.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.StartDate = Convert.ToDateTime(model.StartDate);
            obj.EndDate = Convert.ToDateTime(model.EndDate);
            obj.Comission = Convert.ToInt16(model.Comission);           
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelComissionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_HotelComission.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelComission.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(TB_HotelComissionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_HotelComission obj = new TB_HotelComission();
            //obj.ID = model.ID;
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.StartDate = Convert.ToDateTime(model.StartDate);
            obj.EndDate = Convert.ToDateTime(model.EndDate);
            obj.Comission = Convert.ToInt16(model.Comission);
            obj.OpDateTime = DateTime.Now;         
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_HotelComission.Add(obj);
            db.SaveChanges();
            int id = obj.ID;
            return status;
        }
    }
    public class TB_HotelComissionExt
    {
        public int ID { get; set; }
        public int HotelID { get; set; }
        public string Hotel { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public int Comission { get; set; }      
    }
}