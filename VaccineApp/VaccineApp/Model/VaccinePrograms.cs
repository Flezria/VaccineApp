using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineApp.Model
{
    class VaccinePrograms
    {

        #region Properties

        public int program_id { get; set; }
        public int year { get; set; } // developer note : change to datetime in database since vaccine programs might change multiple times during a year.

        #endregion

        public VaccinePrograms(int programId, int year)
        {
            this.program_id = programId;
            this.year = year;
        }

    }
}
