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

		public async Task UpdateLiveRequest(UpdateLiveRequestInput input)
		{
			var newAttachments = new List<Attachment>();			

			var existingRequest = _liveQueryManagerDbContext.LiveDataRequests.SingleOrDefault(a => a.RequestId == input.RequestId);

			if (existingRequest != null)
			{
				existingRequest.RequestId = input.RequestId;
				existingRequest.LastUpdatedOn = DateTime.UtcNow;
				existingRequest.Title = input.Title;
				existingRequest.Description = input.Description;
				existingRequest.Status = (Status)input.StatusTypeId;
				existingRequest.Attachments = newAttachments;
				existingRequest.AssignedTo = input.AssignedTo;

				foreach (var attachmentInput in input.Attachments)
				{
					var attachmentRequest = new Attachment
					{
						Path = attachmentInput.Path,
						Type = (AttachmentType)attachmentInput.TypeId
					};
					if (attachmentInput.Id.HasValue) attachmentRequest.Id = attachmentInput.Id.Value;
					newAttachments.Add(attachmentRequest);
				}

				await _liveQueryManagerDbContext.SaveChangesAsync();
			}
		}

		public async Task<List<LiveDataRequest>> GetAllLiveDataRequest()
		{
			return await _liveQueryManagerDbContext.LiveDataRequests.Where(a => !a.IsDeleted).ToListAsync();
		}

		public async Task<LiveDataRequest> GetLiveDataRequestByRequestId(int requestId)
		{
			var request = await _liveQueryManagerDbContext.LiveDataRequests
				.Include(a => a.Attachments)
				.Where(a => a.RequestId == requestId && !a.IsDeleted).FirstOrDefaultAsync();

			if (request == null) return null;

			List<AttachmentInput> newAttachments = new List<AttachmentInput>();
			foreach (var item in request.Attachments)
			{
				newAttachments.Add(new AttachmentInput { Id = item.Id, Path = item.Path, TypeId = (int)item.Type });
			}

			UpdateLiveRequestInput model = new UpdateLiveRequestInput
			{
				RequestId = request.RequestId,
				Title = request.Title,
				Description = request.Description,
				StatusTypeId = (int)request.Status,
				AssignedTo = request.AssignedTo,
				Attachments = newAttachments
			};

			return request;
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
