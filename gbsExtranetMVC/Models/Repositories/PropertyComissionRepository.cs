using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Transactions;
using System.Net;

namespace gbsExtranetMVC.Models.Repositories
{
    public class PropertyComissionRepository : BaseRepository
    {
         string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<PropertyComissionExt> GetComission(long id)
        {
           
            string Date=null;
            List<PropertyComissionExt> list = new List<PropertyComissionExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelComissions", SQLCon);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "StartDate");
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@HotelID", id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            // return dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyComissionExt EmailObj = new PropertyComissionExt();
                    EmailObj.ID = Convert.ToInt32(dr["ID"].ToString());
                    EmailObj.HotelID = dr["HotelID"].ToString();
                    EmailObj.StartDate = dr["StartDate"].ToString();
                    EmailObj.EndDate = dr["EndDate"].ToString();
                    EmailObj.Comission = dr["Comission"].ToString();
                    EmailObj.HotelName = dr["HotelName"].ToString();
                    list.Add(EmailObj);
                }
            }
            return list;

        }

       public DataTable GetComissionTable(int HotelID)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelComissions", SQLCon);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "StartDate");
            cmd.Parameters.AddWithValue("@Date", null);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            return dt;
        }

       public int SaveComission(int HotelID, DateTime StartDate, DateTime EndDate, string Comission, Controller ctrl)
        {
  
            //DateTime StartDat = DateTime.ParseExact(StartDate, @"d/M/yyyy", null);
            //DateTime EndDat = DateTime.ParseExact(EndDate, @"d/M/yyyy", null);
            int status = 1;
            TB_HotelComission ComissionObj = new TB_HotelComission();
            ComissionObj.HotelID = HotelID;
            ComissionObj.Comission = Convert.ToInt16(Comission);
            ComissionObj.StartDate = StartDate;
            ComissionObj.EndDate = EndDate;
            ComissionObj.OpDateTime = DateTime.Now;
            ComissionObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_HotelComission.Add(ComissionObj);
            db.SaveChanges();
            int id = ComissionObj.ID;
            return status;
        }

        public int UpdateComission(int ComissionID, string Comission, Controller ctrl)
        {

            int status = 1;
            var ComissionObj = db.TB_HotelComission.Where(x => x.ID == ComissionID).FirstOrDefault();
            ComissionObj.Comission =Convert.ToInt16(Comission);
            ComissionObj.OpDateTime = DateTime.Now;
            ComissionObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public int DeleteComission( string IdtoDelete)
        {
            int status = 1;

            foreach (var ids in IdtoDelete.Split(','))
            {
                if (ids != "")
                {
                    try
                    {

                        int ComissionId = Convert.ToInt32(ids);
                        var MessageTable = db.TB_HotelComission.Where(x => x.ID == ComissionId).FirstOrDefault();
                        db.TB_HotelComission.Remove(MessageTable);
                        db.SaveChanges();
                    }
                    catch
                    {

                    }
                }
            }
            return status;
        }


    }

    public class PropertyComissionExt
    {
        public int ID { get; set; }
        public string HotelID { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Comission { get; set; }

        public string HotelName { get; set; }

    }
}