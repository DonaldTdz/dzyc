using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Common.Base;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 微信用户基本信息数据访问层
    /// </summary>
    public class WeiXinUserInfoRepository : ProRep<WeiXinUserInfo>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinUserInfoRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinUserInfo> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinUserInfos; }
        }

        /// <summary>
        /// 同步微信用户基础信息
        /// </summary>
        /// <param name="users"></param>
        /// <param name="dohandle"></param>
        public void SynWeiXinUserInfo(IList<WeiXinUserInfo> users, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="同步失败" };
            foreach (var item in users)
            {
                var oldData = ActiveContext.WeiXinUserInfos.SingleOrDefault(f => f.openid == item.openid);
                if (oldData == null)
                {
                    item.Id = Guid.NewGuid();
                    ActiveContext.WeiXinUserInfos.AddObject(item);
                }
                else
                {
                    oldData.subscribe = item.subscribe;
                    oldData.nickname = item.nickname;
                    oldData.sex = item.sex;
                    oldData.language = item.language;
                    oldData.city = item.city;
                    oldData.province = item.province;
                    oldData.country = item.country;
                    oldData.headimgurl = item.headimgurl;
                    oldData.subscribe_time = item.subscribe_time;
                    oldData.remark = item.remark;
                    oldData.groupid = item.groupid;
                }
            }
            try
            {
                ActiveContext.SaveChanges();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "同步成功";
            }
            catch (Exception ex)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = ex.Message;
            }
        }


        /// <summary>
        /// 根据openid获取用户
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public WeiXinUserInfo GetByOpenId(string openid)
        {
            return ActiveContext.WeiXinUserInfos.SingleOrDefault(f => f.openid == openid);
        }


        /// <summary>
        /// 更新用户分组和用户备注名
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="groupid"></param>
        /// <param name="remark"></param>
        /// <param name="dohandle"></param>
        public void UpdateUser(string openid, int groupid, string remark, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="更新失败" };

            var user = ActiveContext.WeiXinUserInfos.SingleOrDefault(f => f.openid == openid);
            var orignGroup = ActiveContext.WeiXinUserGroups.SingleOrDefault(f => f.groupid == user.groupid);
            var currentGroup = ActiveContext.WeiXinUserGroups.SingleOrDefault(f => f.groupid == groupid);
            user.groupid = groupid;
            user.remark = remark;
            orignGroup.count = orignGroup.count - 1;
            currentGroup.count = currentGroup.count + 1;
            try
            {
                ActiveContext.SaveChanges();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "更新成功";
            }
            catch (Exception ex)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = ex.Message;
            }
        }


        /// <summary>
        /// 根据分组ID获取用户
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public IList<WeiXinUserInfo> GetByGroupId(int groupid)
        {
            return ActiveContext.WeiXinUserInfos.Where(f => f.groupid == groupid).ToList();
        }

        /// <summary>
        /// 删除该分组用户
        /// </summary>
        /// <param name="openids"></param>
        /// <param name="groupid"></param>
        /// <param name="dohandle"></param>
        public void DelFromGroupByUserIds(IList<string> openids, out DoHandle dohandle)
        {
            dohandle=new DoHandle{IsSuccessful=false,OperateMsg="操作失败"};
            var users = ActiveContext.WeiXinUserInfos.Where(f => openids.Contains(f.openid)).ToList();
            foreach (var item in users)
            {
                item.groupid = 0;
            }
            try
            {
                ActiveContext.SaveChanges();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }
            catch(Exception ex)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = ex.Message;

            }
        }

        /// <summary>
        /// 将指定用户加入目标分组
        /// </summary>
        /// <param name="openids"></param>
        /// <param name="groupid"></param>
        /// <param name="dohandle"></param>
        public void AddToGroupByUserIds(IList<string> openids, int groupid, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败" };
            var users = ActiveContext.WeiXinUserInfos.Where(f => openids.Contains(f.openid)).ToList();
            foreach (var item in users)
            {
                item.groupid = groupid;
            }
            try
            {
                ActiveContext.SaveChanges();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }
            catch (Exception ex)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = ex.Message;

            }
        }




        #region 到货确认年终会

        /// <summary>
        /// 零售户2015年度数据
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public WeiXinRetailerPro GetWeiXinRetailerPro(string openId)
        {
            WeiXinRetailerPro result = new WeiXinRetailerPro();

            //微信用户信息
            WeiXinUserInfo wxUserInfo = ActiveContext.WeiXinUserInfos.SingleOrDefault(f => f.openid == openId);

            //零售户绑定信息
            WeiXinUser wxUser = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.WxUserName == openId);

            if (wxUser == null || wxUserInfo == null)
            {
                return result;
            }
            else
            {
                if (wxUser != null)
                {
                    var orderCount = ActiveContext.LdmDistLines.Where(f => f.LICENSE_CODE == wxUser.SysName).Count();
                    //var AddFriendNum=from a in ActiveContext.LdmDistLines
                    //                  join b in ActiveContext.LdmDists
                    //                  on a.DIST_NUM equals b.DIST_NUM
                    //                  where a.LICENSE_CODE == wxUser.SysName
                    //                  group b.DLVMAN_ID


                    var orders = (from a in ActiveContext.LdmDistLines
                                   join b in ActiveContext.LdmDists
                                   on a.DIST_NUM equals b.DIST_NUM
                                   where a.LICENSE_CODE == wxUser.SysName
                                   orderby b.DIST_DATE
                                   select a
                                   );
                    var dists = (from a in ActiveContext.LdmDistLines
                                  join b in ActiveContext.LdmDists
                                  on a.DIST_NUM equals b.DIST_NUM
                                  where a.LICENSE_CODE == wxUser.SysName
                                  orderby b.DIST_DATE
                                  select b
                                  );
                    var firstDist = (from a in ActiveContext.LdmDistLines
                                      join b in ActiveContext.LdmDists
                                      on a.DIST_NUM equals b.DIST_NUM
                                      where a.LICENSE_CODE == wxUser.SysName
                                      orderby b.DIST_DATE
                                      select b
                                    ).FirstOrDefault();
                    var firstOrder = (from a in ActiveContext.LdmDistLines
                                      join b in ActiveContext.LdmDists
                                      on a.DIST_NUM equals b.DIST_NUM
                                      where a.LICENSE_CODE == wxUser.SysName
                                      orderby b.DIST_DATE
                                      select a
                                    ).FirstOrDefault();
                    var dlvManCounts = dists.Select(f => f.DLVMAN_ID).GroupBy(t => t).Count();
                    var dlvMan = ActiveContext.DistDlvmans.SingleOrDefault(f => f.POSITION_CODE == firstDist.DLVMAN_ID);
                    var fstDlvMan = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.SysName == dlvMan.USER_ID);
                    var fstDlvManWx = ActiveContext.WeiXinUserInfos.SingleOrDefault(f => f.openid == fstDlvMan.WxUserName);
                    var distOrders = (from a in ActiveContext.DistCusts
                                     join b in ActiveContext.LdmDistLines
                                     on a.CO_NUM equals b.CO_NUM
                                      where b.LICENSE_CODE == wxUser.SysName
                                     select a);

                    result.AddFriendNum = dlvManCounts.ToString();
                    result.EvaluateNum=distOrders.Count().ToString();
                    result.FstFriendImg = fstDlvManWx==null?"":fstDlvManWx.headimgurl;
                    result.FstFriendNickName = fstDlvMan == null ? "" : fstDlvMan.Name;
                    result.FstUseWXTime = GetTime(wxUserInfo.subscribe_time).ToString("yyyyMMdd");
                    result.FstSnsTime = firstDist.DIST_DATE;
                    result.RecLike = distOrders.Where(f=>f.DIST_SATIS=="10").Count().ToString();
                    result.RecRedEnvelope = distOrders.Count().ToString();
                    result.SnsNum = orders.Count().ToString();
                    result.WXRankNum =new Random().Next(6800).ToString();                                    
                }
                else
                {
                    return result;
                }
                return result;
            }
        }

        #endregion


        /// <summary>

        /// 时间戳转为C#格式时间

        /// </summary>

        /// <param name=”timeStamp”></param>

        /// <returns></returns>

        private DateTime GetTime(string timeStamp)
        {

            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            long lTime = long.Parse(timeStamp + "0000000");

            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);

        }

        /// <summary>

        /// DateTime时间格式转换为Unix时间戳格式

        /// </summary>

        /// <param name=”time”></param>

        /// <returns></returns>

        private int ConvertDateTimeInt(System.DateTime time)
        {

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

            return (int)(time - startTime).TotalSeconds;

        }
    }
}
