using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApplication.Data.Configuration;
using TaskApplication.Data.Entities;

namespace TaskApplication.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration config;

        public ApplicationContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            this.config = config;
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("CodeCamp"));
        }

        protected override void OnModelCreating(ModelBuilder bldr)
        {
            bldr.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);

                bldr.Entity<ToDoTask>().HasData(new
            {
                Id = 1,
                Name = "Schoonmaken",
                Status = TypeStatus.Planned,
                BeginDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(4),
                Notes = "Opruimen en netjes maken"
            });
            bldr.Entity<ToDoTask>().HasData(new
            {
                Id = 2,
                Name = "Schilderen",
                Status = TypeStatus.Planned,
                BeginDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(4),
                Notes = "Muren schilderen"
            });
            bldr.Entity<ToDoTask>().HasData(new
            {
                Id = 3,
                Name = "Meubels bouwen",
                Status = TypeStatus.Planned,
                BeginDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(4),
                Notes = "Ikea meubels in elkaar zetten"
            });

            
        }
    }
}
