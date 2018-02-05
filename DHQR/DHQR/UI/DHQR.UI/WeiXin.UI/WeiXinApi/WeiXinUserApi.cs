using System;
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


namespace DHQR.UI.Models
{
    /// <summary>
    /// 微信用户API
    /// </summary>
    public class WeiXinUserApi
    {
        //查询关注用户列表
        public static string queryUserListUrl1 = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}";
        public static string queryUserListUrl2 = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";


        //查询用户基础信息接口
        public static string queryUserInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";

        //设置用户备注名接口
        public static string setUserRemarkUrl = "https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}";


        //获取微信凭证access_token的接口
        public static string getAccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        //浏览器用于 HTTP 请求的用户代理头的值 
        public static string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.172 Safari/537.22";

        WeiXinAppModelService wechatdal = new WeiXinAppModelService();//微信配置
        JavaScriptSerializer Jss = new JavaScriptSerializer();

        public WeiXinUserApi()
        { }

        #region 获取关注用户列表

        /// <summary>
        /// 获取关注用户列表
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="next_openid"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<string> GetSubUserList(Guid weiXinAppId, string next_openid,out string next_next_openid)
        {
            ConmmonApi commonApi = new ConmmonApi();

            string respText = "";
            string accessToken = commonApi.GetAccessToken(weiXinAppId);
            string url = "";
            if (string.IsNullOrEmpty(next_openid))
            {
                url = string.Format(queryUserListUrl1, accessToken);
            }
            else
            {
                url = string.Format(queryUserListUrl2, accessToken, next_openid);
            }


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            request.ContentType = "application/json; encoding=utf-8";

            using (Stream resStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
                respText = reader.ReadToEnd();
                resStream.Close();
            }
            var subUserLst = Jss.Deserialize<WxSubUserList>(respText);
            next_next_openid = subUserLst.next_openid;
            return subUserLst.data.openid;

        }

        #endregion


        #region 获取用户基础信息

        /// <summary>
        /// 获取微信用户基础信息
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <returns></returns>
        public IList<WeiXinUserInfoModel> QueryWeiXinUserInfo(Guid weiXinAppId)
        {
            IList<WeiXinUserInfoModel> result = new List<WeiXinUserInfoModel>();
            ConmmonApi commonApi = new ConmmonApi();
            string accessToken = commonApi.GetAccessToken(weiXinAppId);
            string next_next_openid;
            //获取关注用户列表
            IList<string> subUserLst = GetSubUserList(weiXinAppId, null, out next_next_openid);

            foreach (var item in subUserLst)
            {

                string respText = "";
                string url = string.Format(queryUserInfoUrl, accessToken,item);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                request.ContentType = "application/json; encoding=utf-8";

                using (Stream resStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
                    respText = reader.ReadToEnd();
                    resStream.Close();
                }

                var userInfo = Jss.Deserialize<WeiXinUserInfoModel>(respText);
                result.Add(userInfo);
            }
            return result;

        }

        #endregion

        #region 设置用户备注名

        /// <summary>
        /// 设置用户备注名
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="openid"></param>
        /// <param name="remark"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string SetUserRemark(Guid weiXinAppId,string openid,string remark,out string msg)
        {
            ConmmonApi commonApi = new ConmmonApi();
            string accessToken = commonApi.GetAccessToken(weiXinAppId);
            string respText = "";

            string url = string.Format(setUserRemarkUrl, accessToken);
            //string param = "{'openid':'"+openid+"','remark':'"+remark+"'}";
            string param = "{\"openid\":\""+openid+"\",\"remark\":\""+remark+"\"}";
            byte[] requestBytes = Encoding.UTF8.GetBytes(param);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json; encoding=utf-8";
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


        #endregion



    }
}