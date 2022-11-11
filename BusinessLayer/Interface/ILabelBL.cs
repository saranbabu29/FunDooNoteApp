﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabel(LabelModel labelModel, long userId);
        public IEnumerable<LabelEntity> ReadLabel(long UserId);
        public LabelEntity UpdateLabel(LabelModel labelModel, long LabelId);
        public bool DeleteLabel(long UserId, long LabelId);
    }
}
