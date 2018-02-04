using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeminarMVCServis.DAL.Models
{
    public class SeminarContext:DbContext
    {
        public virtual DbSet<Restoran> Restoran { get; set; }
        public virtual DbSet<Adresa> Adresa { get; set; }
        public virtual DbSet<Gost> Gost { get; set; }
        public virtual DbSet<Sastojak> Sastojak { get; set; }
        public virtual DbSet<Jelo> Jelo { get; set; }
        public virtual DbSet<SastojakJelo> SastojakJelo { get; set; }
        public virtual DbSet<GostJelo> GostJelo { get; set; }
        public virtual DbSet<User> User { get; set; }

        public SeminarContext() { }
        public SeminarContext(DbContextOptions<SeminarContext> options) : base(options)
        {
        }
    }
}
