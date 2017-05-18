using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineApp.Model
{
    class VaccineInfo
    {
        #region Properties

        public int vaccineinfo_id { get; set; }
        public string name { get; set; }
        public string general_info { get; set; }
        public string injection_spot { get; set; }
        public string side_effects { get; set; }

        #endregion

        public VaccineInfo(int vaccineInfoId, string name, string generalInfo, string injectionSpot, string sideEffects)
        {
            this.vaccineinfo_id = vaccineInfoId;
            this.name = name;
            this.general_info = generalInfo;
            this.injection_spot = injectionSpot;
            this.side_effects = sideEffects;
        }

    }
}
