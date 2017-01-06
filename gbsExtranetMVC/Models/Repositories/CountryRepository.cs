using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class CountryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<CountryExt> ReadAll()
        {
            List<CountryExt> list = new List<CountryExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetCountrytble_TB_Country_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            //return dt;

          
            //string MailTemplateID = "";
            //string Description = "";
          //  CountryTble = objcountry.GetCountriestble(CultureCode);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CountryExt CountryObj = new CountryExt();
                    CountryObj.ID = Convert.ToInt32(dr["ID"]);
                    CountryObj.Name = dr["Name"].ToString();
                    CountryObj.Code = dr["Code"].ToString();
                    CountryObj.CultureCode = dr["CultureCode"].ToString();
                    CountryObj.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                    CountryObj.Currency = dr["Currency"].ToString();
                    CountryObj.VAT = dr["VAT"].ToString();
                    CountryObj.CityTax = Convert.ToBoolean(dr["HasCityTax"]);
                    CountryObj.HitCount = Convert.ToInt64(dr["HitCount"]);
                    CountryObj.Sort = Convert.ToInt16(dr["Sort"]);
                    CountryObj.Active = Convert.ToBoolean(dr["Active"]);
                    CountryObj.TemparoryCode = dr["TempCode"].ToString();
                    list.Add(CountryObj);
                }
            }

            return list;

        }

        public bool Create(CountryExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_Country MsgObj = new TB_Country();
            MsgObj.CurrencyID = model.CurrencyID;
            MsgObj.Name_en = model.Name;
            MsgObj.Code = model.Code;
            MsgObj.CultureCode = model.CultureCode;
            MsgObj.VAT = Convert.ToInt32(model.VAT);
            MsgObj.HasCityTax = model.CityTax;
            MsgObj.HitCount = model.HitCount;
            MsgObj.Sort = model.Sort;
            MsgObj.Active = model.Active;
            MsgObj.TempCode = Convert.ToInt32(model.TemparoryCode);
            MsgObj.OpDateTime= DateTime.Now;
            MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.TB_Country.Add(MsgObj);
            insertentity.SaveChanges();
            int id = MsgObj.ID;
            return status;
        }

        public bool Delete(CountryExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            using (DBEntities DE = new DBEntities())
            {
                var MessageTable = DE.TB_Country.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_Country.Remove(MessageTable);
                DE.SaveChanges();
            }
            return status;

        }

        public bool Update(CountryExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var MessageTable = DE.TB_Country.Where(x => x.ID == model.ID).FirstOrDefault();
                MessageTable.CurrencyID = model.CurrencyID;
                MessageTable.Name_en = model.Name;
                MessageTable.Code = model.Code;
                MessageTable.CultureCode = model.CultureCode;
                MessageTable.VAT = Convert.ToInt32(model.VAT);
                MessageTable.HasCityTax = model.CityTax;
                MessageTable.HitCount = model.HitCount;
                MessageTable.Sort = model.Sort;
                MessageTable.Active = model.Active;
                MessageTable.TempCode = Convert.ToInt32(model.TemparoryCode);
                DE.SaveChanges();
            }
            return status;
        }
    }

    public class CountryExt
    {
        public int ID { get; set; }

       
        public string Name { get; set; }

         [Required(ErrorMessage = "This field is required")]
        public string Code { get; set; }

        
       
        public string CultureCode { get; set; }
        public int CurrencyID { get; set; }
        public string Currency { get; set; }
        public string VAT { get; set; }
        public bool CityTax { get; set; }
        public long HitCount { get; set; }
        public short Sort { get; set; }
        public bool Active { get; set; }
        public string TemparoryCode { get; set; }
        public DateTime OpDateTime { get; set; }
       
    }
}