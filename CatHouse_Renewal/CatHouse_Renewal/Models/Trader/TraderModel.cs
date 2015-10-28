using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatHouse_Renewal.Models
{
    public class TraderModel
    {
        public int tmemID { get; set; }
        public int homePrice { get; set; }
        public string existPetIntro { get; set; }
        public bool existPet { get; set; }
        public string homeIntro { get; set; }
        public string homePhotoURL { get; set; }
    }
}