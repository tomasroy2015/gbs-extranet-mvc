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
    public class TB_DateRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_DateExt> ReadAll(int TableID)
        {
            List<TB_DateExt> list = new List<TB_DateExt>();
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
                    TB_DateExt model = new TB_DateExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Date = Convert.ToDateTime(dr["Date"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_DateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_Date obj = new TB_Date();
            obj.ID = model.ID;
            obj.Date = model.Date;


            db.TB_Date.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_DateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Date.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Date.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_DateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Date.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Date = model.Date;
            db.SaveChanges();

            return status;
        }

        
    }

    public class TB_DateExt
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

    }
}