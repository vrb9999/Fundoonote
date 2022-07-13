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
        public async Task<List<NoteResponseModel>> GetAllNotes(int UserId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<NoteResponseModel> notes = new List<NoteResponseModel>();
            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand com = new SqlCommand("spGetAllNotes", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader rd = await com.ExecuteReaderAsync();
                    while (rd.Read())
                    {
                        NoteResponseModel note = new NoteResponseModel();
                        note.NoteId = rd["NoteId"] == DBNull.Value ? default : rd.GetInt32("NoteId");
                        note.Title = rd["Title"] == DBNull.Value ? default : rd.GetString("Title");
                        note.Description = rd["Description"] == DBNull.Value ? default : rd.GetString("Description");
                        note.Bgcolor = rd["Bgcolor"] == DBNull.Value ? default : rd.GetString("Bgcolor");
                        note.IsPin = rd["IsPin"] == DBNull.Value ? default : rd.GetBoolean("IsPin");
                        note.IsRemainder = rd["IsRemainder"] == DBNull.Value ? default : rd.GetBoolean("IsRemainder");
                        note.IsArchive = rd["IsArchive"] == DBNull.Value ? default : rd.GetBoolean("IsArchive");
                        note.IsTrash = rd["IsTrash"] == DBNull.Value ? default : rd.GetBoolean("IsTrash");
                        note.UserId = rd["UserId"] == DBNull.Value ? default : rd.GetInt32("UserId");
                        note.RegisteredDate = rd["RegisteredDate"] == DBNull.Value ? default : rd.GetDateTime("RegisteredDate");
                        note.Remainder = rd["Remainder"] == DBNull.Value ? default : rd.GetDateTime("Remainder");
                        note.ModifiedDate = rd["ModifiedDate"] == DBNull.Value ? default : rd.GetDateTime("ModifiedDate");
                        notes.Add(note);

                    }
                    return notes;
                }
            }

            catch (Exception)
            {

                throw;
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
        public async Task DeleteNote(int UserId, int NoteId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var result = 0;
            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand com = new SqlCommand("spDeleteNote", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserId", UserId);
                    com.Parameters.AddWithValue("@NoteId", NoteId);
                    result = await com.ExecuteNonQueryAsync();
                    if (result <= 0)
                    {
                        throw new Exception("Note Does not Exists");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
