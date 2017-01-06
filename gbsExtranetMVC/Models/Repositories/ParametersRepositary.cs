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
    public class ParametersRepositary : BaseRepository
    {
        string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<ParameterExt> GetParameters()
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetParameters_BizTbl_Parameters_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<ParameterExt> list = new List<ParameterExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ParameterExt ParaObj = new ParameterExt();
                    ParaObj.ID = Convert.ToInt32(dr["ID"]);
                    ParaObj.Code = dr["Code"].ToString();
                    ParaObj.Value = dr["Value"].ToString();
                    ParaObj.Description = dr["Description"].ToString();
                    ParaObj.IsCommon = Convert.ToBoolean(dr["IsCommon"]);
                    ParaObj.Date = Convert.ToDateTime(dr["OpDateTime"]);
                    list.Add(ParaObj);
                }
            }
            return list;
        }


        public bool Create(ParameterExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            BizTbl_Parameter MsgObj = new BizTbl_Parameter();
            MsgObj.ID = model.ID;
            MsgObj.Code = model.Code;
            MsgObj.Value = model.Value;
            MsgObj.Description_en = model.Description;
            MsgObj.IsCommon = model.IsCommon;
            MsgObj.OpDateTime = DateTime.Now;
            MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.BizTbl_Parameter.Add(MsgObj);
            insertentity.SaveChanges();
            int id = MsgObj.ID;
            return status;
        }

        public bool Update(ParameterExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var MessageTable = DE.BizTbl_Parameter.Where(x => x.ID == model.ID).FirstOrDefault();
                MessageTable.Code = model.Code;
                MessageTable.Value = model.Value;
                MessageTable.Description_en = model.Description;
                MessageTable.IsCommon = model.IsCommon;
                MessageTable.OpDateTime = DateTime.Now;
                MessageTable.OpUserID = model.OpUserID;
                DE.SaveChanges();
            }
            return status;
        }

    

        public bool Delete(ParameterExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            
            using (DBEntities DE = new DBEntities())
            {
                var MessageTable = DE.BizTbl_Parameter.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.BizTbl_Parameter.Remove(MessageTable);
                DE.SaveChanges();
            }
            return status;

        }

       


    }
    public class ParameterExt
    {
        public int ID { get; set; }

         [Required(ErrorMessage = "This field is required")]
        public string Code { get; set; }

         [Required(ErrorMessage = "This field is required")]
        public string Value { get; set; }

         [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        public bool IsCommon { get; set; }

        public DateTime Date { get; set; }

        public string Operation { get; set; }

        public long OpUserID { get; set; }
    }


}