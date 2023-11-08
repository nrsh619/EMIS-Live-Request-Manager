using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveQueryManager.AWS.Configuration
{
	public class AwsConfiguration
	{
		public string BucketName { get; set; }
		public string Region { get; set; }
		public string AwsAccessKey { get; set; }
		public string AwsSecretAccessKey { get; set; }
	}
}
