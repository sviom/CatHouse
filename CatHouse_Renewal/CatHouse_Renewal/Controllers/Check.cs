using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CatHouse_Renewal.Controllers
{
    public class Check
    {
        // 이메일이 맞는지 안맞는지 체크
        public bool CheckEmail(string memEmail)
        {
            bool emailCheckResult =
                Regex.IsMatch(memEmail, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\\.([a-z][a-z|0-9]*(\\.[a-z][a-z|0-9]*)?)$");
            if (!emailCheckResult)
            {
                return false;
            }
            return true;
        }

        // 패스워드 길이 체크
        public bool CheckPasswordLength(string memPassword)
        {
            // 패스워드의 길이는 최소 8자 이상이어야 한다.
            if (memPassword.Length < 8)
            {
                return false;
            }
            return true;
        }
    }
}