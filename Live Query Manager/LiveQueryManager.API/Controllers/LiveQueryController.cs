using LiveQueryManager.DataAccess.Context;
using LiveQueryManager.DataAccess.DA;
using LiveQueryManager.Models.Enum;
using LiveQueryManager.Models.Models;
using LiveQueryManager.Models.Models.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

		[HttpPost]
		public async Task CreateLiveRequest(CreateLiveRequestInput input)
		{
			//ToDo: need to call S3 to save attachments and retrive S3 path
			await _liveRequestDA.CreateLiveRequest(input);
		}

	}
}
