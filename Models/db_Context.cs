using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EducationWebApi.Models;

public partial class db_Context : DbContext
{
    public db_Context()
    {
    }

    public db_Context(DbContextOptions<db_Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CourseByCycleMaster> CourseByCycleMasters { get; set; }

    public virtual DbSet<CourseMaster> CourseMasters { get; set; }

    public virtual DbSet<CouseImageMap> CouseImageMaps { get; set; }

    public virtual DbSet<CycleMaster> CycleMasters { get; set; }

    public virtual DbSet<EnrollmentMaster> EnrollmentMasters { get; set; }

    public virtual DbSet<ImageMaster> ImageMasters { get; set; }

    public virtual DbSet<ImageType> ImageTypes { get; set; }

    public virtual DbSet<SlideMaster> SlideMasters { get; set; }

    public virtual DbSet<StudentMaster> StudentMasters { get; set; }

    public virtual DbSet<SubjectMaster> SubjectMasters { get; set; }

    public virtual DbSet<SubjectPointMaster> SubjectPointMasters { get; set; }

    public virtual DbSet<SubjectPointStepMaster> SubjectPointStepMasters { get; set; }

    public virtual DbSet<TrainingCenterMaster> TrainingCenterMasters { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    public virtual DbSet<UserTypeMaster> UserTypeMasters { get; set; }

    public virtual DbSet<VideoMaster> VideoMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db_sipseducation;User id=sa;password=dev_1234567;Connect Timeout=30;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseByCycleMaster>(entity =>
        {
            entity.HasKey(e => e.CourseByCycleId);

            entity.ToTable("CourseByCycleMaster");

            entity.Property(e => e.CourseByCycleId).HasColumnName("CourseByCycleID");
            entity.Property(e => e.CourseEndDate).HasColumnType("datetime");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseStartDate).HasColumnType("datetime");
            entity.Property(e => e.CycleId).HasColumnName("CycleID");
        });

        modelBuilder.Entity<CourseMaster>(entity =>
        {
            entity.HasKey(e => e.CourseId);

            entity.ToTable("CourseMaster");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseDescription).HasMaxLength(150);
            entity.Property(e => e.CourseFee).HasColumnType("money");
            entity.Property(e => e.CourseImageLg).HasColumnName("CourseImageLG");
            entity.Property(e => e.CourseImageSm).HasColumnName("CourseImageSM");
            entity.Property(e => e.CourseName).HasMaxLength(100);
        });

        modelBuilder.Entity<CouseImageMap>(entity =>
        {
            entity.HasKey(e => e.CourseImageId).HasName("PK_CouseImageMapp");

            entity.ToTable("CouseImageMap");

            entity.Property(e => e.CourseImageId).HasColumnName("CourseImageID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
        });

        modelBuilder.Entity<CycleMaster>(entity =>
        {
            entity.HasKey(e => e.CycleId);

            entity.ToTable("CycleMaster");

            entity.Property(e => e.CycleId).HasColumnName("CycleID");
            entity.Property(e => e.CycleCreateDate).HasColumnType("datetime");
            entity.Property(e => e.CycleDescription).HasMaxLength(250);
            entity.Property(e => e.CycleEndDate).HasColumnType("datetime");
            entity.Property(e => e.CycleStartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<EnrollmentMaster>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId);

            entity.ToTable("EnrollmentMaster");

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.CancellationReason).HasMaxLength(50);
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CycleId).HasColumnName("CycleID");
            entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TrainingCenterId).HasColumnName("TrainingCenterID");
        });

        modelBuilder.Entity<ImageMaster>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("ImageMaster");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.Extension).HasMaxLength(50);
            entity.Property(e => e.ImageDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageName).HasMaxLength(250);
            entity.Property(e => e.ImageTypeId).HasColumnName("ImageTypeID");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.ImageType).WithMany(p => p.ImageMasters)
                .HasForeignKey(d => d.ImageTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImageMaster_ImageTypeID");
        });

        modelBuilder.Entity<ImageType>(entity =>
        {
            entity.ToTable("ImageType");

            entity.Property(e => e.ImageTypeId).HasColumnName("ImageTypeID");
            entity.Property(e => e.ImageType1)
                .HasMaxLength(50)
                .HasColumnName("ImageType");
        });

        modelBuilder.Entity<SlideMaster>(entity =>
        {
            entity.HasKey(e => e.SlideId);

            entity.ToTable("SlideMaster");

            entity.Property(e => e.SlideId).HasColumnName("SlideID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.SlideName).HasMaxLength(50);
        });

        modelBuilder.Entity<StudentMaster>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("StudentMaster");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .HasColumnName("EmailID");
            entity.Property(e => e.StudentName).HasMaxLength(50);
        });

        modelBuilder.Entity<SubjectMaster>(entity =>
        {
            entity.HasKey(e => e.SubjectId);

            entity.ToTable("SubjectMaster");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.SubjectName).HasMaxLength(250);
        });

        modelBuilder.Entity<SubjectPointMaster>(entity =>
        {
            entity.HasKey(e => e.SubjectPointId);

            entity.ToTable("SubjectPointMaster");

            entity.Property(e => e.SubjectPointId).HasColumnName("SubjectPointID");
            entity.Property(e => e.SubjectDescription).HasMaxLength(450);
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.SubjectImageId).HasColumnName("SubjectImageID");
            entity.Property(e => e.SubjectPointName).HasMaxLength(250);
            entity.Property(e => e.VideoId).HasColumnName("VideoID");
        });

        modelBuilder.Entity<SubjectPointStepMaster>(entity =>
        {
            entity.HasKey(e => e.SubjectStepId);

            entity.ToTable("SubjectPointStepMaster");

            entity.Property(e => e.SubjectStepId).HasColumnName("SubjectStepID");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.SubjectPointId).HasColumnName("SubjectPointID");
            entity.Property(e => e.SubjectStepDescription).HasMaxLength(450);
            entity.Property(e => e.SubjectStepImageId).HasColumnName("SubjectStepImageID");
            entity.Property(e => e.SubjectStepName).HasMaxLength(250);
            entity.Property(e => e.SubjectStepVideoId).HasColumnName("SubjectStepVideoID");
        });

        modelBuilder.Entity<TrainingCenterMaster>(entity =>
        {
            entity.HasKey(e => e.CenterId);

            entity.ToTable("TrainingCenterMaster");

            entity.Property(e => e.CenterId).HasColumnName("CenterID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CenterName).HasMaxLength(150);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .HasColumnName("EmailID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserMaster");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.UserName).HasMaxLength(150);
            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
        });

        modelBuilder.Entity<UserTypeMaster>(entity =>
        {
            entity.HasKey(e => e.UserTypeId);

            entity.ToTable("UserTypeMaster");

            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
            entity.Property(e => e.UserTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<VideoMaster>(entity =>
        {
            entity.HasKey(e => e.VideoId);

            entity.ToTable("VideoMaster");

            entity.Property(e => e.VideoId).HasColumnName("VideoID");
            entity.Property(e => e.VideoUrl).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
