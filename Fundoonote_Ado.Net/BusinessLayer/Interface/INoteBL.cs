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
        public List<NoteResponseModel> GetAllNotes();
    }
}
