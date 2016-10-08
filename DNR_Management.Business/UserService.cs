using DNR_Manager.Data.Customer;
using DNR_Manager.Data.Customer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNR_Manager.Business
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public int GetUser(string userName, string password)
        {
            return _userRepository.GetUser(userName, password);
        }
    }
}
