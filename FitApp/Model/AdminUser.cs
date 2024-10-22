using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.Model
{
    class AdminUser : User
    {
        public AdminUser(string Country, string Username, string Password ) : base ( Country, Username, Password )
        {
            User admin = new AdminUser( "Sverige", "admin", "ABCD1234" );
        }
        public void ManageAllWorkouts()
        {

        }
    }
}
