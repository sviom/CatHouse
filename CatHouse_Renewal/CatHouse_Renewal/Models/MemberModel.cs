using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.Models
{
    public class MemberModel
    {
        // 회원 ID
        public int memID { get; set; }

        // 회원 Email
        [EmailAddress(ErrorMessage = "이메일이 잘못 입력되었습니다.")]
        public string memEmail { get; set; }

        // 회원 이름
        public string memName { get; set; }

        // 회원 집 주소
        public string memAddress { get; set; }

        // 회원 비밀번호
        public string memPassword { get; set; }

        // 회원 전화번호
        public int memPhone { get; set; }
    }
}