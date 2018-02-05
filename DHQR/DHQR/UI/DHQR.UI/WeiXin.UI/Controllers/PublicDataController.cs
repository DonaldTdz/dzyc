using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.DHQRCommon;
using DHQR.UI.Models;
using Common.Base;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using DHQR.UI.Common;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 公共控制层
    /// </summary>
    public class PublicDataController : BaseController
    {

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="actionTarget"></param>
        /// <param name="expData"></param>
        /// <param name="bs"></param>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public void VoidExportToExcel(DataTable expData, HttpResponseBase bs)
        {
            var grid = new GridView { DataSource = expData };
            grid.DataBind();
            bs.ClearContent();
            bs.ContentType = "application/excel";
            bs.Charset = "GB2312";
            bs.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            bs.AddHeader("content-disposition", "attachment; filename=Export.xls");

            var sw = new StringWriter();
            var htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);
            bs.Write(sw.ToString());
            //bs.End();
        }

        /// <summary>
        /// 微信用户导出Excel
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult ExportWeiXinUsers(string rutId, string fileName)
        {
            WeiXinUserModelService modelService = new WeiXinUserModelService();
            var listEntity = modelService.GetByRutId(rutId);
                

            string logFolder;
            var file = string.Format("/Excel/" + fileName + "Excel");
            if (null != HttpContext.ApplicationInstance)
                logFolder = HttpContext.ApplicationInstance.Request.PhysicalApplicationPath + file;
            else
                logFolder = AppDomain.CurrentDomain.BaseDirectory + file;


            var relativePath = Path.Combine(file, string.Format(fileName + "{0:yyyyMMddHHmmss}.xls", DateTime.Now));
            var path = Server.MapPath(relativePath);
            if (System.IO.Directory.Exists(logFolder) == false)//如果不存在就创建file文件夹 
            {
                System.IO.Directory.CreateDirectory(logFolder);
            }

            ExcelHelper.Export(listEntity, fileName, path);
            return JsonForDoHandle(new DoHandle { IsSuccessful = true, OperateMsg = relativePath });
        }

    }
}
