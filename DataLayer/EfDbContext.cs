using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer
{
    public class EfDbContext: DbContext
    {
        public DbSet<DirectoryObject> directoryObjects { get; set; }
        public DbSet<Entities.Version> ObjectVersions{ get; set; }
        public DbSet<DirectoryObjectVersion> directoryObjectVersions { get; set; }

        public EfDbContext(DbContextOptions<EfDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        public IConfiguration Configuration { get; }
        
    }
}
