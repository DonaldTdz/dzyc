using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DHQR.UI.DHQRCommon
{
    public static class HtmlExtendHelper
    {
        /// <summary>
        /// 生成下拉列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="SelectListName">下拉列表的Name值</param>
        /// <param name="optionLabel">默认信息</param>  
        /// <returns></returns>
        public static string DropDownListExtend(this HtmlHelper helper, string SelectListName,
                                          IEnumerable<SelectListItem> SelectItems,
            string optionLabel)
        {
            var sb = new StringBuilder();
            sb.Append("<select");

            if (SelectListName.Trim() != "")
            {
                sb.Append(" name=\"" + SelectListName + "\"" + " id=\"" + SelectListName + "\"");
            }
            else
            {
                return "";
            }
            sb.Append(">");

            if (!string.IsNullOrEmpty(optionLabel))
            {
                sb.Append("<option value=\"" + "\">" + optionLabel + "</option>");
            }
            foreach (SelectListItem item in SelectItems)
            {
                if (item.Selected)
                {
                    sb.Append("<option value=\"" + item.Value + "\" selected=\"selected\">" + item.Text + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
                }
            }

            sb.Append("</select>");

            return sb.ToString();

        }

        /// <summary>
        /// 生成下拉列表
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="SelectListName">下拉列表的Name值</param>
        /// <param name="optionLabel">默认信息</param>  
        /// <returns></returns>
        public static string DropDownListExtend(this HtmlHelper helper, string SelectListName, string SelectListId,
                                          IEnumerable<SelectListItem> SelectItems,
            string optionLabel)
        {
            var sb = new StringBuilder();
            sb.Append("<select");

            if (SelectListName.Trim() != "")
            {
                sb.Append(" name=\"" + SelectListName + "\"" + " id=\"" + SelectListId + "\"");
            }
            else
            {
                return "";
            }
            sb.Append(">");

            if (!string.IsNullOrEmpty(optionLabel))
            {
                sb.Append("<option value=\"" + "\">" + optionLabel + "</option>");
            }
            foreach (SelectListItem item in SelectItems)
            {
                if (item.Selected)
                {
                    sb.Append("<option value=\"" + item.Value + "\" selected=\"selected\">" + item.Text + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
                }
            }

            sb.Append("</select>");

            return sb.ToString();

        }


        /// <summary>
        /// 生成下拉列表
        /// </summary> 
        public static string DropDownListExtend(this HtmlHelper helper, string SelectListName,
                                          IEnumerable<SelectListItem> SelectItems,
            string optionLabel, string htmlAttr)
        {
            var sb = new StringBuilder();
            sb.Append("<select");

            if (SelectListName.Trim() != "")
            {
                sb.Append(" name=\"" + SelectListName + "\"" + " id=\"" + SelectListName + "\"");
            }
            else
            {
                return "";
            }


            if (!string.IsNullOrEmpty(htmlAttr))
            {
                sb.Append(" " + htmlAttr + " ");
            }
            sb.Append(">");
            if (!string.IsNullOrEmpty(optionLabel))
            {
                sb.Append("<option value=\"" + "\">" + optionLabel + "</option>");
            }
            foreach (SelectListItem item in SelectItems)
            {
                if (item.Selected)
                {
                    sb.Append("<option value=\"" + item.Value + "\" selected=\"selected\">" + item.Text + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
                }
            }

            sb.Append("</select>");

            return sb.ToString();

        }


        /// <summary>
        /// 生成下拉列表
        /// </summary> 
        public static string DropDownListExtend(this HtmlHelper helper, string SelectListName, string SelectListId,
                                          IEnumerable<SelectListItem> SelectItems,
            string optionLabel, string htmlAttr)
        {
            var sb = new StringBuilder();
            sb.Append("<select");

            if (SelectListName.Trim() != "")
            {
                sb.Append(" name=\"" + SelectListName + "\"" + " id=\"" + SelectListId + "\"");
            }
            else
            {
                return "";
            }


            if (!string.IsNullOrEmpty(htmlAttr))
            {
                sb.Append(" " + htmlAttr + " ");
            }
            sb.Append(">");
            if (!string.IsNullOrEmpty(optionLabel))
            {
                sb.Append("<option value=\"" + "\">" + optionLabel + "</option>");
            }
            foreach (SelectListItem item in SelectItems)
            {
                if (item.Selected)
                {
                    sb.Append("<option value=\"" + item.Value + "\" selected=\"selected\">" + item.Text + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + item.Value + "\">" + item.Text + "</option>");
                }
            }

            sb.Append("</select>");

            return sb.ToString();

        }


    }
}