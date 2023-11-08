using LiveQueryManager.Models.Models.InputModels;

namespace LiveQueryManager.Services
{
	public interface IAttachementService
	{
		Task<AttachmentInput> GetAttachment(int id);	 
		Task DeleteAsync(int id);	 
	}
}
