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
    public class TB_TypeCreditCardRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TypeCreditCardExt> GetAllTableValue(int TableID)
        {
            List<TB_TypeCreditCardExt> list = new List<TB_TypeCreditCardExt>();

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
                    TB_TypeCreditCardExt model = new TB_TypeCreditCardExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Name = dr["Name"].ToString();
                    model.Code = dr["Code"].ToString();
                    model.CVCLength = Convert.ToInt32(dr["CVCLength"]);
                    model.CanBeUsedForReservation = Convert.ToBoolean(dr["CanBeUsedForReservation"]);
                    model.Sorts = dr["Sort"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_TypeCreditCardExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_TypeCreditCard DepObj = new TB_TypeCreditCard();
            //DepObj.ID = model.ID;
            DepObj.Name = model.Name;
            DepObj.Code = model.Code;
            DepObj.CVCLength = model.CVCLength;
            DepObj.CanBeUsedForReservation = model.CanBeUsedForReservation;
            DepObj.Sort = Convert.ToInt16(model.Sorts);
            DepObj.Active = model.Active;
            DepObj.OpDateTime = DateTime.Now;
            DepObj.OpUserID = 0;
            insertentity.TB_TypeCreditCard.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_TypeCreditCardExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeCreditCard.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.Name = model.Name;
                DepObj.Code = model.Code;
                DepObj.CVCLength = model.CVCLength;
                DepObj.CanBeUsedForReservation = model.CanBeUsedForReservation;
                DepObj.Sort = Convert.ToInt16(model.Sorts);
                DepObj.Active = model.Active;
                DepObj.OpDateTime = DateTime.Now;
                DepObj.OpUserID = 0;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_TypeCreditCardExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeCreditCard.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_TypeCreditCard.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }

    public class TB_TypeCreditCardExt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CVCLength { get; set; }
        public string Sorts { get; set; }
        public bool Active { get; set; }
        public bool CanBeUsedForReservation { get; set; }
        
    }
}