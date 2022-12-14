using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabratorRL
    {
        public CollabratorEntity AddCollabrator(CollabratorModel collabratorModel, long userId);
        public IEnumerable<CollabratorEntity> ReadCollabrator(long UserId);
        public bool DeleteCollabrator(long UserId, long CollabratorId);
    }
}
