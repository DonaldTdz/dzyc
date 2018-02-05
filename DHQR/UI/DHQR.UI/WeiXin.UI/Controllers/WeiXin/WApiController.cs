using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using DHQR.UI.Controllers;
using DHQR.UI.DHQRCommon;
using DHQR.UI.Models;


namespace WeiXin.UI.Controllers
{
    public class WApiController : Controller
    {
        //
        // GET: /Api/

        private readonly WeiXinAppModelService wechatService;
        Replyset replyset = new Replyset();

        /// <summary>
        /// 构造函数
        /// </summary>
        public WApiController()
        {
            wechatService = new WeiXinAppModelService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Index()
        {
            RequestMsg();
            //string msg="";
            //Guid wppId = Guid.Parse("0CD98D1B-C476-4A02-B4B9-0F619FCCD36F");
            //ConmmonApi cpi = new ConmmonApi();
            //cpi.ResponseMsgTemplate(wppId, out msg);
        }

        //[HttpGet]
        //public void Index()
        //{
        //    CheckWeChat();
        //}

        #region 接收微信消息

        private void RequestMsg()
        {
            /*
            Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
            byte[] requestByte = new byte[requestStream.Length];
            requestStream.Read(requestByte, 0, (int)requestStream.Length);
            string requestStr = Encoding.UTF8.GetString(requestByte);
             */
            string requestStr = GetXml();

            if (!string.IsNullOrEmpty(requestStr))
            {
                //封装请求类
                XmlDocument requestDocXml = new XmlDocument();
                requestDocXml.LoadXml(requestStr);
                XmlElement rootElement = requestDocXml.DocumentElement;
                XmlNode MsgType = rootElement.SelectSingleNode("MsgType");

                RequestXml requestXml = new RequestXml();
                requestXml.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                requestXml.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                requestXml.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                requestXml.MsgType = MsgType.InnerText;

                switch (requestXml.MsgType)
                {
                    case "text":
                        requestXml.Content = rootElement.SelectSingleNode("Content").InnerText;
                        break;
                    case "image":
                        requestXml.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                        break;
                    case "event":
                        requestXml.Event = rootElement.SelectSingleNode("Event").InnerText;
                        if (requestXml.Event != "subscribe" && requestXml.Event != "MASSSENDJOBFINISH")
                        {
                            requestXml.EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                        }
                        if (requestXml.Event == "MASSSENDJOBFINISH")
                        {
                            requestXml.MsgID = rootElement.SelectSingleNode("MsgID").InnerText;
                            requestXml.msg_desc = rootElement.SelectSingleNode("Status").InnerText;
                            requestXml.TotalCount = rootElement.SelectSingleNode("TotalCount").InnerText;
                            requestXml.FilterCount = rootElement.SelectSingleNode("FilterCount").InnerText;
                            requestXml.SentCount = rootElement.SelectSingleNode("SentCount").InnerText;
                            requestXml.ErrorCount = rootElement.SelectSingleNode("ErrorCount").InnerText;
                        }
                        break;
                }

                string selday = "0";
                int hh = selday == "0" ? 60 : int.Parse(selday) * 24 * 60;
                CookieHelper.WriteCookie("WeChatFrom", "ToUserName", requestXml.ToUserName, hh);
                CookieHelper.WriteCookie("WeChatFrom", "FromUserName", requestXml.FromUserName, hh);

                //回复消息
                ResponseMsg(requestXml);
            }

        }
        #endregion 接收微信消息

        #region 验证微信API接口
        /// <summary>
        /// 验证微信API接口
        /// </summary>
        private void CheckWeChat()
        {
            string echoStr = Request.QueryString["echoStr"];

            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    Response.Write(echoStr);
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// <returns></returns>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        private bool CheckSignature()
        {
           
            string WeChat_Token = "";
            string WeChat_Key = "d1e21850-4f25-46ff-a4ff-e7883f6dea98";

            //获取微信公众号信息
            WeiXinAppModel dtWeChat = wechatService.GetByWeiXinKey(WeChat_Key);

            if (dtWeChat!=null)
            {
                WeChat_Token = dtWeChat.Token;
            }

            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            string[] ArrTmp = { WeChat_Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
        #endregion 验证微信API接口

        #region 回复消息(微信信息返回)

        /// <summary>
        /// 回复消息(微信信息返回)
        /// </summary>
        /// <param name="weixinXML"></param>
        private void ResponseMsg(RequestXml requestXml)
        {
            string resXml = "";
            string WeChat_Key = "d1e21850-4f25-46ff-a4ff-e7883f6dea98";

            try
            {
                //获取微信公众号信息
                WeiXinAppModel dtWeChat = wechatService.GetByWeiXinKey(WeChat_Key);
                if (dtWeChat!=null)
                {

                    replyset.User_ID = dtWeChat.UserId;
                    replyset.WeChat_ID = dtWeChat.WeiXinKey;
                    replyset.WeChat_Type = dtWeChat.WeiXinType.ToString();
                    replyset.WeChat_Name = dtWeChat.Name;


                    switch (requestXml.MsgType)
                    {
                        case "text":
                            resXml = replyset.GetKeyword(requestXml.FromUserName, requestXml.ToUserName, requestXml.Content, dtWeChat.Id, WeChat_Key);
                            break;
                        case "image":
                            resXml = replyset.GetImageReply(requestXml.FromUserName, requestXml.ToUserName, requestXml.PicUrl, dtWeChat.Id);
                            break;
                        case "event":
                            switch (requestXml.Event)
                            {
                                case "subscribe":
                                    resXml = replyset.GetSubscribe(requestXml.FromUserName, requestXml.ToUserName, dtWeChat.Id, WeChat_Key);
                                    break;
                                case "CLICK":
                                    resXml = replyset.GetMenuClick(requestXml.FromUserName, requestXml.ToUserName, requestXml.EventKey, dtWeChat.Id, WeChat_Key);
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Writebug("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());
            }

            Response.Write(resXml);
            Response.End();
        }

        #endregion 回复消息(微信信息返回)

        #region 通用方法
        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        /// <returns></returns>
        public bool Writebug(string str)
        {
            try
            {
                FileStream FileStream = new FileStream(Server.MapPath("/wxbugLog.txt"), FileMode.Append);
                StreamWriter StreamWriter = new StreamWriter(FileStream);
                //开始写入
                StreamWriter.WriteLine(str);
                //清空缓冲区
                StreamWriter.Flush();
                //关闭流
                StreamWriter.Close();
                StreamWriter.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion 通用方法

        /// <summary>
        /// 模拟微信消息
        /// </summary>
        /// <returns></returns>
        public string GetXml()
        {
            //string data = "<xml><ToUserName><![CDATA[toUser]]></ToUserName><FromUserName><![CDATA[FromUser]]></FromUserName>";
            //data = data + "<CreateTime>123456789</CreateTime><MsgType><![CDATA[event]]></MsgType><Event><![CDATA[MASSSENDJOBFINISH]]></Event>";
            //data = data + "<MsgID>1988</MsgID><Status><![CDATA[sendsuccess]]></Status><TotalCount>100</TotalCount><FilterCount>80</FilterCount>";
            //data = data + "<SentCount>75</SentCount><ErrorCount>5</ErrorCount>";
            //data = data + "</xml>";
            string data = "<xml><ToUserName><![CDATA[gh_bb4d48e9a7dc]]></ToUserName><FromUserName>";
            data = data + "<![CDATA[oWK26syW_VXQKB-fRqtRaG-3PJcQ]]></FromUserName>";
            data = data + "<CreateTime>1434540629</CreateTime><MsgType><![CDATA[event]]></MsgType>";
            data = data + "<Event><![CDATA[subscribe]]></Event><EventKey><![CDATA[]]></EventKey></xml>";
            return data;
        }
    }

}
