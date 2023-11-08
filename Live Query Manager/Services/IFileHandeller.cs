using Microsoft.AspNetCore.Http;

namespace LiveQueryManager.Services
{
	public interface IFileHandeller
	{
		Task<string> UploadAsync(IFormFile file);
		Task<byte[]> DownloadAsync(string file);
		Task DeleteAsync(string file);
		Task<bool> IsFileExists(string file);
	}
}
