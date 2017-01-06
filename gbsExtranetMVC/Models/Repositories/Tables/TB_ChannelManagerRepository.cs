using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_ChannelManagerRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_ChannelManagerExt> ReadAll(int TableID)
        {
            List<TB_ChannelManagerExt> list = new List<TB_ChannelManagerExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTableNew_BizTbl_Table_Sp", SQLCon);
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
                    TB_ChannelManagerExt model = new TB_ChannelManagerExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Code = dr["Code"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Sorts = Convert.ToInt32(dr["Sort"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_ChannelManagerExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_ChannelManager obj = new TB_ChannelManager();
           // obj.ID = model.ID;
            obj.Code = model.Code;
            obj.Name = model.Name;
            obj.Sort = Convert.ToInt16(model.Sorts);
            obj.Active = Convert.ToBoolean(model.Active);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_ChannelManager.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_ChannelManagerExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_ChannelManager.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_ChannelManager.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_ChannelManagerExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_ChannelManager.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Code = model.Code;
            obj.Name = model.Name;
            obj.Sort = Convert.ToInt16(model.Sorts);
            obj.Active = Convert.ToBoolean(model.Active);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.SaveChanges();

            return status;
        }


    }

    public class TB_ChannelManagerExt
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Sorts { get; set; }
        public bool Active { get; set; }
        public DateTime OpDateTime { get; set; }
        public int OpUserID { get; set; }
    }
}