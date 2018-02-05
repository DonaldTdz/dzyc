using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Script.Serialization;
using DHQR.DataAccess.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IBM.Data.DB2;
using IBM.Data.DB2Types;
using System.Data;
using DHQR.BusinessLogic.Implement;
using Common.Base;
using DHQR.DataAccess.Langchao;
using DHQR.BasicLib;
using System.Linq;

namespace DHQR.UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            LangchaoLogic logic = new LangchaoLogic();
            logic.SysDistCars(out dohandle);
            var result = jser.Serialize(dohandle);
            
        }

        [TestMethod]
        public void testMethod111()
        {
            var data = "{'LATITUDE':'0.0','LOG_DATE':'20141218','LOG_SEQ':'00001','LOG_TIME':'161840','LONGITUDE':'0.0','NOTE':'','OPERATE_MODE':'0','OPERATION_TYPE':'carOutWhse','REF_ID':'GYPS000000117633','REF_TYPE':'1','USER_ID':'GYO00000000000498','IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var distRecord = jser.Deserialize<DistRecordLog>(data);

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            distRecord.OPERATION_TYPE = opType.carOutWhse; ;//车辆出库
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            if (dohandle.IsSuccessful)
            {
            DistRecordLogLogic logLogic = new DistRecordLogLogic();
            distRecord.Id = Guid.NewGuid();
            logLogic.Create(distRecord, out dohandle);
            }
            #endregion

            var result = jser.Serialize(dohandle);

        }


        [TestMethod]
        public void TestMethod2()
        {



            string data = "{'DLVMAN_ID':'P0000000000000005263','DistDate':['2015-12-22'],'IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<DownloadDistByDateParam>(data);
            DoHandle dohandle;
            #region 取浪潮配送单数据

            List<LdmDist> ldmDists = new List<LdmDist>();//配送单
            List<LdmDistLine> ldmDistLines = new List<LdmDistLine>();//配送单明细
            List<LdmDisItem> ldmDisItems = new List<LdmDisItem>();//配送单商品信息
            List<I_CO_TEMP_RETURN> tmpReturns = new List<I_CO_TEMP_RETURN>();//再出库暂存订单


            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.DownloadDistByDate(param, out ldmDists, out ldmDistLines, out ldmDisItems, out tmpReturns);

            #endregion

            #region 写本地数据库

            LdmDistLogic ldmLogic = new LdmDistLogic();
            ldmLogic.DownloadDists(ldmDists, ldmDistLines, ldmDisItems, out dohandle);

            #endregion

            if (ldmDists.Count == 0)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "未找到配送单,请与配送调度管理员联系!";
            }

            if (dohandle.IsSuccessful)
            {

                #region 返回配送单到终端

                LdmInfo ldmInfo = new LdmInfo { LdmDists = ldmDists, LdmDistLines = ldmDistLines, LdmDisItems = ldmDisItems };
                var result = jser.Serialize(ldmInfo);

                #endregion

            }

            else
            {
                var result = jser.Serialize(dohandle);
            }


       
        }


        [TestMethod]
        public void TestMethod3()
        {

            string resultData;
            string data = "{'userName':'100865','psw':'654321','IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            DistDlvmanLogic logic = new DistDlvmanLogic();
            LoginParam loginParam = jser.Deserialize<LoginParam>(data);
            DoHandle dohandle;
            DistDlvman car = logic.Login(loginParam.UserName, loginParam.Psw, out dohandle);
            //登录成功
            if (dohandle.IsSuccessful)
            {
                var result = jser.Serialize(car);
               
            }
            //登录失败
            else
            {
                var ss= jser.Serialize(car);
            }

        }

        /// <summary>
        /// 下载配送单
        /// </summary>
       [TestMethod]
        public void TestDownDist()
        {
            string data = "{'DLVMAN_ID':'P0000000000000005290','IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<DownloadDistParam>(data);
            DoHandle dohandle;
            #region 取浪潮配送单数据

            List<LdmDist> ldmDists = new List<LdmDist>();//配送单
            List<LdmDistLine> ldmDistLines = new List<LdmDistLine>();//配送单明细
            List<LdmDisItem> ldmDisItems = new List<LdmDisItem>();//配送单商品信息

            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.DownloadDists(param, out ldmDists, out ldmDistLines, out ldmDisItems);

            #endregion

            #region 写本地数据库

            LdmDistLogic ldmLogic = new LdmDistLogic();
            ldmLogic.DownloadDists(ldmDists, ldmDistLines, ldmDisItems, out dohandle);

            #endregion

            if (ldmDists.Count == 0)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "未找到配送单,请与配送调度管理员联系!";
            }

            if (dohandle.IsSuccessful)
            {

                #region 返回配送单到终端

                LdmInfo ldmInfo = new LdmInfo { LdmDists = ldmDists, LdmDistLines = ldmDistLines, LdmDisItems = ldmDisItems };
                var result = jser.Serialize(ldmInfo);

                #endregion

            }

            else
            {
                var result = jser.Serialize(dohandle);
            }

        }

        /// <summary>
        /// 同步零售户信息
        /// </summary>
       [TestMethod]
       public void TestSysCustomer()
       {
           RetailerLogic logic = new RetailerLogic();
           DoHandle dohandle;
           logic.SysCustomer("300000001", out dohandle);
       }



       /// <summary>
       /// 获取日志流水号
       /// </summary>
       [TestMethod]
       public void TestGetLogkey()
       {
           CarRunKeyLogic logic = new CarRunKeyLogic();
           var ss = logic.GetLogkey();
       }

       /// <summary>
       /// 获取日志流水号
       /// </summary>
       [TestMethod]
       public void TestDownFinish()
       {
           var data = "{'DistNums':['GYPS000000117633'],'LOG_DATE':'20150127','LOG_TIME':'150028','OPERATE_MODE':'0','OPERATION_TYPE':'downDistFinish','REF_TYPE':'1','USER_ID':'GYO00000000000498','CAR_ID':'GYd9c8961217437e0112178503700005','CAR_LICENSE':'川HB7598','DLVMAN_ID':'GYO00000000000498','DRIVER_ID':'GYO00000000000688','IsSuccessful':false}";
           var jser = new JavaScriptSerializer();
           DoHandle dohandle;
           var param = jser.Deserialize<DownDistFinish>(data);
           if (param.DistNums == null)
           {
               dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "传入参数错误！" };
            //   return jser.Serialize(dohandle);
           }

           #region 浪潮数据库回写日志

           IList<I_DIST_RECORD_LOG> logs = new List<I_DIST_RECORD_LOG>();
           foreach (var item in param.DistNums)
           {

               I_DIST_RECORD_LOG log = new I_DIST_RECORD_LOG();
               var logKey = new LogKeyLogic().GetLogkey();
               OperationType opType;
               log.LOG_SEQ = logKey;
               log.OPERATION_TYPE = opType.downDistFinish;//下载完成
               log.REF_TYPE = param.REF_TYPE;
               log.REF_ID = item;
               log.LOG_DATE = param.LOG_DATE;
               log.LOG_TIME = param.LOG_TIME;
               log.USER_ID = param.USER_ID;
               log.LONGITUDE = param.LONGITUDE;
               log.LATITUDE = param.LATITUDE;
               log.OPERATE_MODE = param.OPERATE_MODE;
               logs.Add(log);
           }

           #endregion

           IList<DistRecordLog> distRecords = logs.Select(f => ConvertFromLC.ConvertRecordLog(f)).ToList();

           #region 写浪潮数据表【送货员操作日志】

           LangchaoLogic lcLogic = new LangchaoLogic();
           lcLogic.WriteDistRecordLog(logs, out dohandle);

           #endregion

           #region 写本地服务器数据表【送货员操作日志】

           if (dohandle.IsSuccessful)
           {
               DistRecordLogLogic logLogic = new DistRecordLogLogic();
               foreach (var d in distRecords)
               {
                   d.Id = Guid.NewGuid();
               }
               logLogic.Create(distRecords, out dohandle);
           }
           #endregion

           var result = jser.Serialize(dohandle);


       }

       /// <summary>
       /// 备车检查
       /// </summary>
       [TestMethod]
       public void TestCheckData()
       {

           var dd = DateTime.Now.ToString("yyyyMMdd HHmmss");

           var data = "{'CheckDatas':[{'ABNORMAL_DETAIL':'a02','ABNORMAL_TYPE':'a','CAR_ID':'GYd9c8961217437e0112178503700005','CHECK_ID':'','CHECK_TIME':'20150125040052','CHECK_TYPE':'2','LATITUDE':'0.0','LONGITUDE':'0.0','NOTE':'','OPERATE_MODE':'0','REF_ID':'GYPS000000117633','REF_TYPE':'1','USER_ID':'GYO00000000000498','IsSuccessful':false},{'ABNORMAL_DETAIL':'a03','ABNORMAL_TYPE':'a','CAR_ID':'GYd9c8961217437e0112178503700005','CHECK_ID':'','CHECK_TIME':'20150125040052','CHECK_TYPE':'2','LATITUDE':'0.0','LONGITUDE':'0.0','NOTE':'','OPERATE_MODE':'0','REF_ID':'GYPS000000117633','REF_TYPE':'1','USER_ID':'GYO00000000000498','IsSuccessful':false},{'ABNORMAL_DETAIL':'a04','ABNORMAL_TYPE':'a','CAR_ID':'GYd9c8961217437e0112178503700005','CHECK_ID':'','CHECK_TIME':'20150125040052','CHECK_TYPE':'2','LATITUDE':'0.0','LONGITUDE':'0.0','NOTE':'','OPERATE_MODE':'0','REF_ID':'GYPS000000117633','REF_TYPE':'1','USER_ID':'GYO00000000000498','IsSuccessful':false},{'ABNORMAL_DETAIL':'a05','ABNORMAL_TYPE':'a','CAR_ID':'GYd9c8961217437e0112178503700005','CHECK_ID':'','CHECK_TIME':'20150125040052','CHECK_TYPE':'2','LATITUDE':'0.0','LONGITUDE':'0.0','NOTE':'','OPERATE_MODE':'0','REF_ID':'GYPS000000117633','REF_TYPE':'1','USER_ID':'GYO00000000000498','IsSuccessful':false},{'ABNORMAL_DETAIL':'a06','ABNORMAL_TYPE':'a','CAR_ID':'GYd9c8961217437e0112178503700005','CHECK_ID':'','CHECK_TIME':'20150125040052','CHECK_TYPE':'2','LATITUDE':'0.0','LONGITUDE':'0.0','NOTE':'','OPERATE_MODE':'0','REF_ID':'GYPS000000117633','REF_TYPE':'1','USER_ID':'GYO00000000000498','IsSuccessful':false},{'ABNORMAL_DETAIL':'a07','ABNORMAL_TYPE':'a','CAR_ID':'GYd9c8961217437e0112178503700005','CHECK_ID':'','CHECK_TIME':'20150125040052','CHECK_TYPE':'2','LATITUDE':'0.0','LONGITUDE':'0.0','NOTE':'','OPERATE_MODE':'0','REF_ID':'GYPS000000117633','REF_TYPE':'1','USER_ID':'GYO00000000000498','IsSuccessful':false}],'IsSuccessful':false}";
           var jser = new JavaScriptSerializer();
           DoHandle dohandle;
           var checkInfor = jser.Deserialize<CheckCarParam>(data);
           DistCarCheckLogic carCheckLogic = new DistCarCheckLogic();
           carCheckLogic.CheckCar(checkInfor.CheckDatas, out dohandle);
           var result = jser.Serialize(dohandle);


       }


       /// <summary>
       /// 开始装车
       /// </summary>
       [TestMethod]
       public void TestStartLoad()
       {
           var data = "{'LOG_DATE':'20150125','LOG_TIME':'153352','OPERATE_MODE':'1','OPERATION_TYPE':'startLoad','REF_ID':'GYPS000000117633','REF_TYPE':'1','USER_ID':'GYO00000000000498','CAR_ID':'GYd9c8961217437e0112178503700005','CAR_LICENSE':'ԨHB7598','DLVMAN_ID':'GYO00000000000498','DRIVER_ID':'GYO00000000000688','IsSuccessful':false}";
           var jser = new JavaScriptSerializer();
           DoHandle dohandle;
           var distRecord = jser.Deserialize<DistRecordLog>(data);

           var logKey = new LogKeyLogic().GetLogkey();
           OperationType opType;
           distRecord.LOG_SEQ = logKey;
           distRecord.OPERATION_TYPE = opType.startLoad;//开始装车
           I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

           #region 写浪潮数据表【送货员操作日志】

           //LangchaoLogic lcLogic = new LangchaoLogic();
           //lcLogic.WriteDistRecordLog(log, out dohandle);

           #endregion

           #region 写本地服务器数据表【送货员操作日志】

           //if (dohandle.IsSuccessful)
           //{
               DistRecordLogLogic logLogic = new DistRecordLogLogic();
               distRecord.Id = Guid.NewGuid();
               logLogic.Create(distRecord, out dohandle);
           //}
           #endregion

           var result = jser.Serialize(dohandle);


       }


       [TestMethod]
       public void TestConfirm()
       {
           var data = "{'CO_NUM':'XGY30001569519','CUST_ID':'510811102381','DIST_NUM':'GY000000133909','DIST_SATIS':'10','DLVMAN_ID':'P0000000000000014688','EVALUATE_INFO':'','EVALUATE_TIME':'20151030110852','EXP_SIGN_REASON':'','HANDOVER_TIME':'6','IS_RECEIVED':'01','REC_ARRIVE_TIME':'20151030110846','REC_DATE':'20151030','REC_LEAVE_TIME':'20151030110852','SIGN_TYPE':'0','UNLOAD_REASON':'28','UNUSUAL_TYPE':'01','IsSuccessful':false}";
           var jser = new JavaScriptSerializer();
           DoHandle dohandle;
           var param = jser.Deserialize<DistCust>(data);
           if (param == null)
           {
               dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "传入参数错误！" };
           }
           DistCustLogic logic = new DistCustLogic();
           logic.ConfirmDelivery(param, out dohandle);
           var result = jser.Serialize(dohandle);

       }

        [TestMethod]
       public void TestReturn()
       {
           var data = "{'CO_NUM':'XGY30001248739','CUST_ID':'2613953','DIST_NUM':'GYPS000000117633','DIST_SATIS':'13','DLVMAN_ID':'GYO00000000000498','EVALUATE_INFO':'','EVALUATE_TIME':'20150115104241','EXP_SIGN_REASON':'','HANDOVER_TIME':'31','IS_RECEIVED':'00','NOTSATIS_REASON':'','REC_ARRIVE_TIME':'20150115104210','REC_DATE':'20150115','REC_LEAVE_TIME':'20150115104241','SIGN_TYPE':'3','UNLOAD_DISTANCE':'','UNLOAD_LAT':'30.55716951','UNLOAD_LON':'104.07268829','UNLOAD_REASON':'21','UNUSUAL_TYPE':'03','IsSuccessful':false}";

           var jser = new JavaScriptSerializer();
           DoHandle dohandle;
           var param = jser.Deserialize<DistCust>(data);
           if (param == null)
           {
               dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "传入参数错误！" };
           }
           CoReturnLogic logic = new CoReturnLogic();
           logic.ReturnAllOrder(param, out dohandle);
           var result = jser.Serialize(dohandle);

       }

        [TestMethod]
        public void TestPatialReturn()
        {
            var data = "{'Cust':{'CO_NUM':'XGY30001340214','CUST_ID':'SC0000043579507','DIST_NUM':'GYPS000000122740','DIST_SATIS':'11','DLVMAN_ID':'GYO00000000000498','EVALUATE_INFO':'','EVALUATE_TIME':'20150313101124','EXP_SIGN_REASON':'','HANDOVER_TIME':'29','IS_RECEIVED':'01','REC_ARRIVE_TIME':'20150313101055','REC_DATE':'20150313','REC_LEAVE_TIME':'20150313101124','UNLOAD_REASON':'31','UNUSUAL_TYPE':'04','IsSuccessful':false},'Order':{'AMT_SUM':'4.00','CRT_DATE':'20150313','CRT_USER_NAME':'GYO00000000000498','CUST_ID':'SC0000043579507','NOTE':'','ORG_CO_NUM':'','QTY_SUM':'736.00','STATUS':'01','TYPE':'01','IsSuccessful':false},'OrderDetails':[{'ITEM_ID':'4205084','NOTE':'黄鹤楼(硬好运）','PRICE':'260.00','QTY_ORD':'2','RETURN_CO_NUM':'XGY30001340214','IsSuccessful':false},{'ITEM_ID':'5101016','NOTE':'娇子(蓝)','PRICE':'108.00','QTY_ORD':'2','RETURN_CO_NUM':'XGY30001340214','IsSuccessful':false}],'IsSuccessful':false}";

            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var param = jser.Deserialize<ReturnPatialOrderParam>(data);
            if (param == null)
            {
                dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "传入参数错误！" };
            }
            CoReturnLogic logic = new CoReturnLogic();
            logic.ReturnPatialOrder(param, out dohandle);
            var result = jser.Serialize(dohandle);

        }

         [TestMethod]
        public void TestMyDB2()
        {
            

            List<LdmDist> ldmDists = new List<LdmDist>();
            List<LdmDistLine> lines = new List<LdmDistLine>();
            List<LdmDisItem> items = new List<LdmDisItem>();

            string connectionString = "Server=10.88.0.76:60000;DataBase=SCV6TD;UID=CDHC;PWD=cdhc";
            string commandtext = "select * from I_DIST where CAR_LICENSE='川HB7598' and DIST_DATE='20141216'";
            DB2Connection connection = new DB2Connection(connectionString);
            DB2Command command = new DB2Command(commandtext, connection);
            command.Connection = connection;
            connection.Open();
            DB2DataAdapter adapter = new DB2DataAdapter(command);
            DataSet dss = new DataSet();
            adapter.Fill(dss);
            DataTable dt = dss.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                var kk = dt.Rows[i][0].ToString();
                var mm = dt.Rows[i][0].ToString();
                var tt = dt.Rows[i][0].ToString();
                LdmDist l = new LdmDist
                {
                    Id = Guid.NewGuid(),
                    DIST_NUM = dt.Rows[i][0].ToString(),
                    RUT_ID = dt.Rows[i][1].ToString(),
                    RUT_NAME = dt.Rows[i][2].ToString(),
                    DIST_DATE = dt.Rows[i][3].ToString(),
                    DLVMAN_ID = dt.Rows[i][4].ToString(),
                    DLVMAN_NAME = dt.Rows[i][5].ToString(),
                    DRIVER_ID = dt.Rows[i][6].ToString(),
                    DRIVER_NAME = dt.Rows[i][7].ToString(),
                    CAR_ID = dt.Rows[i][8].ToString(),
                    CAR_LICENSE = dt.Rows[i][9].ToString(),
                    DIST_CUST_SUM = int.Parse(dt.Rows[i][10].ToString()),
                    QTY_BAR = decimal.Parse(dt.Rows[i][11].ToString()),
                    AMT_SUM = decimal.Parse(dt.Rows[i][12].ToString()),
                    STATUS = dt.Rows[i][13].ToString(),
                    IS_DOWNLOAD = dt.Rows[i][14].ToString(),
                    IS_MRB = dt.Rows[i][15].ToString(),
                    DRIVER_CARD_ID = dt.Rows[i][16].ToString()
                };
                ldmDists.Add(l);
            }
            connection.Close();
            
            DoHandle dohandle;
            LdmDistLogic logic = new LdmDistLogic();
            logic.DownloadDists(ldmDists, lines, items, out dohandle);

        }

         [TestMethod]
         public void TestMyLCDb2()
         {
             //string sql = "select * from I_DIST where CAR_LICENSE='川HB7598' and DIST_DATE='20141216'";
             //LangchaoDB2Repository rep = new LangchaoDB2Repository();
             //var dt= rep.GetData(sql);

             //var result = ConvertHelper<LdmDist>.ConvertToList(dt);

             //var result = new DB2Helper< I_CO_RETURN>().GetAll();
             IDictionary<string,string> s =new Dictionary<string,string>();
             s.Add("CAR_LICENSE", "川HB7598");
             s.Add("DIST_DATE", "20141216");
             var result = new DB2Helper<I_DIST>().QueryData(s);
         }


         [TestMethod]
         public void TestUpDateDB2()
         {
             DoHandle dohandle;
             IDictionary<string, string> s = new Dictionary<string, string>();
             s.Add("RETURN_CO_NUM", "RO9");
             IDictionary<string, string> s1 = new Dictionary<string, string>();
             s1.Add("TYPE", "03");
             new DB2Helper<I_CO_RETURN>().Update(s1, s, out dohandle);
         }


        /// <summary>
        /// 回程登记
        /// </summary>
         [TestMethod]
         public void TestBackRegist()
         {
             var data = "{'CarRun':{'REF_TYPE':'1','AMT_SUM':'19.00','CAR_ID':'GYd9c8961787cb1901178c14782f004b','CRT_DATE':'20150613140119','DLVMAN_ID':'P0000000000000005278','NOTE':'','REF_ID':'GYPS000000127638','PRE_MIL':68915.0,'ACT_MIL':146.0,'THIS_MIL':69061.0,'IsSuccessful':false},'CarRunLine':[{'AMT':'19.00','COST_TYPE':'2','IsSuccessful':false}],'IsSuccessful':false}";
             var jser = new JavaScriptSerializer();
             DoHandle dohandle;
             var param = jser.Deserialize<BackRegistParam>(data);
             DistCarRunLogic logic = new DistCarRunLogic();
             logic.BackRegist(param, out dohandle);
             var result = jser.Serialize(dohandle);

         }

         /// <summary>
         /// 
         /// </summary>
         [TestMethod]
         public void TestUploadLocation()
         {
             string data = "{'DIRECTION':'4.50','GPSTIME':'20150126100348','HEIGHT':'389.00','INPUTDATE':'20150126100348','M_TYPE':'65 ','ORIGINAL_LATITUDE':'30.55439085','ORIGINAL_LONGITUDE':'104.07530949','SPEED':'0.26','STATE':'1','STATLLITE_NUM':'3','IsSuccessful':false}";
             var jser = new JavaScriptSerializer();
             var param = jser.Deserialize<GisLastLocrecord>(data);
             DoHandle dohandle;
             GisLastLocrecordLogic logic = new GisLastLocrecordLogic();
             logic.UploadLocation(param, out dohandle);
             var result = jser.Serialize(dohandle);

         }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
         public void TestLatLon()
         {

          //   CoordinateTransform coTran = new CoordinateTransform();
             Double lat = 104.07480172;
             Double lon = 30.55503481;
             double outLat,outLon;
             CoordinateTransform.transform(lon, lat, out outLat, out outLon);

         }



        /// <summary>  
        ///   
        /// 将对象属性转换为key-value对  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        public static Dictionary<String, Object> ToMap(Object o)
        {
            Dictionary<String, Object> map = new Dictionary<string, object>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic && ((System.Reflection.MemberInfo)(p)).Name!="Item")
                {
                    map.Add(p.Name, mi.Invoke(o, new Object[] { }));
                }
            }

            return map;

        }

        public static IDictionary<string, object> GetDictionary(object source)
          {
             if (source == null)
              {
                  return new Dictionary<string, object>();
              }
              PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(source);

              Dictionary<string, object> dictionary = new Dictionary<string, object>();
              for (int i = 0; i < properties.Count; i++)
             {
                dictionary.Add(properties[i].Name, properties[i].GetValue(source));
             }
             return dictionary;
         }



        [TestMethod]
        public void TestGetType()
        {
            var data = "{'CO_NUM':'XGY30001248322','CUST_ID':'2613953','DIST_NUM':'GYPS000000117633','DIST_SATIS':'13','DLVMAN_ID':'GYO00000000000498','EVALUATE_INFO':'','EVALUATE_TIME':'20150115104241','EXP_SIGN_REASON':'','HANDOVER_TIME':'31','IS_RECEIVED':'00','NOTSATIS_REASON':'','REC_ARRIVE_TIME':'20150115104210','REC_DATE':'20150115','REC_LEAVE_TIME':'20150115104241','SIGN_TYPE':'3','UNLOAD_DISTANCE':'','UNLOAD_LAT':'30.55716951','UNLOAD_LON':'104.07268829','UNLOAD_REASON':'21','UNUSUAL_TYPE':'03','IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            DoHandle dohandle = new DoHandle() ;
            DistCust param = jser.Deserialize<DistCust>(data);

            I_DIST_CUST lcParam = ConvertToLC.ConvertDistCust(param);

            var tableName = lcParam.GetType().Name;
            string sql = string.Format("insert into {0} values('", tableName);
            foreach (PropertyInfo p in lcParam.GetType().GetProperties())
            {
                var pValue = p.GetValue(lcParam, null);
                var ptype = p.PropertyType.Name;

                if ( pValue == null)
                {
                    //if (i == 0)
                    //{
                    sql = sql.Remove(sql.Length - 1, 1);
                    sql = sql + "null,'";
                    //}
                    //else
                    //{
                    //    sql = sql.Remove(sql.Length - 2, 2);
                    //    sql = sql + "null,'";
                    //}
                }
                else
                {
                    sql = sql + pValue.ToString() + "','";
                }
            }
            sql = sql.Remove(sql.Length - 2, 2);
            sql = sql + ")";

        }


        [TestMethod]
        public void TestMd5()
        {
            string kk = "654321";
            var currentPsw = "c33367701511b4f6020ec61ded352059";
            var tt= CreateMd5.Md5String(kk);
            var mm = kk == currentPsw;
        }



        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestChangeLoginPsw()
        {

            var data = "{'OriginalPsw':'aa','Psw':'aa','UserName':'00498','IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            ChageLoginPswParam param = jser.Deserialize<ChageLoginPswParam>(data);
            LdmDistCarLogic logic = new LdmDistCarLogic();
            DoHandle dohandle;
            logic.ChangeLoginPsw(param, out dohandle);
            var result = jser.Serialize(dohandle);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestChangeDeliveryPsw()
        {

            var data = "{'CUST_ID':'2610560','OriginalPsw':'rjks','Psw':'sgjdks','IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            ChangeDeliveryPswParam param = jser.Deserialize<ChangeDeliveryPswParam>(data);
            RetailerLogic logic = new RetailerLogic();
            DoHandle dohandle;
            logic.ChangeDeliveryPswParam(param, out dohandle);
            var result = jser.Serialize(dohandle);
        }

        [TestMethod]
        public void TestCollectRetailerXY()
        {

            var datalst = "[{'COL_TIME':'20150415113300','CRT_TIME':'20150415113300','CRT_USER_ID':'P0000000000000005268','CUST_ID':'2613612','IMP_STATUS':'00','IS_DEFAULT':'1','MOBILE_TYPE':'2','ORIGINAL_LATITUDE':'32.64482128','ORIGINAL_LONGITUDE':'105.88816358','IsSuccessful':false},{'COL_TIME':'20150415121531','CRT_TIME':'20150415121531','CRT_USER_ID':'P0000000000000005268','CUST_ID':'300002429','IMP_STATUS':'00','IS_DEFAULT':'1','MOBILE_TYPE':'2','ORIGINAL_LATITUDE':'32.64505574','ORIGINAL_LONGITUDE':'105.88618809','IsSuccessful':false},{'COL_TIME':'20150415121942','CRT_TIME':'20150415121942','CRT_USER_ID':'P0000000000000005268','CUST_ID':'300002452','IMP_STATUS':'00','IS_DEFAULT':'1','MOBILE_TYPE':'2','ORIGINAL_LATITUDE':'32.64537047','ORIGINAL_LONGITUDE':'105.88590027','IsSuccessful':false},{'COL_TIME':'20150415132509','CRT_TIME':'20150415132509','CRT_USER_ID':'P0000000000000005268','CUST_ID':'300007913','IMP_STATUS':'00','IS_DEFAULT':'1','MOBILE_TYPE':'2','ORIGINAL_LATITUDE':'32.65757431','ORIGINAL_LONGITUDE':'105.85731056','IsSuccessful':false}]";

            

          //  var data = "{'COL_TIME':'20150331162559','CRT_TIME':'20150331162559','CUST_ID':'SC0000043598547','IMP_STATUS':'00','IS_DEFAULT':'1','MOBILE_TYPE':'2','ORIGINAL_LATITUDE':'30.55546928','ORIGINAL_LONGITUDE':'104.07442515','IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            DoHandle dohandle =new DoHandle();
            var param = jser.Deserialize<IList<GisCustPois>>(datalst);
            GisCustPoisLogic logic = new GisCustPoisLogic();
            foreach (var item in param)
            {
                logic.CollectRetailerXY(item, out dohandle);
            }



            var result = jser.Serialize(dohandle);

          //  GisCustPoisLogic logic = new GisCustPoisLogic();
           // logic.SynLatLng();

        }

        [TestMethod]
        public void TestSynOrders()
        {
            var data = "{'DIST_NUM':'GYPS000000120675','IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
    
            var param = jser.Deserialize<SynOrderParam>(data);
            LdmDistLineLogic logic = new LdmDistLineLogic();
            var orderInfos = logic.SynOrders(param);
            var result = jser.Serialize(orderInfos);

        }

        [TestMethod]
        public void TestSynDlvmans()
        {

            DistDlvmanLogic logic = new DistDlvmanLogic();
            DoHandle dohandle;
            logic.SynDlvmans( out dohandle);
         

        }

        [TestMethod]
        public void TestCheckVersion()
        {

            AppVersionLogic logic = new AppVersionLogic();
            var data="{'ApkPacket':'com.hc.logistics','VersionCode':4,'IsSuccessful':false}";
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<AppVersion>(data);
            
          var result=  logic.CheckAppVersion(param);


        }

        [TestMethod]
        public void TestNFC()
        {

            var jser = new JavaScriptSerializer();
            EntranceCardLogic logic = new EntranceCardLogic();
            var cards = logic.GetAll().Where(f => f.IsValid == true).ToList();
            var result = jser.Serialize(cards);
            //return result;


        }

        [TestMethod]
        public void TestSynLdmDist()
        {
            var data = "{'DIST_NUM':'GYPS000000127433'}";
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<SynLdmDistParam>(data);
            DistRecordLogLogic logic = new DistRecordLogLogic();
            var records = logic.SynLdmDist(param);

            SynOrderParam orderParam = new SynOrderParam { DIST_NUM = param.DIST_NUM };
            LdmDistLineLogic orderlogic = new LdmDistLineLogic();
            var orderInfos = orderlogic.SynOrders(orderParam);

            SynLdmInfoData totalData = new SynLdmInfoData { Logs = records.ToList(), OrderInfo = orderInfos.ToList() };

            var result = jser.Serialize(totalData);

          
            

        }

        [TestMethod]
        public void TestWeiXinMsg()
        {
            var data = "{'DIST_NUM':'GYPS000000125192','CO_NUM':'XGY30001383267'}";
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<WeiXinNotifyParam>(data);
            DistCustLogic logic = new DistCustLogic();
            DoHandle dohandle;
            logic.WeiXinNotify(param,out dohandle);

            var result = jser.Serialize(dohandle);


        }

        [TestMethod]
        public void TestGetNfcCustomer()
        {
            var data = "{'CARD_ID':'047B4D120B3C80'}";
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<GetNfcCustomerParam>(data);
            RetailerLogic logic = new RetailerLogic();
            var returnData = logic.GetNfcCustomer(param);
            var result = jser.Serialize(returnData);
   

        }

        [TestMethod]
        public void TestQueryTimeRecord()
        {
            //var queryDate = DateTime.Parse("2015-5-27");
            //TimeRecordQueryParam queryParam = new TimeRecordQueryParam {QueryDate=queryDate };
            //DistRecordLogLogic logic = new DistRecordLogLogic();
            //var returnData = logic.QueryTimeRecord(queryParam);
            //var id = Guid.Parse("2DAC29A9-FBF0-47BE-B47C-87224A555B69");
            //var weiXinAppId = Guid.Parse("0CD98D1B-C476-4A02-B4B9-0F619FCCD36F");
            //string CO_NUM = "XGY30001454495";
            //WeiXinUserLogic logic = new WeiXinUserLogic();
            //WeiXinAppLogic appLogic = new WeiXinAppLogic();
            //var app = appLogic.GetByKey(weiXinAppId);
            //var user = logic.GetByKey(id);
            //WeiXinCommonApi pk = new WeiXinCommonApi();
            //string msg;
            //pk.SendOrderTmp(app, user, CO_NUM, out msg);

        }

        [TestMethod]
        public void TestGpsDistance()
        {
            double lat1 = 32.20175603;
            double lon1 = 105.56852826;
            double lat2 = 32.20419118;
            double lon2 = 105.56501153;
            var tt = GpsDistanceHelper.Distance(lat1, lon1, lat2, lon2);
            var kk= GpsDistanceHelper.GetDistance(lat1,lon1,lat2,lon2);
        }

    }
}
