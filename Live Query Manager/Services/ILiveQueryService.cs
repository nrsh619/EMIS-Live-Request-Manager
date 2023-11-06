using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveQueryManager.Models.Enum;
using LiveQueryManager.Models.Models;
using LiveQueryManager.Models.Models.InputModels;

namespace LiveQueryManager.Services
{
	public interface ILiveQueryService
	{
		Task UpdateLiveRequestStatus(int requestId, Status currentStatus);
		Task DeleteLiveRequest(int requestId);
		Task<LiveDataRequest> GetLiveDataRequestByRequestId(int requestId);
		Task<List<LiveDataRequest>> GetAllLiveDataRequest();
		Task CreateLiveRequest(CreateLiveRequestInput input);

	}
}
