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
    public class ContactUsRepository : BaseRepository
    {
        public List<ContactUsExt> GetContactUs()
        {             
                List<ContactUsExt> list = new List<ContactUsExt>();                
                 string[] Code = new string[] { "PhoneForPartner", "EmailForPartner", "FaxForPartner" };
                 ContactUsExt obj = new ContactUsExt(); 
                 for(int i=0;i<Code.Length;i++)
                 {
                     

                     var value = Code[i];

                 
                     obj.Values = GetValue(value);
                     if (obj.Values == "+33-184883175")
                     {
                         obj.Phone = obj.Values;
                       
                     }
                     else if (obj.Values == "partner@gbshotels.com")
                     {
                         
                         obj.Email = obj.Values;
                       
                     }
                     else  if(obj.Values=="")
                     {
                         
                         obj.Fax = obj.Values;
                       
                     }                                       
                    

                 }
                 list.Add(obj);


                 return list;
        }

        public string GetValue(String Code)
        {
          //  List<ContactUsExt> list = new List<ContactUsExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetContactUs_BizTbl_Parameter_SP", SQLCon);
            cmd.Parameters.AddWithValue("@code", Code);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ContactUsExt EmailObj = new ContactUsExt();

                    Code = dr["value"].ToString();
                   
                }
            }
            return Code;                
        }
    }

    public class ContactUsExt
    {

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ID { get; set; }
        public string Values { get; set; }
    }
}