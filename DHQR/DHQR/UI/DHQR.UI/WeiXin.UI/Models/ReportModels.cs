using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using Common.BLL.Implement;
using Common.Base;
using System.Web.Mvc;

namespace DHQR.UI.Models
{

    #region 折线图、柱状图

    /// <summary>
    /// 报表抬头
    /// </summary>
    public class ReportMaster
    {
        /// <summary>
        /// X轴集合
        /// </summary>
        public IList<string> xAxis { get; set; }

        /// <summary>
        /// 数据块
        /// </summary>
        public IList<ReportDetail> data { get; set; }
    }

    /// <summary>
    /// 报表明细
    /// </summary>
    public class ReportDetail
    {
        /// <summary>
        /// 分析类型
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Y轴
        /// </summary>
        public IList<int> data { get; set; }


        /// <summary>
        /// Y轴
        /// </summary>
        public IList<decimal> decimalData { get; set; }

    }


    #endregion

    /// <summary>
    /// 饼状图
    /// </summary>
    public class PieModel
    {
        public string name { set; get; }

        public int data { set; get; }


    }


    #region 带子表的柱状图

    /// <summary>
    /// 柱状图模型
    /// </summary>
    public class ColunReportModel
    {
        /// <summary>
        /// X轴集合
        /// </summary>
        public IList<string> xAxis { get; set; }

        /// <summary>
        /// 数据块
        /// </summary>
        public IList<ColunLineModel> data { get; set; }

    }

    /// <summary>
    /// 柱状图行模型
    /// </summary>
    public class ColunLineModel
    {
        /// <summary>
        /// 分析类型
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Y轴
        /// </summary>
        public IList<int> data { get; set; }


        /// <summary>
        /// Y轴
        /// </summary>
        public IList<decimal> decimalData { get; set; }

        /// <summary>
        /// 子表信息
        /// </summary>
        public ColunItemModel drilldown { get; set; }

    }

    /// <summary>
    /// 柱状图子行项目模型
    /// </summary>
    public class ColunItemModel
    {
        /// <summary>
        /// 分析类型
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 行名称
        /// </summary>
        public IList<string> categories { get; set; }

        /// <summary>
        /// Y轴
        /// </summary>
        public IList<int> data { get; set; }

        /// <summary>
        /// Y轴
        /// </summary>
        public IList<decimal> decimalData { get; set; }
    }

    #endregion


    #region 柱状百分比

    /// <summary>
    /// 柱状百分比
    /// </summary>
    public class ColumnRange
    {
        /// <summary>
        /// X轴集合
        /// </summary>
        public IList<string> xAxis { get; set; }

        /// <summary>
        /// 数据块
        /// </summary>
        public IList<decimal> data { get; set; }

    }

    #endregion
}