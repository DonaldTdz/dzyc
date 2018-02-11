using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBM.Data.DB2;
using IBM.Data.DB2Types;
using Common.Base;
using DHQR.DataAccess.Entities;
using System.Reflection;


namespace DHQR.DataAccess.Langchao
{
    /// <summary>
    /// DB2操作帮助类
    /// </summary>
    public class DB2Helper<TEntity>
        where TEntity : new()
    {

        /// <summary>
        /// 根据表名获取所有数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<TEntity> GetAll()
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}",tableName);
            CommonDB2 db2Rep = new CommonDB2();
            var dt= db2Rep.ExeForDtl(sql);
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            return result;
        }


        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public List<TEntity> QueryData(IDictionary<string, string> queryParam)
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}", tableName);
            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
                sql = sql.Remove(sql.Length - 3,3);
            }
            CommonDB2 db2Rep = new CommonDB2();
            var dt = db2Rep.ExeForDtl(sql);
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            return result;
        }

        /// <summary>
        /// 根据条件查询 并传入新的链接字符串
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public List<TEntity> QueryData(IDictionary<string, string> queryParam, string connstr)
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}", tableName);
            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
                sql = sql.Remove(sql.Length - 3, 3);
            }
            CommonDB2 db2Rep = new CommonDB2(connstr);
            var dt = db2Rep.ExeForDtl(sql);
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            return result;
        }

        /// <summary>
        /// 根据条件查询 并传入新的链接字符串
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public List<TEntity> QueryDataByOle(IDictionary<string, string> queryParam, string connstr)
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}", tableName);
            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
                sql = sql.Remove(sql.Length - 3, 3);
            }
            CommonOle db2Rep = new CommonOle(connstr);
            var dt = db2Rep.ExeForDtl(sql);
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            return result;
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <param name="containParam"></param>
        /// <returns></returns>
        public List<TEntity> QueryData(IDictionary<string, string> queryParam,IDictionary<string,IList<string>> containParam)
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}", tableName);
            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
            }
            if (containParam.Count != 0)
            {
                if (queryParam.Count == 0)
                {
                    sql = sql + " where ";
                }
                foreach (var c in containParam)
                {
                    sql = sql + " " + c.Key + " in ('";
                    foreach (var t in c.Value)
                    {
                        sql = sql + t + "','";
                    }
                }
                sql = sql.Remove(sql.Length - 2,2);
                sql = sql + ")";
            }
            CommonDB2 db2Rep = new CommonDB2();
            var dt = db2Rep.ExeForDtl(sql);
            db2Rep.Close();
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            return result;
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <param name="containParam"></param>
        /// <returns></returns>
        public List<TEntity> QueryData(IDictionary<string, string> queryParam, IDictionary<string, IList<string>> containParam, string connStr)
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}", tableName);
            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
            }
            if (containParam.Count != 0)
            {
                if (queryParam.Count == 0)
                {
                    sql = sql + " where ";
                }
                foreach (var c in containParam)
                {
                    sql = sql + " " + c.Key + " in ('";
                    foreach (var t in c.Value)
                    {
                        sql = sql + t + "','";
                    }
                }
                sql = sql.Remove(sql.Length - 2, 2);
                sql = sql + ")";
            }
            CommonDB2 db2Rep = new CommonDB2(connStr);
            var dt = db2Rep.ExeForDtl(sql);
            db2Rep.Close();
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            return result;
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <param name="containParam"></param>
        /// <returns></returns>
        public List<TEntity> QueryDataByOle(IDictionary<string, string> queryParam, IDictionary<string, IList<string>> containParam, string connStr)
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}", tableName);
            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
            }
            if (containParam.Count != 0)
            {
                if (queryParam.Count == 0)
                {
                    sql = sql + " where ";
                }
                foreach (var c in containParam)
                {
                    sql = sql + " " + c.Key + " in ('";
                    foreach (var t in c.Value)
                    {
                        sql = sql + t + "','";
                    }
                }
                sql = sql.Remove(sql.Length - 2, 2);
                sql = sql + ")";
            }
            CommonOle db2Rep = new CommonOle(connStr);
            var dt = db2Rep.ExeForDtl(sql);
            db2Rep.Close();
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            return result;
        }


        /// <summary>
        /// 查找第一个
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(IDictionary<string, string> queryParam)
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}", tableName);
            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
                sql = sql.Remove(sql.Length - 3, 3);
                sql = sql + " FETCH FIRST 1 ROWS ONLY";
            }
            CommonDB2 db2Rep = new CommonDB2();
            var dt = db2Rep.ExeForDtl(sql);
            db2Rep.Close();
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            return result.FirstOrDefault();
           
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dohandle"></param>
        public void Insert(TEntity entity,out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败" };
            CommonDB2 db2Rep = new CommonDB2();
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("insert into {0} (", tableName);
            foreach (PropertyInfo p in entity.GetType().GetProperties())
            {
                sql = sql + p.Name + ",";
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql = sql + ") values('";

            foreach (PropertyInfo p in entity.GetType().GetProperties())
            {
                var pValue = p.GetValue(entity, null);
                if (pValue == null)
                {
                    var ptype = p.PropertyType.Name;
                    if (ptype == "Nullable`1")
                    {
                        sql = sql.Remove(sql.Length - 1, 1);
                        sql = sql + "null,'";
                    }
                    else if (ptype == "Decimal" || ptype == "Int32")
                    {
                        sql = sql + 0 + "','";
                    }
                    else
                    {
                        sql = sql + string.Empty + "','";
                    }
                }
                else
                {
                    sql = sql + pValue.ToString() + "','";
                }
            }
            sql = sql.Remove(sql.Length - 2, 2);
            sql = sql + ")";
            try
            {
                db2Rep.Execute(sql);
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }

            catch (Exception ex)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = ex.Message;
            }

            finally
            {
                db2Rep.Close();
            }

        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dohandle"></param>
        public void Insert(TEntity entity, CommonDB2 db2Rep, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败" };
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("insert into {0} (", tableName);
            foreach (PropertyInfo p in entity.GetType().GetProperties())
            {
                sql = sql + p.Name + ",";
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql = sql + ") values('";

            foreach (PropertyInfo p in entity.GetType().GetProperties())
            {
                var pValue = p.GetValue(entity, null);
                if (pValue == null)
                {
                     var ptype = p.PropertyType.Name;
                     if (ptype == "Nullable`1")
                     {
                         sql = sql.Remove(sql.Length - 1, 1);
                         sql = sql + "null,'";
                     }
                     else if (ptype == "Decimal" || ptype == "Int32")
                     {
                         sql = sql + 0 + "','";
                     }
                     else
                     {
                         sql = sql + string.Empty + "','";
                     }
                }
                else
                {
                    sql = sql + pValue.ToString() + "','";
                }
            }
            sql = sql.Remove(sql.Length - 2, 2);
            sql = sql + ")";
            try
            {
                db2Rep.Execute(sql);
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }

            catch (Exception ex)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = ex.Message;
            }
            finally
            {
                db2Rep.Close();
            }


        }


        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="dohandle"></param>
        public void Insert(IList<TEntity> entities, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败" };
            CommonDB2 db2Rep = new CommonDB2();
            db2Rep.BeginTrans();
            foreach (var item in entities)
            {
                Insert(item, db2Rep,out dohandle);
            }
            try
            {
                db2Rep.CommitTrans();
            }
            catch (Exception ex)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = ex.Message;
            }
            finally
            {
                db2Rep.Close();
            }
        }

        
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="db2Rep"></param>
        /// <param name="needrTransaction"></param>
        /// <param name="dohandle"></param>
        public void Insert(IList<TEntity> entities, CommonDB2 db2Rep,out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败" };

            foreach (var item in entities)
            {
                Insert(item, db2Rep, out dohandle);
            }
         
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="updateParam"></param>
        /// <param name="dohandle"></param>
        public void Update(IDictionary<string, string> updateParam, IDictionary<string, string> queryParam, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="更新失败" };
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("update {0} set ", tableName);
            if (updateParam.Count != 0)
            {
                foreach (var q in updateParam)
                {
                    sql =sql+  q.Key + "='" + q.Value + "',";
                }
                sql = sql.Remove(sql.Length - 1, 1);
            }

            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
                sql = sql.Remove(sql.Length - 3, 3);
            }
            CommonDB2 db2Rep = new CommonDB2();
            try
            {
                db2Rep.Execute(sql);
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }

            catch (Exception ex)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = ex.Message;
            }
            finally
            {
                db2Rep.Close();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="updateParam"></param>
        /// <param name="queryParam"></param>
        /// <param name="db2Rep"></param>
        /// <param name="dohandle"></param>
        public void Update(IDictionary<string, string> updateParam, IDictionary<string, string> queryParam, CommonDB2 db2Rep, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "更新失败" };
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("update {0} set ", tableName);
            if (updateParam.Count != 0)
            {
                foreach (var q in updateParam)
                {
                    sql = sql + q.Key + "='" + q.Value + "',";
                }
                sql = sql.Remove(sql.Length - 1, 1);
            }

            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
                sql = sql.Remove(sql.Length - 3, 3);
            }
            try
            {
                db2Rep.Execute(sql);
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }

            catch (Exception ex)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = ex.Message;
            }
            finally
            {
                db2Rep.Close();
            }

        }




        /// <summary>
        /// 查询是否存在值
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public bool Any(IDictionary<string, string> queryParam)
        {
            var tableName = typeof(TEntity).Name;
            string sql = string.Format("select * from {0}", tableName);
            if (queryParam.Count != 0)
            {
                sql = sql + " where";
                foreach (var q in queryParam)
                {
                    sql = sql + " " + q.Key + "='" + q.Value + "' and";
                }
                sql = sql.Remove(sql.Length - 3, 3);
                sql = sql + " FETCH FIRST 1 ROWS ONLY";
            }
            CommonDB2 db2Rep = new CommonDB2();
            var dt = db2Rep.ExeForDtl(sql);
            var result = ConvertHelper<TEntity>.ConvertToList(dt);
            if (result.Count > 0)
            {
                db2Rep.Close();
                return true;
            }
            else
            {
                db2Rep.Close();
                return false;
            }
        }



    }
}
