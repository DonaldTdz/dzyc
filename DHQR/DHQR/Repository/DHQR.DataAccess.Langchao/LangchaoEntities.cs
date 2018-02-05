using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DHQR.DataAccess.Langchao
{
    #region 实体

    /// <summary>
    /// 退货单抬头
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_CO_RETURN 
    {
        #region Model
        private string _return_co_num;
        private string _cust_id;
        private string _type;
        private string _status;
        private string _crt_date;
        private string _crt_user_name;
        private string _org_co_num;
        private string _note;
        private decimal _amt_sum;
        private decimal _qty_sum;

        /// <summary>
        /// 退货订单编号
        /// </summary>
        public string RETURN_CO_NUM
        {
            set { _return_co_num = value; }
            get { return _return_co_num; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string CUST_ID
        {
            set { _cust_id = value; }
            get { return _cust_id; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string TYPE
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CRT_DATE
        {
            set { _crt_date = value; }
            get { return _crt_date; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CRT_USER_NAME
        {
            set { _crt_user_name = value; }
            get { return _crt_user_name; }
        }
        /// <summary>
        /// 原始订单编号
        /// </summary>
        public string ORG_CO_NUM
        {
            set { _org_co_num = value; }
            get { return _org_co_num; }
        }
        /// <summary>
        /// 注释
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal AMT_SUM
        {
            set { _amt_sum = value; }
            get { return _amt_sum; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal QTY_SUM
        {
            set { _qty_sum = value; }
            get { return _qty_sum; }
        }
        #endregion Model
    }

    /// <summary>
    /// 退货单明细
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_CO_RETURN_LINE 
    {

        #region Model
        private string _return_co_num;
        private int _line_num;
        private string _item_id;
        private decimal _qty_ord;
        private string _note;

        /// <summary>
        /// 退货订单编号
        /// </summary>
        public string RETURN_CO_NUM
        {
            set { _return_co_num = value; }
            get { return _return_co_num; }
        }
        /// <summary>
        /// 行号
        /// </summary>
        public int LINE_NUM
        {
            set { _line_num = value; }
            get { return _line_num; }
        }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string ITEM_ID
        {
            set { _item_id = value; }
            get { return _item_id; }
        }
        /// <summary>
        /// 退货数量
        /// </summary>
        public decimal QTY_ORD
        {
            set { _qty_ord = value; }
            get { return _qty_ord; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        #endregion Model

    }

    /// <summary>
    /// 客户信息
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_CUST 
    {

        #region Model
        private string _cust_id;
        private string _cust_name;
        private string _license_code;
        private string _status;
        private string _com_id;
        private string _card_id;
        private string _card_code;
        private string _order_tel;
        private string _busi_addr;
        private string _rut_id;

        /// <summary>
        /// 客户内码
        /// </summary>
        public string CUST_ID
        {
            set { _cust_id = value; }
            get { return _cust_id; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CUST_NAME
        {
            set { _cust_name = value; }
            get { return _cust_name; }
        }
        /// <summary>
        /// 专卖证号
        /// </summary>
        public string LICENSE_CODE
        {
            set { _license_code = value; }
            get { return _license_code; }
        }
        /// <summary>
        /// 客户状态
        /// </summary>
        public string STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 公司号
        /// </summary>
        public string COM_ID
        {
            set { _com_id = value; }
            get { return _com_id; }
        }

        /// <summary>
        /// NFC卡全球唯一码
        /// </summary>
        public string CARD_ID
        {
            set { _card_id = value; }
            get { return _card_id; }
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CARD_CODE
        {
            set { _card_code = value; }
            get { return _card_code; }
        }
        /// <summary>
        /// 订货电话
        /// </summary>
        public string ORDER_TEL
        {
            set { _order_tel = value; }
            get { return _order_tel; }
        }

        /// <summary>
        /// 经营地址
        /// </summary>
        public string BUSI_ADDR
        {
            set { _busi_addr = value; }
            get { return _busi_addr; }
        }

        /// <summary>
        /// 线路ID
        /// </summary>
        public string RUT_ID
        {
            set { _rut_id = value; }
            get { return _rut_id; }
        }

        #endregion Model

    }

    /// <summary>
    /// 到货确认信息
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST 
    {

        #region Model
        private string _dist_num;
        private string _rut_id;
        private string _rut_name;
        private string _dist_date;
        private string _dlvman_id;
        private string _dlvman_name;
        private string _driver_id;
        private string _driver_name;
        private string _car_id;
        private string _car_license;
        private int? _dist_cust_sum;
        private decimal? _qty_bar;
        private decimal? _amt_sum;
        private string _status;
        private string _is_download;
        private string _is_mrb;
        private string _driver_card_id;

        /// <summary>
        /// 配送单编号
        /// </summary>
        public string DIST_NUM
        {
            set { _dist_num = value; }
            get { return _dist_num; }
        }
        /// <summary>
        /// 线路编码
        /// </summary>
        public string RUT_ID
        {
            set { _rut_id = value; }
            get { return _rut_id; }
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RUT_NAME
        {
            set { _rut_name = value; }
            get { return _rut_name; }
        }
        /// <summary>
        /// 送货日期
        /// </summary>
        public string DIST_DATE
        {
            set { _dist_date = value; }
            get { return _dist_date; }
        }
        /// <summary>
        /// 送货员编码
        /// </summary>
        public string DLVMAN_ID
        {
            set { _dlvman_id = value; }
            get { return _dlvman_id; }
        }
        /// <summary>
        /// 送货员名称
        /// </summary>
        public string DLVMAN_NAME
        {
            set { _dlvman_name = value; }
            get { return _dlvman_name; }
        }
        /// <summary>
        /// 驾驶员编码
        /// </summary>
        public string DRIVER_ID
        {
            set { _driver_id = value; }
            get { return _driver_id; }
        }
        /// <summary>
        /// 驾驶员名称
        /// </summary>
        public string DRIVER_NAME
        {
            set { _driver_name = value; }
            get { return _driver_name; }
        }
        /// <summary>
        /// 送货车编码
        /// </summary>
        public string CAR_ID
        {
            set { _car_id = value; }
            get { return _car_id; }
        }
        /// <summary>
        /// 送货车车牌号
        /// </summary>
        public string CAR_LICENSE
        {
            set { _car_license = value; }
            get { return _car_license; }
        }
        /// <summary>
        /// 客户数
        /// </summary>
        public int? DIST_CUST_SUM
        {
            set { _dist_cust_sum = value; }
            get { return _dist_cust_sum; }
        }
        /// <summary>
        /// 送货量
        /// </summary>
        public decimal? QTY_BAR
        {
            set { _qty_bar = value; }
            get { return _qty_bar; }
        }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal? AMT_SUM
        {
            set { _amt_sum = value; }
            get { return _amt_sum; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 下载状态
        /// </summary>
        public string IS_DOWNLOAD
        {
            set { _is_download = value; }
            get { return _is_download; }
        }
        /// <summary>
        /// 是否使用
        /// </summary>
        public string IS_MRB
        {
            set { _is_mrb = value; }
            get { return _is_mrb; }
        }
        /// <summary>
        /// 送货员卡编码
        /// </summary>
        public string DRIVER_CARD_ID
        {
            set { _driver_card_id = value; }
            get { return _driver_card_id; }
        }
        #endregion Model

    }

    /// <summary>
    /// 配送车辆信息
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_CAR 
    {

        #region Model
        private string _car_id;
        private string _car_name;
        private string _car_license;
        private string _dlvman_id;
        private string _dlvman_name;
        private string _driver_id;
        private string _driver_name;
        private string _is_mrb;
        private string _com_id;
        private string _psw;
        /// <summary>
        /// 车辆编号
        /// </summary>
        public string CAR_ID
        {
            set { _car_id = value; }
            get { return _car_id; }
        }
        /// <summary>
        /// 车辆名称
        /// </summary>
        public string CAR_NAME
        {
            set { _car_name = value; }
            get { return _car_name; }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CAR_LICENSE
        {
            set { _car_license = value; }
            get { return _car_license; }
        }
        /// <summary>
        /// 送货员编码
        /// </summary>
        public string DLVMAN_ID
        {
            set { _dlvman_id = value; }
            get { return _dlvman_id; }
        }
        /// <summary>
        /// 送货员名称
        /// </summary>
        public string DLVMAN_NAME
        {
            set { _dlvman_name = value; }
            get { return _dlvman_name; }
        }
        /// <summary>
        /// 驾驶员编码
        /// </summary>
        public string DRIVER_ID
        {
            set { _driver_id = value; }
            get { return _driver_id; }
        }
        /// <summary>
        /// 驾驶员名称
        /// </summary>
        public string DRIVER_NAME
        {
            set { _driver_name = value; }
            get { return _driver_name; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string IS_MRB
        {
            set { _is_mrb = value; }
            get { return _is_mrb; }
        }
        /// <summary>
        /// 公司号
        /// </summary>
        public string COM_ID
        {
            set { _com_id = value; }
            get { return _com_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PSW
        {
            set { _psw = value; }
            get { return _psw; }
        }
        #endregion Model



    }

    /// <summary>
    /// 车辆检查信息
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_CAR_CHECK 
    {
        #region Model
        private string _check_id;
        private string _car_id;
        private string _ref_type;
        private string _ref_id;
        private string _abnormal_detail;
        private string _abnormal_type;
        private string _check_time;
        private decimal? _longitude;
        private decimal? _latitude;
        private string _check_type;
        private string _operate_mode;
        private string _note;

        /// <summary>
        /// 车辆检查流水号
        /// </summary>
        public string CHECK_ID
        {
            set { _check_id = value; }
            get { return _check_id; }
        }
        /// <summary>
        /// 送货车编码
        /// </summary>
        public string CAR_ID
        {
            set { _car_id = value; }
            get { return _car_id; }
        }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string REF_TYPE
        {
            set { _ref_type = value; }
            get { return _ref_type; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string REF_ID
        {
            set { _ref_id = value; }
            get { return _ref_id; }
        }
        /// <summary>
        /// 三检异常明细
        /// </summary>
        public string ABNORMAL_DETAIL
        {
            set { _abnormal_detail = value; }
            get { return _abnormal_detail; }
        }
        /// <summary>
        /// 三检异常类型
        /// </summary>
        public string ABNORMAL_TYPE
        {
            set { _abnormal_type = value; }
            get { return _abnormal_type; }
        }
        /// <summary>
        /// 检查时间
        /// </summary>
        public string CHECK_TIME
        {
            set { _check_time = value; }
            get { return _check_time; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? LONGITUDE
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? LATITUDE
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 检查类型
        /// </summary>
        public string CHECK_TYPE
        {
            set { _check_type = value; }
            get { return _check_type; }
        }
        /// <summary>
        /// 操作方式
        /// </summary>
        public string OPERATE_MODE
        {
            set { _operate_mode = value; }
            get { return _operate_mode; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        #endregion Model



    }

    /// <summary>
    /// 回程登记抬头
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_CAR_RUN 
    {
        #region Model
        private string _info_num;
        private string _ref_type;
        private string _ref_id;
        private string _car_id;
        private string _dlvman_id;
        private string _crt_date;
        private decimal? _amt_sum;
        private decimal? _pre_mil;
        private decimal? _this_mil;
        private decimal? _act_mil;
        private string _note;
        /// <summary>
        /// 流水号
        /// </summary>
        public string INFO_NUM
        {
            set { _info_num = value; }
            get { return _info_num; }
        }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string REF_TYPE
        {
            set { _ref_type = value; }
            get { return _ref_type; }
        }
        /// <summary>
        /// 配送单编号
        /// </summary>
        public string REF_ID
        {
            set { _ref_id = value; }
            get { return _ref_id; }
        }
        /// <summary>
        /// 送货车编码
        /// </summary>
        public string CAR_ID
        {
            set { _car_id = value; }
            get { return _car_id; }
        }
        /// <summary>
        /// 送货员编码
        /// </summary>
        public string DLVMAN_ID
        {
            set { _dlvman_id = value; }
            get { return _dlvman_id; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public string CRT_DATE
        {
            set { _crt_date = value; }
            get { return _crt_date; }
        }
        /// <summary>
        /// 费用总和
        /// </summary>
        public decimal? AMT_SUM
        {
            set { _amt_sum = value; }
            get { return _amt_sum; }
        }

        /// <summary>
        /// 上期里程数
        /// </summary>
        public decimal? PRE_MIL
        {
            set { _pre_mil = value; }
            get { return _pre_mil; }
        }

        /// <summary>
        /// 本期里程数
        /// </summary>
        public decimal? THIS_MIL
        {
            set { _this_mil = value; }
            get { return _this_mil; }
        }

        /// <summary>
        /// 行驶里程数
        /// </summary>
        public decimal? ACT_MIL
        {
            set { _act_mil = value; }
            get { return _act_mil; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        #endregion Model

    }

    /// <summary>
    /// 回程登记明细
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_CAR_RUN_LINE 
    {
        #region Model
        private string _info_num;
        private int _line_id;
        private string _cost_type;
        private string _fuel_type;
        private decimal? _litre_sum;
        private decimal? _fuel_pri;
        private decimal _amt;
        private string _inv_num;
        /// <summary>
        /// 流水号(头表流水号)
        /// </summary>
        public string INFO_NUM
        {
            set { _info_num = value; }
            get { return _info_num; }
        }
        /// <summary>
        /// 行号
        /// </summary>
        public int LINE_ID
        {
            set { _line_id = value; }
            get { return _line_id; }
        }
        /// <summary>
        /// 费用类型
        /// </summary>
        public string COST_TYPE
        {
            set { _cost_type = value; }
            get { return _cost_type; }
        }
        /// <summary>
        /// 燃油类型
        /// </summary>
        public string FUEL_TYPE
        {
            set { _fuel_type = value; }
            get { return _fuel_type; }
        }
        /// <summary>
        /// 加油总量
        /// </summary>
        public decimal? LITRE_SUM
        {
            set { _litre_sum = value; }
            get { return _litre_sum; }
        }
        /// <summary>
        /// 燃油单价
        /// </summary>
        public decimal? FUEL_PRI
        {
            set { _fuel_pri = value; }
            get { return _fuel_pri; }
        }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal AMT
        {
            set { _amt = value; }
            get { return _amt; }
        }
        /// <summary>
        /// 相关票据
        /// </summary>
        public string INV_NUM
        {
            set { _inv_num = value; }
            get { return _inv_num; }
        }
        #endregion Model

    }

    /// <summary>
    /// 配送任务
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_CUST 
    {
        #region Model
        private string _dist_num;
        private string _co_num;
        private string _cust_id;
        private string _is_received;
        private string _dist_satis;
        private string _unload_reason;
        private string _rec_date;
        private string _rec_arrive_time;
        private string _rec_leave_time;
        private decimal _handover_time;
        private string _notsatis_reason;
        private string _unusual_type;
        private string _evaluate_info;
        private string _sign_type;
        private string _exp_sign_reason;
        private decimal? _unload_lon;
        private decimal? _unload_lat;
        private decimal? _unload_distance;
        private string _evaluate_time;
        private string _dlvman_id;
        /// <summary>
        /// 配送单编号
        /// </summary>
        public string DIST_NUM
        {
            set { _dist_num = value; }
            get { return _dist_num; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string CO_NUM
        {
            set { _co_num = value; }
            get { return _co_num; }
        }
        /// <summary>
        /// 客户内码
        /// </summary>
        public string CUST_ID
        {
            set { _cust_id = value; }
            get { return _cust_id; }
        }
        /// <summary>
        /// 收货方式
        /// </summary>
        public string IS_RECEIVED
        {
            set { _is_received = value; }
            get { return _is_received; }
        }
        /// <summary>
        /// 送货满意度
        /// </summary>
        public string DIST_SATIS
        {
            set { _dist_satis = value; }
            get { return _dist_satis; }
        }
        /// <summary>
        /// 收货状态
        /// </summary>
        public string UNLOAD_REASON
        {
            set { _unload_reason = value; }
            get { return _unload_reason; }
        }
        /// <summary>
        /// 实际送达日期
        /// </summary>
        public string REC_DATE
        {
            set { _rec_date = value; }
            get { return _rec_date; }
        }
        /// <summary>
        /// 实际到达时间
        /// </summary>
        public string REC_ARRIVE_TIME
        {
            set { _rec_arrive_time = value; }
            get { return _rec_arrive_time; }
        }
        /// <summary>
        /// 实际离开时间
        /// </summary>
        public string REC_LEAVE_TIME
        {
            set { _rec_leave_time = value; }
            get { return _rec_leave_time; }
        }
        /// <summary>
        /// 交接时间
        /// </summary>
        public decimal HANDOVER_TIME
        {
            set { _handover_time = value; }
            get { return _handover_time; }
        }
        /// <summary>
        /// 不满意原因
        /// </summary>
        public string NOTSATIS_REASON
        {
            set { _notsatis_reason = value; }
            get { return _notsatis_reason; }
        }
        /// <summary>
        /// 异常处理方式
        /// </summary>
        public string UNUSUAL_TYPE
        {
            set { _unusual_type = value; }
            get { return _unusual_type; }
        }
        /// <summary>
        /// 送货评价
        /// </summary>
        public string EVALUATE_INFO
        {
            set { _evaluate_info = value; }
            get { return _evaluate_info; }
        }
        /// <summary>
        /// 签到方式
        /// </summary>
        public string SIGN_TYPE
        {
            set { _sign_type = value; }
            get { return _sign_type; }
        }
        /// <summary>
        /// 异常签到原因
        /// </summary>
        public string EXP_SIGN_REASON
        {
            set { _exp_sign_reason = value; }
            get { return _exp_sign_reason; }
        }
        /// <summary>
        /// 卸货经度
        /// </summary>
        public decimal? UNLOAD_LON
        {
            set { _unload_lon = value; }
            get { return _unload_lon; }
        }
        /// <summary>
        /// 卸货纬度
        /// </summary>
        public decimal? UNLOAD_LAT
        {
            set { _unload_lat = value; }
            get { return _unload_lat; }
        }
        /// <summary>
        /// 卸货距离
        /// </summary>
        public decimal? UNLOAD_DISTANCE
        {
            set { _unload_distance = value; }
            get { return _unload_distance; }
        }
        /// <summary>
        /// 评价时间
        /// </summary>
        public string EVALUATE_TIME
        {
            set { _evaluate_time = value; }
            get { return _evaluate_time; }
        }
        /// <summary>
        /// 送货员编码
        /// </summary>
        public string DLVMAN_ID
        {
            set { _dlvman_id = value; }
            get { return _dlvman_id; }
        }

        #endregion Model



    }

    /// <summary>
    /// 日志信息
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_FILE_LINE 
    {
        #region Model
        private string _doc_id;
        private string _dist_num;
        private string _co_num;
        private string _cust_id;
        private string _file_type;
        private string _crt_time;
        private string _crt_man_id;
        private string _note;
        /// <summary>
        /// 附件编号
        /// </summary>
        public string DOC_ID
        {
            set { _doc_id = value; }
            get { return _doc_id; }
        }
        /// <summary>
        /// 配送单编码
        /// </summary>
        public string DIST_NUM
        {
            set { _dist_num = value; }
            get { return _dist_num; }
        }
        /// <summary>
        /// 订单编码
        /// </summary>
        public string CO_NUM
        {
            set { _co_num = value; }
            get { return _co_num; }
        }
        /// <summary>
        /// 零售户编码
        /// </summary>
        public string CUST_ID
        {
            set { _cust_id = value; }
            get { return _cust_id; }
        }
        /// <summary>
        /// 附件类型
        /// </summary>
        public string FILE_TYPE
        {
            set { _file_type = value; }
            get { return _file_type; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CRT_TIME
        {
            set { _crt_time = value; }
            get { return _crt_time; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CRT_MAN_ID
        {
            set { _crt_man_id = value; }
            get { return _crt_man_id; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        #endregion Model

    }

    /// <summary>
    /// 订单明细
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_ITEM 
    {
        #region Model
        private string _co_num;
        private string _item_id;
        private string _item_name;
        private decimal _price;
        private decimal _qty;
        private decimal _amt;
        private string _is_abnormal;
 
        /// <summary>
        /// 订单编号
        /// </summary>
        public string CO_NUM
        {
            set { _co_num = value; }
            get { return _co_num; }
        }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string ITEM_ID
        {
            set { _item_id = value; }
            get { return _item_id; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ITEM_NAME
        {
            set { _item_name = value; }
            get { return _item_name; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal PRICE
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal QTY
        {
            set { _qty = value; }
            get { return _qty; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal AMT
        {
            set { _amt = value; }
            get { return _amt; }
        }
        /// <summary>
        /// 是否异性烟
        /// </summary>
        public string IS_ABNORMAL
        {
            set { _is_abnormal = value; }
            get { return _is_abnormal; }
        }
        #endregion Model


    }

    /// <summary>
    /// 订单信息
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_LINE 
    {
        #region Model
        private string _dist_num;
        private string _co_num;
        private string _cust_id;
        private string _cust_code;
        private string _cust_name;
        private string _manager;
        private string _addr;
        private string _tel;
        private decimal _qty_bar;
        private decimal _amt_ar;
        private decimal? _amt_or;
        private string _pmt_status;
        private string _type;
        private int _seq;
        private string _license_code;
        private string _pay_type;
        private decimal? _longitude;
        private decimal? _latitude;
        private string _cust_card_id;
        private string _cust_card_code;
      
        /// <summary>
        /// 配送单编号
        /// </summary>
        public string DIST_NUM
        {
            set { _dist_num = value; }
            get { return _dist_num; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string CO_NUM
        {
            set { _co_num = value; }
            get { return _co_num; }
        }
        /// <summary>
        /// 客户内码
        /// </summary>
        public string CUST_ID
        {
            set { _cust_id = value; }
            get { return _cust_id; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string CUST_CODE
        {
            set { _cust_code = value; }
            get { return _cust_code; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CUST_NAME
        {
            set { _cust_name = value; }
            get { return _cust_name; }
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public string MANAGER
        {
            set { _manager = value; }
            get { return _manager; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string ADDR
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string TEL
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal QTY_BAR
        {
            set { _qty_bar = value; }
            get { return _qty_bar; }
        }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal AMT_AR
        {
            set { _amt_ar = value; }
            get { return _amt_ar; }
        }
        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal? AMT_OR
        {
            set { _amt_or = value; }
            get { return _amt_or; }
        }
        /// <summary>
        /// 付款状态
        /// </summary>
        public string PMT_STATUS
        {
            set { _pmt_status = value; }
            get { return _pmt_status; }
        }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string TYPE
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 送货顺序
        /// </summary>
        public int SEQ
        {
            set { _seq = value; }
            get { return _seq; }
        }
        /// <summary>
        /// 许可证专卖号
        /// </summary>
        public string LICENSE_CODE
        {
            set { _license_code = value; }
            get { return _license_code; }
        }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string PAY_TYPE
        {
            set { _pay_type = value; }
            get { return _pay_type; }
        }
        /// <summary>
        /// 位置经度
        /// </summary>
        public decimal? LONGITUDE
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 位置纬度
        /// </summary>
        public decimal? LATITUDE
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 零售户物理卡全球唯一码
        /// </summary>
        public string CUST_CARD_ID
        {
            set { _cust_card_id = value; }
            get { return _cust_card_id; }
        }
        /// <summary>
        /// 零售户物理卡编号
        /// </summary>
        public string CUST_CARD_CODE
        {
            set { _cust_card_code = value; }
            get { return _cust_card_code; }
        }
        #endregion Model

    }

    /// <summary>
    /// 送货员操作日志表
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_RECORD_LOG 
    {

        #region Model
        private string _log_seq;
        private string _ref_type;
        private string _ref_id;
        private string _operation_type;
        private string _log_date;
        private string _log_time;
        private string _user_id;
        private decimal? _longitude;
        private decimal? _latitude;
        private string _note;
        private string _operate_mode;
       
        /// <summary>
        /// 流水号
        /// </summary>
        public string LOG_SEQ
        {
            set { _log_seq = value; }
            get { return _log_seq; }
        }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string REF_TYPE
        {
            set { _ref_type = value; }
            get { return _ref_type; }
        }
        /// <summary>
        /// 配送任务单编号
        /// </summary>
        public string REF_ID
        {
            set { _ref_id = value; }
            get { return _ref_id; }
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OPERATION_TYPE
        {
            set { _operation_type = value; }
            get { return _operation_type; }
        }
        /// <summary>
        /// 操作日期
        /// </summary>
        public string LOG_DATE
        {
            set { _log_date = value; }
            get { return _log_date; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string LOG_TIME
        {
            set { _log_time = value; }
            get { return _log_time; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string USER_ID
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 操作经度
        /// </summary>
        public decimal? LONGITUDE
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 操作纬度
        /// </summary>
        public decimal? LATITUDE
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 操作方式
        /// </summary>
        public string OPERATE_MODE
        {
            set { _operate_mode = value; }
            get { return _operate_mode; }
        }
        #endregion Model

    }

    /// <summary>
    /// 客户经纬度
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_GIS_CUST_POI 
    {
        #region Model
        private string _cust_id;
        private string _mobile_type;
        private decimal? _original_longitude;
        private decimal? _original_latitude;
        private string _is_default;
        private string _crt_time;
        private string _crt_user_id;
        private string _col_time;
        private string _imp_status;
        private string _note;
      
        /// <summary>
        /// 零售户编码
        /// </summary>
        public string CUST_ID
        {
            set { _cust_id = value; }
            get { return _cust_id; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string MOBILE_TYPE
        {
            set { _mobile_type = value; }
            get { return _mobile_type; }
        }
        /// <summary>
        /// 原始经度
        /// </summary>
        public decimal? ORIGINAL_LONGITUDE
        {
            set { _original_longitude = value; }
            get { return _original_longitude; }
        }
        /// <summary>
        /// 原始纬度
        /// </summary>
        public decimal? ORIGINAL_LATITUDE
        {
            set { _original_latitude = value; }
            get { return _original_latitude; }
        }
        /// <summary>
        /// 是否默认
        /// </summary>
        public string IS_DEFAULT
        {
            set { _is_default = value; }
            get { return _is_default; }
        }
        /// <summary>
        /// 插入时间
        /// </summary>
        public string CRT_TIME
        {
            set { _crt_time = value; }
            get { return _crt_time; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public string CRT_USER_ID
        {
            set { _crt_user_id = value; }
            get { return _crt_user_id; }
        }
        /// <summary>
        /// 采集时间
        /// </summary>
        public string COL_TIME
        {
            set { _col_time = value; }
            get { return _col_time; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string IMP_STATUS
        {
            set { _imp_status = value; }
            get { return _imp_status; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        #endregion Model

    }

    /// <summary>
    /// 送货员经纬度
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_GIS_LAST_LOCRECORD 
    {
        #region Model
        private string _m_code;
        private string _m_type;
        private decimal _original_longitude;
        private decimal _original_latitude;
        private decimal _speed;
        private decimal _direction;
        private decimal _height;
        private int _statllite_num;
        private string _gpstime;
        private string _inputdate;
        private string _state;

        /// <summary>
        /// 移动编码
        /// </summary>
        public string M_CODE
        {
            set { _m_code = value; }
            get { return _m_code; }
        }
        /// <summary>
        /// 移动类型
        /// </summary>
        public string M_TYPE
        {
            set { _m_type = value; }
            get { return _m_type; }
        }
        /// <summary>
        /// 原始经度
        /// </summary>
        public decimal ORIGINAL_LONGITUDE
        {
            set { _original_longitude = value; }
            get { return _original_longitude; }
        }
        /// <summary>
        /// 原始纬度
        /// </summary>
        public decimal ORIGINAL_LATITUDE
        {
            set { _original_latitude = value; }
            get { return _original_latitude; }
        }
        /// <summary>
        /// 速度
        /// </summary>
        public decimal SPEED
        {
            set { _speed = value; }
            get { return _speed; }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public decimal DIRECTION
        {
            set { _direction = value; }
            get { return _direction; }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public decimal HEIGHT
        {
            set { _height = value; }
            get { return _height; }
        }
        /// <summary>
        /// 卫星数
        /// </summary>
        public int STATLLITE_NUM
        {
            set { _statllite_num = value; }
            get { return _statllite_num; }
        }
        /// <summary>
        /// 卫星时间
        /// </summary>
        public string GPSTIME
        {
            set { _gpstime = value; }
            get { return _gpstime; }
        }
        /// <summary>
        /// 写入时间
        /// </summary>
        public string INPUTDATE
        {
            set { _inputdate = value; }
            get { return _inputdate; }
        }
        /// <summary>
        /// 定位状态
        /// </summary>
        public string STATE
        {
            set { _state = value; }
            get { return _state; }
        }
        #endregion Model

    }

    /// <summary>
    /// 配送员基础信息
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_DLVMAN
    {
        #region Model
        private Guid _id;
        private string _user_id;
        private string _user_name;
        private string _organ_id;
        private string _position_code;
        private string _com_id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 送货员帐号
        /// </summary>
        public string USER_ID
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 送货员名称
        /// </summary>
        public string USER_NAME
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 送货员组织编码
        /// </summary>
        public string ORGAN_ID
        {
            set { _organ_id = value; }
            get { return _organ_id; }
        }
        /// <summary>
        /// 送货员岗位编码
        /// </summary>
        public string POSITION_CODE
        {
            set { _position_code = value; }
            get { return _position_code; }
        }
        /// <summary>
        /// 公司号
        /// </summary>
        public string COM_ID
        {
            set { _com_id = value; }
            get { return _com_id; }
        }

        #endregion Model

    }

     /// <summary>
    /// 配送线路基础信息
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_DIST_RUT
    {
        #region Model
        private string _rut_id;
        private string _rut_name;
        private string _is_mrb;
        private string _com_id;
        /// <summary>
        ///线路ID
        /// </summary>
        public string RUT_ID
        {
            set { _rut_id = value; }
            get { return _rut_id; }
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RUT_NAME
        {
            set { _rut_name = value; }
            get { return _rut_name; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string IS_MRB
        {
            set { _is_mrb = value; }
            get { return _is_mrb; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string COM_ID
        {
            set { _com_id = value; }
            get { return _com_id; }
        }
        #endregion Model
    }




    /// <summary>
    /// 暂存
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_CO_TEMP
    {
        #region Model
        private string _co_num;
        private string _dist_num;
        private string _ret_time;
        private string _ret_user_id;
        private string _status;
        /// <summary>
        /// 
        /// </summary>
        public string DIST_NUM
        {
            set { _dist_num = value; }
            get { return _dist_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CO_NUM
        {
            set { _co_num = value; }
            get { return _co_num; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RET_TIME
        {
            set { _ret_time = value; }
            get { return _ret_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RET_USER_ID
        {
            set { _ret_user_id = value; }
            get { return _ret_user_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model
    }



    /// <summary>
    /// 暂存订单
    /// </summary>
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class I_CO_TEMP_RETURN
    {
        #region Model
        private string _dist_num;
        private string _co_num;
        private string _status;
        private string _out_dist_num;
        /// <summary>
        /// 配送单编号
        /// </summary>
        public string DIST_NUM
        {
            set { _dist_num = value; }
            get { return _dist_num; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string CO_NUM
        {
            set { _co_num = value; }
            get { return _co_num; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 再次出库配送单编码
        /// </summary>
        public string OUT_DIST_NUM
        {
            set { _out_dist_num = value; }
            get { return _out_dist_num; }
        }
        #endregion Model

    }

    #endregion
}
