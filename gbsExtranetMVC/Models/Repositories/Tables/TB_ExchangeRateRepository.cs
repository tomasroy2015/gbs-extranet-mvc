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
    public class TB_ExchangeRateRepository : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_ExchangeRateExt> ReadAll(int TableID)
        {
            List<TB_ExchangeRateExt> list = new List<TB_ExchangeRateExt>();
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
                    TB_ExchangeRateExt model = new TB_ExchangeRateExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Currency = dr["FK_CurrencyID_ID"].ToString();
                    model.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                    model.Rate = Convert.ToDecimal( dr["Rate"]);
                    model.DateID = Convert.ToInt32(dr["DateID"]);
                    model.Date = Convert.ToDateTime(dr["FK_DateID_ID"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_ExchangeRateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_ExchangeRate obj = new TB_ExchangeRate();
            obj.ID = model.ID;
            obj.CurrencyID = model.CurrencyID;
            obj.DateID = Convert.ToInt32(model.DateID);
            obj.Rate = model.Rate;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.TB_ExchangeRate.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_ExchangeRateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_ExchangeRate.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_ExchangeRate.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_ExchangeRateExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_ExchangeRate.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.CurrencyID = model.CurrencyID;
            obj.DateID = Convert.ToInt32(model.DateID);
            obj.Rate = model.Rate;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }
    }
    public class TB_ExchangeRateExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int CurrencyID { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int DateID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public decimal Rate { get; set; }
       // public DateTime OpDateTime { get; set; }
       // public long OpUserID { get; set; }
    }
}