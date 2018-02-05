using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using System.ComponentModel;


namespace DHQR.UI.Models
{
    /// <summary>
    /// 配送日期配置
    /// </summary>
    public class DistDateConfigModel
    {
        #region Model
        private Guid _id;
        private string _value;
        private string _enumtype;
        private string _typenote;
        private string _valuenote;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 枚举类型
        /// </summary>
        public string EnumType
        {
            set { _enumtype = value; }
            get { return _enumtype; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string TypeNote
        {
            set { _typenote = value; }
            get { return _typenote; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ValueNote
        {
            set { _valuenote = value; }
            get { return _valuenote; }
        }

        /// <summary>
        /// 系统配送日期
        /// </summary>
        public string SysDistDate
        {
            get;
            set;
        }

        /// <summary>
        /// 实际配送日期
        /// </summary>
        public string RealDistDate
        {
            get;
            set;
        }


        #endregion Model
    }

    /// <summary>
    /// 配送日期配置服务
    /// </summary>
    public class DistDateConfigModelService
    {
        /// <summary>
        /// 获取当前配置
        /// </summary>
        /// <returns></returns>
        public DistDateConfigModel GetDistDateConfig()
        {
            var data = new BaseEnumLogic().GetByType("DistDate").FirstOrDefault();
            DistDateConfigModel result = new DistDateConfigModel 
            {
                Id=data.Id,
                Value=data.Value,
                EnumType=data.EnumType,
                TypeNote=data.TypeNote,
                ValueNote=data.Value,
                SysDistDate=DateTime.Now.ToString("yyyy-MM-dd"),
                RealDistDate=DateTime.Now.AddDays(int.Parse(data.Value)).ToString("yyyy-MM-dd")
            };
            return result;
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dohandle"></param>
        public void SaveDistDateConfig(DistDateConfigModel model,out DoHandle dohandle)
        {
            var sysDate = DateTime.Parse(model.SysDistDate);
            var realDate = DateTime.Parse(model.RealDistDate);
            TimeSpan dateValue = realDate - sysDate;
            var realValue = dateValue.Days;
            BaseEnumLogic logic = new BaseEnumLogic();
            BaseEnum baseEnum = logic.GetAll().SingleOrDefault(f => f.Id == model.Id);
           // baseEnum.Value =model
            baseEnum.Value = realValue.ToString();
            logic.Update(baseEnum, out dohandle);
        }
    }


    #region 关键词类型[图片、文本、连接]
    public class KeyWordType
    {
        public enum Type : int { Text, Image, LinkUrl };

        //文本消息
        public static int Text
        {
            get
            {
                return (int)Type.Text;
            }
        }

        //图片消息
        public static int Image
        {
            get
            {
                return (int)Type.Image;
            }
        }


        //连接类型
        public static int LinkUrl
        {
            get
            {
                return (int)Type.LinkUrl;
            }
        }


    }
    #endregion

    #region 匹配模式[模糊匹配、精确匹配]
    public class KeyWordPatternMethod
    {
        public enum PatternMethod : int { Fuzzy, Exact };

        //模糊匹配
        public static int Fuzzy
        {
            get
            {
                return (int)PatternMethod.Fuzzy;
            }
        }

        //精确匹配
        public static int Exact
        {
            get
            {
                return (int)PatternMethod.Exact;
            }
        }
    }
    #endregion

    #region 会员来源[注册、微信]
    public class VipUserFrom
    {
        public enum From : int { Register, WeiXin };

        //微信
        public static int WeiXin
        {
            get
            {
                return (int)From.WeiXin;
            }
        }

        //注册
        public static int Register
        {
            get
            {
                return (int)From.Register;
            }
        }
    }
    #endregion

    #region 会员积分操作
    public class CreditOperate
    {
        public enum Operate : int { Manually, Daily };

        //手动添加
        public static int Manually
        {
            get
            {
                return (int)Operate.Manually;
            }
        }

        //每日签到
        public static int Daily
        {
            get
            {
                return (int)Operate.Daily;
            }
        }
    }
    #endregion

    #region 官网动画效果
    public class AnimationType
    {
        public enum Animation : int
        {
            None, Snowflake,
            Fireworks
        };

        //无
        public static int None
        {
            get
            {
                return (int)Animation.None;
            }
        }

        //雪花
        public static int Snowflake
        {
            get
            {
                return (int)Animation.Snowflake;
            }
        }


        //烟花
        public static int Fireworks
        {
            get
            {
                return (int)Animation.Fireworks;
            }
        }
    }
    #endregion

    #region 官网动引导页
    public class GuidePageType
    {
        public enum GuidePage : int
        {
            None, Mosaic,
            FadeOut,OpenDoor
        };

        //无
        public static int None
        {
            get
            {
                return (int)GuidePage.None;
            }
        }

        //马赛克
        public static int Mosaic
        {
            get
            {
                return (int)GuidePage.Mosaic;
            }
        }


        //淡出
        public static int FadeOut
        {
            get
            {
                return (int)GuidePage.FadeOut;
            }
        }

        //开门
        public static int OpenDoor
        {
            get
            {
                return (int)GuidePage.OpenDoor;
            }
        }
    }
    #endregion

    #region 官网风格类型
    public class SkinType
    {
        public enum Type : int
        {
            Car
        };

        //汽车
        public static int Car
        {
            get
            {
                return (int)Type.Car;
            }
        }

    }
    #endregion


    #region 官网首页内容配置 每一个可配置节点的类型
    public class SkinItemType
    {
        public enum Type : int
        {
            PosterNoLinker,//0
            PosterLinker,//1
            ImageLinker,//2
            TextLinker,//3
            ImageTextLinker//4
        };

        //不可点击的海报
        public static int PosterNoLinker
        {
            get
            {
                return (int)Type.PosterNoLinker;
            }
        }

        //可点击的海报
        public static int PosterLinker
        {
            get
            {
                return (int)Type.PosterLinker;
            }
        }

        //图片链接
        public static int ImageLinker
        {
            get
            {
                return (int)Type.ImageLinker;
            }
        }

        //文字链接
        public static int TextLinker
        {
            get
            {
                return (int)Type.TextLinker;
            }
        }


        //图片文字链接
        public static int ImageTextLinker
        {
            get
            {
                return (int)Type.ImageTextLinker;
            }
        }

    }
    #endregion


    #region 官网栏目列表样式
    //对应样式图片WeiXinImages/banner/column-article-list..
    public class ListType
    {
        public enum Type : int
        {
            One, Tow, Three, Four, Five, Six
        };


        public static int One
        {
            get
            {
                return (int)Type.One;
            }
        }

        public static int Tow
        {
            get
            {
                return (int)Type.Tow;
            }
        }

        public static int Three
        {
            get
            {
                return (int)Type.Three;
            }
        }

        public static int Four
        {
            get
            {
                return (int)Type.Four;
            }
        }

        public static int Five
        {
            get
            {
                return (int)Type.Five;
            }
        }
        
        public static int Six
        {
            get
            {
                return (int)Type.Six;
            }
        }

    }
    #endregion

    #region 问卷题目类型
    public class QuestionType
    {
        public enum Type : int
        {
            ChoiceQuestion, MultipleChoiceQuestion, EssayQquestion
        };

        //单选题
        [DisplayName("单选题")]
        public static int ChoiceQuestion
        {
            get
            {
                return (int)Type.ChoiceQuestion;
            }
        }

        //多选题
        [DisplayName("多选题")]
        public static int MultipleChoiceQuestion
        {
            get
            {
                return (int)Type.MultipleChoiceQuestion;
            }
        }

        //问答题
        [DisplayName("问答题")]
        public static int EssayQquestion
        {
            get
            {
                return (int)Type.EssayQquestion;
            }
        }

    }
    #endregion
}