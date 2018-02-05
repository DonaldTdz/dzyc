using System;
using System.Data.Objects;
using DHQR.DataAccess.Entities;
using System.Linq;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 用户实体操作类
    /// </summary>
    public class WeiXinKeyWordRep : ProRep<WeiXinKeyWord>
    {
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        //public WeiXinKeyWordRep()
        //{
        //    ActiveContext = new BaseDataEntities();
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinKeyWordRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinKeyWord> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinKeyWords; }
        }

        /// <summary>
        /// 根据内容和匹配方式获取关键字
        /// 如果多条返回第一条
        /// </summary>
        /// <param name="weiXinAppId">公众号ID</param>
        /// <param name="content">内容</param>
        /// <param name="patternMethod">匹配方式</param>
        /// <returns></returns>
        public WeiXinKeyWord GetByContent(Guid weiXinAppId, string content)
        {
            //PatternMethod 0-模糊 1-精确
            WeiXinKeyWord result;
            result = ActiveContext.WeiXinKeyWords
                                   .FirstOrDefault(f => f.WeiXinAppId == weiXinAppId
                                                      &&( (f.KeyWord == content && f.PatternMethod==1) || (content.Contains(f.KeyWord) && f.PatternMethod==0)
                                                        || (f.PatternMethod==1 && f.KeyWord.Contains(content))
                                                      )
                                                      );
            return result;
        }
    }
}
