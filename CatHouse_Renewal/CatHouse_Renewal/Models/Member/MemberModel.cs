using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.Models
{
    public class MemberModel
    {
        public int memID { get; set; }
        [EmailAddress(ErrorMessage = "이메일이 잘못 입력되었습니다.")]
        public string memEmail { get; set; }
        public string memName { get; set; }
        public string memAddress { get; set; }
        public string memPassword { get; set; }
        public int memPhone { get; set; }
    }
}