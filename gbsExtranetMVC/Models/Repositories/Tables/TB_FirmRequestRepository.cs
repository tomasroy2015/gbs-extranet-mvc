using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_FirmRequestRepository : BaseRepository
    {

        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_FirmRequestExt> ReadAll(int TableID)
        {
            List<TB_FirmRequestExt> list = new List<TB_FirmRequestExt>();

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
                    TB_FirmRequestExt model = new TB_FirmRequestExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.FirmID = dr["FirmID"].ToString();
                    model.Firm = dr["FK_FirmID"].ToString();
                    model.RequestTypeID = dr["RequestTypeID"].ToString();
                    model.RequestType = dr["FK_RequestTypeID"].ToString();
                    model.ReservationID = dr["ReservationID"].ToString();
                    model.Reservation = Convert.ToInt32(dr["FK_ReservationID"]);

                    model.FirmRequestStatusID = dr["FirmRequestStatusID"].ToString();
                    model.FirmRequestStatus = dr["FK_FirmRequestStatusID"].ToString();
                    string CheckDateIn = dr["CheckInDate"].ToString();
                    string CheckDateOut = dr["CheckOutDate"].ToString();
                    if (CheckDateIn=="" || CheckDateIn==null )
                    {
                        model.CheckInDate = null;
                    }
                    else
                    {
                        model.CheckInDate = Convert.ToDateTime(dr["CheckInDate"]);
                    }

                    if (CheckDateOut == "" || CheckDateOut == null)
                    {
                        model.CheckInDate = null;
                    }
                    else
                    {
                        model.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"]);
                    }
                   
                   
                    //try
                    //{
                    //    model.CheckInDate = Convert.ToDateTime(dr["CheckInDate"].ToString());
                    //}
                    //catch
                    //{
                    //    model.CheckInDate = Convert.ToDateTime("");
                    //}
                    //try
                    //{
                    //    model.CheckOutDate = Convert.ToDateTime(dr["CheckOutDate"].ToString());
                    //}
                    //catch
                    //{
                    //    model.CheckOutDate = Convert.ToDateTime("");
                    //}



                    model.Active = Convert.ToBoolean(dr["Active"].ToString());
                    model.IPAddress = dr["IPAddress"].ToString();               
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_FirmRequestExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_FirmRequest obj = new TB_FirmRequest();
           // obj.ID = model.ID;
            obj.FirmID = Convert.ToInt32(model.FirmID);
            obj.RequestTypeID = Convert.ToInt32(model.RequestTypeID);
            obj.ReservationID = Convert.ToInt32(model.ReservationID);
            obj.FirmRequestStatusID = Convert.ToInt32(model.FirmRequestStatusID);
            //obj.CheckInDate = model.CheckInDate;
            //obj.CheckOutDate = model.CheckOutDate;
            obj.CheckInDate = Convert.ToDateTime(model.CheckInDate);
            obj.CheckOutDate = Convert.ToDateTime(model.CheckOutDate);

            //if (model.CheckInDate != null)
            //{
            //    obj.CheckInDate = Convert.ToDateTime(model.CheckInDate);
            //}
            //else
            //{
            //    obj.CheckInDate = Convert.ToDateTime("");
            //}
            //if (model.CheckOutDate != null)
            //{
            //    obj.CheckOutDate = Convert.ToDateTime(model.CheckOutDate);
            //}
            //else
            //{
            //    obj.CheckOutDate = Convert.ToDateTime("");
            //}
          

            obj.Active = Convert.ToBoolean(model.Active);
            obj.OpDateTime = DateTime.Now;          
            obj.CreateDateTime = DateTime.Now;
            obj.IPAddress = model.IPAddress;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_FirmRequest.Add(obj);
            db.SaveChanges();
            int id = Convert.ToInt32(obj.ID);
            return status;
        }
        public bool Update(TB_FirmRequestExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_FirmRequest.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.FirmID = Convert.ToInt32(model.FirmID);
            obj.RequestTypeID = Convert.ToInt32(model.RequestTypeID);
            obj.ReservationID = Convert.ToInt32(model.ReservationID);
            obj.FirmRequestStatusID = Convert.ToInt32(model.FirmRequestStatusID);
            obj.CheckInDate = Convert.ToDateTime(model.CheckInDate);
            obj.CheckOutDate = Convert.ToDateTime(model.CheckOutDate);
           

            //if (model.CheckInDate!=null)
            //{
            //    obj.CheckInDate = Convert.ToDateTime(model.CheckInDate);
            //}
            //else
            //{
            //    obj.CheckInDate = Convert.ToDateTime("");
            //}
            //if (model.CheckOutDate != null)
            //{
            //    obj.CheckOutDate = Convert.ToDateTime(model.CheckOutDate);
            //}
            //else
            //{
            //    obj.CheckOutDate = Convert.ToDateTime("");
            //}
          
           
            obj.Active = Convert.ToBoolean(model.Active);
            obj.OpDateTime = DateTime.Now;
            obj.CreateDateTime = DateTime.Now;
            obj.IPAddress = model.IPAddress;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_FirmRequestExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_FirmRequest.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_FirmRequest.Remove(obj);
            db.SaveChanges();
            return status;
        }
    }

    public class TB_FirmRequestExt
    {
        public int ID { get; set; }
        public string FirmID { get; set; }
        public string Firm { get; set; }
        public string RequestTypeID { get; set; }
        public string RequestType { get; set; }
        public string ReservationID { get; set; }
        public int Reservation { get; set; }
        public string FirmRequestStatusID { get; set; }
        public string FirmRequestStatus { get; set; }
        // [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
      //  public DateTime CheckInDate { get; set; }

       // public Nullable<DateTime> CheckInDate { get; set; }
        
        public DateTime? CheckInDate { get; set; }
       //  [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

      //  public Nullable<DateTime> CheckOutDate { get; set; }
       // public DateTime CheckOutDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public bool Active { get; set; }
        public string IPAddress { get; set; }

    }
}