using DatabaseLayer.User;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {

        private readonly string connectionString;
        public UserRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("Fundoonotes");
        }

        public void AddUser(UsersModel users)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand com = new SqlCommand("spAddUser", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Firstname", users.FirstName);
                    com.Parameters.AddWithValue("@Lastname", users.LastName);
                    com.Parameters.AddWithValue("@Email", users.Email);
                    com.Parameters.AddWithValue("@password", users.Password);
                    var result = com.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<UserResponseModel> GetAllUsers()
        {

            List<UserResponseModel> users = new List<UserResponseModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spGetAllUser", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        UserResponseModel user = new UserResponseModel();
                        user.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        user.FirstName = reader["Firstname"] == DBNull.Value ? default : reader.GetString("Firstname");
                        user.LastName = reader["Lastname"] == DBNull.Value ? default : reader.GetString("Lastname");
                        user.Email = reader["Email"] == DBNull.Value ? default : reader.GetString("Email");
                        user.password = reader["Password"] == DBNull.Value ? default : reader.GetString("password");
                        user.CreatedDate = reader["CreatedDate"] == DBNull.Value ? default : reader.GetDateTime("CreatedDate");
                        user.ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? default : reader.GetDateTime("ModifiedDate");
                        users.Add(user);
                    }
                    return users;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
