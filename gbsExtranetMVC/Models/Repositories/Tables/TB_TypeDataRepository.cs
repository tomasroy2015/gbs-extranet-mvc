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
    public class TB_TypeDataRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TypeDataExt> GetAllTableValue(int TableID)
        {
            List<TB_TypeDataExt> list = new List<TB_TypeDataExt>();

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
                    TB_TypeDataExt model = new TB_TypeDataExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Name = dr["Name"].ToString();
                  
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_TypeDataExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_TypeData DepObj = new TB_TypeData();
            DepObj.ID = model.ID;
            DepObj.Name = model.Name;
            insertentity.TB_TypeData.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_TypeDataExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeData.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.Name = model.Name;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_TypeDataExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeData.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_TypeData.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }

    public class TB_TypeDataExt
    {

        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int ID { get; set; }
        public string Name { get; set; }
     
    }
}