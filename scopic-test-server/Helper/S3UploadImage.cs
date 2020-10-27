using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace scopic_test_server.Helper
{
    public class S3UploadImage
    {
        private const string bucketName = "*** provide bucket name ***";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSoutheast1;
        // private static IAmazonS3 s3Client = new AmazonS3Client("sss", www, bucketRegion);
        public string Upload()
        {
            return null;
        }
    }
}