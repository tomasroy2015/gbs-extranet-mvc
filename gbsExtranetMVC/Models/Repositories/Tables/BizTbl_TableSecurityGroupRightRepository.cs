using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_TableSecurityGroupRightRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_TableSecurityGroupRightExt> ReadAll(int TableID)
        {
            List<BizTbl_TableSecurityGroupRightExt> list = new List<BizTbl_TableSecurityGroupRightExt>();
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
                    BizTbl_TableSecurityGroupRightExt model = new BizTbl_TableSecurityGroupRightExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.TableID = Convert.ToInt32(dr["TableID"]);
                    model.SecurityGroupId = Convert.ToInt32(dr["SecurityGroupId"]);
                    model.Table = dr["FK_TableID_ID"].ToString();
                    model.Role = dr["FK_SecurityGroupID_ID"].ToString();
                    model.EditableStatus = Convert.ToBoolean( dr["CanUpdate"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(BizTbl_TableSecurityGroupRightExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_TableSecurityGroupRight obj = new BizTbl_TableSecurityGroupRight();
            //obj.ID = model.ID;
            obj.TableID = model.TableID;
            obj.SecurityGroupID = model.SecurityGroupId;
            obj.CanUpdate = model.EditableStatus;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.BizTbl_TableSecurityGroupRight.Add(obj);
            db.SaveChanges();

            int id = obj.ID;

            return status;
        }

        public bool Delete(BizTbl_TableSecurityGroupRightExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_TableSecurityGroupRight.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_TableSecurityGroupRight.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_TableSecurityGroupRightExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_TableSecurityGroupRight.Where(x => x.ID == model.ID).FirstOrDefault();
            //obj.ID = model.ID;
            obj.TableID = model.TableID;
            obj.SecurityGroupID = model.SecurityGroupId;
            obj.CanUpdate = model.EditableStatus;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }

        public List<HitcountExt> SecurityGroup()
        {

            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            dt.Columns.Add("ID");
            dt.Columns.Add("Description");
            List<HitcountExt> ListOfModel = new List<HitcountExt>();
            var result = entity.Database.SqlQuery<GetSecurityGroupdropdown_Result>("B_Ex_GetSecurityGroup_BizTbl_SecurityGroup_SP @CultureCode", Culture).ToList();


            foreach (GetSecurityGroupdropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Description);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HitcountExt HitObj = new HitcountExt();
                    HitObj.ID = Convert.ToInt32(dr["ID"]);
                    HitObj.Name = dr["Description"].ToString();
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }
    }

    public class BizTbl_TableSecurityGroupRightExt
    {
        public int ID { get; set; }
        public int TableID { get; set; }
        public int SecurityGroupId { get; set; }
        public string Table { get; set; }
        public string Role { get; set; }
        public bool EditableStatus { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}