using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace DHQR.UI.Models
{
    public class WechatImageApi
    {
        /// <summary>
        /// 图片
        /// </summary>
        /// <param name="fromImgUrl">远程图片地址</param>
        /// <returns>收集后在本地服务器的相对路径</returns>
        public static string AddVImage(string fromImgUrl, Guid User_ID)
        {
            /*
            #region 获取原图
            WebRequest wReq = WebRequest.Create(fromImgUrl);
            HttpWebResponse wResp = (HttpWebResponse)wReq.GetResponse();
            Stream s = wResp.GetResponseStream();
            System.Drawing.Image fsImage = System.Drawing.Image.FromStream(s);
            System.Drawing.Image fromImg = new System.Drawing.Bitmap(fsImage);
            WechatImageModelService imagesdal = new WechatImageModelService();

            WechatImageModel dtimage = imagesdal.GetByUserId(User_ID);

            if (dtimage.ImagesSet == 1)
            {
                fsImage.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                fromImg.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);//图片旋转90度
            }

            #endregion

            Random ran = new Random();
            string jVimgUrl = "/remoteimages/" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ran.Next(0, 99) + ".jpg";
            fromImg.Save(HttpContext.Current.Server.MapPath(jVimgUrl), System.Drawing.Imaging.ImageFormat.Jpeg);
            fsImage.Dispose();
            fromImg.Dispose();
            return jVimgUrl;
             * */

            return null;
        }

    }
}