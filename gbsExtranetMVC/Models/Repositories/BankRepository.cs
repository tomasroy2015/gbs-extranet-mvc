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
    public class BankRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<BankExt> GetBankDetails()
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetBank_TB_Bank_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<BankExt> list = new List<BankExt>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BankExt BankObj = new BankExt();
                    BankObj.ID = Convert.ToInt32(dr["ID"]);
                    BankObj.CountryID = Convert.ToInt32(dr["CountryID"]);
                    BankObj.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                    BankObj.Country = dr["Country"].ToString();
                    BankObj.Currency = dr["Currency"].ToString();
                    BankObj.Bank = dr["BankName"].ToString();
                    BankObj.BankBranch = dr["BankBranchName"].ToString();
                    BankObj.BankAccountNumber = dr["BankAccountNumber"].ToString();
                    BankObj.IBAN = dr["IBAN"].ToString();
                    BankObj.SWIFT = dr["SWIFT"].ToString();
                    BankObj.OtherInfo = dr["OtherInfo"].ToString();

                    list.Add(BankObj);
                }
            }
            return list;
            //DBEntities entity = new DBEntities();
            //var Culture = new SqlParameter("@CultureCode", CultureValue);
            //var result = entity.Database.SqlQuery<GetBankDetails_Result>("B_GetBank_TB_Bank_SP @CultureCode", Culture).ToList();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ID");
            //dt.Columns.Add("Country");
            //dt.Columns.Add("Currency");
            //dt.Columns.Add("BankName");
            //dt.Columns.Add("BankBranchName");
            //dt.Columns.Add("BankAccountNumber");
            //dt.Columns.Add("IBAN");
            //dt.Columns.Add("SWIFT");
            //dt.Columns.Add("OtherInfo");

            //foreach (GetBankDetails_Result dr in result)
            //{
            //    dt.Rows.Add(dr.ID, dr.Country, dr.Currency, dr.BankName, dr.BankBranchName, dr.BankAccountNumber, dr.IBAN,
            //        dr.SWIFT, dr.OtherInfo);
            //}
            //return dt;

        }
        public bool Create(BankExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_Bank MsgObj = new TB_Bank();
            MsgObj.CountryID = Convert.ToInt32(model.CountryID);
            MsgObj.CurrencyID = Convert.ToInt32(model.CurrencyID);
            MsgObj.BankName = model.Bank;
            MsgObj.BankBranchName = model.BankBranch;
            MsgObj.BankAccountNumber = model.BankAccountNumber;
            MsgObj.IBAN = model.IBAN;
            MsgObj.SWIFT = model.SWIFT;
            MsgObj.OtherInfo = model.OtherInfo;
            MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            MsgObj.OpDateTime = DateTime.Now;
            insertentity.TB_Bank.Add(MsgObj);           
            insertentity.SaveChanges();
            int id = MsgObj.ID;
            return status;
        }

        public bool Delete(BankExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            using (DBEntities DE = new DBEntities())
            {
                var BankTable = DE.TB_Bank.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_Bank.Remove(BankTable);
                DE.SaveChanges();
            }
            return status;

        }

        public bool Update(BankExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var BankTable = DE.TB_Bank.Where(x => x.ID == model.ID).FirstOrDefault();
                BankTable.CountryID = model.CountryID;
                BankTable.CurrencyID = Convert.ToInt32(model.CurrencyID);
                BankTable.BankName = model.Bank;
                BankTable.BankBranchName = model.BankBranch;
                BankTable.BankAccountNumber = model.BankAccountNumber;
                BankTable.IBAN = model.IBAN;
                BankTable.SWIFT = model.SWIFT;
                BankTable.OtherInfo = model.OtherInfo;
               
                DE.SaveChanges();
            }
            return status;
        }
     
    }
    public class BankExt
    {
        public int ID { get; set; }

         [Required(ErrorMessage = "Please select from list!")]
        public int CountryID { get; set; }
        public int CurrencyID { get; set; }

       
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Bank { get; set; }
        public string BankBranch { get; set; }
        public string BankAccountNumber { get; set; }
        public string IBAN { get; set; }
        public string SWIFT { get; set; }
        public string OtherInfo { get; set; }

    }
}