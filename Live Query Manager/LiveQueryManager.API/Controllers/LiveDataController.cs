using LiveQueryManager.API.DataAccess;
using LiveQueryManager.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiveQueryManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LiveDataController : ControllerBase
	{
		private readonly LiveQueryManagerDbContext _dbContext;
		public LiveDataController(LiveQueryManagerDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public List<LiveDataRequest> GetALL()
		{
			return _dbContext.LiveDataRequests.ToList();
		}

		[Route("api/[controller]/CreateRequest")]
		[HttpPost]
		public async Task Insert(LiveDataRequest request)
		{
			_dbContext.LiveDataRequests.Add(request);
			await _dbContext.SaveChangesAsync();
		}
	}

}
