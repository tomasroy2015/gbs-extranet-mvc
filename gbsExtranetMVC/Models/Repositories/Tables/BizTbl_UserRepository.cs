using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_UserRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_UserExt> ReadAll(int TableID)
        {
            List<BizTbl_UserExt> list = new List<BizTbl_UserExt>();

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
                    BizTbl_UserExt PageObj = new BizTbl_UserExt();

                    PageObj.ID = dr["ID"].ToString();
                    PageObj.Name = dr["Name"].ToString();
                    PageObj.Surname = dr["Surname"].ToString();
                    try
                    {
                        PageObj.CountryID = Convert.ToInt32(dr["CountryID"]);
                    }
                    catch
                    {
                        PageObj.CountryID = 0;
                    }
                    
                    PageObj.Country = dr["FK_CountryID_ID"].ToString();
                    PageObj.City = dr["City"].ToString();
                    PageObj.Address = dr["Address"].ToString();
                    PageObj.Email = dr["Email"].ToString();
                    PageObj.Phone = dr["Phone"].ToString();
                    PageObj.PostCode = dr["PostCode"].ToString();
                    PageObj.UserName = dr["UserName"].ToString();
                    PageObj.Password = dr["Password"].ToString();
                    PageObj.Firm = dr["FK_FirmID_ID"].ToString();                    
                    PageObj.FirmID = dr["FirmID"].ToString();
                    PageObj.StatusID = dr["StatusID"].ToString();
                    PageObj.StatusName = dr["FK_StatusID_ID"].ToString();
                    //try
                    //{
                    //    PageObj.StatusID = Convert.ToInt32(dr["StatusID"]);
                    //}
                    //catch
                    //{
                    //    PageObj.StatusID = 0;
                    //}
                    
                    
                        //try
                        //{
                        //    PageObj.FirmID = Convert.ToInt32(dr["FirmID"]);
                        //}
                        //catch
                        //{
                        //    PageObj.FirmID = 0;
                        //}
                        try
                        {
                            PageObj.PromotionalEmail = Convert.ToBoolean(dr["PromotionalEmail"]);
                        }
                        catch
                        {
                            PageObj.PromotionalEmail = false;
                        }
                        PageObj.VerificationCode = dr["VerificationCode"].ToString();
                        PageObj.DisplayName = dr["DisplayName"].ToString();
                        try
                        {
                            PageObj.Locked = Convert.ToBoolean(dr["Locked"]);
                        }
                        catch
                        {
                            PageObj.Locked = false;
                        }
                        try
                        {
                            PageObj.Active = Convert.ToBoolean(dr["Active"]);
                        }
                        catch
                        {
                            PageObj.Active = false;
                        }

                        PageObj.IPAddress = dr["IPAddress"].ToString();
                        list.Add(PageObj);
                    
                    
                }
            }


            return list;
        }

        public bool Create(BizTbl_UserExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            SQLCon.Open();

            SqlCommand cmd = new SqlCommand("B_InsertUser_BizTbl_User_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_Name", model.Name);
            cmd.Parameters.AddWithValue("@p_Surname", model.Surname);
            cmd.Parameters.AddWithValue("@p_CountryID", model.CountryID);
            cmd.Parameters.AddWithValue("@p_City", model.City);
            cmd.Parameters.AddWithValue("@p_Address", model.Address);
            cmd.Parameters.AddWithValue("@p_Email", model.Email);
            cmd.Parameters.AddWithValue("@p_Phone", model.Phone);
            cmd.Parameters.AddWithValue("@p_PostCode", model.PostCode);
            cmd.Parameters.AddWithValue("@p_UserName", model.UserName);
            cmd.Parameters.AddWithValue("@p_Password", model.Password);
            cmd.Parameters.AddWithValue("@p_FirmID", model.FirmID);
            cmd.Parameters.AddWithValue("@p_StatusID", model.StatusID);          
            //if (model.FirmID == 0)
            //{
            //    cmd.Parameters.AddWithValue("@p_FirmID", null);                
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@p_FirmID", model.FirmID);                
            //}
            //if (model.StatusID == 0)
            //{               
            //    cmd.Parameters.AddWithValue("@p_StatusID", null);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@p_StatusID", model.StatusID);                
            //}

            cmd.Parameters.AddWithValue("@p_PromotionalEmail", Convert.ToBoolean(model.PromotionalEmail));   
            cmd.Parameters.AddWithValue("@p_VerificationCode", model.VerificationCode);
            cmd.Parameters.AddWithValue("@p_DisplayName", model.DisplayName);
            cmd.Parameters.AddWithValue("@p_Locked", Convert.ToBoolean(model.Locked));
            cmd.Parameters.AddWithValue("@p_Active", Convert.ToBoolean(model.Active));
            cmd.Parameters.AddWithValue("@p_IPAddress", model.IPAddress);
            cmd.Parameters.AddWithValue("@p_CreateUserID", Convert.ToInt64(ctrl.Session["UserID"]));
            cmd.Parameters.AddWithValue("@p_OpUserID", Convert.ToInt64(ctrl.Session["UserID"]));
            // cmd.Parameters.AddWithValue("@p_ID", model.ID);

            int i = Convert.ToInt32(cmd.ExecuteNonQuery());

            SQLCon.Close();

           // DBEntities insertentity = new DBEntities();
           // BizTbl_User MsgObj = new BizTbl_User();
           //// MsgObj.ID = model.ID;            
           // MsgObj.Name = model.Name;
           // MsgObj.Surname = model.Surname;
           // MsgObj.SalutationTypeID = null;
           // MsgObj.RegionID = null;
           // MsgObj.CityID = null;
           // MsgObj.CountryID = model.CountryID;
           // MsgObj.City = model.City;
           // MsgObj.Address = model.Address;
           // MsgObj.Email = model.Email;
           // MsgObj.Phone = model.Phone;
           // MsgObj.PostCode = model.PostCode;
           // MsgObj.UserName = model.UserName;
           // MsgObj.Password = model.Password;
           // if (model.FirmID == 0)
           // {
           //     MsgObj.FirmID = null;
           // }
           // else
           // {
           //     MsgObj.FirmID = model.FirmID;
           // }
           // if (model.StatusID==0)
           // {
           //     MsgObj.StatusID = null;
           // }
           // else
           // {
           //     MsgObj.StatusID = model.StatusID;
           // }

           // MsgObj.PromotionalEmail =Convert.ToBoolean( model.PromotionalEmail);
           // MsgObj.VerificationCode = model.VerificationCode;
           // MsgObj.DisplayName = model.DisplayName;
           // MsgObj.Locked = Convert.ToBoolean(model.Locked);
           // MsgObj.Active = Convert.ToBoolean(model.Active);
           // MsgObj.IPAddress = model.IPAddress;
           // MsgObj.CreateDateTime = DateTime.Now;
           // MsgObj.OpDateTime = DateTime.Now;
           // MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
           // MsgObj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
           // db.BizTbl_User.Add(MsgObj);
           // db.SaveChanges();
           // Int64 id = MsgObj.ID;
            return status;
        }


        public bool Delete(BizTbl_UserExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            long IDValue = Convert.ToInt64(model.ID);
            var obj = db.BizTbl_User.Where(x => x.ID == IDValue).FirstOrDefault();
            db.BizTbl_User.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_UserExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            long IDValue = Convert.ToInt64(model.ID);
           // var PageObj = db.BizTbl_User.Where(x => x.ID == IDValue).FirstOrDefault();

            SQLCon.Open();

            SqlCommand cmd = new SqlCommand("B_InsertUser_BizTbl_User_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_Name", model.Name);
            cmd.Parameters.AddWithValue("@p_Surname", model.Surname);
            cmd.Parameters.AddWithValue("@p_CountryID", model.CountryID);
            cmd.Parameters.AddWithValue("@p_City", model.City);
            cmd.Parameters.AddWithValue("@p_Address", model.Address);
            cmd.Parameters.AddWithValue("@p_Email", model.Email);
            cmd.Parameters.AddWithValue("@p_Phone", model.Phone);
            cmd.Parameters.AddWithValue("@p_PostCode", model.PostCode);
            cmd.Parameters.AddWithValue("@p_UserName", model.UserName);
            cmd.Parameters.AddWithValue("@p_Password", model.Password);
            cmd.Parameters.AddWithValue("@p_FirmID", model.FirmID);
            cmd.Parameters.AddWithValue("@p_StatusID", model.StatusID);
            //if (model.FirmID == 0)
            //{
            //    cmd.Parameters.AddWithValue("@p_FirmID", null);                
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@p_FirmID", model.FirmID);                
            //}
            //if (model.StatusID == 0)
            //{
            //    cmd.Parameters.AddWithValue("@p_StatusID", null);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@p_StatusID", model.StatusID);
            //}

            cmd.Parameters.AddWithValue("@p_PromotionalEmail", Convert.ToBoolean(model.PromotionalEmail));
            cmd.Parameters.AddWithValue("@p_VerificationCode", model.VerificationCode);
            cmd.Parameters.AddWithValue("@p_DisplayName", model.DisplayName);
            cmd.Parameters.AddWithValue("@p_Locked", Convert.ToBoolean(model.Locked));
            cmd.Parameters.AddWithValue("@p_Active", Convert.ToBoolean(model.Active));
            cmd.Parameters.AddWithValue("@p_IPAddress", model.IPAddress);
            cmd.Parameters.AddWithValue("@p_CreateUserID", Convert.ToInt64(ctrl.Session["UserID"]));
            cmd.Parameters.AddWithValue("@p_OpUserID", Convert.ToInt64(ctrl.Session["UserID"]));
            cmd.Parameters.AddWithValue("@p_ID", IDValue);

            int i = Convert.ToInt32(cmd.ExecuteNonQuery());

            //PageObj.ID = IDValue;
            //PageObj.Name = model.Name;
            //PageObj.Surname = model.Surname;
            //PageObj.CountryID = model.CountryID;
            //PageObj.City = model.City;
            //PageObj.Address = model.Address;
            //PageObj.Email = model.Email;
            //PageObj.Phone = model.Phone;
            //PageObj.PostCode = model.PostCode;
            //PageObj.UserName = model.UserName;
            //PageObj.Password = model.Password;
            //if (model.FirmID == 0)
            //{
            //    if(IDValue==0)
            //    {
            //        PageObj.FirmID = 0;
            //    }
            //    else
            //    {
            //        PageObj.FirmID = null;
            //    }
            //}
            //else
            //{
            //    PageObj.FirmID = model.FirmID;
            //}
            //if (model.StatusID == 0)
            //{
            //    PageObj.StatusID = null;
            //}
            //else
            //{
            //    PageObj.StatusID = model.StatusID;
            //}
            //PageObj.PromotionalEmail = Convert.ToBoolean(model.PromotionalEmail);
            //PageObj.VerificationCode = model.VerificationCode;
            //PageObj.DisplayName = model.DisplayName;
            //PageObj.Locked = Convert.ToBoolean(model.Locked);
            //PageObj.Active = Convert.ToBoolean(model.Active);
            //PageObj.IPAddress = model.IPAddress;
            //PageObj.CreateDateTime = DateTime.Now;
            //PageObj.OpDateTime = DateTime.Now;
            //PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            //PageObj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            //db.SaveChanges();
            return status;
        }
    }

    public class BizTbl_UserExt
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Surname { get; set; }
        public string Country { get; set; }
        public int CountryID { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Firm { get; set; }
        public string FirmID { get; set; }
        public string StatusName { get; set; }
        public string StatusID { get; set; }
        public bool PromotionalEmail { get; set; }
        public string VerificationCode { get; set; }
        public string DisplayName    { get; set; }
        public bool Locked { get; set; }
        public bool Active { get; set; }
        public string IPAddress { get; set; }
        
    }
}