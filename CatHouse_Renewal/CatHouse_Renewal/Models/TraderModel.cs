using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.Models
{
    public class TraderModel
    {

        // 업체 멤버 ID. MemberID와 연동된다 (ForignKEY)
        public int tmemID { get; set; }

        // 집 가격
        public int homePrice { get; set; }

        // 기존 반려동물 소개
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