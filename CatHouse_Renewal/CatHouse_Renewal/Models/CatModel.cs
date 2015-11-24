using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.Models
{
    public class CatModel
    {
        // 고유아이디/이름/나이/성별/중성화상태/사진/상태메모

        // 고양이 고유 아이디
        public int catID { get; set; }

        // 고양이 이름
        public string catName { get; set; }

        // 고양이 나이
        public int catAge { get; set; }

        // 고양이 성별
        public int catGender { get; set; }

        // 고양이 중성화 상태
        public int catNeuter { get; set; }

        // 고양이 사진 URL
        public string catPhotoURL { get; set; }

        // 고양이 관련된 메모
        public string catMemo { get; set; }

        // FK로 연동되는 멤버 아이디
        public int memID { get; set; }
    }
}