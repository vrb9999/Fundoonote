using DatabaseLayer.NoteModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        Task AddNote(int UserId, NoteModel noteModel);
        public List<NoteResponseModel> GetAllNotes();
    }
}
