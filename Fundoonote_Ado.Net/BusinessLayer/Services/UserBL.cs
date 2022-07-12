using BusinessLayer.Interface;
using DatabaseLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public void AddUser(UsersModel users)
        {
            try
            {
                this.userRL.AddUser(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserResponseModel> GetAllUsers()
        {
            try
            {
                return this.userRL.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string LoginUser(LoginUserModel loginUser)
        {
            try
            {
                return this.userRL.LoginUser(loginUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ForgetPasswordUser(string email)
        {
            try
            {
                return this.userRL.ForgetPasswordUser(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
