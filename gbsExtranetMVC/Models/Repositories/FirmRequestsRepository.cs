using Business;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class FirmRequestsRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<FirmRequestExt> ReadAll(Controller ctrl)
        {

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetFirmRequests_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "FirmName");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@Active", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<FirmRequestExt> ListOfModel = new List<FirmRequestExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FirmRequestExt HotelObj = new FirmRequestExt();
                    Encryption64 objEncryptreservation = new Encryption64();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.Firm = dr["FirmName"].ToString();
                    HotelObj.RequestTypeName = dr["RequestTypeName"].ToString();
                    HotelObj.ReservationID = dr["ReservationID"].ToString();
                    string ReservationID = dr["ReservationID"].ToString();
                    ReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(ReservationID, "58421043")));
                    HotelObj.decryptReservationID = ReservationID;
                    HotelObj.FirmRequestStatusName = dr["FirmRequestStatusName"].ToString();
                   // string Status = dr["FirmRequestStatusName"].ToString();
                    HotelObj.FirmRequestStatusID = Convert.ToInt32(dr["FirmRequestStatusID"]);
                    string Status = dr["FirmRequestStatusID"].ToString();
                    if (Status == "1")
                    {
                        HotelObj.StatusID = Convert.ToBoolean(1);
                    }
                    else
                    {
                        HotelObj.StatusID = Convert.ToBoolean(0);
                    }
                    if (dr["CheckInDate"].ToString()!="")
                    {
                        try
                        {
                            HotelObj.ChechInDate = Convert.ToDateTime(dr["CheckInDate"]).ToString("dd/MM/yyyy");
                        }
                        catch
                        {
                            HotelObj.ChechInDate = dr["CheckInDate"].ToString();
                        }
                        
                    }
                    else
                    {
                        HotelObj.ChechInDate = dr["CheckInDate"].ToString();
                    }
                    if (dr["CheckOutDate"].ToString() != "")
                    {
                        try
                        {
                            HotelObj.ChechOutDate = Convert.ToDateTime(dr["CheckOutDate"]).ToString("dd/MM/yyyy");
                        }
                        catch
                        {
                            HotelObj.ChechOutDate = dr["CheckOutDate"].ToString();
                        }
                    }
                    else
                    {
                        HotelObj.ChechOutDate = dr["CheckOutDate"].ToString();
                    }

                   
                   
                    HotelObj.CreateDateTime = dr["CreateDateTime"].ToString();
                    HotelObj.OpDateTime = dr["OpDateTime"].ToString();
                    HotelObj.RequestTypeID = Convert.ToInt32(dr["RequestTypeID"]);
                    HotelObj.FirmID = Convert.ToInt32(dr["FirmID"]);
                   


                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }

        public bool UpdateFirmRequestStatus(int ID, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_FirmRequest.Where(x => x.ID == ID).FirstOrDefault();
            PageObj.ID = ID;
            PageObj.FirmRequestStatusID = 2;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool SaveReservationStatus(int ReservationID, Controller ctrl)
        {
                bool status = true;                     
                var PageObjNew = db.TB_Reservation.Where(x => x.ID == ReservationID).FirstOrDefault();
                PageObjNew.StatusID = 4;
                PageObjNew.OpDateTime = DateTime.Now;
                PageObjNew.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                db.SaveChanges();      
                return status;
        }

        public bool AddReservationStatusHistory(int ReservationID, Controller ctrl)
        {
            bool status = true;
            TB_ReservationStatusHistory PageObjNew = new TB_ReservationStatusHistory();
           // var PageObjNew = db.TB_Reservation.Where(x => x.ID == ReservationID).FirstOrDefault();
            PageObjNew.ReservationID = ReservationID;
            PageObjNew.StatusID = 4;
            PageObjNew.OpDateTime = DateTime.Now;
            PageObjNew.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_ReservationStatusHistory.Add(PageObjNew);
            db.SaveChanges();
            return status;
        }

        public bool SaveHotelReservationDates(int ReservationID, DateTime? ChechInDa, DateTime? ChechOutDa, Controller ctrl)
        {
            DateTime ChechInDate = Convert.ToDateTime(ChechInDa);
            DateTime ChechOutDate = Convert.ToDateTime(ChechOutDa);
            DateTime nowMinusDays = ChechOutDate.AddDays(-1);
            bool status = true;
          //  var PageObjNew = db.TB_HotelReservation.Where(x => x.ReservationID == ReservationID).FirstOrDefault();
            dynamic hotelReservations = from reservation in db.TB_HotelReservation
                                        where reservation.ReservationID == ReservationID
                                            && reservation.Active == true
                                        select reservation;

           // var totalAmount = db.TB_HotelReservation.Sum(hres => hres.Amount);
            var totalAmount = "";
           // var totalAmount = db.TB_HotelReservation.Sum(hres => hres.Amount);
            foreach(var hotelReservationAmount in hotelReservations)
            {
                totalAmount = Convert.ToString(hotelReservationAmount.Amount);
            }
            //var totalAmount = hotelReservations.Sum(hotelReservations.Amount);
            //double totalAmount = hotelReservations.Sum();
            double totalDeductedAmount = 0;
            foreach (var hotelReservation in hotelReservations)
            {
               
                dynamic hotelReservationRates = from hotelReservationRate in db.TB_HotelReservationRate
                                            join Date in db.TB_Date on hotelReservationRate.DateID equals
                                            Date.ID
                                            where hotelReservationRate.Active == true && hotelReservationRate.HotelReservationID == ReservationID &&
                                                (Date.Date < ChechInDate || Date.Date > nowMinusDays)
                                            select hotelReservationRate;

              
                double deductedAmount = 0;

                foreach (var hotelReservationRate in hotelReservationRates)
                {
                    deductedAmount += Convert.ToDouble(hotelReservationRate.RoomPrice);
                    hotelReservationRate.Active = false;
                    hotelReservationRate.OpDateTime = DateTime.Now;
                    hotelReservationRate.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                }
                
                hotelReservation.CheckInDate = Convert.ToDateTime(ChechInDate);
                hotelReservation.CheckOutDate = Convert.ToDateTime(ChechOutDate);
                hotelReservation.NightCount = Convert.ToInt16(DateDiff(DateInterval.Day, Convert.ToDateTime(ChechInDate), Convert.ToDateTime(ChechOutDate)));
                hotelReservation.Amount = hotelReservation.Amount - Convert.ToDecimal(deductedAmount);
                hotelReservation.OpDateTime = DateTime.Now;
                hotelReservation.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

                totalDeductedAmount = totalDeductedAmount + deductedAmount;

            }
               double Amount = Convert.ToDouble(totalAmount) - totalDeductedAmount;
               SaveReservationAmount(ReservationID, Amount, Convert.ToInt64(ctrl.Session["UserID"]));
               db.SaveChanges();
               return status;                      
          //  double totalDeductedAmount = 0;

               //var entryPoint = db.TB_HotelReservationRate
               //    .Join(db.TB_Date,
               //    c => c.DateID,
               //    cm => cm.ID,
               //    (c, cm) => new
               //    {
               //        UID = cm.ID,
               //        DID = cm.Date,
               //        TID = c.Active,
               //        EID = c.DateID,
               //        HID = c.HotelReservationID,
               //    }).
               //    Where(a => a.TID == true && a.HID == ReservationID && (a.DID < Convert.ToDateTime(ChechInDate) || a.DID > Convert.ToDateTime(ChechOutDate).AddDays(-1))).Take(10);
            
            
          //  Where(a => a.UID == user.UID).Take(10);
             //dynamic hotelReservationRates = from hotelReservationRate in DataContext.TB_HotelReservationRatesDataContext.TB_Datesdate.IDhotelReservationRate.DateID where hotelReservationRate.Active == true && hotelReservationRate.HotelReservationID == hotelReservation.ID && (date.Date < CheckInDate || date.Date > Convert.ToDateTime(CheckOutDate).AddDays(-1))hotelReservationRate;
            //PageObjNew.StatusID = 4;
            //PageObjNew.OpDateTime = DateTime.Now;
            //PageObjNew.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            
        }

        public bool SaveReservationAmount(int ReservationID, double Amount, double OpUserID)
        {
            bool status = true;
            dynamic reservation = (from rsv in db.TB_Reservation where rsv.ID == ReservationID select rsv).Single();
            reservation.Amount = Convert.ToDecimal(Amount);
	        reservation.PayableAmount = Convert.ToDecimal(Amount) * (1 - ((reservation.GeneralPromotionDiscountPercentage + reservation.PromotionDiscountPercentage) / 100));
	        reservation.ComissionAmount = (reservation.PayableAmount * reservation.ComissionRate) / 100;
	        reservation.OpDateTime = DateAndTime.Now;
	        reservation.OpUserID = Convert.ToInt64(OpUserID);
            db.SaveChanges();
            return status;
        }

        private dynamic DateDiff(DateInterval dateInterval, DateTime dateTime1, DateTime dateTime2)
        {

            
            int? Interval = Convert.ToInt16((dateTime1 - dateTime2).TotalDays);

            return Interval;

            throw new NotImplementedException();
        }

       
        public bool SaveReservationOperation(int ReservationID,string ChargedAmount,int ReservationOperationID, string ChargedAmountCurrencyID, string ChargeDate, Controller ctrl)
        {

            
                 bool status = true;
                 
	            //Rezervasyon işlemi güncellenir
                 dynamic reservation = (from rsv in db.TB_Reservation where rsv.ID == ReservationID select rsv).Single();

	                reservation.ReservationOperationID = 4;
            
	            if (ReservationOperationID == 6) {
	                	reservation.ChargedAmount = ChargedAmount;
	                	reservation.ChargedAmountCurrencyID = ChargedAmountCurrencyID;
	                	reservation.ChargeDate = ChargeDate;
            	}
                reservation.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
	                reservation.OpDateTime = DateAndTime.Now;

	                //Rezervasyona bağlı otel rezervasyon işlemleri güncellenir
                	dynamic hotelReservations = from rsv in db.TB_HotelReservation where rsv.ReservationID == ReservationID & rsv.Active == true select rsv;

	                foreach (var hotelReservation in hotelReservations) {
	                    	hotelReservation.ReservationOperationID = ReservationOperationID;
                            hotelReservation.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
		                    hotelReservation.OpDateTime = DateAndTime.Now;
	                }

	//Rezervasyona bağlı transfer rezervasyon işlemleri güncellenir
	             dynamic transferReservations = from rsv in db.TB_TransferReservation where rsv.ReservationID == ReservationID & rsv.Active == true select rsv;

	                foreach (var transferReservation in transferReservations) {	
	                	transferReservation.ReservationOperationID = ReservationOperationID;
                        transferReservation.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
		                transferReservation.OpDateTime = DateAndTime.Now;
	                   }

	            //Rezervasyona bağlı tur rezervasyon işlemleri güncellenir
               	dynamic tourReservations = from rsv in db.TB_TourReservation where rsv.ReservationID == ReservationID & rsv.Active == true select rsv;

                  foreach (var tourReservation in tourReservations)
                   {		
	            	tourReservation.ReservationOperationID = ReservationOperationID;
                    tourReservation.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
		            tourReservation.OpDateTime = DateAndTime.Now;
                	}

	            //Rezervasyona bağlı fırsat rezervasyon işlemleri güncellenir
            	dynamic dealReservations = from rsv in db.TB_DealReservation where rsv.ReservationID == ReservationID & rsv.Active == true select rsv;

                   foreach (var dealReservation in dealReservations)
                   {		
	            	dealReservation.ReservationOperationID = ReservationOperationID;
                    dealReservation.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
	            	dealReservation.OpDateTime = DateAndTime.Now;
	                }
                    db.SaveChanges();
                    return status;
         }
        public DataTable GetMailTemplates(string MaiTemplateCode, string Culturecode)
        {
            SQLCon.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("B_GetMailTemplate_BizTbl_MailTemplate_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MailTemplateCode", MaiTemplateCode);
            cmd.Parameters.AddWithValue("@Culture", Culturecode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            return dt;
        }

        //public bool Mailfunction(string FirmreqID, string RequesttyID, string ReservationIDs)
        //{
        
        //    bool Status = true;

        //    var firmRequest = from FirmRequest in db.TB_FirmRequest
        //                          join Reservation in db.TB_Reservation on FirmRequest.ReservationID equals
        //                          Reservation.ID
        //                          where FirmRequest.ID == Convert.ToInt64(FirmreqID) && 
        //                          FirmRequest.RequestTypeID == Convert.ToInt32(RequesttyID) &&
        //                              Reservation.ID == Convert.ToInt64(ReservationIDs)
        //                          select FirmRequest;  
            
        //    return Status;
        //}

        

        public bool RejiectStatus(int ID, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_FirmRequest.Where(x => x.ID == ID).FirstOrDefault();
            PageObj.ID = ID;
            PageObj.FirmRequestStatusID = 3;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;


        }

        public bool deletefirmrequest(int ID, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_FirmRequest.Where(x => x.ID == ID).FirstOrDefault();
            obj.ID = ID;
            obj.Active = false;
            obj.ID = ID;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
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

        public string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }
        public class Encryption64
        {

            private byte[] key = { };
            //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            public string Decrypt(string stringToDecrypt, string sEncryptionKey)
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length];
                try
                {
                    key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV),
                                                                      CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }


            public string Encrypt(string stringToEncrypt, string sEncryptionKey)
            {
                try
                {
                    key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    Byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV),
                                                                      CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

        }
    }

    public class FirmRequestExt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FirmName { get; set; }
        public string HotelTypeName { get; set; }
        public string HotelClassName { get; set; }
        public string HotelAccommodationTypeName { get; set; }
        public string HotelChainName { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
        public string MainRegionName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebAddress { get; set; }
        public string StatusName { get; set; }
        public bool Active { get; set; }
        public string CreateDateTime { get; set; }
        public string OpDateTime { get; set; }
        public int FirmID { get; set; }
        public string CountryID { get; set; }

        public string HotelClassID { get; set; }
        public string HotelChainID { get; set; }
        public string MainRegionID { get; set; }
        public string CityTaxApplied { get; set; }
        public string ClosestAirportID { get; set; }
        public string CountryCode { get; set; }
        public string VAT { get; set; }
        public string ClosestAirportName { get; set; }
        public string ClosestAirportNameWithParentNameAndCode { get; set; }
        public string ClosestAirportDistance { get; set; }
        public string Description { get; set; }
        public string RoomCount { get; set; }
        public string CheckinStart { get; set; }
        public string CheckinEnd { get; set; }
        public string CheckoutStart { get; set; }
        public string CheckoutEnd { get; set; }
        public string FloorCount { get; set; }
        public string BuiltYear { get; set; }
        public string RenovationYear { get; set; }
        public string HitCount { get; set; }
        public string Sort { get; set; }
        public string MapZoomIndex { get; set; }
        public Boolean StatusID { get; set; }
        public string IsSecret { get; set; }
        public string IsPreferred { get; set; }
        public string ShowOffline { get; set; }
        public string ChannelManagerID { get; set; }
        public string CurrencySymbol { get; set; }
        public string CreditCardNotRequired { get; set; }
        public string IPAddress { get; set; }


        public string FirmRequestStatusName { get; set; }

        public string ReservationID { get; set; }

        public string RequestTypeName { get; set; }

        public string Firm { get; set; }

        public string ChechInDate { get; set; }

        public string ChechOutDate { get; set; }

        public string decryptReservationID { get; set; }

        public int RequestTypeID { get; set; }

        public int FirmRequestStatusID { get; set; }
    }
}