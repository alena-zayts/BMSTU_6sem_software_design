using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BL;

#nullable disable

namespace AccessToDB2
{
    public partial class TransfersystemContext : DbContext
    {
        private string ConnectionString { get; set; }
        public TransfersystemContext(string conn)
        {
            ConnectionString = conn;
        }

        public TransfersystemContext(DbContextOptions<TransfersystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessToDB2.Models.Card> Cards { get; set; }
        public virtual DbSet<AccessToDB2.Models.CardReading> CardReadings { get; set; }
        public virtual DbSet<AccessToDB2.Models.Lift> Lifts { get; set; }
        public virtual DbSet<AccessToDB2.Models.LiftSlope> LiftSlopes { get; set; }
        public virtual DbSet<AccessToDB2.Models.Message> Messages { get; set; }
        public virtual DbSet<AccessToDB2.Models.Slope> Slopes { get; set; }
        public virtual DbSet<AccessToDB2.Models.Turnstile> Turnstiles { get; set; }
        public virtual DbSet<AccessToDB2.Models.User> Users { get; set; }

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

            modelBuilder.Entity<AccessToDB2.Models.Card>(entity =>
            {
                entity.HasKey(e => e.CardID)
                    //.ValueGeneratedNever()
                    .HasName("card_id");

                entity.ToTable("cards");

                entity.Property(e => e.CardID)
                    .ValueGeneratedNever()
                    .HasColumnName("card_id");

                entity.Property(e => e.ActivationTime).HasColumnName("activation_time");

                entity.Property(e => e.Type).HasColumnName("type");

            });

            modelBuilder.Entity<AccessToDB2.Models.CardReading>(entity =>
            {
                entity.HasKey(e => e.RecordID)
                    //.ValueGeneratedNever()
                    .HasName("record_id");

                entity.ToTable("card_readings");

                entity.Property(e => e.RecordID)
                    .ValueGeneratedNever()
                    .HasColumnName("record_id");

                entity.Property(e => e.TurnstileID).HasColumnName("turnstile_id");

                entity.Property(e => e.CardID).HasColumnName("card_id");

                entity.Property(e => e.ReadingTime).HasColumnName("reading_time");

            
            });

            modelBuilder.Entity<AccessToDB2.Models.Lift>(entity =>
            {
                entity.HasKey(e => e.LiftID)
                    //.ValueGeneratedNever()
                    .HasName("lift_id");

                entity.ToTable("lifts");

                entity.Property(e => e.LiftID)
                    .ValueGeneratedNever()
                    .HasColumnName("lift_id");

                entity.Property(e => e.LiftName).HasColumnName("lift_name");

                entity.Property(e => e.IsOpen).HasColumnName("is_open");
                entity.Property(e => e.QueueTime).HasColumnName("queue_time");
                entity.Property(e => e.LiftingTime).HasColumnName("lifting_time");
                entity.Property(e => e.SeatsAmount).HasColumnName("seats_amount");

            });

            modelBuilder.Entity<AccessToDB2.Models.LiftSlope>(entity =>
            {
                entity.HasKey(e => e.RecordID)
                    .HasName("resord_id");

                entity.ToTable("lifts_slopes");

                entity.Property(e => e.RecordID)
                    .ValueGeneratedNever()
                    .HasColumnName("resord_id");

                entity.Property(e => e.LiftID).HasColumnName("lift_id");

                entity.Property(e => e.SlopeID).HasColumnName("slope_id");
            });

            modelBuilder.Entity<AccessToDB2.Models.Message>(entity =>
            {
                entity.HasKey(e => e.MessageID)
                    //.ValueGeneratedNever()
                    .HasName("message_id");

                entity.ToTable("messages");

                entity.Property(e => e.MessageID)
                    .ValueGeneratedNever()
                    .HasColumnName("message_id");

                entity.Property(e => e.Text).HasColumnName("text");
                entity.Property(e => e.SenderID).HasColumnName("sender_id");
                entity.Property(e => e.CheckedByID).HasColumnName("checked_by_id");

            });

            modelBuilder.Entity<AccessToDB2.Models.Slope>(entity =>
            {
                entity.HasKey(e => e.SlopeID)
                    //.ValueGeneratedNever()
                    .HasName("slope_id");

                entity.ToTable("slopes");

                entity.Property(e => e.SlopeID)
                    .ValueGeneratedNever()
                    .HasColumnName("slope_id");

                entity.Property(e => e.IsOpen).HasColumnName("is_open");
                entity.Property(e => e.DifficultyLevel).HasColumnName("difficulty_level");
                entity.Property(e => e.SlopeName).HasColumnName("slope_name");

            });

            modelBuilder.Entity<AccessToDB2.Models.Turnstile>(entity =>
            {
                entity.HasKey(e => e.TurnstileID)
                    .HasName("turnstile_id");

                entity.ToTable("turnstiles");

                entity.Property(e => e.TurnstileID)
                    .ValueGeneratedNever()
                    .HasColumnName("turnstile_id");

                entity.Property(e => e.LiftID).HasColumnName("lift_id");

                entity.Property(e => e.IsOpen).HasColumnName("is_open");
            });

            modelBuilder.Entity<AccessToDB2.Models.User>(entity =>
            {
                entity.HasKey(e => e.UserID)
                    .HasName("users_pk");

                entity.ToTable("users");

                entity.Property(e => e.UserID)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.CardID).HasColumnName("card_id");

                entity.Property(e => e.UserEmail)
                    .HasColumnName("user_email");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Permissions)
                    .HasColumnName("permissions");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
