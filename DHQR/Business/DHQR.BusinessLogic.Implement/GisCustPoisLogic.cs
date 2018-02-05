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
    /// 零售户位置信息逻辑层
    /// </summary>
    public class GisCustPoisLogic : BaseLogic<GisCustPois>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private GisCustPoisRepository GisCustPoisRep { get { return new GisCustPoisRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<GisCustPois> Repository
        {
            get { return GisCustPoisRep; }
        }


        #region 零售户位置信息上传

        /// <summary>
        /// 零售户位置信息上传
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohanle"></param>
        public void CollectRetailerXY(GisCustPois poi, out DoHandle dohandle)
        {
            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.CollectRetailerXY(poi, out dohandle);
            if (dohandle.IsSuccessful)
            {
                GisCustPoisRep.CollectRetailerXY(poi, out dohandle);
            }
            try
            {
               // SynLatLng();
            }
            catch
            {
 
            }
        }

        public void SynLatLng()
        {
            GisCustPoisRep.SynLatLng();
        }

        #endregion

        #region 获取零售户位置信息

        /// <summary>
        /// 根据零售户ID集合获取零售户位置信息
        /// </summary>
        /// <param name="custIds"></param>
        /// <returns></returns>
        public List<GisCustPois> GetRetailerLocation(List<string> custIds)
        {
            return GisCustPoisRep.GetRetailerLocation(custIds);
        }

        #endregion

        #region 同步零售户采点信息

        /// <summary>
        /// 同步零售户采点信息
        /// </summary>
        public void SynCustPois()
        {
            LangchaoLogic lcLogic = new LangchaoLogic();
            var datas = lcLogic.GetCustPois();
            GisCustPoisRep.SynCustPois(datas);
        }


        #endregion
    }
}
