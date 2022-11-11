using CommonLayer.Model;
using Microsoft.AspNetCore.SignalR;
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
    public class LabelRL : ILabelRL
    {
        private readonly IConfiguration config;
        private readonly FundooContext fundooContext;

        public LabelRL(IConfiguration config, FundooContext fundooContext)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public LabelEntity AddLabel(LabelModel labelModel, long userId)
        {
            try
            {
                var FindNote = fundooContext.noteTable.Where(e => e.NoteId == labelModel.NoteId).FirstOrDefault();
                if (FindNote != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.LabelName = labelModel.LabelName;
                    labelEntity.NoteId = labelModel.NoteId;
                    labelEntity.UserId = userId;
                    fundooContext.labelTable.Add(labelEntity);
                    fundooContext.SaveChanges();

                    return labelEntity;

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
        public IEnumerable<LabelEntity> ReadLabel(long UserId)
        {
            try
            {
                var result = this.fundooContext.labelTable.Where(x => x.UserId == UserId);
                if(result != null)
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
        public LabelEntity UpdateLabel(LabelModel labelModel, long LabelId)
        {
            try
            {
                var result = this.fundooContext.labelTable.Where(x => x.LabelId == LabelId).FirstOrDefault();
                if(result != null && result.LabelId==LabelId)
                {
                    result.LabelName = labelModel.LabelName;
                    result.NoteId = labelModel.NoteId;
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
        public bool DeleteLabel(long UserId, long LabelId)
        {
            try
            {
                var result = this.fundooContext.labelTable.Where(x => x.LabelId == LabelId ).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.labelTable.Remove(result);
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
