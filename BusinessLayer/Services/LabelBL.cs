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
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL ilabelRL;
        public LabelBL(ILabelRL ilabelRL)
        {
            this.ilabelRL = ilabelRL;
        }
        public LabelEntity AddLabel(LabelModel labelModel, long userId)
        {
            try
            {
                return ilabelRL.AddLabel(labelModel, userId);
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
                return ilabelRL.ReadLabel(UserId);
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
                return ilabelRL.UpdateLabel(labelModel,LabelId);
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
                return ilabelRL.DeleteLabel(UserId, LabelId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
