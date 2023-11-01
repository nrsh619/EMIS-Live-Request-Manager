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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			/*	var attachmentEntityBuilder = modelBuilder.Entity<Attachment>();
				attachmentEntityBuilder.ToTable("LiveDataAttachment");

				var pathPropertyBuilder = attachmentEntityBuilder.Property(e => e.Path);
				pathPropertyBuilder.HasColumnName("LinkPadPath");

				attachmentEntityBuilder.HasOne(a => a.LiveDataRequest).WithMany(l => l.Attachments).HasConstraintName("customForginKey").HasForeignKey("CustomId");
			*/
			base.OnModelCreating(modelBuilder);
		}
	}
}
