namespace LiveQueryManager.API.Models
{
	public class LiveDataRequest
	{
        public int Id { get; set; }
		public string CreatedBy { get; set; }
		public string Title { get; set;}
		public Status Status { get; set;}
		public virtual List<Attachment> Attachments { get; set;}
    }

	public class Status
	{
        public int Id { get; set; }
		public string Description { get; set; }
    }

	public class Attachment
	{
        public int Id { get; set; }
		public string Path { get; set; }
        public virtual LiveDataRequest LiveDataRequest { get; set; }
    }
}
