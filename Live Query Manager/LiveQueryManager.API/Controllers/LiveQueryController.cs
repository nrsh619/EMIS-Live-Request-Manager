using LiveQueryManager.DataAccess.Context;
using LiveQueryManager.DataAccess.DA;
using LiveQueryManager.Models.Enum;
using LiveQueryManager.Models.Models;
using LiveQueryManager.Models.Models.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiveQueryManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LiveQueryController : ControllerBase
	{
		private LiveRequestDA _liveRequestDA;

		public LiveQueryController(LiveRequestDA liveRequestDA)
		{
			_liveRequestDA = liveRequestDA;
		}

		[HttpGet]
		public async Task<List<LiveDataRequest>> GetAllRequest()
		{
			return await _liveRequestDA.GetAllLiveDataRequest();
		}

		[HttpGet("{requestId}")]
		public async Task<LiveDataRequest> GetAllRequestByRequestId(int requestId)
		{
			return await _liveRequestDA.GetLiveDataRequestByRequestId(requestId);
		}

		[HttpPost]
		public async Task CreateLiveRequest(CreateLiveRequestInput input)
		{
			//ToDo: need to call S3 to save attachments and retrive S3 path
			await _liveRequestDA.CreateLiveRequest(input);
		}

		[HttpPut]
		public async Task DeleteLiveRequest(int requestId)
		{
			await _liveRequestDA.DeleteLiveRequest(requestId);
		}

		//[HttpPut]
		//public async Task UpdateLiveRequestStatus(int requestId, Status currentStatus)
		//{
		//	await _liveRequestDA.UpdateLiveRequestStatus(requestId, currentStatus);
		//}

	}
}
