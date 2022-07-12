using DatabaseLayer.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public string LoginUser(LoginUserModel loginUser)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spLoginUser", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Email", loginUser.Email);
                    com.Parameters.AddWithValue("@password", loginUser.Password);
                    var result = com.ExecuteNonQuery();
                    SqlDataReader rd = com.ExecuteReader();
                    UserResponseModel response = new UserResponseModel();
                    if (rd.Read())
                    {
                        response.UserId = rd["UserId"] == DBNull.Value ? default : rd.GetInt32("UserId");
                        response.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                        response.password = rd["password"] == DBNull.Value ? default : rd.GetString("password");
                    }
                    return GenerateJWTToken(response.Email, response.UserId);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateJWTToken(string email, int userId)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim("UserId",userId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
