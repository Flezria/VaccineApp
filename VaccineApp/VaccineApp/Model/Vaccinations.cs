using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineApp.Model
{
    class Vaccinations
    {
        #region Properties

        public int vaccine_id { get; set; }
        public string name { get; set; }
        public int injection_month { get; set; }
        public int program_id { get; set; }
        public int vaccineinfo_id { get; set; }


        #endregion

        public Vaccinations(int vaccineId, string name, int injectionMonth, int programId, int vaccineinfo_id)
        {
            this.vaccine_id = vaccineId;
            this.name = name;
            this.injection_month = injectionMonth;
            this.program_id = programId;
            this.vaccineinfo_id = vaccine_id;
        }
    }
}
