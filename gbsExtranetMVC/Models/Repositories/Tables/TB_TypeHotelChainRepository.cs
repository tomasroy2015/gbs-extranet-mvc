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
    public class TB_TypeHotelChainRepository: BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TypeHotelChainExt> GetAllTableValue(int TableID)
        {
            List<TB_TypeHotelChainExt> list = new List<TB_TypeHotelChainExt>();

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
                    TB_TypeHotelChainExt model = new TB_TypeHotelChainExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Name = dr["Name"].ToString();
                    model.Sorts = Convert.ToInt32(dr["Sort"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_TypeHotelChainExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_TypeHotelChain DepObj = new TB_TypeHotelChain();
            DepObj.Name = model.Name;
            DepObj.Sort = Convert.ToInt16(model.Sorts);
            DepObj.Active = model.Active;
            DepObj.OpDateTime = DateTime.Now;
            DepObj.OpUserID = 0;
            insertentity.TB_TypeHotelChain.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_TypeHotelChainExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeHotelChain.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.Name = model.Name;
                DepObj.Sort = Convert.ToInt16(model.Sorts);
                DepObj.Active = model.Active;
                DepObj.OpDateTime = DateTime.Now;
                DepObj.OpUserID = 0;
                DepObj.Sort = Convert.ToInt16(model.Sorts);
                DepObj.Active = model.Active;
                DepObj.OpDateTime = DateTime.Now;
                DepObj.OpUserID = 0;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_TypeHotelChainExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeHotelChain.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_TypeHotelChain.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }
    public class TB_TypeHotelChainExt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Sorts { get; set; }
        public bool Active { get; set; }
    }
}