using LiveQueryManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiveQueryManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AttachementController : ControllerBase
	{
		private IFileHandeller _fileHandeller;
		private readonly IAttachementService _attachementService;

		public AttachementController(IFileHandeller fileHandeller, IAttachementService attachementService)
		{
			_fileHandeller = fileHandeller;
			_attachementService = attachementService;
		}

		[HttpPost]
		public async Task<string> UploadAsync(IFormFile formFile)
		{
			return await _fileHandeller.UploadAsync(formFile);
		}

		[HttpGet]
		public async Task<IActionResult> DownloadAttachement(int attachmentId)
		{
			var attachement = await _attachementService.GetAttachment(attachmentId);

			var document = await _fileHandeller.DownloadAsync(attachement.Path);

			return File(document, "application/octet-stream", attachement.Path);
		}

		[HttpDelete]
		[Route("DeleteAttachementByID")]
		public async Task DeleteAttachementByID(int attachmentId)
		{
			var attachement = await _attachementService.GetAttachment(attachmentId);

			await _fileHandeller.DeleteAsync(attachement.Path);

			await _attachementService.DeleteAsync(attachmentId);

		}

		[HttpDelete]
		public async Task DeleteAttachement(string file)
		{
			await _fileHandeller.DeleteAsync(file); ;
		}
	}
}
