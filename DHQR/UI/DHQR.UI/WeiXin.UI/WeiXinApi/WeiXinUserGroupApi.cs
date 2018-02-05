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
    /// 微信用户管理API
    /// </summary>
    public class WeiXinUserGroupApi
    {
        //创建分组接口
        public static string createGroupUrl = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";

        //查询所有分组接口
        public static string queryGroupUrl = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}";

        //查询用户分组接口
        public static string queryUserGroupUrl = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}";

        //移动用户分组接口
        public static string updateGroupUrl = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";

        //修改分组名
        public static string updateGroupNameUrl = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}";


        //获取微信凭证access_token的接口
        public static string getAccessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        //浏览器用于 HTTP 请求的用户代理头的值 
        public static string userAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.172 Safari/537.22";

        WeiXinAppModelService wechatdal = new WeiXinAppModelService();//微信配置
        JavaScriptSerializer Jss = new JavaScriptSerializer();

        public WeiXinUserGroupApi()
        { }

        #region 获取所有分组
        /// <summary>
        /// 获取所有分组
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public IList<WxOriginUserGroup> QueryGroup(Guid weiXinAppId, out string msg)
        {
            ConmmonApi commonApi = new ConmmonApi();

            string respText = "";
            string accessToken = commonApi.GetAccessToken(weiXinAppId);
            string url = string.Format(queryGroupUrl, accessToken);


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            request.ContentType = "application/json; encoding=utf-8";

            using (Stream resStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
                respText = reader.ReadToEnd();
                resStream.Close();
            }
           
           msg = "";
           var groupList = Jss.Deserialize<WxOriginUserGroupList>(respText);

           return groupList.groups ;

        }

        #endregion 发布菜单

        #region 移动用户分组
        
        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="openid"></param>
        /// <param name="groupid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string UpdateUserGroup(Guid weiXinAppId, string openid, int groupid, out string msg)
        {
            ConmmonApi commonApi = new ConmmonApi();
            string accessToken = commonApi.GetAccessToken(weiXinAppId);
            string respText = "";
            SetUserGroupParam setParam = new SetUserGroupParam { openid = openid, to_groupid = groupid };
            string url = string.Format(updateGroupUrl, accessToken);
            string param = Jss.Serialize(setParam); //"{\"openid\":\"" + openid + "\",\"to_groupid\":" + groupid + "}";


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

        #region 修改分组名

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="groupid"></param>
        /// <param name="groupname"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string UpdateUserGroup(Guid weiXinAppId, int groupid, string groupname,out string msg)
        {
            ConmmonApi commonApi = new ConmmonApi();
            string accessToken = commonApi.GetAccessToken(weiXinAppId);
            string respText = "";
            string url = string.Format(updateGroupNameUrl, accessToken);
            string param = "{\"group\":{\"id\":" + groupid + ",\"name\":\"" + groupname + "\"}}";


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
                StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
                respText = reader.ReadToEnd();
                resStream.Close();
            }

            Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(respText);
            msg = respDic["errmsg"].ToString();
            return respDic["errcode"].ToString();

        }

        #endregion

        #region 创建分组

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="groupid"></param>
        /// <param name="groupname"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string CreateGroup(Guid weiXinAppId, string groupname, out string msg)
        {
            ConmmonApi commonApi = new ConmmonApi();
            string accessToken = commonApi.GetAccessToken(weiXinAppId);
            string respText = "";
            string url = string.Format(createGroupUrl, accessToken);
            string param = "{\"group\":{\"name\":\"" + groupname + "\"}}";


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
                StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
                respText = reader.ReadToEnd();
                resStream.Close();
            }

            Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(respText);
            if (respDic.Count > 1)
            {
                msg = respDic["errmsg"].ToString();
                return respDic["errcode"].ToString();
            }
            else
            {
                msg = "操作成功";
                return "0";
            }

        }

        #endregion

    }

    public class SetUserGroupParam
    {
        public string openid { get; set; }

        public int to_groupid { get; set; }
    }

}