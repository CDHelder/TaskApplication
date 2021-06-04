using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskApplication.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskApplication.Data.Configuration
{
    public class ToDoTaskConfiguration : IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            builder.ToTable("ToDoTasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.BeginDate)
                .IsRequired();

            builder.Property(t => t.Name)
                .IsRequired();

            builder.Property(t => t.Status);

            builder.Property(t => t.EndDate);

            builder.Property(t => t.Notes);
        }
    }
}
