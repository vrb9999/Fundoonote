using DatabaseLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public void AddUser(UsersModel users);
        public List<UserResponseModel> GetAllUsers();
    }
}
