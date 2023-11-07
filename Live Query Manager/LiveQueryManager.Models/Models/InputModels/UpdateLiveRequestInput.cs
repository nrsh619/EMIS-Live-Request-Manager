namespace LiveQueryManager.Models.Models.InputModels
{
	public class UpdateLiveRequestInput
	{
		public int RequestId { get; set; }
		public string Title { get; set; }
		public int StatusTypeId { get; set; }
		public string AssignedTo { get; set; }

		public string Description { get; set; }
		public List<AttachmentInput> Attachments { get; set; }
	}
}
