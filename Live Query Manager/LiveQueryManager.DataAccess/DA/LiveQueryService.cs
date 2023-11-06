using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveQueryManager.DataAccess.Context;
using LiveQueryManager.Models.Enum;
using LiveQueryManager.Models.Models;
using LiveQueryManager.Models.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using LiveQueryManager.Services;

namespace LiveQueryManager.DataAccess.DA
{
	public class LiveQueryService : ILiveQueryService
	{

		private LiveQueryManagerDbContext _liveQueryManagerDbContext;

        public LiveQueryService(LiveQueryManagerDbContext liveQueryManagerDbContext)
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

		public async Task<List<LiveDataRequest>> GetAllLiveDataRequest()
		{
			return await _liveQueryManagerDbContext.LiveDataRequests.Where(a=> !a.IsDeleted).ToListAsync();
		}

		public async Task<LiveDataRequest> GetLiveDataRequestByRequestId(int requestId)
		{
			return await _liveQueryManagerDbContext.LiveDataRequests.Where(a => a.RequestId == requestId && !a.IsDeleted)
							.Select(a => new LiveDataRequest
							{
								RequestId = a.RequestId,
								Status = a.Status,
								Title = a.Title,
								Description = a.Description,
								CreatedBy = a.CreatedBy,
								CreatedOn = a.CreatedOn,
								AssignedTo = a.AssignedTo
							}).FirstOrDefaultAsync();
		}

		public async Task DeleteLiveRequest(int requestId)
        {
			var liveRequest = _liveQueryManagerDbContext.LiveDataRequests.Where(a => a.RequestId == requestId && !a.IsDeleted).SingleOrDefault();

			if (liveRequest != null)
			{
				liveRequest.IsDeleted = true;
                liveRequest.LastUpdatedOn = DateTime.UtcNow;
            }

            await _liveQueryManagerDbContext.SaveChangesAsync();
        }

        public async Task UpdateLiveRequestStatus(int requestId, Status currentStatus)
        {
            var liveRequest = _liveQueryManagerDbContext.LiveDataRequests.Where(a => a.RequestId == requestId && !a.IsDeleted).SingleOrDefault();

			if (liveRequest != null)
			{
				liveRequest.Status = currentStatus;
                liveRequest.LastUpdatedOn = DateTime.UtcNow;
            }

            await _liveQueryManagerDbContext.SaveChangesAsync();
        }
    }
}
