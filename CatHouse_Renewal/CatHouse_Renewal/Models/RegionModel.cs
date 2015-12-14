using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.Models
{
    public class RegionModel
    {
        // 시/도
        public class City
        {
            public string cityName { get; set; }
        }

        // 군/구
        public class County
        {
            public string countyName { get; set; }
        }
        
        // 동/읍/리
        public class Town
        {
            public string townName { get; set; }
        }
    }
}