using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public NoteEntity UpdateNote(AddNoteModel addNoteModel, long UserId, long NoteId)
        {
            try
            {
                return inoteRL.UpdateNote(addNoteModel,UserId,NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool PinNote(long UserId, long NoteId)
        {
            try
            {
                return inoteRL.PinNote(UserId, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ArchieveNote(long UserId, long NoteId)
        {
            try
            {
                return inoteRL.ArchieveNote(UserId, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool TrashNote(long UserId, long NoteId)
        {
            try
            {
                return inoteRL.TrashNote(UserId, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity NoteColour(string colour, long NoteId)
        {
            try
            {
                return inoteRL.NoteColour(colour, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string AddImage(IFormFile Image, long NoteId, long UserId)
        {
            try
            {
                return inoteRL.AddImage(Image, NoteId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
