﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using DHQR.DataAccess.Entities;
using Common.Base;

namespace DHQR.UI.Models
{
    public class ConmmonApi
    {
        //创建菜单接口
        public static string setMenuUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";

        //模板消息发送接口
        public static string setTemplateUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";


        //获取微信凭证access_token的接口
        public static string getAccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        //浏览器用于 HTTP 请求的用户代理头的值 
        public static string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.172 Safari/537.22";

        WeiXinMenuModelService caidandal = new WeiXinMenuModelService();//菜单
        WeiXinAppModelService wechatdal = new WeiXinAppModelService();//微信配置
        JavaScriptSerializer Jss = new JavaScriptSerializer();

        public ConmmonApi()
        { }

        #region 菜单Json数据
        public string GetMenuJson(Guid weiXinAppId)
        {
            string menuJson = "";

            IList<WeiXinMenuModel> dtMenu = caidandal.GetListByWeiXinAppId(weiXinAppId);
            var oneLevels = dtMenu.Where(f => f.ParentId == null).ToList();//一级菜单
            if (dtMenu.Count > 0)
            {
                int i = 1;
                menuJson = "{\"button\":[";

                foreach (var Row in oneLevels)
                {
                    var secondRows = dtMenu.Where(f => f.ParentId == Row.Id).ToList();//二级菜单

                    if (secondRows.Count > 0)
                    {
                        menuJson += "{";
                        menuJson += "\"name\":\"" + Row.Name + "\",";
                        menuJson += "\"sub_button\":[";

                        #region 子菜单
                        int j = 1;
                        foreach (var secondRow in secondRows)
                        {
                            menuJson += "{";
                            menuJson += "\"type\":\"" + secondRow.WxType + "\",";
                            menuJson += "\"name\":\"" + secondRow.Name.ToString() + "\",";
                            menuJson += "\"key\":\"" + secondRow.Key + "\",";
                            menuJson += "\"url\":\"" + secondRow.Url + "\"";
                            menuJson += "}";

                            if (secondRows.Count != j)
                            {
                                menuJson += ",";
                            }

                            j++;
                        }
                        #endregion 子菜单

                        menuJson += " ]}";
                    }
                    else
                    {
                        menuJson += "{";
                        menuJson += "\"type\":\"" + Row.WxType + "\",";
                        menuJson += "\"name\":\"" + Row.Name + "\",";
                        menuJson += "\"key\":\"" + Row.Key + "\",";
                        menuJson += "\"url\":\"" + Row.Url + "\"";
                        menuJson += "}";
                    }

                    if (oneLevels.Count != i)
                    {
                        menuJson += ",";
                    }

                    i++;
                }

                menuJson += "]}";
            }

            return menuJson;
        }
        #endregion 菜单Json数据

        #region 发布菜单
        /// <summary>
        /// 发布自定义菜单
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string SetMenu(Guid weiXinAppId, out string msg)
        {
            string respText = "";
            string menuJson = GetMenuJson(weiXinAppId);
            string accessToken = GetAccessToken(weiXinAppId);
            string url = string.Format(setMenuUrl, accessToken);
            byte[] requestBytes = Encoding.UTF8.GetBytes(menuJson);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestBytes.Length;

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(requestBytes, 0, requestBytes.Length);
                reqStream.Flush();
                reqStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream resStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(resStream, Encoding.Default);
                respText = reader.ReadToEnd();
                resStream.Close();
            }

            Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(respText);
            msg = respDic["errmsg"].ToString();
            return respDic["errcode"].ToString();
        }
        #endregion 发布菜单

        #region 获取微信凭证


        public string GetAccessToken(Guid weiXinAppId)
        {
            string accessToken = "";

            WeiXinAppModel dtwecaht = wechatdal.GetByKey(weiXinAppId);

            if (dtwecaht != null)
            {
                if (dtwecaht.access_token != null && dtwecaht.next_gettime > DateTime.Now)
                {
                    accessToken = dtwecaht.access_token;
                }
                else
                {
                    accessToken = GetNewAccessToken(weiXinAppId);
                }
            }

            return accessToken;
        }


        /// <summary>
        /// 重新获取AccessToken
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <returns></returns>
        public string GetNewAccessToken(Guid weiXinAppId)
        {
            WeiXinAppModel dtwecaht = wechatdal.GetByKey(weiXinAppId);
            string respText = "";
            string wechat_appid = dtwecaht.AppId;
            string wechat_appsecret = dtwecaht.AppSecret;
            string url = string.Format(getAccessTokenUrl, wechat_appid, wechat_appsecret);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream resStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(resStream, Encoding.Default);
                respText = reader.ReadToEnd();
                resStream.Close();
            }
            Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(respText);
            string accessToken = respDic["access_token"].ToString();
            dtwecaht.access_token = accessToken;
            var expires_in = int.Parse(respDic["expires_in"].ToString());
            dtwecaht.next_gettime = DateTime.Now.AddSeconds(expires_in - 60);
            dtwecaht.expires_in = expires_in;
            DoHandle dohandle;
            wechatdal.Update(dtwecaht, out dohandle);

            return accessToken;
        }

        #endregion 获取微信凭证

        #region  post提交带数据流数据

        public string UploadFileByHttpWebRequest(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (nvc != null)
            {
                foreach (string key in nvc.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            var result = "";
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //成功回传结果
                result = reader2.ReadToEnd();
            }
            catch (Exception ex)
            {
                if (wresp != null)
                    wresp.Close();
            }
            wr = null;
            return result;
        }
        #endregion


    }
}