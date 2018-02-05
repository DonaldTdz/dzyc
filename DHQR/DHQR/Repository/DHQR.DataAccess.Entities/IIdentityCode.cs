using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DAL.Entities;

namespace DHQR.DataAccess.Entities
{
    ///<summary>
    /// 具有唯一代码的接口
    ///</summary>
    public interface IIdentityCode : IEntityKey
    {
        /// <summary>
        /// 代码
        /// </summary>
        string Code { get; set; }

        ///<summary>
        /// 名称
        ///</summary>
        string Name { get; set; }
    }

    ///<summary>
    /// 具有唯一代码和名称的接口
    ///</summary>
    public interface IIdentityCodeName : IIdentityCode
    {
    }

    ///<summary>
    /// 有数据库自动分配代码的接口
    ///</summary>
    public interface ICodeId : IIdentityCode
    {
        ///<summary>
        /// 系统分配的代码号
        ///</summary>
        long CodeId { get; set; }
    }

    /// <summary>
    /// 具有唯一代码类的扩展方法
    /// </summary>
    public static class IdentityCodeExtensions
    {
        ///<summary>
        /// 获取描述信息
        ///</summary>
        ///<param name="identityCode">具有唯一代码类</param>
        ///<returns>描述信息</returns>
        public static string ToDescription(this IIdentityCode identityCode)
        {
            return string.Format("{0}-{1}", identityCode.Code, identityCode.Name);
        }
    }
}
