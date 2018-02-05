using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 配送车辆信息逻辑层
    /// </summary>
    public class LdmDistCarLogic : BaseLogic<LdmDistCar>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private LdmDistCarRepository LdmDistCarRep { get { return new LdmDistCarRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<LdmDistCar> Repository
        {
            get { return LdmDistCarRep; }
        }



        #region 业务

        /// <summary>
        /// 登录终端
        /// </summary>
        /// <param name="userName">用户名（车牌号）</param>
        /// <param name="psw">密码</param>
        public LdmDistCar Login(string userName,string psw,out DoHandle dohandle)
        {
            string realUserName =LdmDistCarRep.GetRealName(userName);
            //查询配送单
            //LangchaoB

            return LdmDistCarRep.Login(userName, psw, out dohandle);
        }


        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void ChangeLoginPsw(ChageLoginPswParam param, out DoHandle dohandle)
        {
            LdmDistCarRep.ChangeLoginPsw(param, out dohandle);
        }
        /// <summary>
        /// 从浪潮更新数据
        /// </summary>
        /// <param name="cars"></param>
        /// <param name="dohandle"></param>
        public void InsertCar(List<LdmDistCar> cars, out DoHandle dohandle)
        {
            LdmDistCarRep.InsertCar(cars, out dohandle);
        }


        
        /// <summary>
        /// 根据配送员获取配送车辆
        /// </summary>
        /// <param name="dlvmanIds"></param>
        /// <returns></returns>
        public IList<LdmDistCar> GetByDlvManIds(IList<string> dlvmanIds)
        {
            return LdmDistCarRep.GetByDlvManIds(dlvmanIds);
        }


        /// <summary>
        /// 同步车辆信息
        /// </summary>
        /// <param name="cars"></param>
        /// <param name="dohandle"></param>
        public void SynLdmCars( out DoHandle dohandle)
        {
            LangchaoLogic lcLogic = new LangchaoLogic();
            var cars = lcLogic.GetCarList();
            LdmDistCarRep.SynLdmCars(cars, out dohandle);

        }
        #endregion

    }
}
