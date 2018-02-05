using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Transactions;
using Common.Base;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// IQueryable扩展
    /// </summary>
    public static class QueryableExtension
    {
        /// <summary>
        /// 将查询语句转换为查询结果
        /// </summary>
        /// <typeparam name="T">查询结果的类型</typeparam>
        /// <param name="source">查询语句</param>
        /// <param name="queryParam">查询参数</param>
        /// <returns></returns>
        public static PagedResults<T> ToPagedResults<T>(this IQueryable<T> source, QueryParam queryParam)
        {
            if (queryParam.StartIndex < 0)
                throw new InvalidOperationException("起始记录数不能小于0");
            if (queryParam.Rows <= 0)
                throw new InvalidOperationException("每页记录数不能小于0");

            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                var pageInfo = new PagerInfo(queryParam) { TotalRowCount = source.Count() };
                List<T> data = source.OrderBy(queryParam.OrderString).Skip(queryParam.StartIndex).Take(queryParam.Rows).ToList();
                scope.Complete();
                var pagedResults = new PagedResults<T> { PagerInfo = pageInfo, Data = data };
                return pagedResults;
            }
        }

    }
}
