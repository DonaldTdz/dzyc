using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 微信回复
    /// </summary>
    public class Replyset
    {
    
        public string hostUrl = "http://" + HttpContext.Current.Request.Url.Authority;          //域名
        public string upfileurl = "http://file.api.weixin.qq.com/cgi-bin/media/upload";
        public string baiduImg = "http://api.map.baidu.com/staticimage?center={0},{1}&width=700&height=300&zoom=11";
        public Guid User_ID = Guid.Empty;
        public string WeChat_ID = "";
        public string WeChat_Type = "";
        public string WeChat_Name = "";

        WeiXinMenuModelService caidandal = new WeiXinMenuModelService();//自定义菜单服务

        WeiXinFirstInModelService firstService = new WeiXinFirstInModelService();//首次关注服务
        WeiXinKeyWordModelService keyWordService = new WeiXinKeyWordModelService();//关键字服务
        WeiXinTriggerInfoModelService triggerInfoService = new WeiXinTriggerInfoModelService();//图文触发服务
        WeiXinPicMsgMatserModelService picMsgService = new WeiXinPicMsgMatserModelService();//图文消息服务
        WeiXinPicMsgDetailModelService picDetailService = new WeiXinPicMsgDetailModelService();//图文明细服务

        ConmmonApi wxCommand = new ConmmonApi();

        JavaScriptSerializer Jss = new JavaScriptSerializer();

        public Replyset()
        { }
        
        #region 关注回复

        /// <summary>
        /// 关注回复
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="weiXinAppId"></param>
        /// <returns></returns>
        public string GetSubscribe(string FromUserName, string ToUserName, Guid weiXinAppId, string WeixinKey)
        {

            string resXml = "";
            
            //获取首次关注表数据
            var dtSubscribe = firstService.GetByWeiXinAppId(weiXinAppId);

            if (dtSubscribe!=null )
            {
                Guid? article_id = dtSubscribe.PicMsgOrTirggerInfoId; //图文信息表
                int reply_type = dtSubscribe.Type;//回复类型
                string reply_text = dtSubscribe.ContenInfo;//文本回复内容

                if (reply_type == 0)
                {
                    resXml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + reply_text + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                }
                else
                {
                    resXml = GetPicMsg(FromUserName, ToUserName, article_id.Value,WeixinKey);
                }
            }

            return resXml;
        }
        #endregion 关注回复
        
        #region 默认回复 (待开发)
        public string GetDefault(string FromUserName, string ToUserName, Guid weiXinAppId)
        {
            /*
            string resXml = "";

            if (dtDefault.Rows.Count > 0)
            {
                string article_id = dtDefault.Rows[0]["article_id"].ToString();
                string reply_type = dtDefault.Rows[0]["reply_type"].ToString();
                string reply_text = dtDefault.Rows[0]["reply_text"].ToString();

                if (reply_type == "text")
                {
                    resXml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + reply_text + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                }
                else
                {
                    resXml = GetPicMsg(FromUserName, ToUserName, article_id);
                }
            }

            return resXml;
             */
            var resXml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + "欢迎关注广元烟草微信平台，零售户可通过账号绑定，绑定后可进行订单查询！" + "]]></Content><FuncFlag>0</FuncFlag></xml>";
            return resXml;
        }
        #endregion 默认回复

        #region 图片回复 （待开发）
        public string GetImageReply(string FromUserName, string ToUserName, string UrlImg,Guid masterId)
        {
            string resXml = "";
            

            return resXml;
        }
        #endregion 图片回复

        #region 关键字回复
        
        /// <summary>
        /// 关键字回复
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="Content"></param>
        /// <param name="PatternMethod"></param>
        /// <param name="weiXinAppId"></param>
        /// <returns></returns>
        public string GetKeyword(string FromUserName, string ToUserName, string Content,Guid weiXinAppId,string weiXinKey)
        {
            string resXml = "";

            
            WeiXinKeyWordModel dtKeyword=keyWordService.GetByContent(weiXinAppId,Content);
            
            if (dtKeyword!=null)
            {
                    Guid? article_id =dtKeyword.PicMsgOrTirggerInfoId;//关联图文ID
                    int keyword_type = dtKeyword.Type;//类型 0为文本 1为图文
                    string keyword_text = dtKeyword.ContenInfo;

                    switch (keyword_type)
                    {
                        case 0://文本
                            resXml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + keyword_text + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                            break;
                        case 1://图文
                            resXml = GetPicMsg(FromUserName, ToUserName, article_id.Value, weiXinKey);
                            break;
                    }
            }
            else
            {
                resXml = GetDefault(FromUserName, ToUserName, weiXinAppId);
            }

            return resXml;
        }
        #endregion 关键字回复

        #region 菜单单击

        /// <summary>
        /// 菜单单击
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="EventKey">菜单KEY</param>
        /// <returns></returns>
        public string GetMenuClick(string FromUserName, string ToUserName, string EventKey,Guid WeixinAppId,string WeixinKey)
        {
            string resXml = "";          
            try
            {
                 WeiXinMenuModel dtMenu=caidandal.GetByEventKey(WeixinAppId,EventKey);
                if (dtMenu!=null)
                {
                    int caidan_retype = dtMenu.Type;//菜单回复类型
                    string caidan_retext = dtMenu.ContenInfo;//菜单回复文本信息
                    Guid? masterId = dtMenu.PicMsgOrTirggerInfoId;

                    switch (caidan_retype)
                    {
                        case (int)WeiXinMenuType.Word://文字
                            resXml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + caidan_retext + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                            break;
                        case (int)WeiXinMenuType.View://连接
                            resXml = GetPicMsg(FromUserName, ToUserName, masterId.Value, WeixinKey);
                            break;
                        case (int)WeiXinMenuType.Pic://图文
                            resXml = GetPicMsg(FromUserName, ToUserName, masterId.Value, WeixinKey);
                            break;                         
                    }
                }
            }
            catch (Exception ex)
            {
                WriteTxt("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());
            }

            return resXml;
        }
        #endregion 菜单单击
        
        #region 获取图文列表
        public string GetPicMsg(string FromUserName, string ToUserName, Guid masterId,string wxKey)
        {
            string resXml = "";
            WeiXinTriggerInfoModel triggerInfo = triggerInfoService.GetByKey(masterId);
            if (triggerInfo != null)
            {
                resXml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + 1 + "</ArticleCount><Articles>";
                string article_title = triggerInfo.MsgTitle;
                string article_description = triggerInfo.MsgTitle;
                string article_picurl =hostUrl+ triggerInfo.MsgCoverPath.Replace("..","");
                string article_url = triggerInfo.ActionUrl;
                resXml += "<item><Title><![CDATA[" + article_title + "]]></Title><Description><![CDATA[" + article_description + "]]></Description><PicUrl><![CDATA[" + article_picurl + "]]></PicUrl><Url><![CDATA[" + article_url + "]]></Url></item>";
                resXml += "</Articles><FuncFlag>1</FuncFlag></xml>";
            }
            else
            {
                var master = picMsgService.GetByKey(masterId);
                var details = picDetailService.GetDetails(masterId);
                int count = details.Count + 1;
                if (master != null)
                {
                    var paramUrl=master.Url+"?WxUserName="+FromUserName+"&key="+wxKey;
                    resXml = "<xml><ToUserName><![CDATA[" + FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + count + "</ArticleCount><Articles>";
                    string master_title = master.Title;
                    string master_description = master.Description;
                    string master_picurl = hostUrl + master.PicUrl.Replace("..", "");
                    string master_url = master.NeedParam ? paramUrl : master.Url;
                    resXml += "<item><Title><![CDATA[" + master_title + "]]></Title><Description><![CDATA[" + master_description + "]]></Description><PicUrl><![CDATA[" + master_picurl + "]]></PicUrl><Url><![CDATA[" + master_url + "]]></Url></item>";
                }
                if (details.Count > 0)
                {
                    foreach (var d in details)
                    {
                        string detail_title = d.Title;
                        string detail_description = d.Description;
                        string detail_picurl =hostUrl+ d.PicUrl.Replace("..","");
                        string detail_url = d.Url;
                        resXml += "<item><Title><![CDATA[" + detail_title + "]]></Title><Description><![CDATA[" + detail_description + "]]></Description><PicUrl><![CDATA[" + detail_picurl + "]]></PicUrl><Url><![CDATA[" + detail_url + "]]></Url></item>";
                    }
                }
                resXml += "</Articles><FuncFlag>1</FuncFlag></xml>";
            }

            return resXml;
        }
        #endregion 获取图文列表

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
        public bool WriteTxt(string str)
        {
            try
            {
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Log/wxbugLog.txt"), FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.WriteLine(str);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion 通用方法
        
    }
}