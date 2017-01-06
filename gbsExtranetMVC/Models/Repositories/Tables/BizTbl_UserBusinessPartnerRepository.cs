using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_UserBusinessPartnerRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_UserBusinessPartnerExt> ReadAll(int TableID)
        {
            List<BizTbl_UserBusinessPartnerExt> list = new List<BizTbl_UserBusinessPartnerExt>();
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
                    BizTbl_UserBusinessPartnerExt model = new BizTbl_UserBusinessPartnerExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.User = dr["FK_UserID_ID"].ToString();
                    model.BusinessPartnerID = Convert.ToInt32(dr["BusinessPartnerID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }


        public List<BizTbl_UserBusinessPartnerExt> GetTableValue()
        {

            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            //var Culture = new SqlParameter("@CultureCode", CultureCode);
            dt.Columns.Add("id");
            dt.Columns.Add("Name");
            List<BizTbl_UserBusinessPartnerExt> ListOfModel = new List<BizTbl_UserBusinessPartnerExt>();
            var result = entity.Database.SqlQuery<GetBusinessPartener_Result>("B_Ex_GetBusinessPartner_BizTbl_UserBusinessPartner_SP").ToList();


            foreach (GetBusinessPartener_Result Val in result)
            {
                dt.Rows.Add(Val.id, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_UserBusinessPartnerExt HitObj = new BizTbl_UserBusinessPartnerExt();
                    HitObj.ID = Convert.ToInt32(dr["id"]);
                    HitObj.Name = dr["Name"].ToString();
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }
        public List<BizTbl_UserBusinessPartnerExt> GetTableValue1()
        {

            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            //var Culture = new SqlParameter("@CultureCode", CultureCode);
            dt.Columns.Add("id");
            dt.Columns.Add("Name");
            List<BizTbl_UserBusinessPartnerExt> ListOfModel = new List<BizTbl_UserBusinessPartnerExt>();
            var result = entity.Database.SqlQuery<GetBusinessPartener_Result>("B_Ex_GetBusinessPartner_BizTbl_UserBusinessPartner_SP").ToList();


            foreach (GetBusinessPartener_Result Val in result)
            {
                dt.Rows.Add(Val.id, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                BizTbl_UserBusinessPartnerExt HitObjDefault = new BizTbl_UserBusinessPartnerExt();
                HitObjDefault.BusinessPartnerID = -1;
                HitObjDefault.BusinessPartner = "All...";
                ListOfModel.Add(HitObjDefault);

                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_UserBusinessPartnerExt HitObj = new BizTbl_UserBusinessPartnerExt();
                    HitObj.BusinessPartnerID = Convert.ToInt32(dr["id"]);
                    HitObj.BusinessPartner = dr["Name"].ToString();
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }

    }
    public class BizTbl_UserBusinessPartnerExt
    {
        public int ID { get; set; }
        public string User { get; set; }
        public int BusinessPartnerID { get; set; }
        public string BusinessPartner { get; set; }
        public string Name { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}