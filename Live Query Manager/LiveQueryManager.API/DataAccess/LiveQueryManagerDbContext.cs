using LiveQueryManager.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveQueryManager.API.DataAccess
{
	public class LiveQueryManagerDbContext : DbContext
	{
		public LiveQueryManagerDbContext(DbContextOptions<LiveQueryManagerDbContext> options) : base(options) { }

		public DbSet<LiveDataRequest> LiveDataRequests { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Attachment> Attachments { get; set; }
	}
}
