using MPP_C_.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.repository.template
{
    internal interface IUserRepo : IGenericRepo<long, User>
    {
        public User findUserWithNameAndPassword(string username, string password);
    }
}
