using LiveQueryManager.Models.Enum;

namespace LiveQueryManager.Models.Models
{
	public class Attachment
	{
		public int Id { get; set; }
		public string Path { get; set; }
		public AttachmentType Type { get; set; }
        public virtual LiveDataRequest LiveDataRequest { get; set; }

    }
}
