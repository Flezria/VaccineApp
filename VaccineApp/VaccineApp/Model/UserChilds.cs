using System;
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

        public DateTime birthday { get; set; }

        public int api_key { get; set; }

        public int user_id { get; set; }

        public int program_id { get; set; }

        #endregion

        public UserChilds(int childId, string name, DateTime birthday, int apiKey, int userId, int programId)
        {
            this.child_id = childId;
            this.name = name;
            this.birthday = birthday;
            this.api_key = apiKey;
            this.user_id = userId;
            this.program_id = programId;
        }

    }
}
