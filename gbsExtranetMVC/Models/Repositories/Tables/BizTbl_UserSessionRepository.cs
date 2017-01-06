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
    public class BizTbl_UserSessionRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_UserSessionExt> ReadAll(int TableID)
        {
            List<BizTbl_UserSessionExt> list = new List<BizTbl_UserSessionExt>();
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
                    BizTbl_UserSessionExt model = new BizTbl_UserSessionExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.code = dr["Code"].ToString();
                    model.UserID = dr["UserID"].ToString();
                    model.User = dr["FK_UserID_ID"].ToString();
                    model.Country = dr["FK_CountryID_ID"].ToString();
                    model.CountryID = dr["CountryID"].ToString();
                    model.IpAddress = dr["IPAddress"].ToString();
                    model.Date = Convert.ToDateTime(dr["OpDateTime"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(BizTbl_UserSessionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_UserSession obj = new BizTbl_UserSession();
            obj.ID = model.ID;
            obj.Code = model.code;
            obj.UserID = Convert.ToInt64( model.UserID);
            obj.CountryID = Convert.ToInt32 (model.CountryID);
            obj.IPAddress = model.IpAddress;
            obj.OpDateTime = DateTime.Now;
           

            db.BizTbl_UserSession.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32( obj.ID);

            return status;
        }

        public bool Delete(BizTbl_UserSessionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_UserSession.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_UserSession.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_UserSessionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_UserSession.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Code = model.code;
            obj.UserID = Convert.ToInt64(model.UserID);
            obj.CountryID = Convert.ToInt32(model.CountryID);
            obj.IPAddress = model.IpAddress;
            obj.OpDateTime = DateTime.Now;
            db.SaveChanges();

            return status;
        }

        //public List<HitcountExt> SecurityGroup()
        //{

        //    DBEntities entity = new DBEntities();
        //    DataTable dt = new DataTable();
        //    var Culture = new SqlParameter("@CultureCode", CultureCode);
        //    dt.Columns.Add("ID");
        //    dt.Columns.Add("Description");
        //    List<HitcountExt> ListOfModel = new List<HitcountExt>();
        //    var result = entity.Database.SqlQuery<GetSecurityGroupdropdown_Result>("B_Ex_GetSecurityGroup_BizTbl_SecurityGroup_SP @CultureCode", Culture).ToList();


        //    foreach (GetSecurityGroupdropdown_Result Val in result)
        //    {
        //        dt.Rows.Add(Val.ID, Val.Description);
        //    }
        //    if (dt.Rows.Count > 0)
        //    {
        //        HitcountExt HitObjDefault = new HitcountExt();
        //        HitObjDefault.ID = -1;
        //        HitObjDefault.Name = "All...";
        //        ListOfModel.Add(HitObjDefault);

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            HitcountExt HitObj = new HitcountExt();
        //            HitObj.ID = Convert.ToInt32(dr["ID"]);
        //            HitObj.Name = dr["Description"].ToString();
        //            ListOfModel.Add(HitObj);
        //        }
        //    }
        //    return ListOfModel;
        //}
    }

    public class BizTbl_UserSessionExt
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string code  { get; set; }
        public string User { get; set; }
        public string UserID { get; set; }
        public string Country { get; set; }
        public string CountryID { get; set; }
        public string IpAddress { get; set; }

        

       // [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "This field is required!")]
        public DateTime Date { get; set; }
        public long OpUserID { get; set; }
    }
}