using Common.Base;
using Common.BLL.Implement;
using Common.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    #region 模型
    public class WeiXinPicMsgMatserModel
    {
        #region 基元属性
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> WeiXinSourceId { get; set; }
        public Nullable<System.Guid> WeiXinAppId { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public IList<WeiXinPicMsgDetailModel> MsgDetails { get; set; }
        public int MaterialType { get; set; }
        public bool NeedParam { get; set; }
        public string CreateTimeStr
        {
            get { return this.CreateTime.ToString("yyyy-MM-dd"); }
        }
        #endregion
    }
    #endregion

    #region Services
    public class WeiXinPicMsgMatserModelService : BaseModelService<WeiXinPicMsgMatser, WeiXinPicMsgMatserModel>
    {
        private readonly WeiXinPicMsgMatserLogic _WeiXinPicMsgMatserLogic;
        public WeiXinPicMsgMatserModelService()
        {
            _WeiXinPicMsgMatserLogic = new WeiXinPicMsgMatserLogic();
        }

        protected override BaseLogic<WeiXinPicMsgMatser> BaseLogic
        {
            get { return _WeiXinPicMsgMatserLogic; }
        }

        //获取图片资源下拉列表
        internal IList<SelectListItem> creatSelectList()
        {
            IList<SelectListItem> result = GetAll().Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Title }).ToList();
            return result;
        }

        /// <summary>
        /// 获取微信地址
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetUrlSelectList()
        {
            IList<SelectListItem> result = new List<SelectListItem>();
            WeiXinCustomUrlLogic cUrlLogic = new WeiXinCustomUrlLogic();
            result = cUrlLogic.GetAll().Select(f => new SelectListItem { Value = f.Url, Text = f.Name }).ToList();
            WeiXinSysUrlLogic sysUrlLogic = new WeiXinSysUrlLogic();
            result = result.Concat(sysUrlLogic.GetAll().Select(f => new SelectListItem { Value = f.Url, Text = f.Name }).ToList()).ToList();
            return result;
        }

        /// <summary>
        /// 获取微信地址（返回ID值）
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetUrlSelectIdList()
        {
            IList<SelectListItem> result = new List<SelectListItem>();
            WeiXinCustomUrlLogic cUrlLogic = new WeiXinCustomUrlLogic();
            result = cUrlLogic.GetAll().Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
            WeiXinSysUrlLogic sysUrlLogic = new WeiXinSysUrlLogic();
            result = result.Concat(sysUrlLogic.GetAll().Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList()).ToList();
            return result;
        }


        /// <summary>
        ///保存数据
        /// </summary>
        /// <param name="detailStr"></param>
        /// <param name="master"></param>
        /// <returns></returns>
        public DoHandle SaveData(string detailStr, WeiXinPicMsgMatser master)
        {
            var detatils = JsonExtention.ParseJsonJS<IList<WeiXinPicMsgDetail>>(detailStr);
            var DoHandle = _WeiXinPicMsgMatserLogic.SaveData(master, detatils);
            return DoHandle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DoHandle DeleteData(Guid id) 
        {
            var doHandle = _WeiXinPicMsgMatserLogic.DeleteData(id);
            return doHandle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IList<WeiXinPicMsgMatserModel> GetDataByAppId(Guid appId)
        {
            var masterList = _WeiXinPicMsgMatserLogic.Query(f => f.WeiXinAppId == appId, new string[0]);
            IList<Guid> ids = masterList.Select(f => f.Id).ToList();
            var detailsList = new WeiXinPicMsgDetailLogic().GetDetailbyMsaterIds(ids);
            IList<WeiXinPicMsgMatserModel> result = new List<WeiXinPicMsgMatserModel>();
            foreach (var item in masterList)
            {
                var details = detailsList.Where(f => f.PicMsgMatserId == item.Id).Select(f => new WeiXinPicMsgDetailModel()
                {
                    Id = f.Id,
                    Url = f.Url,
                    Description = f.Description,
                    PicMsgMatserId = f.PicMsgMatserId,
                    PicUrl = f.PicUrl,
                    Title = f.Title,
                    WeiXinSourceId = f.WeiXinSourceId
                }).ToList();
                result.Add(new WeiXinPicMsgMatserModel()
                {
                    Id = item.Id,
                    Url = item.Url,
                    Title = item.Title,
                    PicUrl = item.PicUrl,
                    WeiXinAppId = item.WeiXinAppId,
                    WeiXinSourceId = item.WeiXinSourceId,
                    CreateTime = item.CreateTime,
                    Description = item.Description,
                    MsgDetails = details,
                    MaterialType=item.MaterialType
                });
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeiXinPicMsgMatserModel GetDataById(Guid id)
        {
            var master = GetByKey(id);
            var detail = new WeiXinPicMsgDetailLogic().GetDetails(master.Id).Select(f => new WeiXinPicMsgDetailModel()
                {
                    Id = f.Id,
                    Url = f.Url,
                    Description = f.Description,
                    PicMsgMatserId = f.PicMsgMatserId,
                    PicUrl = f.PicUrl,
                    Title = f.Title,
                    WeiXinSourceId = f.WeiXinSourceId
                }).ToList(); ;
            return new WeiXinPicMsgMatserModel()
            {
                Id = master.Id,
                Url = master.Url,
                Title = master.Title,
                PicUrl = master.PicUrl,
                WeiXinAppId = master.WeiXinAppId,
                WeiXinSourceId = master.WeiXinSourceId,
                CreateTime = master.CreateTime,
                Description = master.Description,
                MaterialType=master.MaterialType,
                MsgDetails = detail
            };
        }

    }
    #endregion
}