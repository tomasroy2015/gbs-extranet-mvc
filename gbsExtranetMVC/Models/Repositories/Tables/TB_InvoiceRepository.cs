﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_InvoiceRepository:BaseRepository
    {
        public static string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_InvoiceExt> ReadAll(int TableID)
        {
            List<TB_InvoiceExt> list = new List<TB_InvoiceExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTableNew_BizTbl_Table_Sp", SQLCon);
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
                    TB_InvoiceExt PageObj = new TB_InvoiceExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.Firm = dr["FK_FirmID_ID"].ToString();
                    PageObj.InvoiceStatus = dr["FK_InvoiceStatusID_ID"].ToString();
                    PageObj.InvoiceDate = dr["InvoiceDate"].ToString();
                    PageObj.Period = dr["FK_PeriodID_ID"].ToString();
                    PageObj.DueDate = dr["DueDate"].ToString();
                    PageObj.Amount = dr["Amount"].ToString();
                    PageObj.Currency = dr["FK_CurrencyID_ID"].ToString();
                    

                    list.Add(PageObj);
                }
            }


            return list;
        }


    }
    public class TB_InvoiceExt
    {
        public int ID { get; set; }
        public string Firm { get; set; }
        public string Period { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
    }

}