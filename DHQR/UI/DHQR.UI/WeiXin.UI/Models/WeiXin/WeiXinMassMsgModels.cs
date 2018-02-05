using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Base;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using System.Web.Script.Serialization;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 微信群发信息模型
    /// </summary>
    public class WeiXinMassMsgModel
    {
        #region Model
        private Guid _id;
        private string _thumb_media_id;
        private string _author;
        private string _title;
        private string _content_source_url;
        private string _content;
        private string _digest;
        private string _show_cover_pic;
        private Guid? _parentid;
        private string _media_id;
        private string _type;
        private string _pic_url;
       
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 图文消息缩略图的media_id
        /// </summary>
        public string thumb_media_id
        {
            set { _thumb_media_id = value; }
            get { return _thumb_media_id; }
        }
        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string author
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        public string content_source_url
        {
            set { _content_source_url = value; }
            get { return _content_source_url; }
        }
        /// <summary>
        /// 图文消息页面的内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string digest
        {
            set { _digest = value; }
            get { return _digest; }
        }
        /// <summary>
        /// 是否显示封面
        /// </summary>
        public string show_cover_pic
        {
            set { _show_cover_pic = value; }
            get { return _show_cover_pic; }
        }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? parentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 媒体文件/图文消息上传后获取的唯一标识
        /// </summary>
        public string media_id
        {
            set { _media_id = value; }
            get { return _media_id; }
        }
        /// <summary>
        /// 媒体文件类型
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }

         /// <summary>
        /// 图片本地地址
        /// </summary>
        public string pic_url
        {
            set { _pic_url = value; }
            get { return _pic_url; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTimeStr
        {
            get;
            set;
        }

        /// <summary>
        /// 文章ID
        /// </summary>
        public Guid? WeiXinArticleId
        {
            get;
            set;
        }


        #endregion Model

    }

    public class WeiXinMassGroupModel
    {
        public WeiXinMassMsgModel MsgHeader { get; set; }

        public IList<WeiXinMassMsgModel> MsgDetails { get; set; }
    }

    #region Services
    public class WeiXinMassMsgModelService : BaseModelService<WeiXinMassMsg, WeiXinMassMsgModel>
    {
        private readonly WeiXinMassMsgLogic BusinessLogic;
        public WeiXinMassMsgModelService()
        {
            BusinessLogic = new WeiXinMassMsgLogic();
        }

        protected override BaseLogic<WeiXinMassMsg> BaseLogic
        {
            get { return BusinessLogic; }
        }


        #region 查询群发多图文集合

        /// <summary>
        /// 查询多图文计划
        /// </summary>
        /// <returns></returns>
        public IList<WeiXinMassGroupModel> QueryMassGroup()
        {
            IList<WeiXinMassGroupModel> result = new List<WeiXinMassGroupModel> ();
            var data=BusinessLogic.QueryMassGroup();
            foreach (var item in data)
            {
                var header = this.ConvertToModel(item.MsgHeader);
                var details = item.MsgDetails.Select(f => ConvertToModel(f)).ToList();
                WeiXinMassGroupModel model = new WeiXinMassGroupModel { MsgHeader=header,MsgDetails=details };
                result.Add(model);
            }
            return result;
        }

        #endregion

        #region 查询单图文信息

        /// <summary>
        /// 查询多图文计划
        /// </summary>
        /// <returns></returns>
        public WeiXinMassGroupModel GetByMasterId(Guid masterId)
        {
            var data = BusinessLogic.GetByMasterId(masterId);

              var header = this.ConvertToModel(data.MsgHeader);
                var details = data.MsgDetails.Select(f => ConvertToModel(f)).ToList();
                WeiXinMassGroupModel result = new WeiXinMassGroupModel { MsgHeader = header, MsgDetails = details };

            return result;
        }



        #endregion


        #region 保存群发信息

        /// <summary>
        /// 保存群发信息
       /// </summary>
       /// <param name="detailsStr">明细信息</param>
       /// <param name="master">消息头信息</param>
       /// <param name="needSend">是否需要立即发送</param>
       /// <param name="dohandle"></param>
        public void SaveData(string detailsStr, WeiXinMassMsg master,Guid weiXinAppId,bool needSend,int target,int? groupid,out DoHandle dohandle)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ConmmonApi commonApi = new ConmmonApi();
            WeiXinSysUrlLogic urlLogic = new WeiXinSysUrlLogic();
            WeiXinArticleModelService articlService=new WeiXinArticleModelService();
            var access_token = commonApi.GetAccessToken(weiXinAppId);
            IList<WeiXinMassMsg> details = jss.Deserialize<IList<WeiXinMassMsg>>(detailsStr);
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="操作失败" };
            if (master.Id == Guid.Empty || master.Id == null)//新增
            {
                #region  新建

                //上传图文头缩略图
                string headPicpath = HttpContext.Current.Server.MapPath(master.pic_url);
                string headmsg = WeiXinMassMsgApi.UploadMedia(access_token, "image", headPicpath); // 上图片返回媒体ID
                string head_media_id = HttpRequestUtil.GetJsonValue(headmsg, "media_id");
                master.Id = Guid.NewGuid();
                master.thumb_media_id = head_media_id;
                master.show_cover_pic = "1";
                master.type = "news";
                master.CreateTime = DateTime.Now;
                if (!string.IsNullOrEmpty(master.content_source_url))
                {
                    var sysUrlId = Guid.Parse(master.content_source_url);
                    var sysUrl = urlLogic.GetByKey(sysUrlId);
                    master.WeiXinArticleId = sysUrl.WeiXinArticleId;
                    master.content_source_url = sysUrl.Url;
                    WeiXinArticleModel article = (sysUrl == null || sysUrl.WeiXinArticleId==null)? null : articlService.GetByKey(sysUrl.WeiXinArticleId.Value);
                    master.content = article != null ? article.Content.Replace("\"", "\\\"") : string.Empty;
                }

                //上传图文明细缩略图
                foreach (var item in details)
                {

                    string picpath = HttpContext.Current.Server.MapPath(item.pic_url);
                    string msg = WeiXinMassMsgApi.UploadMedia(access_token, "image", picpath); // 上图片返回媒体ID
                    string media_id = HttpRequestUtil.GetJsonValue(msg, "media_id");
                    item.Id = Guid.NewGuid();
                    item.thumb_media_id = media_id;
                    item.show_cover_pic = "0";
                    item.type = "news";
                    item.CreateTime = DateTime.Now;
                    item.parentId = master.Id;
                    if (!string.IsNullOrEmpty(item.content_source_url))
                    {
                        var itemUrlId = Guid.Parse(item.content_source_url);
                        var itemUrl = urlLogic.GetByKey(itemUrlId);
                        item.WeiXinArticleId = itemUrl.WeiXinArticleId;
                        item.content_source_url = itemUrl.Url;
                        WeiXinArticleModel article = (itemUrl == null || itemUrl.WeiXinArticleId ==null)? null : articlService.GetByKey(itemUrl.WeiXinArticleId.Value);
                        item.content = article != null ? article.Content.Replace("\"", "\\\"") : string.Empty;
                    }

                }
                //保存本地数据库
                BusinessLogic.Create(master,out dohandle);
                BusinessLogic.Create(details, out dohandle);
                UploadToWeiXin(master, details, access_token, out dohandle);

                #endregion
            }
            else
            {
                #region 更新

                var orignalMaster = BusinessLogic.GetByKey(master.Id);
                //上传图文头缩略图
                string headPicpath = HttpContext.Current.Server.MapPath(master.pic_url);
                string headmsg = WeiXinMassMsgApi.UploadMedia(access_token, "image", headPicpath); // 上图片返回媒体ID
                string head_media_id = HttpRequestUtil.GetJsonValue(headmsg, "media_id");
                orignalMaster.thumb_media_id = head_media_id;
                orignalMaster.show_cover_pic = "1";
                orignalMaster.type = "news";
                if (!string.IsNullOrEmpty(master.content_source_url) && !master.content_source_url.Contains("http"))
                {
                    var sysUrlId = Guid.Parse(master.content_source_url);
                    var sysUrl = urlLogic.GetByKey(sysUrlId);
                    orignalMaster.WeiXinArticleId = sysUrl.WeiXinArticleId;
                    orignalMaster.content_source_url = sysUrl.Url;
                    WeiXinArticleModel article = (sysUrl == null || sysUrl.WeiXinArticleId==null)? null : articlService.GetByKey(sysUrl.WeiXinArticleId.Value);
                    orignalMaster.content = article != null ? article.Content.Replace("\"", "\\\"") : string.Empty;
                }

                BusinessLogic.Update(orignalMaster, out dohandle);

                var orignDetails = BusinessLogic.GetByParentId(master.Id);
                var currentItemIds=details.Where(f=>f.Id!=Guid.Empty).Select(f=>f.Id).ToList();
                IList<WeiXinMassMsg> todelDatas = orignDetails.Where(f => !currentItemIds.Contains(f.Id)).ToList();
                IList < WeiXinMassMsg > toaddDatas = new List<WeiXinMassMsg> { };
                IList<WeiXinMassMsg> tomodDatas = new List<WeiXinMassMsg> { };

                //上传图文明细缩略图
                foreach (var item in details)
                {
                    string picpath = HttpContext.Current.Server.MapPath(item.pic_url);
                    string msg = WeiXinMassMsgApi.UploadMedia(access_token, "image", picpath); // 上图片返回媒体ID
                    string media_id = HttpRequestUtil.GetJsonValue(msg, "media_id");
                    item.thumb_media_id = media_id;
                    item.show_cover_pic = "0";
                    item.type = "news";
                    item.parentId = master.Id;
                    if (!string.IsNullOrEmpty(item.content_source_url) && !item.content_source_url.Contains("http"))
                    {
                        var itemUrlId = Guid.Parse(item.content_source_url);
                        var itemUrl = urlLogic.GetByKey(itemUrlId);
                        item.WeiXinArticleId = itemUrl.WeiXinArticleId;
                        item.content_source_url = itemUrl.Url;
                        WeiXinArticleModel article = (itemUrl == null || itemUrl.WeiXinArticleId==null) ? null : articlService.GetByKey(itemUrl.WeiXinArticleId.Value);
                        item.content = article != null ? article.Content.Replace("\"", "\\\"") : string.Empty;
                    }

                    if (item.Id == null || item.Id == Guid.Empty)
                    {
                        item.Id = Guid.NewGuid();
                        item.CreateTime = DateTime.Now;
                        toaddDatas.Add(item);
                    }
                    else
                    {
                        var orignItem = orignDetails.SingleOrDefault(f => f.Id == item.Id);
                        orignItem.thumb_media_id = media_id;
                        orignItem.show_cover_pic = "0";
                        orignItem.type = "news";
                        orignItem.parentId = master.Id;
                        orignItem.WeiXinArticleId = master.content_source_url.Contains("http")?orignItem.WeiXinArticleId: item.WeiXinArticleId;
                        orignItem.content_source_url = item.content_source_url;
                        orignItem.content =master.content_source_url.Contains("http")?orignItem.content: item.content;
                        tomodDatas.Add(orignItem);
                    }
                    
                }

                BusinessLogic.Create(toaddDatas, out dohandle);
                BusinessLogic.Update(tomodDatas, out dohandle);
                BusinessLogic.Delete(todelDatas, out dohandle);
                List<WeiXinMassMsg> upDetails = new List<WeiXinMassMsg>();
                upDetails.AddRange(toaddDatas);
                upDetails.AddRange(tomodDatas);
                UploadToWeiXin(orignalMaster, upDetails, access_token, out dohandle);

                #endregion
            }

            //群发
            if (needSend)
            {
                SendMassMsg(master.Id, weiXinAppId, target, groupid, out dohandle);
            }
        }

        #endregion

        #region 上传图文至微信平台

        /// <summary>
        /// 上传图文至微信平台
        /// </summary>
        /// <param name="master"></param>
        /// <param name="details"></param>
        /// <param name="dohandle"></param>
        public void UploadToWeiXin(WeiXinMassMsg master, IList<WeiXinMassMsg> details, string access_token, out DoHandle dohandle)
        {
            dohandle = new DoHandle();
            
            //上传图文至微信平台
            var postData = WeiXinMassMsgApi.GetArticlesJsonStr(master,details);
            var msg = WeiXinMassMsgApi.UploadNews(access_token, postData);
            //更新mediaid至本地数据库
            var media_id= HttpRequestUtil.GetJsonValue(msg, "media_id");
            master.media_id = media_id;
            BusinessLogic.Update(master, out dohandle);

        }

        #endregion

        #region 群发消息

        /// <summary>
        /// 群发图文消息
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="weiXinAppId"></param>
        /// <param name="target"></param>
        /// <param name="groupid"></param>
        /// <param name="dohandle"></param>
        public void SendMassMsg(Guid masterId,Guid weiXinAppId,int target,int? groupid,out DoHandle dohandle)
        {
            dohandle = new DoHandle();
            ConmmonApi commonApi = new ConmmonApi();
            var access_token = commonApi.GetAccessToken(weiXinAppId);
            var master = BusinessLogic.GetByKey(masterId);
            string postData = string.Empty;
            string msg = string.Empty;
            string resultMsg = string.Empty;
            if (target == 0)
            {
                List<string> openIds = WeiXinMassMsgApi.GetOpenIDs(access_token);
                postData = WeiXinMassMsgApi.CreateNewsJson(master.media_id, openIds);
                resultMsg = WeiXinMassMsgApi.Send(access_token, postData);
            }
            else
            {
               resultMsg= WeiXinMassMsgApi.SendByGroup(access_token, master.media_id, groupid.Value);
            }
            if (!string.IsNullOrWhiteSpace(resultMsg))
            {
                string errcode = HttpRequestUtil.GetJsonValue(resultMsg, "errcode");
                string errmsg = HttpRequestUtil.GetJsonValue(resultMsg, "errmsg");
                if (errcode == "0")//发送成功
                {
                    string msgid = HttpRequestUtil.GetJsonValue(resultMsg, "msg_id");

                    #region 新增群发历史信息
                    //新增群发历史信息
                    WeiXinMassMsgHist hist = new WeiXinMassMsgHist 
                    {
                        Id=Guid.NewGuid(),
                        msg_id=msgid,
                        type="news",
                        tagetid=target,
                        groupid=groupid,
                        content=string.Empty,
                        msg_status=errcode,
                        msg_desc=errmsg,
                        WeiXinMassMsgId=masterId,
                        CreateTime=DateTime.Now
                    };
                    WeiXinMassMsgHistLogic histLogic = new WeiXinMassMsgHistLogic();
                    DoHandle hisDohandle = new DoHandle();
                    histLogic.Create(hist,out hisDohandle);
                    #endregion
                }
                else
                {
                    msg= "{\"code\":0,\"msg\":\"errcode:"
                        + errcode + ", errmsg:"
                        + errmsg + "\"}";
                }
            }
            else
            {
                msg= "{\"code\":0,\"msg\":\"type参数错误\"}";
            }
        }



        /// <summary>
        /// 群发文字信息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="target"></param>
        /// <param name="groupid"></param>
        /// <param name="dohandle"></param>
        public void SendTextMsg(string content, Guid weiXinAppId, int target, int? groupid, out DoHandle dohandle)
        {
            dohandle = new DoHandle();
            ConmmonApi commonApi = new ConmmonApi();
            var access_token = commonApi.GetAccessToken(weiXinAppId);
            string msg = string.Empty;
            string resultMsg = string.Empty;
            if (target == 0)
            {
              resultMsg=  WeiXinMassMsgApi.SendTextMsg(access_token,content);
            }
            else
            {
                resultMsg = WeiXinMassMsgApi.SendTextMsg(access_token, groupid.Value, content);
            }
            if (!string.IsNullOrWhiteSpace(resultMsg))
            {
                string errcode = HttpRequestUtil.GetJsonValue(resultMsg, "errcode");
                string errmsg = HttpRequestUtil.GetJsonValue(resultMsg, "errmsg");
                if (errcode == "0")//发送成功
                {
                    string msgid = HttpRequestUtil.GetJsonValue(resultMsg, "msg_id");

                    #region 新增群发历史信息
                    //新增群发历史信息
                    WeiXinMassMsgHist hist = new WeiXinMassMsgHist
                    {
                        Id = Guid.NewGuid(),
                        msg_id = msgid,
                        type = "text",
                        tagetid = target,
                        groupid = groupid,
                        content = content,
                        msg_status = errcode,
                        msg_desc = errmsg,
                        CreateTime = DateTime.Now
                    };
                    WeiXinMassMsgHistLogic histLogic = new WeiXinMassMsgHistLogic();
                    DoHandle hisDohandle = new DoHandle();
                    histLogic.Create(hist, out hisDohandle);
                    #endregion
                }
                else
                {
                    msg = "{\"code\":0,\"msg\":\"errcode:"
                        + errcode + ", errmsg:"
                        + errmsg + "\"}";
                }
            }
            else
            {
                msg = "{\"code\":0,\"msg\":\"type参数错误\"}";
            }

        }

        #endregion


        #region 删除群发素材

         /// <summary>
         /// 删除群发素材
         /// </summary>
         /// <param name="masterId"></param>
         /// <param name="dohandle"></param>
        public void DelMassMsg(Guid masterId, out DoHandle dohandle)
        {
            BusinessLogic.DelMassMsg(masterId, out dohandle);
        }

        #endregion
    }

    #endregion
}