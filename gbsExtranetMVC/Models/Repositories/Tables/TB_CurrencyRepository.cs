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
    public class TB_CurrencyRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        
        public List<TB_CurrencyExt> ReadAll(int TableID)
        {
            List<TB_CurrencyExt> list = new List<TB_CurrencyExt>();
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
                    TB_CurrencyExt model = new TB_CurrencyExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Code = dr["Code"].ToString();
                    model.Symbol = dr["Symbol"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Sorts = Convert.ToInt32(dr["Sort"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_CurrencyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_Currency obj = new TB_Currency();
            obj.ID = model.ID;
            obj.Code = model.Code;
            obj.Symbol = model.Symbol;
            obj.Name_en = model.Name;
            obj.Sort = Convert.ToInt16(model.Sorts);
            obj.Active = Convert.ToBoolean(model.Active);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_Currency.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_CurrencyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Currency.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Currency.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_CurrencyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Currency.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Code = model.Code;
            obj.Symbol = model.Symbol;
            obj.Name_en = model.Name;
            obj.Sort = Convert.ToInt16(model.Sorts);
            obj.Active = Convert.ToBoolean(model.Active);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }


    }

    public class TB_CurrencyExt
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(5, ErrorMessage = "This field cannot be longer than 5 characters.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Sorts { get; set; }
        public bool Active { get; set; }
        public DateTime OpDateTime { get; set; }
        public int OpUserID { get; set; }
    }
}