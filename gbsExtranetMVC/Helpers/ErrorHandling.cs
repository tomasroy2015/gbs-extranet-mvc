using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Web.Mvc;

namespace gbsExtranetMVC.Helpers
{
    public static class ErrorHandling
    {
        public static string ErrorCode = null;
        public static string Id;

        //Get Error Number of SQL Exception
        public static void SetErrorCode(Exception ex)
        {
            try
            {
                //ELMAH will log all unhandled errors, but we also need to log the errors that we are handling here
                LogException(ex);

                if (ex is System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    if (ex.InnerException is System.Data.Entity.Core.UpdateException)
                    {
                        if (ex.InnerException.InnerException is System.Data.SqlClient.SqlException)
                        {
                            SqlException sqlex = (SqlException)ex.InnerException.InnerException;
                            ErrorCode = sqlex.Number.ToString();

                            if (sqlex.Message.Contains("Email"))
                            {
                                ErrorCode = "EmailAlreadyExists";
                            }
                        }

                    }
                }

            }
            catch
            {

                ErrorCode = "UnExpectedError";
            }

        }

        public static SQLErrorEnums HandleFormValidations(DbUpdateException ex)
        {

            SQLErrorEnums sqlerr = SQLErrorEnums.Username;
            if (ex.InnerException != null)
            {
                UpdateException ue = (UpdateException)ex.InnerException;
                if (ue.InnerException != null)
                {

                    System.Data.SqlClient.SqlException sqlex = (System.Data.SqlClient.SqlException)ue.InnerException;
                    ErrorCode = sqlex.Number.ToString();
                    if (sqlex.Message.Contains("Username"))
                        sqlerr = SQLErrorEnums.Username;
                    else if (sqlex.Message.Contains("ClientName"))
                        sqlerr = SQLErrorEnums.ClientName_AlreadyExists;

                }
            }


            return sqlerr;
        }


        public static int GetErrorCode(Exception ex)
        {

            SqlException sqlex = (SqlException)ex.InnerException;

            if (sqlex == null) return 0;
            return sqlex.Number;
        }

        public static void SetErrorCode(string erroCode)
        {
            ErrorCode = erroCode;
        }

        public static int GetErrorCode()
        {

            return Convert.ToInt32(ErrorCode);
        }

        //Get Custom Error Description
        public static string GetErrorDescription(Exception ex)
        {

            SqlException sqlex = (SqlException)ex.InnerException;

            string ErrorDesc = sqlex.Number.ToString();

            if (ErrorCode != null)
            {
                if (ErrorCode.Equals("2601"))
                {
                    ErrorDesc = "Error Code = " + ErrorCode + " Description = This data already exists, it must be unique";
                    ErrorCode = null;
                }
                else if (ErrorCode.Equals("547"))
                {
                    ErrorDesc = "Erorr Code = " + ErrorCode + " Description = The data entereted is not long enough.";
                    ErrorCode = null;
                }
            }

            return ErrorDesc;
        }
        public static string GetErrorDescription()
        {
            string ErrorDesc = null;

            if (ErrorCode != null)
            {
                if (ErrorCode.Equals("2601"))
                {
                    ErrorDesc = "Error Code = " + ErrorCode + " Description = This data already exists, it must be unique";
                    ErrorCode = null;
                }
                else if (ErrorCode.Equals("547"))
                {
                    ErrorDesc = "Erorr Code = " + ErrorCode + " Description = The data entereted is not long enough.";
                    ErrorCode = null;
                }
            }

            return ErrorDesc;
        }

        //Get Brief Description About Error
        public static string GetErrorBriefDescription(Exception ex)
        {
            string ErrorBriefDesc = null;

            if (ex != null)
            {
                SqlException sqlex = (SqlException)ex.InnerException;

                ErrorBriefDesc = "Error Code = " + sqlex.Number.ToString() + " Description = " + sqlex.Message;
                ErrorCode = null;
            }

            return ErrorBriefDesc;
        }





        public static string TrimText(string str, int n)
        {
            if (str.Length > n) return str.Substring(0, n) + "...";
            return str;
        }



        /// <summary>
        /// Logs the exception to ELMAH
        /// </summary>
        public static void LogException(Exception ex)
        {

            Elmah.ErrorSignal.FromContext(System.Web.HttpContext.Current).Raise(ex);
        }


        /// <summary>
        /// Returns a message that can be displayed to users, and if necessary logs the error.
        /// Use this when we haven't got a modelstate to put error messages in.
        /// </summary>
        public static string HandleException(Exception ex)
        {

            string errorMessage = GetErrorMessageAndLogException(ex);

            return errorMessage;

        }

        /// <summary>
        /// Generates a message that can be displayed to users and adds it to the modelstate errors, and if necessary logs the error.
        /// Use this when we're posting a model back from a view and want to put error into the modelstate.
        /// </summary>
        public static void HandleModelStateException(Exception ex, ModelStateDictionary modelState)
        {

            string errorMessage = GetErrorMessageAndLogException(ex);

            modelState.AddModelError("", errorMessage);

        }

        /// <summary>
        /// From an exception, creates a user friendly error message, and logs the exception if necessary
        /// </summary>
        private static string GetErrorMessageAndLogException(Exception ex)
        {
            string errorMessage = "";

            if (ex.GetType() == typeof(DbUpdateConcurrencyException))
            {
                //errorMessage = Resources.Messages.Error_Concurrency;
                errorMessage = "Your changes cannot be saved because this record has been updated by another user.  Please re-open it and try again.";
            }

            else if (ex.GetType() == typeof(DbUpdateException))
            {
                //errorMessage = Resources.Messages.Error_DbUpdate;
                errorMessage = "An error has occurred while saving this record.";
                LogException(ex);
            }

            else if (ex.GetType() == typeof(EntityCommandExecutionException))
            {
                //errorMessage = Resources.Messages.Error_DbCommand;
                errorMessage = "An error has occurred while getting data.";
                LogException(ex);
            }

            else
            {
                //errorMessage = Resources.Messages.Error_General;
                errorMessage = "An unexpected error has occurred.";
                LogException(ex);
            }

            return errorMessage;

        }



    }
}
