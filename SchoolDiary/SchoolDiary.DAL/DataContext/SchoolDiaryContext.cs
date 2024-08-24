using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolDiary.DAL.Entities;

namespace SchoolDiary.DAL.DataContext;

public partial class SchoolDiaryContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public SchoolDiaryContext()
    {
    }

    public SchoolDiaryContext(DbContextOptions<SchoolDiaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassPeriod> ClassPeriods { get; set; }

    public virtual DbSet<ClassRoom> ClassRooms { get; set; }

    public virtual DbSet<ClassScore> ClassScores { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<SchoolYear> SchoolYears { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Term> Terms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SchoolDiary");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Class_Id");

            entity.ToTable("Class");

            entity.Property(e => e.Name).HasMaxLength(5);

            entity.HasOne(d => d.ClassPeriod).WithMany(p => p.Classes)
                .HasForeignKey(d => d.ClassPeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_ClassPeriodId");

            entity.HasOne(d => d.ClassRoom).WithMany(p => p.Classes)
                .HasForeignKey(d => d.ClassRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_ClassRoomId");

            entity.HasOne(d => d.Group).WithMany(p => p.Classes)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_GroupId");

            entity.HasOne(d => d.Subject).WithMany(p => p.Classes)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_SubjectId");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_TeacherId");

            entity.HasOne(d => d.Term).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TermId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Class_TermId");
        });

        modelBuilder.Entity<ClassPeriod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ClassPeriod_Id");

            entity.ToTable("ClassPeriod");
        });

        modelBuilder.Entity<ClassRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ClassRoom_Id");

            entity.ToTable("ClassRoom");

            entity.Property(e => e.ClassName).HasMaxLength(30);
        });

        modelBuilder.Entity<ClassScore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ClassScore_Id");

            entity.ToTable("ClassScore");

            entity.HasOne(d => d.Class).WithMany(p => p.ClassScores)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassScore_ClassId");

            entity.HasOne(d => d.Student).WithMany(p => p.ClassScores)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassScore_StudentId");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Group_Id");

            entity.ToTable("Group");

            entity.Property(e => e.Name).HasMaxLength(5);
        });

        modelBuilder.Entity<SchoolYear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SchoolYear_Id");

            entity.ToTable("SchoolYear");

            entity.Property(e => e.YearName).HasMaxLength(30);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Student_Id");

            entity.ToTable("Student");

            entity.HasIndex(e => e.Email, "UQ_Student_Email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ_Student_PhoneNumber").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.EnrolnmentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.MiddleName).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_GroupId");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Subject_Id");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectName).HasMaxLength(30);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Teacher_Id");

            entity.ToTable("Teacher");

            entity.HasIndex(e => e.Email, "UQ_Teacher_Email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ_Teacher_PhoneNumber").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.MiddleName).HasMaxLength(30);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Term_Id");

            entity.ToTable("Term");

            entity.HasOne(d => d.Year).WithMany(p => p.Terms)
                .HasForeignKey(d => d.YearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Term_YearId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
