using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using System.Web.Mvc;
using Common.BLL.Implement;

namespace DHQR.UI.Models
{
    #region 模型
    public class MenuModel
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Sequence { get; set; }
        public Guid? ParentMenuID { get; set; }
        public bool IsVisible { get; set; }
        public bool IsParent { get; set; }

        public string ParentMenuName { get; set; }

        /// <summary>
        /// 是否快捷菜单
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Icons { get; set; }

    }
    #endregion



    #region Services 服务模型
    public class MenuModelService : BaseModelService<Menu, MenuModel>
    {
        private readonly MenuLogic _menuLogic;
        public MenuModelService()
        {
            _menuLogic = new MenuLogic();
        }

        public IEnumerable<MenuModel> GetByLoginName(string loginName)
        {
            return _menuLogic.GetByLoginName(loginName).Select(ConvertToModel);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetParentMenuSelList() 
        {
            var data = BaseLogic.GetAll().Where(f => f.IsParent);
            var result=data.Select(f=>new SelectListItem(){Value=f.Id.ToString(),Text=f.Name}).ToList();
            result.Insert(0, new SelectListItem() { Value = "",Selected=true, Text = "请选择" });
            return result;
        }

        public List<DHQRTreeModel> GetTreeByLoginName(string loginName)
        {
            var data = _menuLogic.GetByLoginName(loginName).Where(f => f.IsVisible).OrderBy(f => f.Sequence); ;
            return data == null ? null : data.Select(item => new DHQRTreeModel
            {
                id = item.Id.ToString(),
                pId = item.ParentMenuID != Guid.Empty
                ? item.ParentMenuID.ToString() : null,
                name = item.Name,
                ename = item.Name,
                open = false,
                info = item.Url,
                isParent = item.IsParent,
                isFavorite=item.IsFavorite,
                icons=item.Icons
            }).ToList();
        }

        /// <summary>
        /// 根据用户权限获取菜单
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<MenuModel> GetMenuByUser(string loginName)
        {
            var data = _menuLogic.GetByLoginName(loginName).Where(f => f.IsVisible).OrderBy(f => f.Sequence);
            var result = data.Select(f => ConvertToModel(f)).ToList();
            return result;
        }

        

        public List<DHQRTreeModel> GetAllTree()
        {
            var data = _menuLogic.GetAll().OrderBy(f => f.Sequence);
            return data == null ? null : data.Select(item => new DHQRTreeModel
            {
                id = item.Id.ToString(),
                pId = item.ParentMenuID != Guid.Empty
                ? item.ParentMenuID.ToString() : null,
                name = item.Name,
                ename = item.Name,
                open = false,
                info = item.Url,
                isParent = item.IsParent,
                isFavorite=item.IsFavorite,
                icons=item.Icons
            }).ToList();
        }
        

        /// <summary>
        /// 根据用户名获取菜单
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public IList<BaseTreeModel> GetMenuTree(string loginName) 
        {
            IList<Menu> data = _menuLogic.GetByLoginName(loginName).Where(f => !f.IsVisible).OrderBy(f=>f.Sequence).ToList(); ;
            var rootData = data.Where(f => f.ParentMenuID == null);
            IList<BaseTreeModel> result = rootData.Select(f => ConvertToBaseTree(f, data)).ToList();
            return result;
        }


        /// <summary>
        /// 转换成菜单需要的树结构
        /// </summary>
        /// <param name="model"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal BaseTreeModel ConvertToBaseTree(Menu model, IList<Menu> data)
        {
            BaseTreeModel baseTreeModel = new BaseTreeModel()
            {
                Id = model.Id,
                Title = model.Name,
                Icons = model.Icons,
                Url = model.Url,
                IsFavorite = model.IsFavorite,
                HasSubMenu=false,
                ChildNodes=null
            };
            var result = data.Where(f => f.ParentMenuID == model.Id);
            if (result.Count() > 0) {
                baseTreeModel.ChildNodes = result.Select(f => ConvertToBaseTree(f, data)).ToList();
                baseTreeModel.HasSubMenu = true;
            }
            return baseTreeModel;
        }

        
        protected override BaseLogic<Menu> BaseLogic
        {
            get { return _menuLogic; }
        }

        internal object GetTreeByRoleId(Guid roleId)
        {
            var data = _menuLogic.GetTreeByRoleId(roleId).Where(f => f.IsVisible).OrderBy(f => f.Sequence); ;
            return data == null ? null : data.Select(item => new DHQRTreeModel
            {
                id = item.Id.ToString(),
                pId = item.ParentMenuID != Guid.Empty
                ? item.ParentMenuID.ToString() : null,
                name = item.Name,
                ename = item.Name,
                open = false,
                info = item.Url,
                isParent = item.IsParent,
                isFavorite=item.IsFavorite,
                icons=item.Icons
            }).ToList();
        }



        internal object GetConfigTreeByRoleId(Guid roleId)
        {

            var data = _menuLogic.GetAll().OrderBy(f => f.Sequence);


            var roleData = _menuLogic.GetTreeByRoleId(roleId).Where(f => f.IsVisible).OrderBy(f => f.Sequence).Select(f => f.Id);


            return data == null ? null : data.Select(item => new DHQRTreeModel
            {
                id = item.Id.ToString(),
                pId = item.ParentMenuID != Guid.Empty
                ? item.ParentMenuID.ToString() : null,
                name = item.Name,
                ename = item.Name,
                open = false,
                info = item.Url,
                isParent = item.IsParent,
                @checked = roleData.Contains(item.Id),
                isFavorite=item.IsFavorite,
                icons=item.Icons
            }).ToList();
        }
    }

    #endregion

    /// <summary>
    /// 菜单树
    /// </summary>
    public class DHQRTreeModel : TreeModel
    {
        /// <summary>
        /// 是否快捷菜单
        /// </summary>
        public bool isFavorite { get; set; }

        /// <summary>
        /// 显示图
        /// </summary>
        public string icons { get; set; }

    }
}