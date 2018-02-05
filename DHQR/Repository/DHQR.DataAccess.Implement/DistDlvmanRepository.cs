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
    /// 配送员基本信息
    /// </summary>
    public class DistDlvmanRepository : ProRep<DistDlvman>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistDlvmanRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistDlvman> EntityCurrentSet
        {
            get { return ActiveContext.DistDlvmans; }
        }


        /// <summary>
        /// 同步配送员基础信息
        /// </summary>
        /// <param name="dlvmans"></param>
        /// <param name="dohandle"></param>
        public void SynDlvmans(List<DistDlvman> dlvmans, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "插入失败" };
            List<string> dlvmanCode=dlvmans.Select(f=>f.USER_ID).ToList();
            IList<DistDlvman> existDlvmans = ActiveContext.DistDlvmans.Where(f => dlvmanCode.Contains(f.USER_ID)).ToList();
            foreach (var item in dlvmans)
            {
                var currentDlvman = existDlvmans.SingleOrDefault(f => f.USER_ID == item.USER_ID);
                if (currentDlvman == null)
                {
                    item.Id = Guid.NewGuid();
                    ActiveContext.DistDlvmans.AddObject(item);
                }
                else
                {
                    currentDlvman.USER_NAME = item.USER_NAME;
                    currentDlvman.COM_ID = item.COM_ID;
                    currentDlvman.ORGAN_ID = item.ORGAN_ID;
                    currentDlvman.POSITION_CODE = item.POSITION_CODE;
                }
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "同步成功!";
        }


        /// <summary>
        /// 登录终端
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="psw">密码</param>
        public DistDlvman Login(string userName, string psw, out DoHandle dohandle)
        {
            dohandle = new DoHandle();

            string word = PassWordMd.HashPassword(psw);
            ///todo
            var user = EntityCurrentSet.SingleOrDefault(f => f.USER_ID == userName && f.PSW == word);
            if (user != null)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "登录成功!";
                user.IsSuccessful = true;
                user.OperateMsg = "登录成功!";

                //获取实际配送日期
                var distDateNum =int.Parse( ActiveContext.BaseEnums.SingleOrDefault(f => f.EnumType == "DistDate").Value);
                var distDate = DateTime.Now.AddDays(-distDateNum-1);
                var distDateStr = distDate.ToString("yyyyMMdd");
                user.DistDate = distDateStr;
                return user;
            }
            else
            {
                DistDlvman us = new DistDlvman { IsSuccessful = false, OperateMsg = "登录失败!" };
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
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "修改密码失败！" };

            string originalPsw = PassWordMd.HashPassword(param.OriginalPsw);
            string currentPsw = PassWordMd.HashPassword(param.Psw);
            var user = EntityCurrentSet.SingleOrDefault(f => f.USER_ID == param.UserName);
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
    }

}
