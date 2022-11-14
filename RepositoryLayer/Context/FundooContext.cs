﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
           : base(options)
        {

        }
        public DbSet<UserEntity> userTable { get; set; }
        public DbSet<NoteEntity> noteTable { get; set; }
        public DbSet<LabelEntity> labelTable { get; set; }
        public DbSet<CollabratorEntity> CollabratorTable { get; set; }
    }
    
}
