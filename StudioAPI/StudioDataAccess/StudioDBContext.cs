using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioModel.Domain;

namespace StudioDataAccess
{
    public class StudioDBContext : IdentityDbContext<UserApp>
    {
        private readonly ILoggerFactory _loggerFactory;
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public StudioDBContext(DbContextOptions<StudioDBContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserApp>(entity =>
            {
                entity.Property(e => e.UserName);
                entity.HasOne(e => e.UserProfile)
                      .WithOne(e => e.UserApp)
                      .HasForeignKey<UserProfile>(e => e.IdUser)
                      .IsRequired(false);
            });

            builder.Entity<UserApp>().Ignore(e => e.PhoneNumber)
                                     .Ignore(e => e.PhoneNumberConfirmed);

            if (Database.IsSqlServer())
            {
                builder.Entity<UserApp>(entity =>
                {
                });
            }

            if (Database.IsSqlite())
            {
                Guid guid;
                builder.Entity<UserApp>(entity =>
                {
                    entity.Property(e => e.UserName).HasColumnType("TEXT");

                    entity
                    .HasMany(e => e.Comments)
                    .WithOne(e => e.Author)
                    .HasForeignKey(e => e.AuthorId);
                });

                builder.Entity<UserProfile>(entity =>
                {
                    entity.Property(e => e.Id).HasConversion(
                        t => t.ToString(),
                        t => Guid.TryParse(t, out guid) ? guid : Guid.Empty).HasColumnType("TEXT");
                    entity.HasOne(e => e.UserApp)
                        .WithOne().HasForeignKey<UserProfile>(e => e.IdUser).HasPrincipalKey<UserApp>(e => e.Id);
                });

                builder.Entity<Video>(entity =>
                {
                    Guid guid;
                    entity.Property(e => e.Id).HasConversion(
                        t => t.ToString(),
                        t => Guid.TryParse(t, out guid) ? guid : Guid.Empty).HasColumnType("TEXT");

                    entity
                    .HasMany(e => e.Comments)
                    .WithOne(e => e.Video)
                    .HasForeignKey(e => e.VideoId);
                });

                builder.Entity<Comment>(entity =>
                {
                    Guid guid;
                    entity.Property(e => e.Id).HasConversion(
                        t => t.ToString(),
                        t => Guid.TryParse(t, out guid) ? guid : Guid.Empty).HasColumnType("TEXT");
                });
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<StudioDBContext>();
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Error, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Error)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
