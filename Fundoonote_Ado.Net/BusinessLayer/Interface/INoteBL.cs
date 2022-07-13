using DatabaseLayer.NoteModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        Task AddNote(int UserId, NoteModel noteModel);
        Task<List<NoteResponseModel>> GetAllNotes(int UserId);
        Task UpdateNote(int UserId, int NoteId, UpdateNoteModel noteModel);
        Task DeleteNote(int UserId, int NoteId);
    }
}
