using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SurveyAPI.DBModels
{
    public partial class SurveyConetxt : DbContext
    {
        public SurveyConetxt()
        {
        }

        public SurveyConetxt(DbContextOptions<SurveyConetxt> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<GeneralInformation> GeneralInformations { get; set; }
        public virtual DbSet<OfferedAnswer> OfferedAnswers { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionOfferedAnswerRelation> QuestionOfferedAnswerRelations { get; set; }
        public virtual DbSet<SurveyQuestionRelation> SurveyQuestionRelations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:code9serverzr.database.windows.net,1433;Initial Catalog=Code9DB;Persist Security Info=False;User ID=adminCode9;Password=Samsung123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answers", "Survey");

                entity.HasIndex(e => new { e.ParticipantId, e.SurveyId, e.QuestionId, e.QuestionAnswersId }, "UN_Answers_ParticipantId_SurveyId_QuestionId_QuestionAnswersId")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_ParticipantId");

                entity.HasOne(d => d.QuestionAnswers)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionAnswersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_OfferedAnswersId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_QuestionId");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_SurveyId");
            });

            modelBuilder.Entity<GeneralInformation>(entity =>
            {
                entity.ToTable("GeneralInformations", "Survey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OfferedAnswer>(entity =>
            {
                entity.ToTable("OfferedAnswers", "Survey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("Participants", "Survey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participants_SurveyId");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Questions", "Survey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuestionOfferedAnswerRelation>(entity =>
            {
                entity.ToTable("QuestionOfferedAnswerRelations", "Survey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.OfferedAnswer)
                    .WithMany(p => p.QuestionOfferedAnswerRelations)
                    .HasForeignKey(d => d.OfferedAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionOfferedAnswerRelations_OfferedAnswerId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOfferedAnswerRelations)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionOfferedAnswerRelations_QuestionId");
            });

            modelBuilder.Entity<SurveyQuestionRelation>(entity =>
            {
                entity.ToTable("SurveyQuestionRelations", "Survey");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChangedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveyQuestionRelations)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestionRelations_QuestionId");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyQuestionRelations)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyQuestionRelations_SurveyId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
