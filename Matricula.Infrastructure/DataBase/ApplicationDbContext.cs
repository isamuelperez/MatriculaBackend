using Matricula.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Infrastructure.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<MateriaTeacher> MateriaTeachers { get; set; }
        public DbSet<StudentMateria> StudentMaterias { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Student>().
                HasIndex(s => s.Identification).IsUnique();
            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.Identification).IsUnique();

            modelBuilder.Entity<MateriaTeacher>()
                .HasOne(mp => mp.Materia)
                .WithMany(m => m.Teachers)
                .HasForeignKey(mp => mp.MateriaId);

            modelBuilder.Entity<MateriaTeacher>()
                .HasOne(mp => mp.Teacher)
                .WithMany(p => p.MateriasAssigned)
                .HasForeignKey(mp => mp.TeacherId);

            modelBuilder.Entity<StudentMateria>()
                .HasOne(em => em.Student)
                .WithMany(e => e.StudentMaterias)
                .HasForeignKey(em => em.StudentId);

            modelBuilder.Entity<StudentMateria>()
                .HasOne(em => em.Materia)
                .WithMany()
                .HasForeignKey(em => em.MateriaId);

            modelBuilder.Entity<StudentMateria>()
                .HasOne(em => em.Teacher)
                .WithMany()
                .HasForeignKey(em => em.TeacherId);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
        }
    }
}
