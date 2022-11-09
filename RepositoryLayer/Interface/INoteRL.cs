using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity AddNote(AddNoteModel addNoteModel, long UserID);
        public IEnumerable<NoteEntity> ReadNote(long UserId);
        public bool DeleteNote(long UserId, long NoteId);
    }
}
