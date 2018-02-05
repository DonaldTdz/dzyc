using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace DHQR.UI.Models
{
    /// <summary>
    /// HttpRequestUtil
    /// </summary>
    public class HttpRequestUtil
    {
        /// Http上传文件 
        public static string HttpUploadFile(string url, string path)
        {
            // 设置参数 
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X");
            // 随机分隔线 
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            int pos = path.LastIndexOf("\\");
            string fileName = path.Substring(pos + 1);
            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length]; fs.Read(bArr, 0, bArr.Length);
            fs.Close();
            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();
            //发送请求并获取相应回应数据 
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求 
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页(html)代码
            string content = sr.ReadToEnd();
            return content;
        }


        // <summary>
        /// 获取Json字符串某节点的值
        /// </summary>
         public static string GetJsonValue(string jsonStr, string key)
         {
             string result = string.Empty;
             if (!string.IsNullOrEmpty(jsonStr))
             {
                 key = "\"" + key.Trim('"') + "\"";
                 int index = jsonStr.IndexOf(key) + key.Length + 1;
                 if (index > key.Length + 1)
                 {
                     //先截逗号，若是最后一个，截“｝”号，取最小值
                     int end = jsonStr.IndexOf(',', index);
                     if (end == -1)
                     {
                         end = jsonStr.IndexOf('}', index);
                     }

                     result = jsonStr.Substring(index, end - index);
                     result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                 }
             }
             return result;
         }
        
    }
}