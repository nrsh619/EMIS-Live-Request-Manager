namespace LiveQueryManager.Models.Models.InputModels
{
	public class CreateLiveRequestInput
	{
		public string Title { get; set; }
		public List<string> AttachmentPaths { get; set; }
		public string Description { get; set;}

	}
}
