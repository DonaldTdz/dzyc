using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IBM.Data.DB2;
using IBM.Data.DB2Types;

namespace DHQR.DataAccess.Langchao
{
    /// <summary>
    /// 对ado.net中各个数据库操作类型的CommandBuilder加一适配
    /// 
    /// </summary>
    public interface ICommandBuilder
    {
        /// <summary>
        /// 设置数据适配器
        /// </summary>
        /// <param name="da"></param>
        void SetDataAdapter(IDataAdapter da);
    }

    /// <summary>
    /// db2
    /// </summary>
    public class DB2CmdBuilder : ICommandBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="da"></param>
        public void SetDataAdapter(IDataAdapter da)
        {
            DB2CommandBuilder cb = new DB2CommandBuilder((DB2DataAdapter)da);
        }
    }


    /// <summary>
    /// sql的命令建造者适配对象
    /// </summary>
    public class SqlCmdBuilder : ICommandBuilder
    {
        /// <summary>
        /// 设置数据适配器
        /// </summary>
        /// <param name="da">sql数据适配器</param>
        public void SetDataAdapter(IDataAdapter da)
        {
            SqlCommandBuilder cb = new SqlCommandBuilder((SqlDataAdapter)da);
        }
    }

    /// <summary>
    /// oledb的命令建造者适配对象
    /// </summary>
    public class OleDbCmdBuilder : ICommandBuilder
    {
        /// <summary>
        /// 设置数据适配器
        /// </summary>
        /// <param name="da">oledb数据适配器</param>
        public void SetDataAdapter(IDataAdapter da)
        {
            OleDbCommandBuilder cb = new OleDbCommandBuilder((OleDbDataAdapter)da);
        }
    }
}
