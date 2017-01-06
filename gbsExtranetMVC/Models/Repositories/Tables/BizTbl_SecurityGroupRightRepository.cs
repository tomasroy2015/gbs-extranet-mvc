using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_SecurityGroupRightRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<BizTbl_SecurityGroupRightExt> ReadAll(int TableID)
        {
            List<BizTbl_SecurityGroupRightExt> list = new List<BizTbl_SecurityGroupRightExt>();

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
                    BizTbl_SecurityGroupRightExt model = new BizTbl_SecurityGroupRightExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Role = dr["Code"].ToString();
                    model.RoleID = Convert.ToInt32(dr["RoleID"]);
                    model.SecurityID = Convert.ToInt32(dr["SecurityID"]);
                    model.SecurityCode = dr["Description"].ToString();
                    model.Right = Convert.ToBoolean(dr["Right_"].ToString());
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(BizTbl_SecurityGroupRightExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_SecurityGroupRight.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.ID = model.ID;
            obj.SecurityGroupID = Convert.ToInt32(model.RoleID);
            obj.SecurityID = Convert.ToInt32(model.SecurityID);
            obj.HasRight =  model.Right;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(BizTbl_SecurityGroupRightExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_SecurityGroupRight.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_SecurityGroupRight.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(BizTbl_SecurityGroupRightExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_SecurityGroupRight obj = new BizTbl_SecurityGroupRight();
            obj.SecurityGroupID = Convert.ToInt32(model.RoleID);
            obj.SecurityID = Convert.ToInt32(model.SecurityID);
            obj.HasRight = model.Right;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.BizTbl_SecurityGroupRight.Add(obj);
            db.SaveChanges();
            int id = obj.ID;
            return status;
        }

    }

    public class BizTbl_SecurityGroupRightExt
    {
        public int ID { get; set; }
        public string Role { get; set; }
        public int RoleID { get; set; }
        public string SecurityCode { get; set; }
        public int SecurityID { get; set; }
        public bool Right { get; set; }
    }
}