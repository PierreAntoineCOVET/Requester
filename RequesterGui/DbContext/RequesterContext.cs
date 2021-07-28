using Microsoft.EntityFrameworkCore;
using RequesterGui.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequesterGui.DbContext
{
	class RequesterContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public DbSet<Host> Hosts { get; set; }
		public DbSet<Endpoint> Endpoints { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlite("Data Source=mydb.db;Version=3;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Host>()
				.HasKey(h => h.Id);

			modelBuilder.Entity<Endpoint>()
				.HasKey(e => e.Id);
			modelBuilder.Entity<Endpoint>()
				.HasOne(e => e.Host)
				.WithMany(h => h.Endpoints)
				.HasForeignKey(e => e.HostId);
		}
	}
}
