using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveQueryManager.DataAccess.Map;
using LiveQueryManager.Models.Enum;
using LiveQueryManager.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveQueryManager.DataAccess.Context
{
	public class LiveQueryManagerDbContext : DbContext
	{
		public LiveQueryManagerDbContext(DbContextOptions<LiveQueryManagerDbContext> options) : base(options) { }

		public DbSet<LiveDataRequest> LiveDataRequests { get; set; }
		public DbSet<Attachment> Attachments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			RemapEntity.Remap(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}
	}
}
