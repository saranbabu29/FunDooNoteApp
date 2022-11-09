using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL inoteRL;
        public NoteBL(INoteRL inoteRL)
        {
            this.inoteRL = inoteRL;
        }
        public NoteEntity AddNote(AddNoteModel addNoteModel , long UserID)
        {
            try
            {
                return inoteRL.AddNote(addNoteModel,UserID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<NoteEntity> ReadNote(long UserId)
        {
            try
            {
                return inoteRL.ReadNote(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNote(long UserId, long NoteId)
        {
            try
            {
                return inoteRL.DeleteNote(UserId,NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
