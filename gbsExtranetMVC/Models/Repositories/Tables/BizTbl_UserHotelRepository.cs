using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_UserHotelRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_UserHotelExt> ReadAll(int TableID)
        {
            List<BizTbl_UserHotelExt> list = new List<BizTbl_UserHotelExt>();
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
                    BizTbl_UserHotelExt model = new BizTbl_UserHotelExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.User = dr["FK_UserID_ID"].ToString();
                    model.HotelID = dr["HotelID"].ToString();
                    model.Hotel = dr["FK_HotelID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }


        public List<BizTbl_UserHotelExt> GetTableValue()
        {

            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            //var Culture = new SqlParameter("@CultureCode", CultureCode);
            dt.Columns.Add("id");
            dt.Columns.Add("Name");
            List<BizTbl_UserHotelExt> ListOfModel = new List<BizTbl_UserHotelExt>();
            var result = entity.Database.SqlQuery<GetUserHotel_Result>("B_Ex_GetUserHotel_BizTbl_UserHotel_SP").ToList();


            foreach (GetUserHotel_Result Val in result)
            {
                dt.Rows.Add(Val.id, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_UserHotelExt HitObj = new BizTbl_UserHotelExt();
                    HitObj.ID = Convert.ToInt32(dr["id"]);
                    HitObj.Name = dr["Name"].ToString();
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }
    }
    public class BizTbl_UserHotelExt
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string HotelID { get; set; }
        public string Hotel { get; set; }
        public string Name { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}