using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        private readonly IConfiguration config;
        private readonly FundooContext fundooContext;
        public NoteRL(IConfiguration config, FundooContext fundooContext)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public NoteEntity AddNote(AddNoteModel addNoteModel, long UserID)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = addNoteModel.Title;
                noteEntity.Description = addNoteModel.Description;
                noteEntity.Reminder = addNoteModel.Reminder;
                noteEntity.Colour = addNoteModel.Colour;
                noteEntity.Image = addNoteModel.Image;
                noteEntity.Archieve = addNoteModel.Archieve;
                noteEntity.Pin = addNoteModel.Pin;
                noteEntity.Trash = addNoteModel.Trash;
                noteEntity.Created = DateTime.Now;
                noteEntity.Updated = DateTime.Now;
                noteEntity.UserId = UserID;
                fundooContext.noteTable.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }
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
                var result=this.fundooContext.noteTable.Where(x=> x.UserId==UserId);
                return result;
                
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
                var result = this.fundooContext.noteTable.Where(x => x.UserId == UserId && x.NoteId==NoteId).FirstOrDefault();
                if(result != null)
                {
                    fundooContext.noteTable.Remove(result);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
   
}
