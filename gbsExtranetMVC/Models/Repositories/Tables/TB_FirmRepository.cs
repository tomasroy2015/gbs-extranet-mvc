using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_FirmRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


        public List<TB_FirmExt> ReadAll(int TableID)
        {
            List<TB_FirmExt> list = new List<TB_FirmExt>();

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
                    TB_FirmExt model = new TB_FirmExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Firm = dr["Name"].ToString();
                    model.CountryID = dr["CountryID"].ToString();
                    model.Country = dr["FK_CountryID"].ToString();
                    model.CityID = dr["CityID"].ToString();
                    model.City = dr["FK_CityID"].ToString();
                    model.Address = dr["Address"].ToString();
                    model.Postal_code = dr["PostCode"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.Fax = dr["Fax"].ToString();
                    model.Tax_Office = dr["TaxDepartment"].ToString();
                    model.Tax_ID = dr["TaxNo"].ToString();
                    model.TitleID = dr["ExecutiveTitleID"].ToString();
                    model.Executive_Title = dr["FK_ContactPersonSalutationTypeID"].ToString();
                    model.Executive_Name = dr["ContactPersonName"].ToString();
                    model.Executive_Surname = dr["ContactPersonSurname"].ToString();
                    model.Executive_Position = dr["ContactPersonPosition"].ToString();
                    model.Executive_Phone = dr["ContactPersonPhone"].ToString();
                    model.Executive_Email = dr["ContactPersonEmail"].ToString();
                    model.StatusID = dr["StatusID"].ToString();
                    model.Status = dr["FK_StatusID"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"].ToString());
                    model.IpAddress = dr["IPAddress"].ToString();                 
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_FirmExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            TB_Firm obj = new TB_Firm();        
            obj.Name = model.Firm;
            obj.CountryID = Convert.ToInt32(model.CountryID);
            obj.CityID = Convert.ToInt64(model.CityID);
            obj.Address = model.Address;
            obj.PostCode = model.Postal_code;
            obj.Email = model.Email;
            obj.Phone = model.Phone;
            obj.Fax = model.Fax;
            obj.TaxDepartment = model.Tax_Office;
            obj.TaxNo = model.Tax_ID;
            obj.ContactPersonSalutationTypeID = Convert.ToInt32(model.TitleID);
            obj.ContactPersonName = model.Executive_Name;
            obj.ContactPersonSurname = model.Executive_Surname;
            obj.ContactPersonPosition = model.Executive_Position;
            obj.ContactPersonPhone = model.Executive_Phone;
            obj.ContactPersonEmail = model.Executive_Email;
            obj.StatusID = Convert.ToInt32(model.StatusID);
            obj.Active = Convert.ToBoolean(model.Active);          
            obj.IPAddress = model.IpAddress;
            obj.CreateDateTime = DateTime.Now;
            obj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_Firm.Add(obj);
            db.SaveChanges();
            int id = Convert.ToInt32(obj.ID);
            return status;
        }
        public bool Update(TB_FirmExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_Firm.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.ID = model.ID;
            obj.Name = model.Firm;
            obj.CountryID = Convert.ToInt32(model.CountryID);
            obj.CityID = Convert.ToInt32(model.CityID);
            obj.Address = model.Address;
            obj.PostCode = model.Postal_code;
            obj.Email = model.Email;
            obj.Phone = model.Phone;
            obj.Fax = model.Fax;
            obj.TaxDepartment = model.Tax_Office;
            obj.TaxNo = model.Tax_ID;
            obj.ContactPersonSalutationTypeID = Convert.ToInt32(model.TitleID);
            obj.ContactPersonName = model.Executive_Name;
            obj.ContactPersonSurname = model.Executive_Surname;
            obj.ContactPersonPosition = model.Executive_Position;
            obj.ContactPersonPhone = model.Executive_Phone;
            obj.ContactPersonEmail = model.Executive_Email;
            obj.StatusID = Convert.ToInt32(model.StatusID);
            obj.Active = Convert.ToBoolean(model.Active);          
            obj.IPAddress = model.IpAddress;
            obj.CreateDateTime = DateTime.Now;
            obj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_FirmExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_Firm.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Firm.Remove(obj);
            db.SaveChanges();
            return status;
        }

    }

    public class TB_FirmExt
    {
        public int ID { get; set; }

        public string FirmID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Firm { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string CountryID { get; set; }
        public string Country { get; set; }

        public string CityID { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Postal_code { get; set; }

        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Fax { get; set; }

        public string Tax_Office { get; set; }

        public string Tax_ID { get; set; }

        public string TitleID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Executive_Title { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Executive_Name { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Executive_Surname { get; set; }
        public string Executive_Position { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Executive_Phone { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Executive_Email { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string StatusID { get; set; }
        public string Status { get; set; }

        public bool Active { get; set; }

        public string IpAddress { get; set; }

    }
}