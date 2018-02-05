using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 配送车辆信息数据访问层
    /// </summary>
    public class LdmDistCarRepository : ProRep<LdmDistCar>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public LdmDistCarRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<LdmDistCar> EntityCurrentSet
        {
            get { return ActiveContext.LdmDistCars; }
        }


        #region 业务

        /// <summary>
        /// 登录终端
        /// </summary>
        /// <param name="userName">用户名（车牌号）</param>
        /// <param name="psw">密码</param>
        public LdmDistCar Login(string userName, string psw,out DoHandle dohandle)
        {
            dohandle = new DoHandle();
            string realUserName = GetRealName(userName);

            string word = PassWordMd.HashPassword(psw);
            ///todo
            var user = EntityCurrentSet.SingleOrDefault(f => f.DLVMAN_ID == realUserName && f.PSW == word && f.IS_MRB=="1");
            if (user != null)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "登录成功!";
                user.IsSuccessful = true;
                user.OperateMsg = "登录成功!";
                return user;
            }
            else
            {
                LdmDistCar us = new LdmDistCar {IsSuccessful=false,OperateMsg="登录失败!" };
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "登录失败!";
                return us;
            }

        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void ChangeLoginPsw(ChageLoginPswParam param, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful=false,OperateMsg="修改密码失败！"};
            string realUserName = "GYO000000000" + param.UserName.Trim() ;



            string originalPsw = PassWordMd.HashPassword(param.OriginalPsw);
            string currentPsw = PassWordMd.HashPassword(param.Psw);
            var user = EntityCurrentSet.SingleOrDefault(f => f.DLVMAN_ID == realUserName);
            if (user == null)
            {
                dohandle.OperateMsg = "指定用户名称不存在,请检查后再修改！";
                return;
            }
            else
            {
                if (user.PSW == originalPsw)
                {
                    user.PSW = currentPsw;
                    ActiveContext.SaveChanges();
                    dohandle.IsSuccessful = true;
                    dohandle.OperateMsg = "密码修改成功!";
                }
                else
                {
                    dohandle.OperateMsg = "原始密码不正确!";
                }
            }
        }

        /// <summary>
        /// 获取系统账号
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetRealName(string userName)
        {
            string realUserName = "GYO000000000" + userName.Trim();
            var isGyo = EntityCurrentSet.Any(f => f.DLVMAN_ID == realUserName);
            if (!isGyo)
            {
                realUserName = "P00000000000000" + userName;
            }
            return realUserName;
        }


        public void InsertCar(List<LdmDistCar> cars, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="插入失败" };
            foreach (var item in cars)
            {
                ActiveContext.LdmDistCars.AddObject(item);
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "插入成功";
        }

        /// <summary>
        /// 根据配送员获取配送车辆
        /// </summary>
        /// <param name="dlvmanIds"></param>
        /// <returns></returns>
        public IList<LdmDistCar> GetByDlvManIds(IList<string> dlvmanIds)
        {
            var result = ActiveContext.LdmDistCars.Where(f => dlvmanIds.Contains(f.DLVMAN_ID)).ToList();

            return result;
        }

        /// <summary>
        /// 同步车辆信息
        /// </summary>
        /// <param name="dohandle"></param>
        public void SynLdmCars(List<LdmDistCar> cars,out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "同步失败" };
            List<string> carIds = cars.Select(f => f.CAR_ID).ToList();
            IList<LdmDistCar> existCars = ActiveContext.LdmDistCars.Where(f => carIds.Contains(f.CAR_ID)).ToList();
            foreach (var item in cars)
            {
                var currentCar = existCars.SingleOrDefault(f => f.CAR_ID == item.CAR_ID);
                if (currentCar == null)
                {
                    item.Id = Guid.NewGuid();
                    ActiveContext.LdmDistCars.AddObject(item);
                }
                else
                {
                    currentCar.CAR_LICENSE = item.CAR_LICENSE;
                    currentCar.CAR_NAME = item.CAR_NAME;
                    currentCar.IS_MRB = item.IS_MRB;
                    currentCar.DLVMAN_ID = item.DLVMAN_ID;
                    currentCar.DLVMAN_NAME = item.DLVMAN_NAME;
                    currentCar.DRIVER_ID = item.DRIVER_ID;
                    currentCar.DRIVER_NAME = item.DRIVER_NAME;
                   
                }
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "同步成功!";

        }

        #endregion
    }
}
