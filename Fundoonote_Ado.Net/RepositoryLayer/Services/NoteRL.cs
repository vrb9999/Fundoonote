using DatabaseLayer.NoteModels;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        private readonly string connectionString;
        public NoteRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("Fundoonotes");
        }

        public async Task AddNote(int UserId, NoteModel noteModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand com = new SqlCommand("spAddNote", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@title", noteModel.Title);
                    com.Parameters.AddWithValue("@description", noteModel.Description);
                    com.Parameters.AddWithValue("@Bgcolor", noteModel.Bgcolor);
                    com.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader rd = com.ExecuteReader();
                    if (rd.Read())
                    {
                        NoteResponseModel response = new NoteResponseModel();
                        response.NoteId = rd["NoteId"] == DBNull.Value ? default : rd.GetInt32("NoteId");
                        response.Title = rd["Title"] == DBNull.Value ? default : rd.GetString("Title");
                        response.Description = rd["Description"] == DBNull.Value ? default : rd.GetString("Description");
                        response.Bgcolor = rd["Bgcolor"] == DBNull.Value ? default : rd.GetString("Bgcolor");
                        response.IsPin = rd["IsPin"] == DBNull.Value ? default : rd.GetBoolean("IsPin");
                        response.IsArchive = rd["IsArchive"] == DBNull.Value ? default : rd.GetBoolean("IsArchive");
                        response.IsRemainder = rd["IsRemainder"] == DBNull.Value ? default : rd.GetBoolean("IsRemainder");
                        response.IsTrash = rd["IsTrash"] == DBNull.Value ? default : rd.GetBoolean("IsTrash");
                        response.UserId = rd["UserId"] == DBNull.Value ? default : rd.GetInt32("UserId");
                        response.RegisteredDate = rd["RegisteredDate"] == DBNull.Value ? default : rd.GetDateTime("RegisteredDate");
                        response.Remainder = rd["Remainder"] == DBNull.Value ? default : rd.GetDateTime("Remainder");
                        response.ModifiedDate = rd["ModifiedDate"] == DBNull.Value ? default : rd.GetDateTime("ModifiedDate");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NoteResponseModel> GetAllNotes()
        {

            List<NoteResponseModel> notes = new List<NoteResponseModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spGetAllNotes", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        NoteResponseModel note = new NoteResponseModel();
                        note.NoteId = reader["NoteId"] == DBNull.Value ? default : reader.GetInt32("NoteId");
                        note.Title = reader["Title"] == DBNull.Value ? default : reader.GetString("Title");
                        note.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                        note.Bgcolor = reader["Bgcolor"] == DBNull.Value ? default : reader.GetString("Bgcolor");
                        note.IsPin = reader["IsPin"] == DBNull.Value ? default : reader.GetBoolean("IsPin");
                        note.IsArchive = reader["IsArchive"] == DBNull.Value ? default : reader.GetBoolean("IsArchive");
                        note.IsRemainder = reader["IsRemainder"] == DBNull.Value ? default : reader.GetBoolean("IsRemainder");
                        note.IsTrash = reader["IsTrash"] == DBNull.Value ? default : reader.GetBoolean("IsTrash");
                        note.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        note.RegisteredDate = reader["RegisteredDate"] == DBNull.Value ? default : reader.GetDateTime("RegisteredDate");
                        note.Remainder = reader["Remainder"] == DBNull.Value ? default : reader.GetDateTime("Remainder");
                        note.ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? default : reader.GetDateTime("ModifiedDate");

                        notes.Add(note);
                    }
                    return notes;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
