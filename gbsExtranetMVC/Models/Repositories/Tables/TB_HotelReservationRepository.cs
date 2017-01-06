using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelReservationRepository:BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelReservationExt> ReadAll(int TableID)
        {
            List<TB_HotelReservationExt> list = new List<TB_HotelReservationExt>();

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
                    TB_HotelReservationExt PageObj = new TB_HotelReservationExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.ReservationID =Convert.ToInt64(dr["FK_ReservationID_ID"]);
                    PageObj.HotelRoomID = dr["FK_HotelRoomID_ID"].ToString();
                    PageObj.Hotel = dr["FK_HotelID_ID"].ToString();
                    PageObj.HotelAccommodation = dr["FK_HotelAccommodationTypeID_ID"].ToString();
                    PageObj.GuestFullName = dr["GuestFullName"].ToString();
                    PageObj.PeopleCount =dr["PeopleCount"].ToString();
                    PageObj.CheckInDate = dr["CheckInDate"].ToString();
                    PageObj.CheckOutDate = dr["CheckOutDate"].ToString();
                    PageObj.NightCount = dr["NightCount"].ToString();
                    PageObj.HotelCancelPolicyID =dr["FK_HotelCancelPolicyID_ID"].ToString();
                    PageObj.PricePolicy = dr["FK_PricePolicyTypeID_ID"].ToString();
                    //PageObj.NonRefundable = Convert.ToBoolean(dr["NonRefundable"].ToString());
                    // PageObj.SingleRate = Convert.ToBoolean(dr["SingleRate"].ToString());
                    //PageObj.DoubleRate = Convert.ToBoolean(dr["DoubleRate"].ToString());
                    //PageObj.Active = Convert.ToBoolean(dr["Active"]);
                    // PageObj.Amount = Convert.ToInt32(dr["Amount"]);
                    string NonRefundable =(dr["NonRefundable"].ToString());
                    string SingleRate = (dr["SingleRate"].ToString());
                    string DoubleRate = (dr["DoubleRate"].ToString());
                    string Active =(dr["Active"]).ToString();
                    string Amount = (dr["Amount"]).ToString();
                    if(NonRefundable=="")
                    {
                        NonRefundable ="False";
                       PageObj.NonRefundable = Convert.ToBoolean(NonRefundable);
                    }
                    else
                    {
                        PageObj.NonRefundable = Convert.ToBoolean(NonRefundable);
                    }


                    if (SingleRate == "")
                    {
                        SingleRate = "False";
                        PageObj.SingleRate = Convert.ToBoolean(SingleRate);
                    }
                    else
                    {
                        PageObj.SingleRate = Convert.ToBoolean(SingleRate);
                    }



                    if (DoubleRate == "")
                    {
                        DoubleRate = "False";
                        PageObj.DoubleRate = Convert.ToBoolean(DoubleRate);
                    }
                    else
                    {
                        PageObj.DoubleRate = Convert.ToBoolean(DoubleRate);
                    }


                    if (Active == "")
                    {
                        Active = "False";
                        PageObj.Active = Convert.ToBoolean(Active);
                    }
                    else
                    {
                        PageObj.Active = Convert.ToBoolean(Active);
                    }

                    PageObj.Amount = dr["Amount"].ToString();
                  
                    PageObj.PromotionDiscountPercentage = (dr["PromotionDiscountPercentage"]).ToString();
                    PageObj.PayableAmount = dr["PayableAmount"].ToString();
                    PageObj.BedOptionNo = dr["BedOptionNo"].ToString();
                    PageObj.Currency = dr["FK_CurrencyID_ID"].ToString();
                    PageObj.TravellerType = dr["FK_TravellerTypeID_ID"].ToString();
                    PageObj.EstimatedArrivalTime = dr["EstimatedArrivalTime"].ToString();
                    PageObj.CancelDateTime = dr["CancelDateTime"].ToString();
                    PageObj.Status = dr["FK_StatusID_ID"].ToString();
                    PageObj.ReservationOperation = dr["FK_ReservationOperationID_ID"].ToString();
                        
                    list.Add(PageObj);
                }
            }


            return list;
        }

    }
    public class TB_HotelReservationExt
    {
        public int ID { get; set; }
        public string Amount { get; set; }
        public Int64 ReservationID { get; set; }
        public string PeopleCount { get; set; }
        public string HotelRoomID { get; set; }
        public string NightCount { get; set; }
        public string HotelCancelPolicyID { get; set; }
        public string PromotionDiscountPercentage { get; set; }
        public string PayableAmount { get; set; }
        public string BedOptionNo { get; set; }
        public string Hotel { get; set; }
        public string Status { get; set; }
        public string ReservationOperation { get; set; }
        public string GuestFullName { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string PricePolicy { get; set; }
        public string HotelAccommodation { get; set; }
        public string EstimatedArrivalTime { get; set; }
        public string TravellerType { get; set; }
        public string CancelDateTime { get; set; }
        public bool NonRefundable { get; set; }
        public bool SingleRate { get; set; }
        public bool DoubleRate { get; set; }
        public bool Active { get; set; }
        public string Currency { get; set; }
       

    }
}