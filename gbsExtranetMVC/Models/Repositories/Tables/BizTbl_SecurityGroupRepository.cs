using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_SecurityGroupRepository : BaseRepository
    {

        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


        public List<BizTbl_SecurityGroupExt> ReadAll(int TableID)
        {
            List<BizTbl_SecurityGroupExt> list = new List<BizTbl_SecurityGroupExt>();

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
                    BizTbl_SecurityGroupExt model = new BizTbl_SecurityGroupExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Code = dr["Code"].ToString();
                    model.Description = dr["Description"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(BizTbl_SecurityGroupExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_SecurityGroup.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Code = model.Code;
            obj.Description_en = model.Description;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

        public bool Delete(BizTbl_SecurityGroupExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_SecurityGroup.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_SecurityGroup.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(BizTbl_SecurityGroupExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_SecurityGroup Object = new BizTbl_SecurityGroup();
            Object.ID = model.ID;
            Object.Code = model.Code;
            Object.Description_en = model.Description;
            Object.OpDateTime = DateTime.Now;
            Object.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            Object.Level = 4;
            db.BizTbl_SecurityGroup.Add(Object);
            db.SaveChanges();

            int id = Object.ID;

            return status;
        }
    }
    public class BizTbl_SecurityGroupExt
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Code { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
    }
}