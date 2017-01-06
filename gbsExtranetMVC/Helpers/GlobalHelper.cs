using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Helpers
{

    public static class GlobalHelper
    {

        #region Submit Buttons

        /// <summary>
        /// Renders an HTML form submit button
        /// </summary>
        /// 

        public static string SubmitButton(string Label, string tabindex = "100")
        {
            return String.Format("<button id=\"btnSubmit\" type=\"submit\" tabindex=" + tabindex + " class=\"k-button\">" + Label + "</button>");
        }
        public static string SubmitButton(string Label, string JAction, string tabindex = "100")
        {
            return String.Format("<button id=\"btnSubmit\" type=\"button\" tabindex=" + tabindex + " class=\"k-button\" onclick=\"" + JAction + "\">" + Label + "</button>");
        }
        public static string SubmitButton(string id, string Label, string JAction, string tabindex = "100")
        {
            return String.Format("<button id=\"" + id + "\" type=\"button\" tabindex=" + tabindex + " class=\"k-button\" onclick=\"" + JAction + "\">" + Label + "</button>");
        }
        public static string AddNewRecordButton(string id, string Label, string JAction, string tabindex = "100")
        {
            return String.Format("<button id=\"" + id + "\" type=\"button\" tabindex=" + tabindex + " class=\"k-button\" onclick=\"" + JAction + "\"><span class='t-icon t-add'></span>" + Label + "</span></button>");
        }
        public static string SubmitButton(string id, string Label, string JAction, string tabindex = "100", bool visible = true)
        {
            //Visibility defaults to true.  If false is passed in, set the style to hidden
            string style = "";
            if (visible == false)
            {
                style = "style=\"display:none\"";
            }
            return String.Format("<button id=\"" + id + "\" type=\"button\" tabindex=" + tabindex + " class=\"k-button\" " + style + " onclick=\"" + JAction + "\">" + Label + "</button>");
        }

        #endregion

        #region Action Button

        public static string ActionButton(string Label, string Action, string Controller, long RouteValue, string tabindex = "100")
        {

            return String.Format("<button id=\"btnReset\" name=\"btnReset\" class=\"k-button \" type=\"button\" tabindex=" + tabindex + " onclick=\"location.href='/" + Controller + "/" + Action + "/" + RouteValue + "'\">" + Label + "</button>");

        }

        public static string ActionButton(string Label, string Action, string Controller, string tabindex = "100")
        {

            return String.Format("<button id=\"btnReset\" name=\"btnReset\" class=\"k-button \" type=\"button\" tabindex=" + tabindex + " onclick=\"location.href='/" + Controller + "/" + Action + "'\">" + Label + "</button>");

        }

        public static string ActionButtonURL(string Label, string URL, string tabindex = "100")
        {

            return String.Format("<button id=\"btnReset\" name=\"btnReset\" class=\"k-button \" type=\"button\" tabindex=" + tabindex + " onclick=\"location.href='" + URL + "'\">" + Label + "</button>");

        }

        public static string ActionButtonNewWindow(string Label, string Action, string Controller, long RouteValue, string tabindex = "100")
        {
            return String.Format("<button id=\"btnReset\" name=\"btnReset\" class=\"k-button \"  type=\"button\" target=\"_blank\"  tabindex=" + tabindex + " onclick=\"var lucky=window.open('','_blank');lucky.location='/" + Controller + "/" + Action + "/" + RouteValue.ToString() + "'\">" + Label + "</button>");
        }

        public static string ActionLinkNewWindow(string Label, string Action, string Controller, long RouteValue, string tabindex = "100")
        {
            return String.Format("<a  target=\"_blank\"  tabindex=" + tabindex + " href='/" + Controller + "/" + Action + "/" + RouteValue.ToString() + "'\">" + Label + "</a>");
        }

        public static string ActionButton(string id, string Label, string Action, string Controller, string tabindex = "100")
        {

            return String.Format("<button id=\"" + id + "\"  name=\"" + id + "\" class=\"k-button \" type=\"button\" tabindex=" + tabindex + " onclick=\"location.href='/" + Controller + "/" + Action + "'\">" + Label + "</button>");

        }

        public static string ActionLink(string Label, string Action, string Controller, string tabindex = "100")
        {

            return String.Format("<a href='#' tabindex=" + tabindex + " onclick=\"location.href='/" + Controller + "/" + Action + "'\"><font color='black'> " + Label + "</font></a>");

        }

        public static string LogOnActionLink(string Label, string Action, string Controller, string tabindex = "100")
        {

            return String.Format("<a href='#' tabindex=" + tabindex + " onclick=\"location.href='/" + Controller + "/" + Action + "'\"> " + Label + "</a>");

        }

        #endregion

        #region Text Boxes

        public static string TextBox(string id)
        {
            return String.Format("<input id=\"" + id + "\" name=\"" + id + "\"  style=\"width: 200px\" type=\"text\" value=\"\" />");
            // <% = Html.TextBox("txtclientTasCode", null, new { @style = "width: 200px" })%>

        }

        public static string TextBox(string id, string value)
        {
            return String.Format("<input id=\"" + id + "\" name=\"" + id + "\"  style=\"width: 200px\" type=\"text\" value=\"" + value + "\" />");
            // <% = Html.TextBox("txtclientTasCode", ViewData["ClientTasCode"].ToString(), new { @style = "width: 200px" })%>
        }

        public static string TextBox(string id, int width)
        {
            return String.Format("<input id=\"" + id + "\" name=\"" + id + "\"  style=\"width: " + width.ToString() + "px\" type=\"text\" value=\"\" />");
            // <% = Html.TextBox("txtclientTasCode", null, new { @style = "width: 200px" })%>
        }

        public static string TextBox(string id, int width, int height)
        {
            return String.Format("<input id=\"" + id + "\" name=\"" + id + "\" style=\"width: " + width.ToString() + "px; height: " + height.ToString() + "\" type=\"text\" value=\"\" />");
        }

        public static string TextBox(string id, string value, int width)
        {
            return String.Format("<input id=\"" + id + "\" name=\"" + id + "\" style=\"width: " + width.ToString() + "px\" type=\"text\" value=\"" + value + "\" />");
        }

        public static string TextBox(string id, string value, int width, int height)
        {
            return String.Format("<input id=\"" + id + "\" name=\"" + id + "\" style=\"width: " + width.ToString() + "px; height: " + height.ToString() + "\" type=\"text\" value=\"" + value + "\" />");
        }

        #endregion

        #region Checkboxes

        /// <summary>
        /// it will return two controls checkbox and hidden value
        /// </summary>
        /// <param name="id">Control/Field ID without chk , it will add it automatically for you</param>
        /// <param name="Checked"></param>
        /// <param name="JqueryFuncName">it should be in form of >>> Funcname();</param>
        /// <returns></returns>
        public static string CheckBox(string id, bool? Checked, string JqueryFuncName = "", bool Disabled = false)
        {
            var IsChecked = Checked.HasValue ? Checked.Value : false;
            if (IsChecked)
            {
                if (JqueryFuncName != "")
                {
                    if (Disabled)
                    {
                        return String.Format(" <input type='checkbox' disabled='disabled' id='chk" + id + "' name='chk" + id + "' checked='checked' onchange='" + JqueryFuncName + "' />  <input type='hidden' id='chk_" + id + "' name='chk_" + id + "' value='true' />");
                    }
                    else
                    {
                        return String.Format(" <input type='checkbox' id='chk" + id + "' name='chk" + id + "' checked='checked' onchange='" + JqueryFuncName + "' />  <input type='hidden' id='chk_" + id + "' name='chk_" + id + "' value='true' />");
                    }
                }
                else
                {
                    if (Disabled)
                    {
                        return String.Format(" <input type='checkbox' disabled='disabled' id='chk" + id + "' name='chk" + id + "' checked='checked' />  <input type='hidden' id='chk_" + id + "' name='chk_" + id + "' value='true' />");
                    }
                    else
                    {
                        return String.Format(" <input type='checkbox' id='chk" + id + "' name='chk" + id + "' checked='checked' />  <input type='hidden' id='chk_" + id + "' name='chk_" + id + "' value='true' />");
                    }
                }
            }
            else
            {
                if (JqueryFuncName != "")
                {
                    if (Disabled)
                    {
                        return String.Format(" <input type='checkbox' disabled='disabled' id='chk" + id + "' name='chk" + id + "' onchange='" + JqueryFuncName + "' />  <input type='hidden' id='chk_" + id + "' name='chk_" + id + "' value='false' />");
                    }
                    else
                    {
                        return String.Format(" <input type='checkbox' id='chk" + id + "' name='chk" + id + "' onchange='" + JqueryFuncName + "' />  <input type='hidden' id='chk_" + id + "' name='chk_" + id + "' value='false' />");
                    }
                }
                else
                {
                    if (Disabled)
                    {
                        return String.Format(" <input type='checkbox' disabled='disabled' id='chk" + id + "' name='chk" + id + "'/>  <input type='hidden' id='chk_" + id + "' name='chk_" + id + "' value='false' />");
                    }
                    else
                    {
                        return String.Format(" <input type='checkbox' id='chk" + id + "' name='chk" + id + "'/>  <input type='hidden' id='chk_" + id + "' name='chk_" + id + "' value='false' />");
                    }
                }

            }
        }

        /// <summary>
        /// It will Return CheckBox to Display on Screen and hidden Value to Pass to the Controller, The Checkbox will have prefix of 'chk' and hidden Value will have the same name as Controller required
        /// </summary>
        /// <param name="id">Property Name</param>
        /// <param name="Checked"></param>
        /// <param name="JqueryFuncName"></param>
        /// <param name="Disabled"></param>
        /// <returns></returns>
        public static string CheckBox_HiddenField(string id, bool? Checked)
        {
            bool IsChecked = Checked.HasValue ? Checked.Value : false;


            return String.Format("<input type='checkbox' id='chk_" + id + "' name='chk_" + id + "'  " + (IsChecked ? "checked='checked'" : "") + "  onchange='CheckBox_" + id + "_OnChange();'  />"
                                      + "  <input type='hidden' id='" + id + "' name='" + id + "' " + (IsChecked ? "value='true'" : "value='false'") + "  />"
                                      + "<script type='text/javascript'>"
                                      + " function CheckBox_" + id + "_OnChange() {{ "
                                      + " if ($('#chk_" + id + "').is(':checked')) {{"
                // + " alert(' " + id + " it is Checked');"
                                      + " $('#" + id + "').val('true'); }}"
                                      + " else {{ "
                // + " alert('" + id + " it is UN-Checked');"
                                      + "  $('#" + id + "').val('false'); }}"
                                      + " }}</script>");
        }


        public static string CheckBox_HiddenField(string id, bool? Checked, bool? Disabled)
        {
            bool IsChecked = Checked.HasValue ? Checked.Value : false;
            bool IsDisabled = Disabled.HasValue ? Disabled.Value : false;

            return String.Format("<input type='checkbox' id='chk_" + id + "' name='chk_" + id + "'  " + (IsChecked ? "checked='checked'" : "") + "  onchange='CheckBox_" + id + "_OnChange();' " + (IsDisabled ? "disabled='disabled'" : "") + "  />"
                                    + "  <input type='hidden' id='" + id + "' name='" + id + "' " + (IsChecked ? "value='true'" : "value='false'") + "  />"
                                    + "<script type='text/javascript'>"
                                    + " function CheckBox_" + id + "_OnChange() {{ "
                                    + " if ($('#chk_" + id + "').is(':checked')) {{"
                // + " alert(' " + id + " it is Checked');"
                                    + " $('#" + id + "').val('true'); }}"
                                    + " else {{ "
                // + " alert('" + id + " it is UN-Checked');"
                                    + "  $('#" + id + "').val('false'); }}"
                                    + " }}</script>");
        }

        #endregion

        #region YesNo DropDownlist Kendo

        public static SelectList CreateCustomSelectList(Dictionary<string, string> Text_Value, object SelectedValue)
        {
            List<SelectListItem> customSelectList = new List<SelectListItem>();

            foreach (var item in Text_Value)
            {
                SelectListItem itrYES = new SelectListItem()
                {
                    Text = item.Key,
                    Value = item.Value,
                    Selected = false
                };
                customSelectList.Add(itrYES);
            }
            return new SelectList(customSelectList, "Value", "Text", SelectedValue);
        }

        #endregion


    }

}
