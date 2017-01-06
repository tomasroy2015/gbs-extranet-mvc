using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_FAQRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_FAQExt> ReadAll(int TableID)
        {
            List<TB_FAQExt> list = new List<TB_FAQExt>();

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
                    TB_FAQExt model = new TB_FAQExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Question = dr["Question"].ToString();
                    model.Answer = dr["Answer"].ToString();
                    model.Sorts = Convert.ToInt32(dr["sort"]);
                    model.Active = Convert.ToBoolean(dr["active"].ToString());
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(TB_FAQExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_FAQ.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Question_en = model.Question;
            obj.Answer_en = model.Answer;
            obj.Sort = model.Sorts;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_FAQExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_FAQ.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_FAQ.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(TB_FAQExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_FAQ obj = new TB_FAQ();
            obj.ID = model.ID;
            obj.Question_en = model.Question;
            obj.Answer_en = model.Answer;
            obj.Sort = model.Sorts;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_FAQ.Add(obj);
            db.SaveChanges();
            int id = obj.ID;

            return status;
        }

    }

    public class TB_FAQExt
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]


        public int ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int Sorts { get; set; }
        public bool Active { get; set; }
    }
}