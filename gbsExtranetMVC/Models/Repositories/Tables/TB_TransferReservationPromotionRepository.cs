using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Business;
using System.Collections;
using gbsExtranetMVC.Models;
using Extension;
namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_TransferReservationPromotionRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TransferReservationPromotionExt> GetAllTableValue(int TableID)
        {
            List<TB_TransferReservationPromotionExt> list = new List<TB_TransferReservationPromotionExt>();

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
                    TB_TransferReservationPromotionExt model = new TB_TransferReservationPromotionExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.ReservationID = Convert.ToInt32(dr["FK_ReservationID_ID"]);
                    model.TransferReservationID = Convert.ToInt32(dr["FK_TransferReservationID_ID"]);
                    model.PromotionID = Convert.ToInt32(dr["PromotionID"]);
                    model.Promotion = dr["FK_PromotionID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_TransferReservationPromotionExt
    {
        public int ID { get; set; }
        public int ReservationID { get; set; }
        public int TransferReservationID { get; set; }
        public string Promotion { get; set; }
        public int PromotionID { get; set; }
        
    }
}