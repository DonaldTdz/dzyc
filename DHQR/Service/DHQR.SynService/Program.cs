using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using DHQR.BasicLib;
using DHQR.BusinessLogic.Implement;

namespace DHQR.SynService
{
    /// <summary>
    /// 数据自动同步服务
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
           
            DoHandle dohandle;
         
           //同步配送员信息
            //DistDlvmanLogic dlvManLogic = new DistDlvmanLogic();
            //Console.WriteLine(string.Format("开始同步配送员数据！"));
            //dlvManLogic.SynDlvmans(out dohandle);
            //Console.WriteLine(string.Format("同步完成，处理结果：{0}！", dohandle.OperateMsg));

       
           Console.WriteLine(string.Format("开始同步零售户数据！"));
           RetailerLogic retailerLogic = new RetailerLogic();
           retailerLogic.SysCustomer("300000001", out dohandle);
           Console.WriteLine(string.Format("同步完成，处理结果：{0}！", dohandle.OperateMsg));
           //Console.ReadLine();
         

           /*
          //同步线路信息
          DistRutLogic rutLogic = new DistRutLogic();
          Console.WriteLine(string.Format("开始同步线路数据！"));
          rutLogic.SynDistRuts(out dohandle);
          Console.WriteLine(string.Format("同步完成，处理结果：{0}！", dohandle.OperateMsg));

           

        //更新零售户坐标
        GisCustPoisLogic cusPoiLogic = new GisCustPoisLogic();
        Console.WriteLine(string.Format("开始更新零售坐标！"));
        cusPoiLogic.SynLatLng();
        Console.WriteLine(string.Format("更新完成！"));


          */

            /*
            //同步车辆信息
            LdmDistCarLogic carLogic = new LdmDistCarLogic();
            Console.WriteLine(string.Format("开始同步线路数据！"));
            carLogic.SynLdmCars(out dohandle);
            Console.WriteLine(string.Format("同步完成，处理结果：{0}！", dohandle.OperateMsg));

             */
            /*
            GisCustPoisLogic custLogic = new GisCustPoisLogic();
            Console.WriteLine(string.Format("开始同步客户位置信息！"));
            custLogic.SynCustPois();
            Console.WriteLine(string.Format("同步完成！"));
            Console.ReadLine();
             */


            Console.ReadLine();

        }
    }
}
