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
        public string LoginUser(LoginUserModel loginUser);
        public bool ForgetPasswordUser(string email);
        public bool ResetPassoword(string email, PasswordModel modelPassword);
    }
}
