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
    public class FirmOperationsRepository :BaseRepository
    {

        public List<FirmOperationsExt> GetFirmOperations()
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            List<FirmOperationsExt> list = new List<FirmOperationsExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetFirm_TB_Firm_SP", SQLCon);
            cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            // return dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FirmOperationsExt FirmObj = new FirmOperationsExt();
                    FirmObj.ID = Convert.ToInt32(dr["ID"]);
                    FirmObj.Firm = dr["Name"].ToString();
                    FirmObj.CountryID = dr["CountryID"].ToString();
                    FirmObj.Country = dr["Country"].ToString();
                    FirmObj.RegionID = dr["RegionID"].ToString();
                    FirmObj.Region = dr["Region"].ToString();
                    FirmObj.City = dr["City"].ToString();
                    FirmObj.Address = dr["Address"].ToString();
                    FirmObj.Postal_code = dr["PostCode"].ToString();
                    FirmObj.Phone = dr["Phone"].ToString(); 
                    FirmObj.Fax = dr["Fax"].ToString();
                    FirmObj.Email = dr["Email"].ToString();
                    FirmObj.Tax_Office = dr["TaxOffice"].ToString();
                    FirmObj.Tax_ID = dr["TaxID"].ToString();
                    FirmObj.Executive_TitleID = dr["ExecutiveTitleID"].ToString();
                    FirmObj.Executive_Title = dr["ExecutiveTitle"].ToString();
                    FirmObj.Executive_Name = dr["ExecutiveName"].ToString();
                    FirmObj.Executive_Surname = dr["ExecutiveSurname"].ToString();
                    FirmObj.Executive_Position = dr["ExecutivePosition"].ToString();
                    FirmObj.Executive_Phone = dr["ExecutivePhone"].ToString();
                    FirmObj.Executive_Email = dr["ExecutiveMail"].ToString();
                    FirmObj.StatusID = dr["StatusID"].ToString();
                    FirmObj.Status = dr["Status"].ToString();
                    FirmObj.Active = Convert.ToBoolean(dr["Active"]);
                    FirmObj.IPaddress = dr["IPAddress"].ToString();
                    FirmObj.Created_Date = dr["CreatedDate"].ToString();
                    FirmObj.Updated_Date = dr["UpdatedDate"].ToString();
                    list.Add(FirmObj);
                }
            }
            return list;
        }


        public bool UpdateRejectStatus(int ID, Controller ctrl)
        {
            bool status = true;
            var PageObj = db.TB_Firm.Where(x => x.ID == ID).FirstOrDefault();
            PageObj.ID = ID;
            PageObj.StatusID = 3;
            PageObj.Active = false;
            PageObj.OpDateTime = DateTime.Now;
            PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }



        public int UpdateFirmOperations(string UserCode, string Firm, string Country, string Region, string Address, string Phone, string Fax, string Postcode,
           string Email, string TaxOffice, string TaxID, string ExecutiveTitle, string ExecutiveName, string ExecutiveSurname, string ExecutivePosition, string ExecutivePhone, string ExecutiveEmail,
            string Active, string Status, string IPAddress)
        {
            DBEntities UpdateUserOperationsobj = new DBEntities();
            var UserCodeParameter = new SqlParameter("@UserCode", UserCode);
            var FirmParameter = new SqlParameter("@Firm", Firm);
            var CountryParameter = new SqlParameter("@Country", Country);
            var RegionParameter = new SqlParameter("@Region", Convert.ToInt64(Region));
            var AddressParameter = new SqlParameter("@Address", Address);
            var PhoneParameter = new SqlParameter("@Phone", Phone);
            var FaxParameter = new SqlParameter("@Fax", Fax);
            var PostcodeParameter = new SqlParameter("@Postcode", Postcode);
            var EmailParameter = new SqlParameter("@Email", Email);
            var TaxOfficeParameter = new SqlParameter("@TaxOffice", TaxOffice);
            var TaxIDParameter = new SqlParameter("@TaxID", TaxID);
            var ExecutiveTitleParameter = new SqlParameter("@ExecutiveTitle", ExecutiveTitle);
            var ExecutiveNameParameter = new SqlParameter("@ExecutiveName", ExecutiveName);
            var ExecutiveSurnameParameter = new SqlParameter("@ExecutiveSurname", ExecutiveSurname);
            var ExecutivePositionParameter = new SqlParameter("@ExecutivePosition", ExecutivePosition);
            var ExecutivePhoneParameter = new SqlParameter("@ExecutivePhone", ExecutivePhone);
            var ExecutiveEmailParameter = new SqlParameter("@ExecutiveEmail", ExecutiveEmail);
            var ActiveParameter = new SqlParameter("@Active", Convert.ToBoolean(Active));
            var StatusParameter = new SqlParameter("@Status", Convert.ToInt32(Status));
            var IPAddressParameter = new SqlParameter("@IPAddress", IPAddress);

            int i = UpdateUserOperationsobj.Database.ExecuteSqlCommand("B_UpdateFirmOperations_TB_Firm_SP @UserCode,@Firm,@Country,@Region,@Address,@Phone,@Fax,@Postcode,@Email,@TaxOffice,@TaxID,@ExecutiveTitle,@ExecutiveName,@ExecutiveSurname,@ExecutivePosition,@ExecutivePhone,@ExecutiveEmail,@Active,@Status,@IPAddress",
                UserCodeParameter, FirmParameter, CountryParameter, RegionParameter, AddressParameter, PhoneParameter, FaxParameter, PostcodeParameter, EmailParameter,
                TaxOfficeParameter, TaxIDParameter, ExecutiveTitleParameter, ExecutiveNameParameter, ExecutiveSurnameParameter, ExecutivePositionParameter,
                ExecutivePhoneParameter, ExecutiveEmailParameter, ActiveParameter,StatusParameter,IPAddressParameter);
            return i;
        }


        public int InsertFirmOperations(string Firm, string Country, string Region, string Address, string Phone, string Fax, string Postcode,
           string Email, string TaxOffice, string TaxID, string ExecutiveTitle, string ExecutiveName, string ExecutiveSurname, string ExecutivePosition, string ExecutivePhone, string ExecutiveEmail,
            string Active, string Status, string IPAddress, Controller ctrl)
        {
            

            DBEntities UpdateUserOperationsobj = new DBEntities();         
            var FirmParameter = new SqlParameter("@Firm", Firm);
            var CountryParameter = new SqlParameter("@CountryID", Country);
            var RegionParameter = new SqlParameter("@RegionID", Convert.ToInt64(Region));
            var AddressParameter = new SqlParameter("@Address", Address);
            var PhoneParameter = new SqlParameter("@Phone", Phone);
            var FaxParameter = new SqlParameter("@Fax", Fax);
            var PostcodeParameter = new SqlParameter("@Postcode", Postcode);
            var EmailParameter = new SqlParameter("@Email", Email);
            var TaxOfficeParameter = new SqlParameter("@TaxOffice", TaxOffice);
            var TaxIDParameter = new SqlParameter("@TaxID", TaxID);
            var ExecutiveTitleParameter = new SqlParameter("@ExecutiveTitleID", ExecutiveTitle);
            var ExecutiveNameParameter = new SqlParameter("@ExecutiveName", ExecutiveName);
            var ExecutiveSurnameParameter = new SqlParameter("@ExecutiveSurname", ExecutiveSurname);
            var ExecutivePositionParameter = new SqlParameter("@ExecutivePosition", ExecutivePosition);
            var ExecutivePhoneParameter = new SqlParameter("@ExecutivePhone", ExecutivePhone);
            var ExecutiveEmailParameter = new SqlParameter("@ExecutiveEmail", ExecutiveEmail);
            var ActiveParameter = new SqlParameter("@Active", Convert.ToBoolean(Active));
            var StatusParameter = new SqlParameter("@StatusID", Convert.ToInt32(Status));
            string IpAddress = GetIpAddres();
            var IPAddressParameter = new SqlParameter("@IPAddress", IpAddress);
            var UserIDParameter = new SqlParameter("@OpUserID",  Convert.ToInt64(ctrl.Session["UserID"]));

            int i = UpdateUserOperationsobj.Database.ExecuteSqlCommand("B_InsertFirmOperations_TB_Firm_SP @Firm,@CountryID,@RegionID,@Address,@Phone,@Fax,@Postcode,@Email,@TaxOffice,@TaxID,@ExecutiveTitleID,@ExecutiveName,@ExecutiveSurname,@ExecutivePosition,@ExecutivePhone,@ExecutiveEmail,@Active,@StatusID,@IPAddress,@OpUserID",
                FirmParameter, CountryParameter, RegionParameter, AddressParameter, PhoneParameter, FaxParameter, PostcodeParameter, EmailParameter,
                TaxOfficeParameter, TaxIDParameter, ExecutiveTitleParameter, ExecutiveNameParameter, ExecutiveSurnameParameter, ExecutivePositionParameter,
                ExecutivePhoneParameter, ExecutiveEmailParameter, ActiveParameter, StatusParameter, IPAddressParameter, UserIDParameter);
            return i;
        }
        public string GetIpAddres()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }

        public DataTable GetSearchDestination(string Keyword, string CultureCode, string CountryID)
        {


            SQLCon.Open();

            SqlCommand cmd = new SqlCommand("[dbo].[TB_SP_GetDestinationSearchResult]", SQLCon);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Culture", CultureCode);

            cmd.Parameters.AddWithValue("@Keyword", Keyword);

            //cmd.Parameters.AddWithValue("@CountryID",CountryID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            SQLCon.Close();

            return dt;
        }
        public bool Create(FirmOperationsExt model, ModelStateDictionary modelState, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction

            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
              
            }
            return status;
        }
    }
    public class FirmOperationsExt
    {
        public int ID { get; set; }

        public string Firm { get; set; }

        public string CountryID { get; set; }
        public string Country { get; set; }

        public string RegionID { get; set; }
        public string Region { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Postal_code { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Tax_Office { get; set; }

        public string Tax_ID { get; set; }

        public string Executive_TitleID { get; set; }
        public string Executive_Title { get; set; }

        public string Executive_Name { get; set; }

        public string Executive_Surname { get; set; }

        public string Executive_Position { get; set; }

        public string Executive_Phone { get; set; }

        public string Executive_Email { get; set; }

        public string StatusID { get; set; }
        public string Status { get; set; }

        public bool Active { get; set; }

        public string IPaddress { get; set; }
        public string Created_Date { get; set; }

        public string Updated_Date { get; set; }



        public string FirmID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Surname { get; set; }
    }
}