using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveQueryManager.Models.Enum;

namespace LiveQueryManager.Models.Models
{
	public class LiveDataRequest
	{
		public int RequestId { get; set; }
		public string CreatedBy { get; set; }
		public string AssignedTo { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? LastUpdatedOn { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsDeleted { get; set; }
		public Status Status { get; set; }
		public virtual List<Attachment> Attachments { get; set; }
	}
}
