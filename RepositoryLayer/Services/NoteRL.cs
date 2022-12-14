using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
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
        public NoteEntity UpdateNote(AddNoteModel addNoteModel,long UserId, long NoteId)
        {
            try
            {
                var result = this.fundooContext.noteTable.Where(x => x.UserId == UserId && x.NoteId == NoteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = addNoteModel.Title;
                    result.Description = addNoteModel.Description;
                    result.Reminder = addNoteModel.Reminder;
                    result.Colour = addNoteModel.Colour;
                    result.Image = addNoteModel.Image;
                    fundooContext.noteTable.Remove(result);
                    this.fundooContext.SaveChanges();
                    return result;
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
        public bool PinNote(long UserId, long NoteId)
        {
            try
            {
                var result = this.fundooContext.noteTable.Where(x => x.UserId == UserId && x.NoteId == NoteId).FirstOrDefault();
                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = this.fundooContext.noteTable.Where(x => x.UserId == UserId && x.NoteId == NoteId).FirstOrDefault();
                if (result.Archieve == true)
                {
                    result.Archieve = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Archieve = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = this.fundooContext.noteTable.Where(x => x.UserId == UserId && x.NoteId == NoteId).FirstOrDefault();
                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = this.fundooContext.noteTable.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if(result != null)
                {
                    if (colour != null)
                    {
                        result.Colour = colour;
                        fundooContext.noteTable.Update(result);
                        fundooContext.SaveChanges();
                        return result;
                    }
                    else
                    {
                        return null;
                    }
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
        public string AddImage(IFormFile Image, long NoteId, long UserId)
        {
            try
            {
                var result = this.fundooContext.noteTable.Where(x => x.UserId == UserId && x.NoteId == NoteId).FirstOrDefault();
                if(result != null)
                {
                    Account account = new Account(
                        this.config["CloudinaryAccount:CloudName"],
                        this.config["CloudinaryAccount:APIKey"],
                        this.config["CloudinaryAccount:APISecret"]);
                    Cloudinary cloudinary = new Cloudinary(account);
                    var UploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(Image.FileName, Image.OpenReadStream()),
                    };
                    var UploadResult = cloudinary.Upload(UploadParameters);
                    string ImagePath = UploadResult.Url.ToString();
                    result.Image = ImagePath;
                    fundooContext.SaveChanges();
                    return "Image Uploaded Successfully";


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

    }
   
}
