using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace scopic_test_server.Helper
{
    public class S3UploadImage
    {
        private const string bucketName = "scopic-bucket";
        public S3UploadImage()
        {
        }
        public async Task UploadFileAsync(IFormFile file, string fileName)
        {
            try
            {
                var AccessKey = "AKIAXBBRIYPVW4AC4E5L";
                var SecretKey = "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";
                var credentials = new BasicAWSCredentials(AccessKey, SecretKey);
                var config = new AmazonS3Config
                {
                    RegionEndpoint = Amazon.RegionEndpoint.APSoutheast1
                };
                using var client = new AmazonS3Client(credentials, config);
                await using var newMemoryStream = new MemoryStream();
                file.CopyTo(newMemoryStream);
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = fileName,
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.PublicRead
                };
                var fileTransferUtility = new TransferUtility(client);
                await fileTransferUtility.UploadAsync(uploadRequest);
            }
            catch (AmazonS3Exception e)
            {
                throw new System.Exception("Error encountered on server. Message:'{0}' when writing an object", e);
            }
            catch (Exception e)
            {
                throw new System.Exception("Unknown encountered on server. Message:'{0}' when writing an object", e);
            }
        }
    }
}