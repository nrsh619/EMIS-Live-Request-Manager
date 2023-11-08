using System;
using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using LiveQueryManager.AWS.Configuration;
using LiveQueryManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LiveQueryManager.AWS
{
	public class AWSS3FileHandeller : IFileHandeller
	{
		private readonly string _bucketName;
		private readonly IAmazonS3 _awsS3Client;

		public AWSS3FileHandeller(IOptions<AwsConfiguration> awsConfig)
		{
			_bucketName = awsConfig.Value.BucketName;
			_awsS3Client = new AmazonS3Client(awsConfig.Value.AwsAccessKey, awsConfig.Value.AwsSecretAccessKey, RegionEndpoint.GetBySystemName(awsConfig.Value.Region));

		}
		public async Task DeleteAsync(string fileName)
		{
			try
			{
				if (string.IsNullOrEmpty(fileName))
					return;

				DeleteObjectRequest request = new DeleteObjectRequest
				{
					BucketName = _bucketName,
					Key = fileName
				};

				await _awsS3Client.DeleteObjectAsync(request);

			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<byte[]> DownloadAsync(string fileName)
		{
			MemoryStream ms = null;

			try
			{
				GetObjectRequest getObjectRequest = new GetObjectRequest
				{
					BucketName = _bucketName,
					Key = fileName
				};

				using (var response = await _awsS3Client.GetObjectAsync(getObjectRequest))
				{
					if (response.HttpStatusCode == HttpStatusCode.OK)
					{
						using (ms = new MemoryStream())
						{
							await response.ResponseStream.CopyToAsync(ms);
						}
					}
				}

				if (ms is null || ms.ToArray().Length < 1)
					throw new FileNotFoundException(string.Format("The document '{0}' is not found", fileName));

				return ms.ToArray();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> IsFileExists(string fileName)
		{
			try
			{
				GetObjectMetadataRequest request = new GetObjectMetadataRequest()
				{
					BucketName = _bucketName,
					Key = fileName,
				};

				var response = _awsS3Client.GetObjectMetadataAsync(request).Result;

				return true;
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null && ex.InnerException is AmazonS3Exception awsEx)
				{
					if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
						return false;

					else if (string.Equals(awsEx.ErrorCode, "NotFound"))
						return false;
				}

				throw;
			}
		}

		public async Task<string> UploadAsync(IFormFile file)
		{
			try
			{
				using (var newMemoryStream = new MemoryStream())
				{
					file.CopyTo(newMemoryStream);

					var uploadRequest = new TransferUtilityUploadRequest
					{
						InputStream = newMemoryStream,
						Key = file.FileName,
						BucketName = _bucketName,
						ContentType = file.ContentType
					};

					var fileTransferUtility = new TransferUtility(_awsS3Client);

					await fileTransferUtility.UploadAsync(uploadRequest);

					return file.FileName;
				}
			}
			catch (Exception) { throw; }
		}
	}
}