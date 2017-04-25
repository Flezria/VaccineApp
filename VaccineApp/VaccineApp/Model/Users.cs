using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineApp.Model
{
    class Users
    {
        #region Properties

        public int user_id { get; set; }

        public String password { get; set; }

        public int mobile { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string email { get; set; }

        public string api_key { get; set; }

        #endregion

        public Users(int userid, String password, int mobile, string name, string surname, string email, string apiKey)
        {
            this.user_id = userid;
            this.password = password;
            this.mobile = mobile;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.api_key = apiKey;
        }
    }
}
