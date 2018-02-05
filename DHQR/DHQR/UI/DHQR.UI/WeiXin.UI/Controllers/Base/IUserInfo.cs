using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHQR.UI.Controllers
{
    public interface IUserInfo
    {
        string GetLogonName();
        string GetUserKey();

    }
}