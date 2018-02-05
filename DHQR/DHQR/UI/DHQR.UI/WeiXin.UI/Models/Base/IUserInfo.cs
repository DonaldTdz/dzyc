using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHQR.UI.Models
{
    public interface IUserInfo
    {
        string GetUserName();
        string GetUserKey();
    }
}