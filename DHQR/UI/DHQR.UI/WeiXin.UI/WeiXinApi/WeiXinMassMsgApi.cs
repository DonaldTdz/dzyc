using DHQR.DataAccess.Entities;
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
    /// 微信群发接口
    /// </summary>
    public class WeiXinMassMsgApi
    {
        /// 上传媒体返回媒体ID 
        public static string UploadMedia(string access_token, string type, string path)
        {
            // 设置参数 
            string url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", access_token, type);
            return HttpRequestUtil.HttpUploadFile(url, path);
        }



        /// <summary>
        /// 请求Url，发送数据
        /// </summary>
        public static string PostUrl(string url, string postData)
        {
            byte[] data = Encoding.UTF8.GetBytes(postData);

            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            Stream outstream = request.GetRequestStream();
            outstream.Write(data, 0, data.Length);
            outstream.Close();

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }


        /// <summary>
        /// 获取关注者OpenID集合
        /// </summary>
        public static List<string> GetOpenIDs(string access_token)
        {
            List<string> result = new List<string>();

            List<string> openidList = GetOpenIDs(access_token, null);
            result.AddRange(openidList);

            while (openidList.Count > 0)
            {
                openidList = GetOpenIDs(access_token, openidList[openidList.Count - 1]);
                result.AddRange(openidList);
            }

            return result;
        }

        /// <summary>
        /// 获取关注者OpenID集合
        /// </summary>
        public static List<string> GetOpenIDs(string access_token, string next_openid)
        {
            // 设置参数
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", access_token, string.IsNullOrWhiteSpace(next_openid) ? "" : next_openid);
            string returnStr = WeiXinMassMsgApi.RequestUrl(url);
            int count = int.Parse(HttpRequestUtil.GetJsonValue(returnStr, "count"));
            if (count > 0)
            {
                string startFlg = "\"openid\":[";
                int start = returnStr.IndexOf(startFlg) + startFlg.Length;
                int end = returnStr.IndexOf("]", start);
                string openids = returnStr.Substring(start, end - start).Replace("\"", "");
                return openids.Split(',').ToList<string>();
            }
            else
            {
                return new List<string>();
            }
        }

        #region 群发图文信息

        /// 上传图文消息素材返回media_id 
        public static string UploadNews(string access_token, string postData)
        {
            return WeiXinMassMsgApi.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", access_token), postData);
        }

        /// <summary>
        /// 拼接图文消息素材Json字符串 
        /// </summary>
        public static string GetArticlesJsonStr(WeiXinMassMsg master, IList<WeiXinMassMsg> massMsg)
        {
            StringBuilder sbArticlesJson = new StringBuilder();

            sbArticlesJson.Append("{\"articles\":[");
            sbArticlesJson.Append("{");
            sbArticlesJson.Append("\"thumb_media_id\":\"" + master.thumb_media_id + "\",");
            sbArticlesJson.Append("\"author\":\"" + master.author + "\",");
            sbArticlesJson.Append("\"title\":\"" + master.title + "\",");
            sbArticlesJson.Append("\"content_source_url\":\"" + master.content_source_url + "\",");
            sbArticlesJson.Append("\"content\":\"" + master.content + "\",");
            sbArticlesJson.Append("\"digest\":\"" + master.digest + "\",");
            sbArticlesJson.Append("\"show_cover_pic\":\"" + master.show_cover_pic + "\"},");
            int i = 0;
            foreach (var dr in massMsg)
            {
                sbArticlesJson.Append("{");
                sbArticlesJson.Append("\"thumb_media_id\":\"" + dr.thumb_media_id + "\",");
                sbArticlesJson.Append("\"author\":\"" + dr.author + "\",");
                sbArticlesJson.Append("\"title\":\"" + dr.title + "\",");
                sbArticlesJson.Append("\"content_source_url\":\"" + dr.content_source_url + "\",");
                sbArticlesJson.Append("\"content\":\"" + dr.content + "\",");
                sbArticlesJson.Append("\"digest\":\"" + dr.digest + "\",");
                if (i == massMsg.Count - 1)
                {
                    sbArticlesJson.Append("\"show_cover_pic\":\"" + dr.show_cover_pic + "\"}");
                }
                else
                {
                    sbArticlesJson.Append("\"show_cover_pic\":\"" + dr.show_cover_pic + "\"},");
                }
                i++;
            }
            sbArticlesJson.Append("]}");
            return sbArticlesJson.ToString();
        }


        /// <summary>
        /// 图文消息json
        /// </summary>
        public static string CreateNewsJson(string media_id, List<string> openidList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"touser\":[");
            sb.Append(string.Join(",", openidList.ConvertAll<string>(a => "\"" + a + "\"").ToArray()));
            sb.Append("],");
            sb.Append("\"msgtype\":\"mpnews\",");
            sb.Append("\"mpnews\":{\"media_id\":\"" + media_id + "\"}");
            sb.Append("}");
            return sb.ToString();
        }



        /// <summary>
        /// 根据OpenID列表群发
        /// </summary>
        public static string Send(string access_token, string postData)
        {
            return WeiXinMassMsgApi.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", access_token), postData);
        }

        /// <summary>
        /// 根据群组群发
        /// </summary>
        public static string SendByGroup(string access_token,string media_id ,int groupid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"filter\":{");
            sb.Append("\"is_to_all\":"+"false"+",");
            sb.Append("\"group_id\":\"" + groupid + "\"},");
            sb.Append("\"mpnews\":{\"media_id\":\"" + media_id + "\"},");
            sb.Append("\"msgtype\":\"mpnews\"");
            sb.Append("}");
            string postData = sb.ToString();
            return WeiXinMassMsgApi.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", access_token), postData);
        }

        #endregion

        #region 群发文字信息

        /// <summary>
        /// 文字消息json
        /// </summary>
        public static string CreateTextJson(string access_token,string content)
        {
            List<string> openidList = GetOpenIDs(access_token);
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"touser\":[");
            sb.Append(string.Join(",", openidList.ConvertAll<string>(a => "\"" + a + "\"").ToArray()));
            sb.Append("],");
            sb.Append("\"msgtype\":\"text\",");
            sb.Append("\"text\":{\"content\":\"" + content + "\"}");
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 根据openid列表群发文字信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string SendTextMsg(string access_token, string content)
        {
            var postData = CreateTextJson(access_token, content);
            return WeiXinMassMsgApi.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", access_token), postData);

        }

        /// <summary>
        /// 根据群组群发文字信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string SendTextMsg(string access_token, int groupid,string content)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"filter\":{");
            sb.Append("\"is_to_all\":" + "false" + ",");
            sb.Append("\"group_id\":\"" + groupid + "\"},");
            sb.Append("\"text\":{\"content\":\"" + content + "\"},");
            sb.Append("\"msgtype\":\"text\"");
            sb.Append("}");
            string postData = sb.ToString();
            return WeiXinMassMsgApi.PostUrl(string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", access_token), postData);
        }


        #endregion

        /// <summary>
        /// 群发
        /// </summary>
        public string Send()
        {
            return null;
            /*

            string type = Request["type"];
            string data = Request["data"];

            string access_token = AdminUtil.GetAccessToken(this); //获取access_token
            List<string> openidList = WXApi.GetOpenIDs(access_token); //获取关注者OpenID列表
            UserInfo loginUser = AdminUtil.GetLoginUser(this); //当前登录用户 

            string resultMsg = null;

            //发送文本
            if (type == "1")
            {
                resultMsg = WXApi.Send(access_token, WXMsgUtil.CreateTextJson(data, openidList));
            }

            //发送图片
            if (type == "2")
            {
                string path = MapPath(data);
                if (!File.Exists(path))
                {
                    return "{\"code\":0,\"msg\":\"要发送的图片不存在\"}";
                }
                string msg = WXApi.UploadMedia(access_token, "image", path);
                string media_id = Tools.GetJsonValue(msg, "media_id");
                resultMsg = WXApi.Send(access_token, WXMsgUtil.CreateImageJson(media_id, openidList));
            }

            //发送图文消息
            if (type == "3")
            {
                DataTable dt = ImgItemDal.GetImgItemTable(loginUser.OrgID, data);
                string articlesJson = ImgItemDal.GetArticlesJsonStr(this, access_token, dt);
                string newsMsg = WXApi.UploadNews(access_token, articlesJson);
                string newsid = Tools.GetJsonValue(newsMsg, "media_id");
                resultMsg = WXApi.Send(access_token, WXMsgUtil.CreateNewsJson(newsid, openidList));
            }

            //结果处理
            if (!string.IsNullOrWhiteSpace(resultMsg))
            {
                string errcode = Tools.GetJsonValue(resultMsg, "errcode");
                string errmsg = Tools.GetJsonValue(resultMsg, "errmsg");
                if (errcode == "0")
                {
                    return "{\"code\":1,\"msg\":\"\"}";
                }
                else
                {
                    return "{\"code\":0,\"msg\":\"errcode:"
                        + errcode + ", errmsg:"
                        + errmsg + "\"}";
                }
            }
            else
            {
                return "{\"code\":0,\"msg\":\"type参数错误\"}";
            }
             */
        }


        #region 请求Url，不发送数据
        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public static string RequestUrl(string url)
        {
            return RequestUrl(url, "POST");
        }
        #endregion

        #region 请求Url，不发送数据
        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public static string RequestUrl(string url, string method)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
        #endregion

    }
}