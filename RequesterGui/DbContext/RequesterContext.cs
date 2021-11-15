using Microsoft.EntityFrameworkCore;
using RequesterGui.Entities;

namespace RequesterGui.DbContext
{
	class RequesterDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public DbSet<HostEntity> Hosts { get; set; }
		public DbSet<EndpointEntity> Endpoints { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlite("Data Source=requester.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<HostEntity>()
				.HasKey(h => h.Id);

			modelBuilder.Entity<EndpointEntity>()
				.HasKey(e => e.Id);
			modelBuilder.Entity<EndpointEntity>()
				.HasOne(e => e.Host)
				.WithMany(h => h.Endpoints)
				.HasForeignKey(e => e.HostId);
		}
	}
}
