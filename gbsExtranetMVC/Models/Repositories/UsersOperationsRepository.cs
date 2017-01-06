using Business;
using System;
using System.Collections;
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
    public class UsersOperationsExt
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Firm { get; set; }
     
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
       // public bool Active { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
       
        public DateTime CreatedDate1 { get; set; }
        public string Password { get; set; }

        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Region { get; set; }
        public string FirmName { get; set; }

        public int StatusID { get; set; }
        public int FirmID { get; set; }
        public int SalutionTypeID { get; set; }
        public int CountryID { get; set; }

        public string IPAddress { get; set; }



        public string SecurityGroupID { get; set; }

        public bool Locked { get; set; }

    
        public string RegionID { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string OpDateTime { get; set; }

        public string OpUserID { get; set; }

        public string HotelName { get; set; }

        public string BusinessPartnerType { get; set; }

        public string SecurityGroupDescription { get; set; }
        
        public string EncryptID { get; set; }

        public string EncryptFirmID { get; set; }
       
    }
   
     public class UserOperationsRepository:BaseRepository
    {
         public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
         public List<UsersOperationsExt> GetUserOperations()
        {
            //DBEntities entity = new DBEntities();
            //var Culture = new SqlParameter("@Culture", CultureValue);
            //var result = entity.Database.SqlQuery<AdminUseroperations_Result>("B_Ex_GetUserOperations_BizTbl_User_SP @Culture", Culture).ToList();
            //DataTable dt = new DataTable();

            //dt.Columns.Add("FirmName");
            //dt.Columns.Add("ID");
            //dt.Columns.Add("City");
            //dt.Columns.Add("Country");
            //dt.Columns.Add("Address");
            //dt.Columns.Add("Name");
            //dt.Columns.Add("Email");
            //dt.Columns.Add("Phone");
            //dt.Columns.Add("PostCode");
            //dt.Columns.Add("Active");
            //dt.Columns.Add("Surname");
            //dt.Columns.Add("Title");
            //dt.Columns.Add("Region");
            //dt.Columns.Add("Status");
            //dt.Columns.Add("CreatedDate");
            //dt.Columns.Add("UpdatedDate");
            //dt.Columns.Add("CountryID");
            //dt.Columns.Add("StatusID");
            //dt.Columns.Add("FirmID");
            //dt.Columns.Add("SalutionTypeID");
            //dt.Columns.Add("IPAddress");
            //dt.Columns.Add("SecurityGroupID");
            //dt.Columns.Add("Locked");
            //dt.Columns.Add("RegionID");
            //dt.Columns.Add("Password");


            //foreach (AdminUseroperations_Result dr in result)
            //{
            //    dt.Rows.Add(dr.FirmName,dr.ID, dr.City, dr.Country, dr.Address, dr.Name, dr.Email, dr.Phone, dr.PostCode, dr.Active,
            //        dr.Surname, dr.Title, dr.Region, dr.Status, dr.UpdatedDate, dr.CreatedDate,
            //        dr.CountryID, dr.StatusID, dr.FirmID, dr.SalutationTypeID, dr.IPAddress, dr.SecurityGroupID, dr.Locked, dr.RegionID,dr.Password);
            //}

            //List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        UserOperationsRepository.Encryption64 ob = new UserOperationsRepository.Encryption64();
            //        Encryption64 objEncryptreservation = new Encryption64();
            //        UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
            //        UseropertionsObj.FirmName = dr["FirmName"].ToString();
            //        UseropertionsObj.ID = Convert.ToInt32(dr["ID"]);
            //        string EncryptID = dr["ID"].ToString();
            //        EncryptID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptID, "58421043")));
            //        UseropertionsObj.EncryptID = EncryptID;
            //        UseropertionsObj.City = dr["City"].ToString();
            //        UseropertionsObj.Country = dr["Country"].ToString();
            //        UseropertionsObj.Address = dr["Address"].ToString();
            //        UseropertionsObj.Name = dr["Name"].ToString();
            //        UseropertionsObj.Phone = dr["Phone"].ToString();
            //        UseropertionsObj.PostalCode = dr["PostCode"].ToString();
            //        UseropertionsObj.SurName = dr["Surname"].ToString();
            //        UseropertionsObj.Title = dr["Title"].ToString();
            //        UseropertionsObj.Region = dr["Region"].ToString();
            //        UseropertionsObj.Email = dr["Email"].ToString();
            //        //int active = Convert.ToInt32(dr["Active"]);
            //        //UseropertionsObj.Active = Convert.ToBoolean(active);
            //        UseropertionsObj.Active = Convert.ToBoolean(dr["Active"].ToString());
            //        UseropertionsObj.Status = dr["Status"].ToString();
            //        UseropertionsObj.RegionID = dr["RegionID"].ToString();
            //        //UseropertionsObj.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
            //        UseropertionsObj.CreatedDate = dr["CreatedDate"].ToString();
            //        UseropertionsObj.UpdatedDate = dr["UpdatedDate"].ToString();
            //        UseropertionsObj.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
            //        UseropertionsObj.StatusID = Convert.ToInt32(dr["StatusID"].ToString());
            //        UseropertionsObj.FirmID = Convert.ToInt32(dr["FirmID"].ToString());
            //        string EncryptFirmID = dr["FirmID"].ToString();
            //        EncryptFirmID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptFirmID, "58421043")));
            //        UseropertionsObj.EncryptFirmID = EncryptFirmID;
            //        UseropertionsObj.SalutionTypeID = Convert.ToInt32(dr["SalutionTypeID"].ToString());
            //        UseropertionsObj.IPAddress = dr["IPAddress"].ToString();
            //        UseropertionsObj.SecurityGroupID = dr["SecurityGroupID"].ToString();
            //        UseropertionsObj.Locked = Convert.ToBoolean(dr["Locked"].ToString());
            //        string UserPassword = dr["Password"].ToString();
            //        string DecryptedUserPassword = Decrypt128New(UserPassword);
            //        //string DecryptedUserPassword = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(UserPassword)), "58421043");
            //        UseropertionsObj.Password = DecryptedUserPassword;

            //        ListOfModel.Add(UseropertionsObj);
            //    }
            //}

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_BizSp_GetUsers_SP", SQLCon);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@SecurityGroupLevel", 1);
            

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            SQLCon.Close();

             if(ds!=null)
             {
                 dt = ds.Tables[1];
             }
             List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

             if (dt != null)
             {
                 if (dt.Rows.Count > 0)
                 {
                     foreach (DataRow dr in dt.Rows)
                     {
                         UserOperationsRepository.Encryption64 ob = new UserOperationsRepository.Encryption64();
                         Encryption64 objEncryptreservation = new Encryption64();
                         UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
                         UseropertionsObj.FirmName = dr["FirmName"].ToString();
                         UseropertionsObj.ID = Convert.ToInt32(dr["ID"]);
                         string EncryptID = dr["ID"].ToString();
                         EncryptID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptID, "58421043")));
                         UseropertionsObj.EncryptID = EncryptID;
                         UseropertionsObj.City = dr["CityName"].ToString();
                         UseropertionsObj.Country = dr["CountryName"].ToString();
                         UseropertionsObj.Address = dr["Address"].ToString();
                         UseropertionsObj.Name = dr["Name"].ToString();
                         UseropertionsObj.Phone = dr["Phone"].ToString();
                         UseropertionsObj.PostalCode = dr["PostCode"].ToString();
                         UseropertionsObj.UserName = dr["UserName"].ToString();
                         UseropertionsObj.SurName = dr["Surname"].ToString();
                         UseropertionsObj.Title = dr["SalutationTypeName"].ToString();
                         UseropertionsObj.Region = dr["RegionName"].ToString();
                         UseropertionsObj.Email = dr["Email"].ToString();
                         if (dr["Active"].ToString() != null && dr["Active"].ToString() != "")
                         {
                            UseropertionsObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                         }
                         //int active = Convert.ToInt32(dr["Active"]);
                         //UseropertionsObj.Active = Convert.ToBoolean(active);
                         
                         UseropertionsObj.Status = dr["StatusName"].ToString();
                         UseropertionsObj.RegionID = dr["RegionID"].ToString();
                         //UseropertionsObj.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                         UseropertionsObj.CreatedDate = dr["CreateDateTime"].ToString();
                         UseropertionsObj.UpdatedDate = dr["OpDateTime"].ToString();

                         if (dr["CountryID"].ToString() != null && dr["CountryID"].ToString()!="")
                         {
                             UseropertionsObj.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
                         }
                         if (dr["StatusID"].ToString() != null && dr["StatusID"].ToString() != "")
                         {
                             UseropertionsObj.StatusID = Convert.ToInt32(dr["StatusID"].ToString());
                         }
                         if (dr["FirmID"].ToString() != null && dr["FirmID"].ToString() != "")
                         {
                             UseropertionsObj.FirmID = Convert.ToInt32(dr["FirmID"].ToString());
                             string EncryptFirmID = dr["FirmID"].ToString();
                             EncryptFirmID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptFirmID, "58421043")));
                             UseropertionsObj.EncryptFirmID = EncryptFirmID;
                         }
                         else
                         {
                             UseropertionsObj.EncryptFirmID = "71536E455441584D3568633D";
                         }
                         if (dr["SalutationTypeID"].ToString() != null && dr["SalutationTypeID"].ToString() != "")
                         {
                             UseropertionsObj.SalutionTypeID = Convert.ToInt32(dr["SalutationTypeID"].ToString());
                         }
                       //  UseropertionsObj.FirmID = Convert.ToInt32(dr["FirmID"].ToString());
                       //  string EncryptFirmID = dr["FirmID"].ToString();
                       //  EncryptFirmID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptFirmID, "58421043")));
                       //  UseropertionsObj.EncryptFirmID = EncryptFirmID;
                       //  UseropertionsObj.SalutionTypeID = Convert.ToInt32(dr["SalutionTypeID"].ToString());
                         UseropertionsObj.IPAddress = dr["IPAddress"].ToString();
                         if (dr["SalutationTypeID"].ToString() != null && dr["SalutationTypeID"].ToString() != "")
                         {
                             UseropertionsObj.SecurityGroupID = dr["SecurityGroupID"].ToString();
                         }
                         
                         UseropertionsObj.Locked = Convert.ToBoolean(dr["Locked"].ToString());
                       //  string UserPassword = dr["Password"].ToString();
                       //  string DecryptedUserPassword = Decrypt128New(UserPassword);
                         //string DecryptedUserPassword = ob.Decrypt(ConvertHexToString(System.Web.HttpContext.Current.Server.UrlDecode(UserPassword)), "58421043");
                       //  UseropertionsObj.Password = DecryptedUserPassword;

                         ListOfModel.Add(UseropertionsObj);
                     }
                 }
             }            
            
            return ListOfModel;
        }
       
         public bool Delete(UsersOperationsExt model, ref string Msg, Controller ctrl)
         {
             bool status = true;
             using (DBEntities DE = new DBEntities())
             {
                 var Bizusertable= DE.BizTbl_User.Where(x => x.ID == model.ID).FirstOrDefault();
                 DE.BizTbl_User.Remove(Bizusertable);
                 DE.SaveChanges();
             }
             return status;

         }

         public bool Update(UsersOperationsExt model, ref string Msg, Controller ctrl)
         {
             bool status = true;

             var obj = db.BizTbl_User.Where(x => x.ID == model.ID).FirstOrDefault();
             obj.ID = model.ID;
             //obj.FirmName = model.FirmName;
             //obj.Country = model.Country;
             //obj.City = model.City;
             //obj.Address = model.Address;
             //obj.Name = model.Name;
             //obj.Phone = model.Phone;
             //obj.PostalCode = model.PostalCode;
             //obj.SurName = model.SurName;
             //obj.Title = model.Title;
             //obj.Region = model.Region;
             //obj.Active = model.Active;
             //obj.Status = model.Status;
             //obj.CreatedDate = model.CreatedDate;
             obj.OpDateTime = DateTime.Now;
             obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
             db.SaveChanges();

             return status;
         }


         public int UpdateUserOperations(string UserCode, string SalutionId, string Name, string SurName, string Country, string Region, string Address, string PostalCode,
          string Email, string Phone, string Firm, string UserName, string Status, string Active, string IPAddress, string Password, string Locked, string hdnRegionId,Controller ctrl)
         {
             //int i = 1;

             DBEntities UpdateUserOperationsobj = new DBEntities();

             //int StatusID = Convert.ToInt32(Status);

             //Int64 id = 0;
             string cityID = string.Empty;
             string cityName = string.Empty;
             using (BaseRepository baseRepo = new BaseRepository())
             {
                 if (hdnRegionId != string.Empty)
                 {
                     Business.TB_Region city = BizApplication.GetRegionCity(baseRepo.BizDB, hdnRegionId);
                     if (city != null)
                     {
                         cityID = Convert.ToString(city.ID);
                         cityName = city.Name;
                     }
                 }
             }

             var UserCodeParameter = new SqlParameter("@UserCode", UserCode);
             var SalutionIdParameter = new SqlParameter("@SalutionId", SalutionId);
             var NameParameter = new SqlParameter("@Name", Name);
             var SurnameParameter = new SqlParameter("@SurName", SurName);
             var CountryParameter = new SqlParameter("@Country", Country);
             //var RegionParameter = new SqlParameter("@Region", hdnRegionId);
             var RegionParameter = new SqlParameter("@Region", hdnRegionId);
             var AddressParameter = new SqlParameter("@Address", Address);
             var PostalCodeParameter = new SqlParameter("@PostalCode", PostalCode);
             var EmailParameter = new SqlParameter("@Email", Email);
             var PhoneParameter = new SqlParameter("@Phone", Phone);
             var FirmParameter = new SqlParameter("@Firm", Firm);
             var UserNameParameter = new SqlParameter("@UserName", UserName);
             var StatusParameter = new SqlParameter("@Status", Status);
             var ActiveParameter = new SqlParameter("@Active", Active);
             var IpaddressParameter = new SqlParameter("@IPAddress", IPAddress);
             var PasswordParameter = new SqlParameter("@Password", Encrypt(Password));
             var LockedParameter = new SqlParameter("@Locked", Locked);
             var CityParameter = new SqlParameter("@City", cityName);
             var CityIDParameter = new SqlParameter("@cityID", cityID);

             int i = UpdateUserOperationsobj.Database.ExecuteSqlCommand("B_UpdateUserOperations_BizTbl_User_SP @UserCode,@SalutionId,@Name,@SurName,@Country,@Region,@Address,@PostalCode,@Email,@Phone,@Firm,@UserName,@Status,@Active,@IPAddress,@Password,@Locked,@City,@cityID",
                 UserCodeParameter, SalutionIdParameter, NameParameter, SurnameParameter, CountryParameter, RegionParameter, AddressParameter, PostalCodeParameter, EmailParameter,
                 PhoneParameter, FirmParameter, UserNameParameter, StatusParameter, ActiveParameter, IpaddressParameter, PasswordParameter, LockedParameter, CityParameter, CityIDParameter);
             return i;

             //int FirmID = Convert.ToInt32(Firm);
             //long RegionID = Convert.ToInt64(hdnRegionId);

             //BizTbl_User user = new BizTbl_User();

             //if (UserCode != string.Empty)
             //{
             //    int UserID = Convert.ToInt32(UserCode);
             //    user = db.BizTbl_User.Where(x => x.ID == UserID).FirstOrDefault();
             //}

             //user.SalutationTypeID = Convert.ToInt32(SalutionId);
             //user.FirmID = Convert.ToInt32(CheckEmptyStringDBParameter(FirmID, true));
             //user.Name = Name;
             //user.Surname = SurName;
             //user.CountryID = Convert.ToInt32(Country);
             //user.RegionID = Convert.ToInt64(CheckEmptyStringDBParameter(RegionID, false, false, false, false, false, true));
             //user.CityID = Convert.ToInt64(CheckEmptyStringDBParameter(cityID, false, false, false, false, false, true));
             //user.City = Convert.ToString(CheckEmptyStringDBParameter(cityName));
             //user.Address = Address;
             //user.Phone = Phone;
             //user.Email = Email;
             //user.PostCode = Convert.ToString(CheckEmptyStringDBParameter(PostalCode));
             //user.UserName = Convert.ToString(CheckEmptyStringDBParameter(UserName));
             //if (Password != string.Empty)
             //{
             //    user.Password = (new BizCrypto.AES128()).Encrypt(Password);
             //}
             //if (Status != string.Empty)
             //{
             //    user.StatusID = Convert.ToInt32(Status);
             //}
             ////if (PromotionalEmail != null) {
             ////    user.PromotionalEmail = PromotionalEmail;
             ////}
             ////if (VerificationCode != string.Empty) {
             ////    user.VerificationCode = VerificationCode;
             ////}
             //user.Active = Convert.ToBoolean(Active);
             //user.Locked = Convert.ToBoolean(Locked);
             //user.OpDateTime = DateTime.Now.Date;
             //user.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

             //if (UserCode == string.Empty)
             //{
             //    user.Genius = false;
             //    user.CreateDateTime = DateTime.Now;
             //    user.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
             //    user.IPAddress = IPAddress;
             //    db.BizTbl_User.Add(user);
             //    //DataContext.BizTbl_Users.InsertOnSubmit(user);
             //}
             //db.SaveChanges();
             //DataContext.SubmitChanges();

             //Int64 UserId = user.ID;

             //BizTbl_User user = new BizTbl_User();

             //if (UserCode != string.Empty)
             //{
             //    int UserID = Convert.ToInt32(UserCode);
             //    user = db.BizTbl_User.Where(x => x.ID == UserID).FirstOrDefault();
             //}

       
        //int FirmID=Convert.ToInt32(Firm);
        //     long RegionID=Convert.ToInt64(hdnRegionId);

        //user.SalutationTypeID = Convert.ToInt32(SalutionId);
        //user.FirmID = Convert.ToInt32(CheckEmptyStringDBParameter(FirmID, true));
        //user.Name = Name;
        //user.Surname = SurName;
        //user.CountryID = Convert.ToInt32(Country);
        //user.RegionID = Convert.ToInt64(CheckEmptyStringDBParameter(RegionID, false,false ,false ,false ,false , true));
        //user.CityID = Convert.ToInt64(CheckEmptyStringDBParameter(cityID, false, false, false, false, false, true));
        //user.City = Convert.ToString(CheckEmptyStringDBParameter(cityName));
        //user.Address = Address;
        //user.Phone = Phone;
        //user.Email = Email;
        //user.PostCode = Convert.ToString(CheckEmptyStringDBParameter(PostalCode));
        //user.UserName = Convert.ToString(CheckEmptyStringDBParameter(UserName));
        //     if(Password!=string.Empty)
        //     {
        //         user.Password = Encrypt(Password);
        //     }
        //     if(Status!=string.Empty)
        //     {
        //         user.StatusID = Convert.ToInt32(Status);
        //     }
             
        //user.Active = Convert.ToBoolean(Active);
        //user.Locked = Convert.ToBoolean(Locked);
        //user.OpDateTime = DateTime.Now.Date;
        //user.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
        //     if(UserCode==string.Empty)
        //     {
        //         user.Genius=false;
        //         user.CreateDateTime=DateTime.Now;
        //         user.CreateUserID=Convert.ToInt64(ctrl.Session["UserID"]);
        //         user.IPAddress = IPAddress;
        //         db.BizTbl_User.Add(user);
        //         //db.SaveChanges();
        //         //return i;
        //     }
             
        //     db.SaveChanges();
    

        //    Int64 ID=user.ID;


           //  return i;

          
         }

         public static object CheckEmptyStringDBParameter(object Value, bool ReturnInteger = false, bool ReturnDate = false, bool ReturnDouble = false, bool ReturnDecimal = false, bool ReturnBoolean = false, bool ReturnLong = false)
         {

             if (Value == string.Empty)
             {
                 return null;
             }

             if (ReturnInteger)
             {
                 return Convert.ToInt32(Value);
             }

             //if (ReturnDate)
             //{

             //    return DateTime.ParseExact(Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

             //}

             if (ReturnDouble)
             {
                 return Convert.ToDouble(Value);
             }

             if (ReturnDecimal)
             {
                 return Convert.ToDecimal(Value);
             }

             if (ReturnBoolean)
             {
                 return Convert.ToBoolean(Value);
             }

             if (ReturnLong)
             {
                 return Convert.ToInt64(Value);
             }

             return Value;

         }

  

         public string GetUsername(long ID)
         {
             string Name1 = "";
              DBEntities UpdateUserOperationsobj = new DBEntities();
              var Username = from U in db.BizTbl_User where U.ID == ID select new { Name = U.Name + " " + U.Surname};
             foreach (var item in Username)
             {
                 Name1 = item.Name;
                // Description=item.
             }
                     
                       
                      return Name1;
         }

         //public string GetSecurityGroup1(long ID)
         //{
         //    //DataTable dt = new DataTable();
         //    string Status = "";
         //    SQLCon.Open();
         //    SqlCommand cmd = new SqlCommand("B_Ex_GetSecurityGroup1_BizTbl_SecurityGroup_SP", SQLCon);
         //    cmd.CommandType = CommandType.StoredProcedure;
         //    cmd.Parameters.AddWithValue("@UserID", ID);
         //    cmd.Parameters.AddWithValue("@Culture", CultureValue);
         //    Status = Convert.ToString(cmd.ExecuteScalar());
         //    //sda.Fill(dt);
         //    SQLCon.Close();

         //    //List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

         //    //if (dt.Rows.Count > 0)
         //    //{

         //    //    foreach (DataRow dr in dt.Rows)
         //    //    {
         //    //        UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
         //    //        UseropertionsObj.Description = dr["BusinessPartnerTypeID"].ToString();
         //    //        UseropertionsObj.ID = Convert.ToInt32(dr["ID"]);
         //    //        UseropertionsObj.BusinessPartnerType = dr["BusinessPartnerTypeName"].ToString();
         //    //        ListOfModel.Add(UseropertionsObj);
         //    //    }

         //    //}
         //    return Status;
         //}
         public static SelectList GetSecurityGroup()
         {
              UserOperationsRepository drpObj = new UserOperationsRepository();
              var SecurityGroup = drpObj.GetSecurityGroups();

             List<SelectListItem> _ListSecurity = new List<SelectListItem>();

             foreach (var item in SecurityGroup)
             {
                 SelectListItem itr = new SelectListItem();
                 itr.Value= item.ID.ToString();
                 itr.Text = item.Description.ToString();
                 itr.Selected = false;

                 _ListSecurity.Add(itr);
             }

             return new SelectList(_ListSecurity, "Value", "Text");
         }

         public List<UsersOperationsExt> GetSecurityGroups()
         {
             List<UsersOperationsExt> list = new List<UsersOperationsExt>();
             DBEntities entity = new DBEntities();
             var Culture = new SqlParameter("@CultureCode", CultureValue);
             DataTable dt = new DataTable();
             dt.Columns.Add("ID");
             dt.Columns.Add("Description");
             dt.Columns.Add("Code");
             var result = entity.Database.SqlQuery<GetsecuritygroupsUseroperations_Result>("B_Ex_GetSecurityGroupsUseroperations_BizTbl_SecurityGroup_SP @CultureCode", Culture).ToList();
             foreach (GetsecuritygroupsUseroperations_Result Val in result)
             {
                 dt.Rows.Add(Val.ID, Val.Description);
             }
             if (dt.Rows.Count > 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     UsersOperationsExt drpObj = new UsersOperationsExt();
                     drpObj.ID = Convert.ToInt32(dr["ID"]);
                     drpObj.Description = dr["Description"].ToString();
                     drpObj.Code = dr["Code"].ToString();
                     list.Add(drpObj);
                 }
             }
             return list;
         }


         public static SelectList GetBusinessPartnersList(string FirmID)
         {
             
              DropDownListsRepository drpObj = new DropDownListsRepository();
             var BusinessPartner = drpObj.GetBusinessPartner();

             List<SelectListItem> _ListBusinessPartner = new List<SelectListItem>();

             foreach (var item in BusinessPartner)
             {
                 SelectListItem itr = new SelectListItem();

                 if (FirmID == "" || FirmID == null)
                 {
                     itr.Value = item.ID.ToString();
                     itr.Text = item.Name.ToString();
                     itr.Selected = false;

                     _ListBusinessPartner.Add(itr);
                 }
                 else if (item.FirmID.ToString() == FirmID)
                 {
                     itr.Value = item.ID.ToString();
                     itr.Text = item.Name.ToString();
                     itr.Selected = false;

                     _ListBusinessPartner.Add(itr);
                 }
                
             }

             return new SelectList(_ListBusinessPartner, "Value", "Text");
         }

         public static SelectList GetBusinessPartners()
         {
             UserOperationsRepository drpObj = new UserOperationsRepository();
             var BusinessPartner = drpObj.GetBusinessPartner();

             List<SelectListItem> _ListBusinessPartner = new List<SelectListItem>();

             foreach (var item in BusinessPartner)
             {
                 SelectListItem itr = new SelectListItem();
                 itr.Value = item.ID.ToString();
                 itr.Text = item.BusinessPartnerType.ToString();
                 itr.Selected = false;

                 _ListBusinessPartner.Add(itr);
             }

             return new SelectList(_ListBusinessPartner, "Value", "Text");
         }

         public List<UsersOperationsExt> GetBusinessPartner()
         {
             // string PropertyConditions = "";
             DataTable dt = new DataTable();
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("B_Ex_GetBusinessPartners_TB_BusinessPartner_SP", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Culture", CultureValue);
             cmd.Parameters.AddWithValue("@OrderBy", "ID");
             cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
             cmd.Parameters.AddWithValue("@PageIndex", 1);
             SqlDataAdapter sda = new SqlDataAdapter(cmd);
             sda.Fill(dt);
             SQLCon.Close();

             List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

             if (dt.Rows.Count > 0)
             {
                
                 foreach (DataRow dr in dt.Rows)
                 {
                     UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
                     UseropertionsObj.Description = dr["BusinessPartnerTypeID"].ToString();
                     UseropertionsObj.ID = Convert.ToInt32(dr["ID"]);
                     UseropertionsObj.BusinessPartnerType = dr["BusinessPartnerTypeName"].ToString();
                     ListOfModel.Add(UseropertionsObj);
                 }

             }
             return ListOfModel;
         }

         public static SelectList GetHotelsList(string ID)
         {
             UserOperationsRepository drpObj = new UserOperationsRepository();
             var HotelsList = drpObj.GetHotelList(ID);

             List<SelectListItem> _ListHotelsList = new List<SelectListItem>();

             foreach (var item in HotelsList)
             {
                 SelectListItem itr = new SelectListItem();
                 itr.Value = item.ID.ToString();
                 itr.Text = item.HotelName.ToString();
                 itr.Selected = false;

                 _ListHotelsList.Add(itr);
             }

             return new SelectList(_ListHotelsList, "Value", "Text");
         }
         public List<UsersOperationsExt> GetHotelList(string ID)
         {
             // string PropertyConditions = "";
             DataTable dt = new DataTable();
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("B_GetHotelsList_TB_Hotel_SP", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@FirmID", ID);
             SqlDataAdapter sda = new SqlDataAdapter(cmd);
             sda.Fill(dt);
             SQLCon.Close();

             List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

             if (dt.Rows.Count > 0)
             {

                 foreach (DataRow dr in dt.Rows)
                 {
                     UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
                     UseropertionsObj.ID = Convert.ToInt32(dr["ID"]);
                     UseropertionsObj.HotelName = dr["Name"].ToString();
                     ListOfModel.Add(UseropertionsObj);
                 }

             }
             return ListOfModel;
         }
         public static SelectList GetHotels()
         {
             UserOperationsRepository drpObj = new UserOperationsRepository();
             var BusinessPartner = drpObj.GetHotel();

             List<SelectListItem> _ListBusinessPartner = new List<SelectListItem>();

             foreach (var item in BusinessPartner)
             {
                 SelectListItem itr = new SelectListItem();
                 itr.Value = item.ID.ToString();
                 itr.Text = item.HotelName.ToString();
                 itr.Selected = false;

                 _ListBusinessPartner.Add(itr);
             }

             return new SelectList(_ListBusinessPartner, "Value", "Text");
         }


         

         public List<UsersOperationsExt> GetHotel()
         {
             // string PropertyConditions = "";
             DataTable dt = new DataTable();
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("B_Ex_GetHotels_TB_Hotel_SP", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Culture", CultureValue);
             cmd.Parameters.AddWithValue("@OrderBy", "ID");
             cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
             cmd.Parameters.AddWithValue("@PageIndex", 1);
             SqlDataAdapter sda = new SqlDataAdapter(cmd);
             sda.Fill(dt);
             SQLCon.Close();

             List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

             if (dt.Rows.Count > 0)
             {

                 foreach (DataRow dr in dt.Rows)
                 {
                     UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
                     UseropertionsObj.ID = Convert.ToInt32(dr["ID"]);
                     UseropertionsObj.HotelName = dr["Name"].ToString();
                     ListOfModel.Add(UseropertionsObj);
                 }

             }
             return ListOfModel;
         }

         public static ArrayList GetUserRights(long ID)
         {
             UserOperationsRepository modelRepo = new UserOperationsRepository();
             var UserRight = modelRepo.GetUserRight(ID);

             ArrayList list = new ArrayList();

             foreach (var item in UserRight)
             {
                 SelectListItem itr = new SelectListItem();
                 // itr.Text = item.Name;
                 itr.Value = item.ID.ToString();
                 //itr.Selected = false;

                 list.Add(item.ID.ToString());
             }


             return new ArrayList(list);
         }
         public List<UsersOperationsExt> GetUserRight(long ID)
         {
             Int64 UserID = ID;
             DataTable dt = new DataTable();
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("TB_SP_GetUserRights", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Culture", CultureValue);
             cmd.Parameters.AddWithValue("@OrderBy", "ID");
             cmd.Parameters.AddWithValue("@UserID", UserID);
             SqlDataAdapter sda = new SqlDataAdapter(cmd);
             sda.Fill(dt);
             SQLCon.Close();

             List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

             if (dt.Rows.Count > 0)
             {

                 foreach (DataRow dr in dt.Rows)
                 {
                     UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
                     UseropertionsObj.ID = Convert.ToInt32(dr["SecurityGroupID"]);
                     UseropertionsObj.SecurityGroupDescription = dr["SecurityGroupDescription"].ToString();
                     ListOfModel.Add(UseropertionsObj);
                 }

             }
             return ListOfModel;
         }




         public static ArrayList GetUserBusinessPartners(long ID)
         {
             UserOperationsRepository modelRepo = new UserOperationsRepository();
             var UserBusinessPartner = modelRepo.GetUserBusinessPartner(ID);

             ArrayList list = new ArrayList();

             foreach (var item in UserBusinessPartner)
             {
                 SelectListItem itr = new SelectListItem();
                 // itr.Text = item.Name;
                 itr.Value = item.ID.ToString();
                 //itr.Selected = false;

                 list.Add(item.ID.ToString());
             }


             return new ArrayList(list);
         }
        
         public List<UsersOperationsExt> GetUserBusinessPartner(long id)
         {
             Int64 UserID = id;
             DataTable dt = new DataTable();
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("TB_SP_GetUserBusinessPartners", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Culture", CultureValue);
             cmd.Parameters.AddWithValue("@UserIDs", UserID);
             cmd.Parameters.AddWithValue("@OrderBy", "ID");
             SqlDataAdapter sda = new SqlDataAdapter(cmd);
             sda.Fill(dt);
             SQLCon.Close();

             List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

             if (dt.Rows.Count > 0)
             {

                 foreach (DataRow dr in dt.Rows)
                 {
                     UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
                     UseropertionsObj.ID = Convert.ToInt32(dr["ID"]);
                     UseropertionsObj.Name = dr["Name"].ToString();
                    
                     ListOfModel.Add(UseropertionsObj);
                 }

             }
             return ListOfModel;
         }

         public static ArrayList GetUserHotels(long id)
         {
             UserOperationsRepository modelRepo = new UserOperationsRepository();
             var UserHotel = modelRepo.GetUserHotel(id);

             ArrayList list = new ArrayList();

             foreach (var item in UserHotel)
             {
                 SelectListItem itr = new SelectListItem();
                 // itr.Text = item.Name;
                 itr.Value = item.ID.ToString();
                 //itr.Selected = false;

                 list.Add(item.ID.ToString());
             }


             return new ArrayList(list);
         }
         public List<UsersOperationsExt> GetUserHotel(long id)
         {
             // string PropertyConditions = "";

             Int64 UserID = id;
             DataTable dt = new DataTable();
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("TB_SP_GetUserHotels", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Culture", CultureValue);
             cmd.Parameters.AddWithValue("@OrderBy", "ID");
             cmd.Parameters.AddWithValue("@UserIDs", UserID);
             SqlDataAdapter sda = new SqlDataAdapter(cmd);
             sda.Fill(dt);
             SQLCon.Close();

             List<UsersOperationsExt> ListOfModel = new List<UsersOperationsExt>();

             if (dt.Rows.Count > 0)
             {

                 foreach (DataRow dr in dt.Rows)
                 {
                     UsersOperationsExt UseropertionsObj = new UsersOperationsExt();
                     UseropertionsObj.ID = Convert.ToInt32(dr["HotelID"]);
                     UseropertionsObj.HotelName = dr["HotelName"].ToString();
                     ListOfModel.Add(UseropertionsObj);
                 }

             }
             return ListOfModel;
         }
         public int AuthurizationUserOperations(string Selectedsecurity, string Selectedhotels, string Selectedbusinesspartner, string UserID)
         {
            
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("B_Ex_InSertUserAuthorizationDetails_UserHotel_SP", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@UserID", UserID);
             cmd.Parameters.AddWithValue("@Selectedsecurity", Selectedsecurity);
             cmd.Parameters.AddWithValue("@Selectedhotels", Selectedhotels);
             cmd.Parameters.AddWithValue("@Selectedbusinesspartner", Selectedbusinesspartner);
           
             int i = cmd.ExecuteNonQuery();
             SQLCon.Close();
             return i;
         }
         public int DeleteUser(int UserID)
         {
             int status = 1;

             using (DBEntities DE = new DBEntities())
             {
                 var UserTable = DE.BizTbl_User.Where(x => x.ID == UserID).FirstOrDefault();
                 DE.BizTbl_User.Remove(UserTable);
                 DE.SaveChanges();
             }
             return status;

         }

         public string Decrypt128New(string Pass)
         {

             //  string EncryptionKey = "MAKV2SPBNI99212";
             byte[] cipherBytes = Convert.FromBase64String(Pass);
             using (Aes encryptor = Aes.Create())
             {
                 encryptor.Key = Encoding.UTF8.GetBytes("2428598755421637");
                 encryptor.IV = Encoding.UTF8.GetBytes("5369523205842148");

                 using (MemoryStream ms = new MemoryStream())
                 {
                     using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                     {
                         cs.Write(cipherBytes, 0, cipherBytes.Length);
                         cs.Close();
                     }
                     Pass = Encoding.Unicode.GetString(ms.ToArray());
                 }
             }
             return Pass;
         }

         public static string Encrypt(string clearText)
         {
             byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
             using (Aes encryptor = Aes.Create())
             {
                 encryptor.Key = Encoding.UTF8.GetBytes("2428598755421637");
                 encryptor.IV = Encoding.UTF8.GetBytes("5369523205842148");

                 using (MemoryStream ms = new MemoryStream())
                 {
                     using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                     {
                         cs.Write(clearBytes, 0, clearBytes.Length);
                         cs.Close();
                     }
                     clearText = Convert.ToBase64String(ms.ToArray());
                 }
             }
             return clearText;
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

         public class Encryption64
         {

             private byte[] key = { };
             //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

             private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

             //public string Decrypt(string stringToDecrypt, string sEncryptionKey)
             //{
             //    byte[] inputByteArray = new byte[stringToDecrypt.Length];
             //    try
             //    {
             //        key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
             //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
             //        inputByteArray = Convert.FromBase64String(stringToDecrypt);
             //        MemoryStream ms = new MemoryStream();
             //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV),
             //                                                          CryptoStreamMode.Write);
             //        cs.Write(inputByteArray, 0, inputByteArray.Length);
             //        cs.FlushFinalBlock();
             //        Encoding encoding = Encoding.UTF8;
             //        return encoding.GetString(ms.ToArray());
             //    }
             //    catch (Exception ex)
             //    {
             //        return ex.Message;
             //    }
             //}


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
        }
       
    }

