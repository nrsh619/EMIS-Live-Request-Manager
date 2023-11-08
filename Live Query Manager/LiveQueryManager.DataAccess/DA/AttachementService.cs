using LiveQueryManager.DataAccess.Context;
using LiveQueryManager.Models.Models;
using LiveQueryManager.Models.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using LiveQueryManager.Services;

namespace LiveQueryManager.DataAccess.DA
{
	public class AttachementService : IAttachementService
	{

		private LiveQueryManagerDbContext _liveQueryManagerDbContext;

		public AttachementService(LiveQueryManagerDbContext liveQueryManagerDbContext)
		{
			_liveQueryManagerDbContext = liveQueryManagerDbContext;
		}

		public async Task DeleteAsync(int id)
		{
			await _liveQueryManagerDbContext.Attachments.Where(a => a.Id == id).ExecuteDeleteAsync();

			await _liveQueryManagerDbContext.SaveChangesAsync();
		}

		public async Task<AttachmentInput> GetAttachment(int id)
		{
			Attachment attachment = await _liveQueryManagerDbContext.Attachments.FirstOrDefaultAsync(a => a.Id == id);

			if (attachment == null)
				return null;

			return new AttachmentInput
			{
				Id = attachment.Id,
				Path = attachment.Path
			};
		}
	}
}
