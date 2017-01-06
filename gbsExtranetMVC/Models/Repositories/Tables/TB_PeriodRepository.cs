using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories.Tables
{
    public class TB_PeriodRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_PeriodExt> ReadAll(int TableID)
        {
            List<TB_PeriodExt> list = new List<TB_PeriodExt>();

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
                    TB_PeriodExt model = new TB_PeriodExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.Period = dr["Period"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"].ToString());                    
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_PeriodExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            TB_Period obj = new TB_Period();
            // MailTable.MailTemplateID =model.MailTemplateID;  
            //obj.ID = model.ID;
            obj.StartDate = model.StartDate;
            obj.EndDate = model.EndDate;
            obj.Period = model.Period;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_Period.Add(obj);
            db.SaveChanges();
            Int64 id = obj.ID;
            return status;
        }

        public bool Update(TB_PeriodExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_Period.Where(x => x.ID == model.ID).FirstOrDefault();
            // MailTable.MailTemplateID =model.MailTemplateID;
            obj.ID = model.ID;
            obj.StartDate = model.StartDate;
            obj.EndDate = model.EndDate;
            obj.Period = model.Period;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_PeriodExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Period.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Period.Remove(obj);
            db.SaveChanges();
            return status;
        }
    }

    public class TB_PeriodExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string Period { get; set; }
        public Boolean Active { get; set; }
    }
}