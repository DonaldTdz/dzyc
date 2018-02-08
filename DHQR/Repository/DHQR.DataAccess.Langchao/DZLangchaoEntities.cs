using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.DataAccess.Langchao
{
    /// <summary>
    /// 达州配送任务单信息
    /// </summary>
    public class DZ_I_DIST : I_DIST
    {
    }

    /// <summary>
    /// 达州订单信息
    /// </summary>
    public class DZ_I_DIST_LINE : I_DIST_LINE
    {
        public string LECENSE_CODE { get; set; }
    }

    /// <summary>
    /// 达州配送任务单商品信息
    /// </summary>
    public class DZ_I_DIST_ITME : I_DIST_ITEM
    {
    }

    /// <summary>
    /// 达州车辆信息
    /// </summary>
    public class DZ_I_DIST_CAR : I_DIST_CAR
    {
    }

    /// <summary>
    /// 达州送货员信息
    /// </summary>
    public class DZ_I_DIST_DLVMAN : I_DIST_DLVMAN
    {
    }

    /// <summary>
    /// 达州驾驶员信息
    /// </summary>
    public class DZ_I_DIST_DRIVER 
    {
        /// <summary>
        /// 驾驶员账号
        /// </summary>
        public string USER_ID { get; set; }

        /// <summary>
        /// 驾驶员名称
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 驾驶员岗位编码
        /// </summary>
        public string POSITION_CODE { get; set; }
    }

    /// <summary>
    /// 达州配送路线信息
    /// </summary>
    public class DZ_I_DIST_RUT : I_DIST_RUT
    {
    }

    /// <summary>
    /// 达州客户信息
    /// </summary>
    public class DZ_I_CUST : I_CUST
    {
        public string LICENSE_COCE { get; set; }
    }

    /// <summary>
    /// 获取用户的经纬度
    /// </summary>
    public class DZ_CUST_INFO
    {
        public string LICENSE_COCE { get; set; }

        public string CUST_ID { get; set; }

        public string LONGITUDE { get; set; }

        public string LATITUDE { get; set; }
    }

     
}
