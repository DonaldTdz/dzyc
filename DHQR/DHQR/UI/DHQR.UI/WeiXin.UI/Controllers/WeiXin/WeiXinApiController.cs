using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using DHQR.DataAccess.Entities;
using DHQR.UI.Models;
using System.Web.Security;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    public class WeiXinApiController : Controller
    {

        private readonly WeiXinAppModelService wechatService;
        Replyset replyset = new Replyset();

        /// <summary>
        /// 构造函数
        /// </summary>
        public WeiXinApiController()
        {
            wechatService = new WeiXinAppModelService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
       [HttpPost]
        public void Index(string key)
        {
            RequestMsg();
        }

        [HttpGet]
        public void Index()
        {
            CheckWeChat();
        }

        #region 接收微信消息

        private void RequestMsg()
        {
            Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
            byte[] requestByte = new byte[requestStream.Length];
            requestStream.Read(requestByte, 0, (int)requestStream.Length);
            string requestStr = Encoding.UTF8.GetString(requestByte);
            //Writebug(requestStr);
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
                            SetWeiXinMassMsgHist(requestXml);
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
            string WeChat_Key = Request.QueryString["key"];

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
            string WeChat_Key = Request.QueryString["key"];

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
                            //ResponseMsgTemplate();
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

        #region 群发事件操作

        /// <summary>
        /// 修改群发历史信息
        /// </summary>
        /// <param name="reqtXml"></param>
        public void SetWeiXinMassMsgHist(RequestXml reqtXml)
        {
            WeiXinMassMsgHistModelService modelService = new WeiXinMassMsgHistModelService();
            WeiXinMassMsgHistModel model = new WeiXinMassMsgHistModel 
            {
                msg_id=reqtXml.MsgID,
                msg_desc=reqtXml.msg_desc,
                TotalCount=int.Parse(reqtXml.TotalCount),
                FilterCount = int.Parse(reqtXml.FilterCount),
                SentCount = int.Parse(reqtXml.SentCount),
                ErrorCount = int.Parse(reqtXml.ErrorCount),
            };
            modelService.SetWeiXinMassMsgHist(model);
        }

        #endregion

        #region 模板消息推送

        /// <summary>
        /// 模板消息推送
        /// </summary>
        public void ResponseMsgTemplate()
        {
           // string msg = "";
           // Guid wppId = Guid.Parse("0CD98D1B-C476-4A02-B4B9-0F619FCCD36F");
           // ConmmonApi cpi = new ConmmonApi();
           //var ss= cpi.ResponseMsgTemplate(wppId, out msg);
           // Writebug(msg+":"+ss);
        }

        #endregion

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



        #region OAuth2.0 授权

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        [HttpPost]
        public void OAuthIndex(string key)
        {
            try
            {
                OAuthRedirectUri ot = new OAuthRedirectUri();
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string Code = Request.QueryString["code"].ToString();
                    //获得Token  
                    OAuth_Token Model = ot.Get_token(Code);
                    //Response.Write(Model.access_token);  
                    OAuthUser OAuthUser_Model = ot.Get_UserInfo(Model.access_token, Model.openid);
                    Response.Write("用户OPENID:" + OAuthUser_Model.openid + "<br>用户昵称:" + OAuthUser_Model.nickname + "<br>性别:" + OAuthUser_Model.sex + "<br>所在省:" + OAuthUser_Model.province + "<br>所在市:" + OAuthUser_Model.city + "<br>所在国家:" + OAuthUser_Model.country + "<br>头像地址:" + OAuthUser_Model.headimgurl + "<br>用户特权信息:" + OAuthUser_Model.privilege);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Writebug("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        [HttpGet]
        public void OAuthIndex()
        {
            try
            {
                OAuthRedirectUri ot = new OAuthRedirectUri();
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string Code = Request.QueryString["code"].ToString();
                    //获得Token  
                    OAuth_Token Model = ot.Get_token(Code);
                    //Response.Write(Model.access_token);  
                    OAuthUser OAuthUser_Model = ot.Get_UserInfo(Model.access_token, Model.openid);
                    Response.Write("用户OPENID:" + OAuthUser_Model.openid + "<br>用户昵称:" + OAuthUser_Model.nickname + "<br>性别:" + OAuthUser_Model.sex + "<br>所在省:" + OAuthUser_Model.province + "<br>所在市:" + OAuthUser_Model.city + "<br>所在国家:" + OAuthUser_Model.country + "<br>头像地址:" + OAuthUser_Model.headimgurl + "<br>用户特权信息:" + OAuthUser_Model.privilege);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Writebug("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());
            }
        }


        #endregion

    }

    #region 微信请求类
    /// <summary>
    /// 微信请求类
    /// </summary>
    public class RequestXml
    {
        private string toUserName;
        /// <summary>
        /// 消息接收方微信号
        /// </summary>
        public string ToUserName
        {
            get { return toUserName; }
            set { toUserName = value; }
        }

        private string fromUserName;
        /// <summary>
        /// 消息发送方微信号
        /// </summary>
        public string FromUserName
        {
            get { return fromUserName; }
            set { fromUserName = value; }
        }

        private string createTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private string msgType;
        /// <summary>
        /// 信息类型 地理位置:location,文本消息:text,消息类型:image
        /// </summary>
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }

        private string content;
        /// <summary>
        /// 信息内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private string location_X;
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X
        {
            get { return location_X; }
            set { location_X = value; }
        }

        private string location_Y;
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y
        {
            get { return location_Y; }
            set { location_Y = value; }
        }

        private string scale;
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private string label;
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        private string picUrl;
        /// <summary>
        /// 图片链接，开发者可以用HTTP GET获取
        /// </summary>
        public string PicUrl
        {
            get { return picUrl; }
            set { picUrl = value; }
        }

        private string _event;
        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)、CLICK(自定义菜单点击事件) 
        /// </summary>
        public string Event
        {
            get { return _event; }
            set { _event = value; }
        }

        private string _eventKey;
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应 
        /// </summary>
        public string EventKey
        {
            get { return _eventKey; }
            set { _eventKey = value; }
        }

        #region 群发相关

        private string _msgID;
        /// <summary>
        /// 群发返回消息ID
        /// </summary>
        public string MsgID
        {
            get { return _msgID; }
            set { _msgID = value; }
        }

        private string _msg_desc;
        /// <summary>
        /// 群发消息状态
        /// </summary>
        public string msg_desc
        {
            get { return _msg_desc; }
            set { _msg_desc = value; }
        }

        private string _totalCount;
        /// <summary>
        /// 群发总粉丝数
        /// </summary>
        public string TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        private string _filterCount;
        /// <summary>
        /// 可发粉丝数
        /// </summary>
        public string FilterCount
        {
            get { return _filterCount; }
            set { _filterCount = value; }
        }

        private string _sentCount;
        /// <summary>
        /// 实际发送粉丝数
        /// </summary>
        public string SentCount
        {
            get { return _sentCount; }
            set { _sentCount = value; }
        }

        private string _errorCount;
        /// <summary>
        /// 发送失败数
        /// </summary>
        public string ErrorCount
        {
            get { return _errorCount; }
            set { _errorCount = value; }
        }


        #endregion

    }


    #endregion 微信请求类
}
