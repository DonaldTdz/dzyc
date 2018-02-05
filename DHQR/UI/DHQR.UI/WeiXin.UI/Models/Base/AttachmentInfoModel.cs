using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.UI.Model;
using Common.Base;

namespace DHQR.UI.Models
{
    public class AttachmentInfoModel
    {
        #region 数据模型
        //Id
        public Guid Id { set; get; }

        //上传文件名
        public string FileName { set; get; }
        //文件保存路径
        public string FilePath { set; get; }
        //文件扩展名
        public string Extention { set; get; }
        //创建时间
        public DateTime CreateTime { set; get; }
        //创建人
        public string Creator { set; get; }
        #endregion

    }
    public class AttachmentInfoModelService : BaseModelService<AttachmentInfo, AttachmentInfoModel>
    {
        private readonly AttachmentInfoLogic logic;

        public AttachmentInfoModelService()
        {
            logic = new AttachmentInfoLogic();
        }

        protected override BaseLogic<AttachmentInfo> BaseLogic
        {
            get { return logic; }
        }


    }

}