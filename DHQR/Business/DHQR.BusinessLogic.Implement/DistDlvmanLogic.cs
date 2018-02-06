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
    /// 配送员基础信息逻辑层
    /// </summary>
    public class DistDlvmanLogic : BaseLogic<DistDlvman>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistDlvmanRepository repository { get { return new DistDlvmanRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistDlvman> Repository
        {
            get { return repository; }
        }

        
        /// <summary>
        /// 同步配送员基础信息
        /// </summary>
        /// <param name="dlvmans"></param>
        /// <param name="dohandle"></param>
        public void SynDlvmans( out DoHandle dohandle)
        {
            LangchaoLogic lcLogic = new LangchaoLogic();
            List<DistDlvman> dlvmans = lcLogic.GetDistDlvmans().ToList();
            repository.SynDlvmans(dlvmans, out dohandle);
        }

        /// <summary>
        /// 添加时间：2018-2-6
        /// 作者：Donald
        /// 说明：同步达州配送员基础信息
        /// </summary>
        /// <param name="dlvmans"></param>
        /// <param name="dohandle"></param>
        public void SynDZDlvmans(string userId, out DoHandle dohandle)
        {
            DZLangchaoLogic lcLogic = new DZLangchaoLogic();
            List<DistDlvman> dlvmans = lcLogic.GetDistDlvmans(userId).ToList();
            repository.SynDlvmans(dlvmans, out dohandle);
        }

        /// <summary>
        /// 登录终端
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="psw">密码</param>
        public DistDlvman Login(string userName, string psw, out DoHandle dohandle)
        {
            return repository.Login(userName, psw, out dohandle);
        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void ChangeLoginPsw(ChageLoginPswParam param, out DoHandle dohandle)
        {
            repository.ChangeLoginPsw(param, out dohandle);
        }
    }
}
