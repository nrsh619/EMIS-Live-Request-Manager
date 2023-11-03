using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveQueryManager.DataAccess.Context;
using LiveQueryManager.Models.Enum;
using LiveQueryManager.Models.Models;
using LiveQueryManager.Models.Models.InputModels;

namespace LiveQueryManager.DataAccess.DA
{
	public class LiveRequestDA
	{

		private LiveQueryManagerDbContext _liveQueryManagerDbContext;

        public LiveRequestDA(LiveQueryManagerDbContext liveQueryManagerDbContext)
        {
			_liveQueryManagerDbContext = liveQueryManagerDbContext;
		}

		public async Task CreateLiveRequest(CreateLiveRequestInput input)
		{
			List<Attachment> attachments = new List<Attachment>();

			foreach (var attachmentPath in input.AttachmentPaths)
			{
				var attachmentRequest = new Attachment
				{
					Path = attachmentPath,
					Type = AttachmentType.Request
				};
				attachments.Add(attachmentRequest);
			}

			var liveRequest = new LiveDataRequest
			{
				CreatedBy = "Admin",
				CreatedOn = DateTime.UtcNow,
				LastUpdatedOn = DateTime.UtcNow,
				Title = input.Title,
				Description = input.Description,
				Status = Status.Created,
				Attachments = attachments,
				AssignedTo = "Uk"
			};

			_liveQueryManagerDbContext.LiveDataRequests.Add(liveRequest);
			await _liveQueryManagerDbContext.SaveChangesAsync();
		}
    }
}
