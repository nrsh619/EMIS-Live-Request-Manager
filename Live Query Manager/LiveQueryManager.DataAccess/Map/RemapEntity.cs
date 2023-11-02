using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using LiveQueryManager.Models.Enum;
using LiveQueryManager.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiveQueryManager.DataAccess.Map
{
	public static class RemapEntity
	{
		public static void Remap(ModelBuilder modelBuilder)
		{
			RemapStatus(modelBuilder);
			RemapAttachmentType(modelBuilder);
			RemapLiveDataRequest(modelBuilder.Entity<LiveDataRequest>());
			RemapAttachement(modelBuilder.Entity<Attachment>());
		}

		private static void RemapAttachement(EntityTypeBuilder<Attachment> attachmentBuilder)
		{
			attachmentBuilder.ToTable("Attachment");
			attachmentBuilder.Property(a => a.Id).UseIdentityColumn().HasColumnName("AttachmentId");
			attachmentBuilder.Property(a => a.Type).HasColumnName("AttachmentType");
			attachmentBuilder.HasOne(a => a.LiveDataRequest).WithMany(l => l.Attachments).HasConstraintName("FK_Attachment_LiveDataRequest_RequestId").HasForeignKey("RequestId").OnDelete(DeleteBehavior.Cascade);
		}
		private static void RemapLiveDataRequest(EntityTypeBuilder<LiveDataRequest> liveDataRequestBuilder)
		{
			liveDataRequestBuilder.HasKey(a => a.RequestId).HasName("PK_LiveDataRequest_RequestId");
			liveDataRequestBuilder.Property(a => a.RequestId).UseIdentityColumn();
			liveDataRequestBuilder.Property(a => a.CreatedOn).HasColumnName("CreationDateTime");
			liveDataRequestBuilder.Property(a => a.Status).HasColumnName("StatusId").HasConversion<int>();
			liveDataRequestBuilder.Property(a => a.LastUpdatedOn).HasColumnName("LastUpdatedDateTime").IsRequired(false);
			liveDataRequestBuilder.Property(a => a.IsDeleted).HasDefaultValue(false);
			liveDataRequestBuilder.Property(a => a.Title).HasMaxLength(200);
			liveDataRequestBuilder.Property(a => a.AssignedTo).HasMaxLength(100);
			liveDataRequestBuilder.Property(a => a.CreatedBy).HasMaxLength(100);
		}

		private static void RemapStatus(ModelBuilder modelBuilder)
		{
			var statusBuilder = modelBuilder.Entity<StatusWrapper>();
			statusBuilder.ToTable("Status");
			statusBuilder.HasKey(a => a.Id).HasName("PK_Status_StatusId");
			statusBuilder.Property(a => a.Id).HasColumnName("StatusId").ValueGeneratedNever();
			statusBuilder.Property(a => a.Description).HasColumnName("Description").HasMaxLength(50);
			foreach (int id in Enum.GetValues(typeof(Status)))
			{
				statusBuilder.HasData(new StatusWrapper()
				{
					Id = id,
					Description = Enum.GetName(typeof(Status),id)
				});
			}
		}
		private static void RemapAttachmentType(ModelBuilder modelBuilder)
		{
			var statusBuilder = modelBuilder.Entity<AttachmentTypeWrapper>();
			statusBuilder.ToTable("AttachmentType");
			statusBuilder.HasKey(a => a.Id).HasName("PK_AttachmentType_AttachmentId");
			statusBuilder.Property(a => a.Id).HasColumnName("AttachmentId").ValueGeneratedNever();
			statusBuilder.Property(a => a.Description).HasColumnName("Description").HasMaxLength(50);
			foreach (int id in Enum.GetValues(typeof(AttachmentType)))
			{
				statusBuilder.HasData(new AttachmentTypeWrapper()
				{
					Id = id,
					Description = Enum.GetName(typeof(AttachmentType), id)
				});
			}
		}
	}
}
