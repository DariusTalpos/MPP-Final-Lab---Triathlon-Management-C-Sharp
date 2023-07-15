using MPP_C_.domain;
using MPP_C_.repository.template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_C_.service
{
    internal class UserService
    {
        private IUserRepo userRepo;

        public UserService(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public int save(String username, String password)
        {
            User user = new User(username, password);
            if (userRepo.Save(user) != null)
                return 1;
            return 0;
        }

        public User userExists(String username, String password)
        {
            return userRepo.findUserWithNameAndPassword(username, password);
        }
    }
}
