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
    public class AuthenticationRepository : BaseRepository
    {       
        public DataTable GetSecurityGroup(string CultureCode)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetSecurityGroup_BizTbl_SecurityGroup_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            return dt;
        }

        public DataTable GetSecurityGroupRights(string CultureCode, int SecurityGroupID)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetSecurities_BizTbl_Security_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@SecurityGroupID", SecurityGroupID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            return dt;
        }



        public bool Delete(int SecurityGroupID)
        {
            bool status = true;

            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DeleteGroup_BizTbl_SecurityGroupRight_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SecurityGroupID", SecurityGroupID);

            status = Convert.ToBoolean(cmd.ExecuteNonQuery());

            //using (DBEntities DE = new DBEntities())
            //{
            //    //var SecurityGroupRightTable = DE.BizTbl_SecurityGroupRight.Where(x => x.SecurityGroupID == SecurityGroupID).FirstOrDefault();
            //    var Security = DE.BizTbl_SecurityGroupRight.Where(x => x.SecurityGroupID == SecurityGroupID).FirstOrDefault();
            //    DE.BizTbl_SecurityGroupRight.Remove(Security);
            //    DE.SaveChanges();
            //}
            return status;
        }

        public bool Update(int SecurityGroupID, string SecutiryIDs, Controller ctrl)
        {
            bool status = true;


            string[] Security = SecutiryIDs.Split(',');

            foreach (string securityID in Security)
            {
                DBEntities insertentity = new DBEntities();
                BizTbl_SecurityGroupRight MsgObj = new BizTbl_SecurityGroupRight();

                MsgObj.SecurityGroupID = SecurityGroupID;
                MsgObj.SecurityID = Convert.ToInt32(securityID);
                MsgObj.HasRight = true;
                MsgObj.OpDateTime = DateTime.Now;
                MsgObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

                insertentity.BizTbl_SecurityGroupRight.Add(MsgObj);
                insertentity.SaveChanges();
                int id = MsgObj.ID;
            }

            // int id = MsgObj.ID;
            return status;
        }
    }
    public class AuthenticationExt
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Info { get; set; }
        public string OpDateTime { get; set; }
        public string OpUserID { get; set; }
        public string HasRight { get; set; }
        public bool HasValue { get; set; }
    }
}