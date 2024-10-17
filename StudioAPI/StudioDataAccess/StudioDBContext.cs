﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioModel.Domain;

namespace StudioDataAccess
{
	public class StudioDBContext : IdentityDbContext<UserApp>
	{
		private readonly ILoggerFactory _loggerFactory;
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }

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
			});

			if (Database.IsSqlServer())
			{
				builder.Entity<UserApp>(entity =>
				{
				});
			}

			if (Database.IsSqlite())
			{
				builder.Entity<UserApp>(entity =>
				{
					entity.Property(e => e.UserName).HasColumnType("TEXT");
				});

                builder.Entity<UserProfile>(entity =>
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
