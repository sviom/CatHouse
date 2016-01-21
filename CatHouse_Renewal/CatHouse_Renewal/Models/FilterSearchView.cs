using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.Models
{
    /// <summary>
    /// 조건 검색했을 때에 해당 정보를 모아서 리턴해야 하기 때문에 클래스를 새로 만들었음
    /// </summary>
    public class FilterSearchView
    {
        // 회원 Email
        [EmailAddress(ErrorMessage = "이메일이 잘못 입력되었습니다.")]
        public string memEmail { get; set; }

        // 회원 이름
        public string memName { get; set; }

        // 회원 집 주소
        public string memAddress { get; set; }

        // 회원 전화번호
        public int memPhone { get; set; }

        // 집 가격
        public int homePrice { get; set; }

        // 기존 반려동물 소개(숫자)
        public string existPetIntro { get; set; }

        // 기존 반려동물 존재 여부
        public bool existPet { get; set; }

        // 집 소개
        public string homeIntro { get; set; }

        // 집 사진 URL
        public string homePhotoURL { get; set; }

        // 집 주소
        public string homeAddress { get; set; }
    }
}