using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;

namespace DHQR.BusinessLogic.Implement
{
    public abstract class BaseEnumLogic<T>
        where T : BaseEnum, new()
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        protected BaseEnumRepository BaseEnumRep { get { return new BaseEnumRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected  Repository<BaseEnum> Repository
        {
            get { return BaseEnumRep; }
        }


        /// <summary>
        /// 根据枚举类型获取基本枚举
        /// </summary>
        /// <param name="enumTypes">枚举类型</param>
        /// <returns>基本枚举</returns>
        public IList<T> GetByTypes(IList<string> enumTypes)
        {

            return this.BaseEnumRep.GetByTypes(enumTypes).Select(f => f.To<T>()).ToList();
        }


        /// <summary>
        /// 通过枚举类型名和枚举值获取一个基本枚举
        /// </summary>
        /// <param name="enumType">枚举类</param>
        /// <param name="value">枚举值</param>
        /// <returns>基本枚举</returns>
        public T GetByUnique(string enumType, string value)
        {
            return BaseEnumRep.GetByUnique(enumType, value).To<T>();
        }

        /// <summary>
        /// 据枚举类型获取基本枚举
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>基本枚举</returns>
        public IList<T> GetByType(string enumType)
        {
            return this.BaseEnumRep.GetByType(enumType).Select(f => f.To<T>()).ToList();
        }

        /// <summary>
        /// 通过枚举值获取一个业务对象
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>一个业务对象</returns>
        public T GetByUnique(string value)
        {
            string enumType = typeof(T).Name;
            //如果T是BaseEnum，返回的是null;
            //子类服务中控制此方法的可操作性。
            return this.BaseEnumRep.GetByUnique(enumType, value).To<T>();
        }


    }


    /// <summary>
    /// 完整基本枚举服务
    /// </summary>
    public class BaseEnumLogic : BaseEnumLogic<BaseEnum>
    {

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IList<BaseEnum> GetAll()
        {
          
            return BaseEnumRep.GetAll();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dohandle"></param>
        public void Update(BaseEnum entity,out DoHandle dohandle)
        {
            BaseEnumRep.Update(entity,out dohandle);
        }


    }

    /// <summary>
    /// 数据状态逻辑层
    /// </summary>
    public class DataStatusLogic : BaseEnumLogic<DataStatus>
    {
        
    }


}
