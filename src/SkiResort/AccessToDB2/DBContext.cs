using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AccessToDB2.Models;

#nullable disable

namespace AccessToDB2
{
    public partial class DBContext : DbContext
    {
        private string ConnectionString { get; set; }
        public DBContext(string conn)
        {
            ConnectionString = conn;
            //Database.EnsureCreated(); не раскомментируй!
        }
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessToDB2.Models.Card> Cards { get; set; }
        public virtual DbSet<AccessToDB2.Models.CardReading> CardReadings { get; set; }
        public virtual DbSet<AccessToDB2.Models.Lift> Lifts { get; set; }
        public virtual DbSet<AccessToDB2.Models.LiftsSlope> LiftSlopes { get; set; }
        public virtual DbSet<AccessToDB2.Models.Message> Messages { get; set; }
        public virtual DbSet<AccessToDB2.Models.Slope> Slopes { get; set; }
        public virtual DbSet<AccessToDB2.Models.Turnstile> Turnstiles { get; set; }
        public virtual DbSet<AccessToDB2.Models.User> Users { get; set; } = null!;

        //public IQueryable<AccessToDB2.Models.VisitorHotelStat> getvisitors() => FromExpression(() => getvisitors());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {               
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //modelBuilder.HasDbFunction(() => getvisitors());

            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("cards");

                entity.Property(e => e.CardId)
                    .ValueGeneratedNever()
                    .HasColumnName("card_id");

                entity.Property(e => e.ActivationTime).HasColumnName("activation_time");

                entity.Property(e => e.Type)
                    .HasColumnType("character varying")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<CardReading>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("card_readings_pk");

                entity.ToTable("card_readings");

                entity.Property(e => e.RecordId)
                    .ValueGeneratedNever()
                    .HasColumnName("record_id");

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.ReadingTime).HasColumnName("reading_time");

                entity.Property(e => e.TurnstileId).HasColumnName("turnstile_id");
            });

            modelBuilder.Entity<Lift>(entity =>
            {
                entity.ToTable("lifts");

                entity.Property(e => e.LiftId)
                    .ValueGeneratedNever()
                    .HasColumnName("lift_id");

                entity.Property(e => e.IsOpen).HasColumnName("is_open");

                entity.Property(e => e.LiftName)
                    .HasColumnType("character varying")
                    .HasColumnName("lift_name");

                entity.Property(e => e.LiftingTime).HasColumnName("lifting_time");

                entity.Property(e => e.QueueTime).HasColumnName("queue_time");

                entity.Property(e => e.SeatsAmount).HasColumnName("seats_amount");
            });

            modelBuilder.Entity<LiftsSlope>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("lifts_slopes_pk");

                entity.ToTable("lifts_slopes");

                entity.Property(e => e.RecordId)
                    .ValueGeneratedNever()
                    .HasColumnName("record_id");

                entity.Property(e => e.LiftId).HasColumnName("lift_id");

                entity.Property(e => e.SlopeId).HasColumnName("slope_id");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.Property(e => e.MessageId)
                    .ValueGeneratedNever()
                    .HasColumnName("message_id");

                entity.Property(e => e.CheckedById).HasColumnName("checked_by_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.Text)
                    .HasColumnType("character varying")
                    .HasColumnName("text");
            });


            modelBuilder.Entity<Slope>(entity =>
            {
                entity.ToTable("slopes");

                entity.Property(e => e.SlopeId)
                    .ValueGeneratedNever()
                    .HasColumnName("slope_id");

                entity.Property(e => e.DifficultyLevel).HasColumnName("difficulty_level");

                entity.Property(e => e.IsOpen).HasColumnName("is_open");

                entity.Property(e => e.SlopeName)
                    .HasColumnType("character varying")
                    .HasColumnName("slope_name");
            });


            modelBuilder.Entity<Turnstile>(entity =>
            {
                entity.ToTable("turnstiles");

                entity.Property(e => e.TurnstileId)
                    .ValueGeneratedNever()
                    .HasColumnName("turnstile_id");

                entity.Property(e => e.IsOpen).HasColumnName("is_open");

                entity.Property(e => e.LiftId).HasColumnName("lift_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.Password)
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                entity.Property(e => e.Permissions).HasColumnName("permissions");

                entity.Property(e => e.UserEmail)
                    .HasColumnType("character varying")
                    .HasColumnName("user_email");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
