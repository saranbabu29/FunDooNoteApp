using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabratorRL : ICollabratorRL
    {
        private readonly IConfiguration config;
        private readonly FundooContext fundooContext;
        public CollabratorRL(IConfiguration config, FundooContext fundooContext)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public CollabratorEntity AddCollabrator(CollabratorModel collabratorModel, long userId)
        {
            try
            {
                var Result = fundooContext.noteTable.Where(e => e.NoteId == collabratorModel.NoteId).FirstOrDefault();
                if (Result != null)
                {
                    CollabratorEntity collabratorEntity = new CollabratorEntity();
                    collabratorEntity.NoteId = collabratorModel.NoteId;
                    collabratorEntity.CollabratorMail = collabratorModel.CollabratorMail;
                    collabratorEntity.UserId = userId;
                    fundooContext.CollabratorTable.Add(collabratorEntity);
                    fundooContext.SaveChanges();
                    return collabratorEntity;
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
        public IEnumerable<CollabratorEntity> ReadCollabrator(long UserId)
        {
            try
            {
                var result = this.fundooContext.CollabratorTable.Where(x => x.UserId == UserId);
                if (result != null)
                {
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
        public bool DeleteCollabrator(long UserId, long CollabratorId)
        {
            try
            {
                var result = this.fundooContext.CollabratorTable.Where(x => x.CollabratorId == CollabratorId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.CollabratorTable.Remove(result);
                    fundooContext.SaveChanges();
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
