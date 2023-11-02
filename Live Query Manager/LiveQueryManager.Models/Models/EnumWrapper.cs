using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveQueryManager.Models.Models
{
	public class EnumWrapper
	{
		public int Id { get; set; }
		public string Description { get; set; }

	}
	public class StatusWrapper : EnumWrapper { }
	public class AttachmentTypeWrapper : EnumWrapper { }
}
