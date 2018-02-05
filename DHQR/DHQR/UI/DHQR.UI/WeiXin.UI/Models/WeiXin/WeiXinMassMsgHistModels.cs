using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using System.Web.Mvc;
using Common.Base;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 微信群发历史模型
    /// </summary>
    public class WeiXinMassMsgHistModel
    {
        #region Model
        private Guid _id;
        private string _msg_id;
        private string _type;
        private int _tagetid;
        private int? _groupid;
        private string _content;
        private string _msg_status;
        private string _msg_desc;
        private int? _totalcount;
        private int? _filtercount;
        private int? _sentcount;
        private int? _errorcount;
        private Guid _weixinmassmsgid;
        private Guid _weixinmediaid;
        private DateTime _createtime;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 群发信息ID
        /// </summary>
        public string msg_id
        {
            set { _msg_id = value; }
            get { return _msg_id; }
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
        /// 群发对象类型
        /// </summary>
        public int tagetid
        {
            set { _tagetid = value; }
            get { return _tagetid; }
        }
        /// <summary>
        /// 群发分组id
        /// </summary>
        public int? groupid
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 群发文字内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 群发状态
        /// </summary>
        public string msg_status
        {
            set { _msg_status = value; }
            get { return _msg_status; }
        }
        /// <summary>
        /// 群发状态描述
        /// </summary>
        public string msg_desc
        {
            set { _msg_desc = value; }
            get { return _msg_desc; }
        }

        /// <summary>
        /// 群发总人数
        /// </summary>
        public int? TotalCount
        {
            set { _totalcount = value; }
            get { return _totalcount; }
        }
        /// <summary>
        /// 准备发送的粉丝数
        /// </summary>
        public int? FilterCount
        {
            set { _filtercount = value; }
            get { return _filtercount; }
        }
        /// <summary>
        /// 发送成功的粉丝数
        /// </summary>
        public int? SentCount
        {
            set { _sentcount = value; }
            get { return _sentcount; }
        }
        /// <summary>
        /// 发送失败的粉丝数
        /// </summary>
        public int? ErrorCount
        {
            set { _errorcount = value; }
            get { return _errorcount; }
        }
        /// <summary>
        /// 图文id
        /// </summary>
        public Guid WeiXinMassMsgId
        {
            set { _weixinmassmsgid = value; }
            get { return _weixinmassmsgid; }
        }
        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid WeiXinMediaId
        {
            set { _weixinmediaid = value; }
            get { return _weixinmediaid; }
        }
        /// <summary>
        /// 群发时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model
    }


    /// <summary>
    /// 微信群发信息服务模型
    /// </summary>
    public class WeiXinMassMsgHistModelService : BaseModelService<WeiXinMassMsgHist, WeiXinMassMsgHistModel>
    {
        private readonly WeiXinMassMsgHistLogic BusinessLogic;

         public WeiXinMassMsgHistModelService()
        {
            BusinessLogic = new WeiXinMassMsgHistLogic();
        }

         protected override BaseLogic<WeiXinMassMsgHist> BaseLogic
        {
            get { return BusinessLogic; }
        }


         public void SetWeiXinMassMsgHist(WeiXinMassMsgHistModel model)
         {
             var msgHist = BusinessLogic.GetByMsgid(model.msg_id);
             if (msgHist != null)
             {
                 DoHandle dohandle;
                 msgHist.msg_desc = model.msg_desc;
                 msgHist.TotalCount = model.TotalCount;
                 msgHist.FilterCount = model.FilterCount;
                 msgHist.SentCount = model.SentCount;
                 msgHist.ErrorCount = model.ErrorCount;
                 BusinessLogic.Update(msgHist, out dohandle);
             }
         }
    }
}