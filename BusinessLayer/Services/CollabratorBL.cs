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
    public class CollabratorBL : ICollabratorBL
    {
        private readonly ICollabratorRL icollabratorRL;
        public CollabratorBL(ICollabratorRL icollabratorRL)
        {
            this.icollabratorRL = icollabratorRL;
        }
        public CollabratorEntity AddCollabrator(CollabratorModel collabratorModel, long userId)
        {
            try
            {
                return icollabratorRL.AddCollabrator(collabratorModel, userId);
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
                return icollabratorRL.ReadCollabrator(UserId);
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
                return icollabratorRL.DeleteCollabrator(UserId, CollabratorId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
