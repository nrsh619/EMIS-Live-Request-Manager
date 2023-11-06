﻿using LiveQueryManager.Models.Models;
using LiveQueryManager.Models.Models.InputModels;
using LiveQueryManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveQueryManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LiveQueryController : ControllerBase
	{
		private ILiveQueryService _liveQueryService;

		public LiveQueryController(ILiveQueryService liveQueryService)
		{
			_liveQueryService = liveQueryService;
		}

		[HttpGet]
		public async Task<List<LiveDataRequest>> GetAllRequest()
		{
			return await _liveQueryService.GetAllLiveDataRequest();
		}

		[HttpGet("{requestId}")]
		public async Task<LiveDataRequest> GetAllRequestByRequestId(int requestId)
		{
			return await _liveQueryService.GetLiveDataRequestByRequestId(requestId);
		}

		[HttpPost]
		public async Task CreateLiveRequest(CreateLiveRequestInput input)
		{
			//ToDo: need to call S3 to save attachments and retrive S3 path
			await _liveQueryService.CreateLiveRequest(input);
		}

		[HttpPut]
		public async Task DeleteLiveRequest(int requestId)
		{
			await _liveQueryService.DeleteLiveRequest(requestId);
		}

		//[HttpPut]
		//public async Task UpdateLiveRequestStatus(int requestId, Status currentStatus)
		//{
		//	await _liveRequestDA.UpdateLiveRequestStatus(requestId, currentStatus);
		//}

	}
}
