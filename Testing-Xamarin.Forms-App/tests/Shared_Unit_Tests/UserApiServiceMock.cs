using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wired_Brain_UserService;

namespace Shared_Unit_Tests
{
    public class UserApiServiceMock : IUserApiService
    {
        public IList<User> _users { get; set; } = new List<User>();

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public UserResponse Register(User user)
        {
            _users.Add(user);

            return new UserResponse() { EntityState = Status.Created, UserEntity = user };
        }

        public string ResetPassword(int id)
        {
            throw new NotImplementedException();
        }
    }
}
