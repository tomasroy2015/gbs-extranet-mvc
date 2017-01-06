using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_FirmHistoryRepository : BaseRepository
    {
         public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


        public List<TB_FirmHistoryExt> ReadAll(int TableID)
        {
            List<TB_FirmHistoryExt> list = new List<TB_FirmHistoryExt>();

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
                    TB_FirmHistoryExt model = new TB_FirmHistoryExt();
                    model.ID = dr["ID"].ToString();
                    model.FirmID = dr["FirmID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Country = dr["FK_CountryID_ID"].ToString();
                    model.City = dr["FK_CityID_ID"].ToString();
                    model.Address = dr["Address"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.Fax = dr["Fax"].ToString();
                    model.Postal_code = dr["PostCode"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.Tax_Office = dr["TaxDepartment"].ToString();
                    model.Tax_ID = dr["TaxNo"].ToString();
                    model.ContactPerson_Title = dr["FK_ContactPersonSalutationTypeID_ID"].ToString();
                    model.ContactPerson_Name = dr["ContactPersonName"].ToString();
                    model.ContactPerson_Surname = dr["ContactPersonSurname"].ToString();
                    model.ContactPerson_Position = dr["ContactPersonPosition"].ToString();
                    model.ContactPerson_Phone = dr["ContactPersonPhone"].ToString();
                    model.ContactPerson_Email = dr["ContactPersonEmail"].ToString();
                    model.Status = dr["FK_StatusID_ID"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"].ToString());
                    model.LogDate = dr["LogDateTime"].ToString();
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();   
                    list.Add(model);
                }
            }

            return list;
        }

    }

    public class TB_FirmHistoryExt
    {
        public string ID { get; set; }

        public string FirmID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Postal_code { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Tax_Office { get; set; }

        public string Tax_ID { get; set; }

        public string ContactPerson_Title { get; set; }

        public string ContactPerson_Name { get; set; }

        public string ContactPerson_Surname { get; set; }

        public string ContactPerson_Position { get; set; }

        public string ContactPerson_Phone { get; set; }

        public string ContactPerson_Email { get; set; }

        public string Status { get; set; }

        public bool Active { get; set; }

        public string LogDate { get; set; }

        public string LogUser { get; set; }

    }
    }
