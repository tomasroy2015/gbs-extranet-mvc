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
    public class FirmInformationRepository : BaseRepository
    {
        public List<FirmInformationExt> GetFirmInfo(int id)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            List<FirmInformationExt> list = new List<FirmInformationExt>();
            DataSet ds = new DataSet();
            
            DataTable dt = new DataTable();
           // dt = ds.Tables[1];
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetFirms", SQLCon);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", 1);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@FirmID", id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            dt = ds.Tables[1];
            SQLCon.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FirmInformationExt EmailObj = new FirmInformationExt();
                    EmailObj.ID = Convert.ToInt32(dr["ID"]);
                    EmailObj.FirmName = dr["Name"].ToString();
                    EmailObj.Country = dr["CountryName"].ToString();
                    EmailObj.City = dr["CityName"].ToString();
                    EmailObj.Address = dr["Address"].ToString();
                    EmailObj.Phone = dr["Phone"].ToString();
                    EmailObj.Fax = dr["Fax"].ToString();
                    EmailObj.PostCode = dr["PostCode"].ToString();
                    EmailObj.EmailID = dr["Email"].ToString();
                    EmailObj.TaxOffice = dr["TaxDepartment"].ToString();
                    EmailObj.TaxNo = dr["TaxNo"].ToString();
                    EmailObj.ExecutiveName = dr["ContactPersonFullName"].ToString();                  
                    list.Add(EmailObj);
                }
            }
            return list;
        }

    }

    public class FirmInformationExt
    {

        public int ID { get; set; }
        public string FirmName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string PostCode { get; set; }

        public string EmailID { get; set; }

        public string TaxOffice { get; set; }

        public string TaxNo { get; set; }

        public string ExecutiveName { get; set; }
    }
}