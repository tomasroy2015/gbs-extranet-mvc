using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Business;
using System.Collections;
using gbsExtranetMVC.Models;
using Extension;


namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_TypeHotelBudgetRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TypeHotelBudgetExt> GetAllTableValue(int TableID)
        {
            List<TB_TypeHotelBudgetExt> list = new List<TB_TypeHotelBudgetExt>();

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
                    TB_TypeHotelBudgetExt model = new TB_TypeHotelBudgetExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                    model.Margin = Convert.ToInt32(dr["Margin"]);
                    model.EndValue = Convert.ToInt32(dr["EndValue"]);
                    model.Currency = dr["FK_CurrencyID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_TypeHotelBudgetExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_TypeHotelBudget DepObj = new TB_TypeHotelBudget();
            //DepObj.ID = model.ID;
            DepObj.CurrencyID = model.CurrencyID;
            DepObj.EndValue = model.EndValue;
            DepObj.Margin = model.Margin;
            DepObj.OpDateTime = DateTime.Now;
            DepObj.OpUserID = 0;
            insertentity.TB_TypeHotelBudget.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_TypeHotelBudgetExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeHotelBudget.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.CurrencyID = model.CurrencyID;
                DepObj.EndValue = model.EndValue;
                DepObj.Margin = model.Margin;
                DepObj.OpDateTime = DateTime.Now;
                DepObj.OpUserID = 0;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_TypeHotelBudgetExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeHotelBudget.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_TypeHotelBudget.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }

    public class TB_TypeHotelBudgetExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int CurrencyID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Currency { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int Margin { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int EndValue { get; set; }

        //IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        //{
        //    List<ValidationResult> res = new List<ValidationResult>();
        //    if (Currency == "-- Select --")
        //    {
        //        ValidationResult mss = new ValidationResult("This field is required!");
        //        res.Add(mss);

        //    }
        //    return res;
        //}
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Currency == "-- Select --")
            {
                yield return new ValidationResult("This field is required!");
            }
        }
    }
}