using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.domain
{
    internal class User : Entity<long>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public User(string username,string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public override string ToString() 
        {
            return "Id: " + this.Id + "; Username: " + Username.ToString() + "; Passowrd: " + Password.ToString();
        }
    }
}
