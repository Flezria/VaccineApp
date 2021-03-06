﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineApp.Model
{
    class UserChilds
    {
        #region Properties

        public int child_id { get; set; }

        public string name { get; set; }

        public String gender { get; set; }

        public DateTime birthday { get; set; }

        public string api_key { get; set; }

        public int program_id { get; set; }

        #endregion

        public UserChilds(int child_id, string name, DateTime birthday, string api_key, string gender, int program_id)
        {
            this.child_id = child_id;
            this.name = name;
            this.gender = gender;
            this.birthday = birthday;
            this.api_key = api_key;
            this.program_id = program_id;
        }


        public override string ToString()
        {
            return name;
        }

    }
}
