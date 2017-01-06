using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class ReviewsExt
    {
        public long ID { get; set; }
        public string FirmName { get; set; }
        public string PartID { get; set; }
        public string Part { get; set; }
        public string ReservationID { get; set; }
        public string EncryptReservationID { get; set; }
        public string UserFullName { get; set; }
        public string CountryName { get; set; }
        public string Review { get; set; }
        public string ReviewStatusID { get; set; }
        public string ReviewStatusName { get; set; }
        public string AveragePoint { get; set; }
        public string ReviewTypeEvaluationName { get; set; }
        public bool Anonymous { get; set; }
        public string CreateDateTime { get; set; }
        public string OpDateTime { get; set; }
        public string IPAddress { get; set; }

        public string ReviewOwner { get; set; }

        public bool StatusID { get; set; }
    }
    public class ReviewsRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

       
        public List<ReviewsExt> GetReservationReviews()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //DBEntities entity = new DBEntities();
            //var Culture = new SqlParameter("@Culture", CultureValue);
            //var OrderBy = new SqlParameter("@OrderBy", "ID");
            //var PagingSize = new SqlParameter("@PagingSize", int.MaxValue);
            //var PageIndex = new SqlParameter("@PageIndex", 1);
            //var result = entity.Database.SqlQuery<GetAdminReservationReview_Result>("B_GetReservationReviews_TB_ReservationReview_SP @Culture,@OrderBy,@PagingSize,@PageIndex",
            //    Culture, OrderBy, PagingSize, PageIndex).ToList();
            //DataTable dt = new DataTable();

            //dt.Columns.Add("ID");
            //dt.Columns.Add("FirmName");
            //dt.Columns.Add("SectionID");
            //dt.Columns.Add("Section");            
            //dt.Columns.Add("ReservationID");
            //dt.Columns.Add("ReviewOwner");
            //dt.Columns.Add("Review");
            //dt.Columns.Add("Status");
            //dt.Columns.Add("Anonymous");
            //dt.Columns.Add("CreatedDate");
            //dt.Columns.Add("UpdatedDate");
            //dt.Columns.Add("IPAddress");


            //foreach (GetAdminReservationReview_Result dr in result)
            //{
            //    dt.Rows.Add(dr.ID, dr.FirmName, dr.Part,dr.PartID, dr.ReservationID, dr.UserFullName, dr.Review, dr.ReviewStatusID, dr.ReviewStatusName, dr.AveragePoint, dr.ReviewTypeEvaluationName,
            //        dr.Anonymous,dr.CreateDateTime, dr.OpDateTime, dr.IPAddress);
            //}


            //ArrayList ArrayID = new ArrayList();
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("TB_SP_GetReservationReviews", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Culture", CultureValue);
             cmd.Parameters.AddWithValue("@OrderBy", "ID");
             cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
             cmd.Parameters.AddWithValue("@PageIndex", 1);
             SqlDataAdapter sda = new SqlDataAdapter(cmd);
             sda.Fill(ds);
             SQLCon.Close();
         
            List<ReviewsExt> ListOfModel = new List<ReviewsExt>();
            Encryption64 objEncryptreservation = new Encryption64();
            string CheckReviewID = "";
            string ReviewDescription = "";
            if (ds != null)
            {
                dt = ds.Tables[1];
            }
          
            int CurrentDatarowCount = 0;
     
            if (dt.Rows.Count > 0)
            {
                DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                foreach (DataRow dr in dt.Rows)
                {
                    //if (!ArrayID.Contains(Convert.ToInt32(dr["ID"])))
                    //{
                    

                    ReviewsExt Review = new ReviewsExt();
                    //if(CheckReviewID!="" && CheckReviewID!= dr["ID"].ToString())
                    //{
                    //    ListOfModel.Add(Review);
                    //}

                   
                    CheckReviewID = dr["ID"].ToString();
                    Review.ID = Convert.ToInt32(dr["ID"]);
                    Review.FirmName = dr["FirmName"].ToString();
                    Review.Part = dr["Part"].ToString();
                    Review.ReservationID = dr["ReservationID"].ToString();
                    string EncryptReservationID = dr["ReservationID"].ToString();
                    EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                    Review.EncryptReservationID = EncryptReservationID;

                    //Review.UserFullName = dr["UserFullName"].ToString();
                    //Review.CountryName = dr["CountryName"].ToString();
                    string UserFullName = dr["UserFullName"].ToString();
                    string CountryName = dr["CountryName"].ToString();
                    Review.ReviewOwner = UserFullName + Environment.NewLine + CountryName;

                    string ReviewTypeName = dr["ReviewTypeName"].ToString();
                    string ReviewTypeScaleName = dr["ReviewTypeScaleName"].ToString();
                    string ReviewTypeEvaluationName = dr["ReviewTypeEvaluationName"].ToString();
                    
                    //Review.Review = dr["Review"].ToString();
                    double AveragePointValue = Convert.ToDouble(dr["AveragePoint"]);
                    double PointValue = Convert.ToDouble(dr["Point"]);
                    //Convert.ToDouble(Math.Floor(PointValue * 10));
                     if (ReviewDescription=="")
                    {

                        ReviewDescription = "<div style='display: flex;'><label class='lblLeft'>" + Resources.Resources.Average + "</label><div class='progress' style='border-radius: 3px;margin-right: 10px;margin-left: 10px;width: 100px;'> <div class='progress-bar' role='progressbar' aria-valuenow='90' aria-valuemin='0' aria-valuemax='100' style='width:100px;background-color: Green;'></div> </div><label class='lblright'>" + ReviewTypeEvaluationName + "</label></div>";
                        ReviewDescription = ReviewDescription + "<div style='display: flex;'><label class='lblLeft'>" + ReviewTypeName + "</label><div class='progress' style='border-radius: 3px;margin-right: 10px;margin-left: 10px;width: 100px;'> <div class='progress-bar' role='progressbar' aria-valuenow='90' aria-valuemin='0' aria-valuemax='100' style='width:" + Math.Floor(PointValue * 10) + "px;background-color: Red;'></div> </div><label class='lblright'>" + ReviewTypeScaleName + "</label></div>";
                    }
                    else
                    {
                        ReviewDescription = ReviewDescription + "<div style='display: flex;'><label class='lblLeft'>" + ReviewTypeName + "</label><div class='progress' style='border-radius: 3px;margin-right: 10px;margin-left: 10px;width: 100px;'> <div class='progress-bar' role='progressbar' aria-valuenow='90' aria-valuemin='0' aria-valuemax='100' style='width:" + Math.Floor(PointValue * 10) + "px;background-color: Red;'></div> </div><label class='lblright'>" + ReviewTypeScaleName + "</label></div>";
                    }
                     Review.Review = ReviewDescription;

                    string Status = dr["ReviewStatusID"].ToString();
                    if (Status == "1")
                    {
                        Review.StatusID = Convert.ToBoolean(1);
                    }
                    else
                    {
                        Review.StatusID = Convert.ToBoolean(0);
                    }

                    if (dr["Anonymous"].ToString() != null && dr["Anonymous"].ToString() != "")
                    {
                        Review.Anonymous = Convert.ToBoolean(dr["Anonymous"]);
                    }
                    else
                    {
                        Review.Anonymous = false;
                    }

                    string IPCountry = GetCountryInfoFromIPAddress(dr["IPAddress"].ToString());

                    if(IPCountry!="")
                    {
                        Review.IPAddress = dr["IPAddress"].ToString() + Environment.NewLine + IPCountry;
                    }
                    else
                    {
                        Review.IPAddress = dr["IPAddress"].ToString();
                    }
                    Review.ReviewStatusName = dr["ReviewStatusName"].ToString();
                   // Review.Anonymous = dr["Anonymous"].ToString();
                    Review.CreateDateTime = dr["CreateDateTime"].ToString();
                    Review.OpDateTime = dr["OpDateTime"].ToString();
                   // Review.IPAddress = dr["IPAddress"].ToString();

                    // ArrayID.Add(Convert.ToInt32(dr["ID"]));
                   
                    //}
                    CurrentDatarowCount++;
                    if(lastRow==dr)
                    {
                        ListOfModel.Add(Review);
                    }
                    else
                    {
                      
                        DataRow NextRow = dt.Rows[CurrentDatarowCount];
                        if (dr["ID"].ToString() != NextRow["ID"].ToString())
                        {
                            ListOfModel.Add(Review);
                            ReviewDescription = "";
                        }
                        
                    }
                   
                }
            }
            return ListOfModel;
        }

        public string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
        public bool UpdateApproveStatus(int ID, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_ReservationReview.Where(x => x.ID == ID).FirstOrDefault();
            PageObj.ID = ID;
            PageObj.ReviewStatusID = 2;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

        public bool UpdateRejectStatus(int ID, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_ReservationReview.Where(x => x.ID == ID).FirstOrDefault();
            PageObj.ID = ID;
            PageObj.ReviewStatusID = 3;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

        public bool deleteReviewStatus(int ID, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_ReservationReview.Where(x => x.ID == ID).FirstOrDefault();
            obj.ID = ID;
            obj.Active = false;
            obj.ID = ID;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }

        public string GetCountryInfoFromIPAddress(string IPAddress)
        {
            double IPNumber = ConvertIpAddressToIPNumber(IPAddress);

            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetCountryName_TB_Country_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IPNumber", IPNumber);
            cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            string Country = Convert.ToString(cmd.ExecuteScalar());
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            SQLCon.Close();
            return Country;
            
        }
        public static long ConvertIpAddressToIPNumber(string IPAddress)
        {
            string[] arrDec;
            int i;
            double intResult=0;
            long IPNumber;
            if ((IPAddress == String.Empty))
            {
                IPNumber = 0;
            }
            else
            {
                arrDec = IPAddress.Split('.');
                for (i = arrDec.Length - 1; i >= 0; i += -1)
                {
                    //intResult = (intResult
                    //            + ((Convert.ToInt32(arrDec[i]) % 256)
                    //            * Math.Pow(256, (3 - i))));
                    //  intResult = intResult + ((Convert.ToInt64(arrDec[i] % 256) * Math.Pow(256, 3 - i)));

                    intResult = intResult + ((Convert.ToInt64(arrDec[i]) % 256) * Math.Pow(256, 3 - i));
                }

                IPNumber = Convert.ToInt64(intResult);
            }

            return IPNumber;
        }

        //public int UpdateReviewStatus(long ID)
        //{
        //    DBEntities UpdateUserOperationsobj = new DBEntities();
        //    var UserCodeParameter = new SqlParameter("@ID", ID);

        //    int i = UpdateUserOperationsobj.Database.ExecuteSqlCommand("B_UpdateUserOperations_BizTbl_User_SP @ID",
        //        UserCodeParameter);
        //    return i;
        //}

        //public List<ReviewsExt> Review()
        //{


        //    List<ReviewsExt> ListOfModel = new List<ReviewsExt>();


        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            ReviewsExt Review = new ReviewsExt();

        //            string ReviewTypeName = dr["ReviewTypeName"].ToString();
        //            string ReviewTypeScaleName = dr["ReviewTypeScaleName"].ToString();
        //            //Review.Review = dr["Review"].ToString();
        //            Review.Review = ReviewTypeName + ReviewTypeScaleName;                 

        //            ListOfModel.Add(Review);

        //        }
        //    }
        //    return ListOfModel;
        //}

    }
}