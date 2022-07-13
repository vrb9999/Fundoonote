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
                    await com.ExecuteNonQueryAsync();

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
        public async Task UpdateNote(int UserId, int NoteId, UpdateNoteModel noteModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var result = 0;

            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand com = new SqlCommand("spUpdateNote", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@title", noteModel.Title);
                    com.Parameters.AddWithValue("@description", noteModel.Description);
                    com.Parameters.AddWithValue("@Bgcolor", noteModel.Bgcolor);
                    com.Parameters.AddWithValue("@UserId", UserId);
                    com.Parameters.AddWithValue("@NoteId", NoteId);
                    com.Parameters.AddWithValue("@IsPin", noteModel.IsPin);
                    com.Parameters.AddWithValue("@IsArchive", noteModel.IsArchive);
                    com.Parameters.AddWithValue("@IsTrash", noteModel.IsTrash);
                    result = await com.ExecuteNonQueryAsync();
                    if (result <= 0)
                    {
                        throw new Exception("Note Does not Exist");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
