using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public NoteEntity UpdateNote(AddNoteModel addNoteModel, long UserId, long NoteId);
        public bool PinNote(long UserId, long NoteId);
        public bool ArchieveNote(long UserId, long NoteId);
        public bool TrashNote(long UserId, long NoteId);
        public NoteEntity NoteColour(string colour, long NoteId);
        public string AddImage(IFormFile Image, long NoteId, long UserId);
    }
}
