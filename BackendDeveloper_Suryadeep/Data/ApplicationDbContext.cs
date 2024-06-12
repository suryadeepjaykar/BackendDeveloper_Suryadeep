﻿using BackendDeveloper_Suryadeep.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendDeveloper_Suryadeep.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne(d => d.ParentDepartment)
                .WithMany(d => d.SubDepartments)
                .HasForeignKey(d => d.ParentDepartmentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
