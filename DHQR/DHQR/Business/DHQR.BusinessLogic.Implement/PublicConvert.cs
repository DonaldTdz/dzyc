using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Langchao;

namespace DHQR.BusinessLogic.Implement
{

    /// <summary>
    /// 浪潮数据转换
    /// </summary>
    public static class ConvertFromLC
    {

        #region 浪潮TO和创

        /// <summary>
        /// 转换车辆信息
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static LdmDistCar ConvertCar(I_DIST_CAR car)
        {
            LdmDistCar result = new LdmDistCar
            {
                Id = Guid.NewGuid(),
                CAR_ID = car.CAR_ID,
                CAR_NAME = car.CAR_NAME,
                CAR_LICENSE = car.CAR_LICENSE,
                DLVMAN_ID = car.DLVMAN_ID,
                DLVMAN_NAME = car.DLVMAN_NAME,
                DRIVER_ID = car.DRIVER_ID,
                DRIVER_NAME = car.DRIVER_NAME,
                IS_MRB = car.IS_MRB,
                COM_ID = car.COM_ID,
                PSW = string.Format("c33367701511b4f6020ec61ded352059")
            };
            return result;
        }

        /// <summary>
        /// 转换配送单
        /// </summary>
        /// <param name="dist"></param>
        /// <returns></returns>
        public static LdmDist ConvertDist(I_DIST dist)
        {

            LdmDist result = new LdmDist
            {
                Id=Guid.NewGuid(),
                DIST_NUM=dist.DIST_NUM,
                RUT_ID = dist.RUT_ID,
                RUT_NAME = dist.RUT_NAME,
                DIST_DATE = dist.DIST_DATE,
                DLVMAN_ID = dist.DLVMAN_ID,
                DLVMAN_NAME = dist.DLVMAN_NAME,
                DRIVER_ID = dist.DRIVER_ID,
                DRIVER_NAME = dist.DRIVER_NAME,
                CAR_ID = dist.CAR_ID,
                CAR_LICENSE = dist.CAR_LICENSE,
                DIST_CUST_SUM =dist.DIST_CUST_SUM.HasValue?dist.DIST_CUST_SUM.Value:0,
                QTY_BAR = dist.QTY_BAR.HasValue ? dist.QTY_BAR.Value : 0,
                AMT_SUM = dist.AMT_SUM.HasValue ? dist.AMT_SUM.Value : 0,
                STATUS = dist.STATUS,
                IS_DOWNLOAD = dist.IS_DOWNLOAD,
                IS_MRB = dist.IS_MRB,
                DRIVER_CARD_ID = string.Empty
            };
            return result;
        }

        /// <summary>
        /// 转换订单
        /// </summary>
        /// <param name="dist"></param>
        /// <returns></returns>
        public static LdmDistLine ConvertDistLine(I_DIST_LINE L)
        {
            LdmDistLine result = new LdmDistLine 
            {
                Id=Guid.NewGuid(),
                DIST_NUM=L.DIST_NUM,
                CO_NUM = L.CO_NUM,
                CUST_ID = L.CUST_ID,
                CUST_CODE = L.CUST_CODE,
                CUST_NAME = L.CUST_NAME,
                MANAGER = L.MANAGER,
                ADDR = L.ADDR,
                TEL = L.TEL,
                QTY_BAR = L.QTY_BAR,
                AMT_AR = L.AMT_AR,
                AMT_OR = L.AMT_OR.HasValue?L.AMT_OR.Value:0,
                PMT_STATUS = L.PMT_STATUS,
                TYPE = L.TYPE,
                SEQ = L.SEQ,
                LICENSE_CODE = L.LICENSE_CODE,
                PAY_TYPE = L.PAY_TYPE,
                LONGITUDE = L.LONGITUDE,
                LATITUDE = L.LATITUDE,
                CUST_CARD_ID = L.CUST_CARD_ID,
                CUST_CARD_CODE = L.CUST_CARD_CODE

            };
            return result;
        }

        /// <summary>
        /// 转换订单明细
        /// </summary>
        /// <param name="L"></param>
        /// <returns></returns>
        public static LdmDisItem ConvertDistItem(I_DIST_ITEM L)
        {
            LdmDisItem result = new LdmDisItem 
            {
                Id=Guid.NewGuid(),
                CO_NUM=L.CO_NUM,
                ITEM_ID = L.ITEM_ID,
                ITEM_NAME = L.ITEM_NAME,
                PRICE = L.PRICE,
                QTY = L.QTY,
                AMT = L.AMT,
                IS_ABNORMAL = L.IS_ABNORMAL,

            };
            return result;
        }

        /// <summary>
        /// 转换暂存订单
        /// </summary>
        /// <param name="dist"></param>
        /// <returns></returns>
        public static CoTempReturn ConvertCoTempReturn(I_CO_TEMP_RETURN L)
        {
            CoTempReturn result = new CoTempReturn
            {
                Id = Guid.NewGuid(),
                DIST_NUM=L.DIST_NUM,
                CO_NUM=L.CO_NUM,
                STATUS=L.STATUS,
                OUT_DIST_NUM=L.OUT_DIST_NUM

            };
            return result;
        }



        /// <summary>
        /// 转换客户信息
        /// </summary>
        /// <param name="cust"></param>
        /// <returns></returns>
        public static Retailer ConvertRetailer(I_CUST cust)
        {
            Retailer result = new Retailer 
            {
                Id=Guid.NewGuid(),
                CUST_ID=cust.CUST_ID,
                CUST_NAME=cust.CUST_NAME,
                LICENSE_CODE=cust.LICENSE_CODE,
                STATUS=cust.STATUS,
                COM_ID=cust.COM_ID,
                PSW = string.Format("c33367701511b4f6020ec61ded352059"),
                CARD_ID=cust.CARD_ID,
                CARD_CODE=cust.CARD_CODE,
                ORDER_TEL=cust.ORDER_TEL,
                BUSI_ADDR=cust.BUSI_ADDR,
                RUT_ID=cust.RUT_ID
            };
            return result;
        }


        /// <summary>
        /// 转换操作日志信息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static DistRecordLog ConvertRecordLog(I_DIST_RECORD_LOG log)
        {
            DistRecordLog result = new DistRecordLog
            {
                LOG_SEQ = log.LOG_SEQ,
                REF_TYPE = log.REF_TYPE,
                REF_ID = log.REF_ID,
                OPERATION_TYPE = log.OPERATION_TYPE,
                LOG_DATE = log.LOG_DATE,
                LOG_TIME = log.LOG_TIME,
                USER_ID = log.USER_ID,
                LONGITUDE = log.LONGITUDE,
                LATITUDE = log.LATITUDE,
                NOTE = log.NOTE,
                OPERATE_MODE = log.OPERATE_MODE
            };
            return result;
        }

        /// <summary>
        /// 转换配送员信息
        /// </summary>
        /// <param name="dlvman"></param>
        /// <returns></returns>
        public static DistDlvman ConvertDistDlvman(I_DIST_DLVMAN dlvman)
        {
            DistDlvman result = new DistDlvman 
            {
                USER_ID=dlvman.USER_ID,
                USER_NAME=dlvman.USER_NAME,
                ORGAN_ID=dlvman.ORGAN_ID,
                POSITION_CODE=dlvman.POSITION_CODE,
                COM_ID=dlvman.COM_ID,
                PSW = string.Format("c33367701511b4f6020ec61ded352059")//默认密码654321
            };
            return result;
        }

        /// <summary>
        /// 转换线路信息
        /// </summary>
        /// <param name="dlvman"></param>
        /// <returns></returns>
        public static DistRut ConvertDistRut(I_DIST_RUT rut)
        {
            DistRut result = new DistRut
            {
             Id=Guid.NewGuid(),
             RUT_ID=rut.RUT_ID,
             RUT_NAME=rut.RUT_NAME,
             IS_MRB=rut.IS_MRB,
             COM_ID=rut.COM_ID
            };
            return result;
        }

        /// <summary>
        /// 转换零售户位置信息
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static GisCustPois ConvertCustPoi(I_GIS_CUST_POI poi)
        {
            GisCustPois result = new GisCustPois
            {
                CUST_ID = poi.CUST_ID,
                MOBILE_TYPE = poi.MOBILE_TYPE,
                ORIGINAL_LONGITUDE = poi.ORIGINAL_LONGITUDE,
                ORIGINAL_LATITUDE = poi.ORIGINAL_LATITUDE,
                IS_DEFAULT = poi.IS_DEFAULT,
                CRT_TIME = poi.CRT_TIME,
                CRT_USER_ID = poi.CRT_USER_ID,
                COL_TIME = poi.COL_TIME,
                IMP_STATUS = poi.IMP_STATUS,
                NOTE = poi.NOTE

            };
            return result;
        }


        #endregion


    }

    /// <summary>
    /// 数据转换到浪潮
    /// </summary>
    public static class ConvertToLC
    {
        #region 和创TO浪潮

        /// <summary>
        /// 转换车辆检查信息
        /// </summary>
        /// <param name="chek"></param>
        /// <returns></returns>
        public static I_DIST_CAR_CHECK ConvertCarCheck(DistCarCheck chek)
        {
            I_DIST_CAR_CHECK result = new I_DIST_CAR_CHECK
            {
                CHECK_ID = chek.CHECK_ID,
                CAR_ID = chek.CAR_ID,
                REF_TYPE = chek.REF_TYPE,
                REF_ID = chek.REF_ID,
                ABNORMAL_DETAIL = chek.ABNORMAL_DETAIL,
                ABNORMAL_TYPE = chek.ABNORMAL_TYPE,
                CHECK_TIME = chek.CHECK_TIME,
                LONGITUDE = chek.LONGITUDE,
                LATITUDE = chek.LATITUDE,
                CHECK_TYPE = chek.CHECK_TYPE,
                OPERATE_MODE = chek.OPERATE_MODE,
                NOTE = chek.NOTE
            };
            return result;
 
        }

        /// <summary>
        /// 转换操作日志信息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static I_DIST_RECORD_LOG ConvertRecordLog(DistRecordLog log)
        {
            I_DIST_RECORD_LOG result = new I_DIST_RECORD_LOG 
            {
                LOG_SEQ=log.LOG_SEQ,
                REF_TYPE = log.REF_TYPE,
                REF_ID = log.REF_ID,
                OPERATION_TYPE = log.OPERATION_TYPE,
                LOG_DATE = log.LOG_DATE,
                LOG_TIME = log.LOG_TIME,
                USER_ID = log.USER_ID,
                LONGITUDE = log.LONGITUDE,
                LATITUDE = log.LATITUDE,
                NOTE = log.NOTE,
                OPERATE_MODE = log.OPERATE_MODE
            };
            return result;
        }


        /// <summary>
        /// 转换车辆运行表头
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static I_DIST_CAR_RUN ConvertCarRun(DistCarRun car)
        {
            I_DIST_CAR_RUN result = new I_DIST_CAR_RUN 
            {
                INFO_NUM =car.INFO_NUM,
                REF_TYPE = car.REF_TYPE,
                REF_ID = car.REF_ID,
                CAR_ID = car.CAR_ID,
                DLVMAN_ID = car.DLVMAN_ID,
                CRT_DATE = car.CRT_DATE,
                AMT_SUM = car.AMT_SUM,
                PRE_MIL=car.PRE_MIL,
                THIS_MIL = car.THIS_MIL,
                ACT_MIL=car.ACT_MIL,
                NOTE = car.NOTE
            };

            return result;
        }


        /// <summary>
        /// 转换车辆运行明细
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static I_DIST_CAR_RUN_LINE ConvertCarRunLine(DistCarRunLine line)
        {
            I_DIST_CAR_RUN_LINE result = new I_DIST_CAR_RUN_LINE
            {
                INFO_NUM =line.INFO_NUM,
                LINE_ID = line.LINE_ID,
                COST_TYPE = line.COST_TYPE,
                FUEL_TYPE = line.FUEL_TYPE,
                LITRE_SUM = line.LITRE_SUM,
                FUEL_PRI = line.FUEL_PRI,
                AMT = line.AMT,
                INV_NUM = line.INV_NUM
            };

            return result;
        }

        /// <summary>
        /// 转换到货确认信息
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static I_DIST_CUST ConvertDistCust(DistCust cust)
        {
            I_DIST_CUST result = new I_DIST_CUST
            {
               DIST_NUM=cust.DIST_NUM,
               CO_NUM = cust.CO_NUM,
               CUST_ID = cust.CUST_ID,
               IS_RECEIVED = cust.IS_RECEIVED,
               DIST_SATIS = cust.DIST_SATIS,
               UNLOAD_REASON = cust.UNLOAD_REASON,
               REC_DATE = cust.REC_DATE,
               REC_ARRIVE_TIME = cust.REC_ARRIVE_TIME,
               REC_LEAVE_TIME = cust.REC_LEAVE_TIME,
               HANDOVER_TIME = cust.HANDOVER_TIME,
               NOTSATIS_REASON = cust.NOTSATIS_REASON,
               UNUSUAL_TYPE = cust.UNUSUAL_TYPE,
               EVALUATE_INFO = cust.EVALUATE_INFO,
               SIGN_TYPE = cust.SIGN_TYPE,
               EXP_SIGN_REASON = cust.EXP_SIGN_REASON,
               UNLOAD_LON = cust.UNLOAD_LON,
               UNLOAD_LAT = cust.UNLOAD_LAT,
               UNLOAD_DISTANCE = cust.UNLOAD_DISTANCE,
               EVALUATE_TIME = cust.EVALUATE_TIME,
               DLVMAN_ID = cust.DLVMAN_ID
            };

            return result;
        }


        /// <summary>
        /// 转换暂存信息
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static I_CO_TEMP ConvertCoTemp(CoTemp tmp)
        {
            I_CO_TEMP result = new I_CO_TEMP
            {
                DIST_NUM = tmp.DIST_NUM,
                CO_NUM = tmp.CO_NUM,
                RET_TIME=tmp.RET_TIME,
                RET_USER_ID=tmp.RET_USER_ID,
                STATUS = tmp.STATUS
            };
            return result;
        }


        /// <summary>
        /// 转换退货订单
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public static I_CO_RETURN ConvertReturnOrder(CoReturn re)
        {
            I_CO_RETURN result = new I_CO_RETURN 
            {
                RETURN_CO_NUM=re.RETURN_CO_NUM,
                CUST_ID = re.CUST_ID,
                TYPE = re.TYPE,
                STATUS = re.STATUS,
                CRT_DATE = re.CRT_DATE,
                CRT_USER_NAME = re.CRT_USER_NAME,
                ORG_CO_NUM = re.ORG_CO_NUM,
                NOTE = re.NOTE,
                AMT_SUM = re.AMT_SUM,
                QTY_SUM = re.QTY_SUM

            };
            return result;
        }

        /// <summary>
        /// 转换退货订单明细
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static I_CO_RETURN_LINE ConvertReturnLine(CoReturnLine line)
        {
            I_CO_RETURN_LINE result = new I_CO_RETURN_LINE
            {
                RETURN_CO_NUM =line.RETURN_CO_NUM,
                LINE_NUM = line.LINE_NUM,
                ITEM_ID = line.ITEM_ID,
                QTY_ORD = line.QTY_ORD,
                NOTE = line.NOTE

            };
            return result;
        }

        /// <summary>
        /// 转换零售户位置信息
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static I_GIS_CUST_POI ConvertCustPoi(GisCustPois poi)
        {
            I_GIS_CUST_POI result = new I_GIS_CUST_POI
            {
                CUST_ID=poi.CUST_ID,
                MOBILE_TYPE = poi.MOBILE_TYPE,
                ORIGINAL_LONGITUDE = poi.ORIGINAL_LONGITUDE,
                ORIGINAL_LATITUDE = poi.ORIGINAL_LATITUDE,
                IS_DEFAULT = poi.IS_DEFAULT,
                CRT_TIME = poi.CRT_TIME,
                CRT_USER_ID = poi.CRT_USER_ID,
                COL_TIME = poi.COL_TIME,
                IMP_STATUS = poi.IMP_STATUS,
                NOTE = poi.NOTE

            };
            return result;
        }

        /// <summary>
        /// 转换配送员位置信息
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static I_GIS_LAST_LOCRECORD ConvertLastPoi(GisLastLocrecord poi)
        {
            I_GIS_LAST_LOCRECORD result = new I_GIS_LAST_LOCRECORD
            {
                M_CODE =poi.M_CODE,
                M_TYPE = poi.M_TYPE,
             //   ID = poi.GisID,
                ORIGINAL_LONGITUDE = poi.ORIGINAL_LONGITUDE,
                ORIGINAL_LATITUDE = poi.ORIGINAL_LATITUDE,
                SPEED = poi.SPEED,
                DIRECTION = poi.DIRECTION,
                HEIGHT = poi.HEIGHT,
                STATLLITE_NUM = poi.STATLLITE_NUM,
                GPSTIME = poi.GPSTIME,
                INPUTDATE = poi.INPUTDATE,
                STATE = poi.STATE
            };
            return result;
        }

         /// <summary>
        /// 转换配送员信息
        /// </summary>
        /// <param name="dlvman"></param>
        /// <returns></returns>
        public static I_DIST_DLVMAN ConvertDistDlvman(DistDlvman dlvman)
        {
            I_DIST_DLVMAN result = new I_DIST_DLVMAN
            {
                USER_ID = dlvman.USER_ID,
                USER_NAME = dlvman.USER_NAME,
                ORGAN_ID = dlvman.ORGAN_ID,
                POSITION_CODE = dlvman.POSITION_CODE,
                COM_ID = dlvman.COM_ID,
            };
            return result;

        }

        /// <summary>
        /// 转换线路信息
        /// </summary>
        /// <param name="dlvman"></param>
        /// <returns></returns>
        public static I_DIST_RUT ConvertDistRut(DistRut rut)
        {
            I_DIST_RUT result = new I_DIST_RUT
            {
                RUT_ID = rut.RUT_ID,
                RUT_NAME = rut.RUT_NAME,
                IS_MRB = rut.IS_MRB,
                COM_ID = rut.COM_ID
            };
            return result;
        }

        /// <summary>
        /// 转换订单
        /// </summary>
        /// <param name="dist"></param>
        /// <returns></returns>
        public static I_DIST_LINE ConvertDistLine(LdmDistLine L)
        {
            I_DIST_LINE result = new I_DIST_LINE
            {
                DIST_NUM = L.DIST_NUM,
                CO_NUM = L.CO_NUM,
                CUST_ID = L.CUST_ID,
                CUST_CODE = L.CUST_CODE,
                CUST_NAME = L.CUST_NAME,
                MANAGER = L.MANAGER,
                ADDR = L.ADDR,
                TEL = L.TEL,
                QTY_BAR = L.QTY_BAR,
                AMT_AR = L.AMT_AR,
                AMT_OR = L.AMT_OR,
                PMT_STATUS = L.PMT_STATUS,
                TYPE = L.TYPE,
                SEQ = L.SEQ,
                LICENSE_CODE = L.LICENSE_CODE,
                PAY_TYPE = L.PAY_TYPE,
                LONGITUDE = L.LONGITUDE,
                LATITUDE = L.LATITUDE,
                CUST_CARD_ID = L.CUST_CARD_ID,
                CUST_CARD_CODE = L.CUST_CARD_CODE

            };
            return result;
        }


        #endregion
    }
}
