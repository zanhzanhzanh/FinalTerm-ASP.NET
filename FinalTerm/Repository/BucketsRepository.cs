using Amazon.S3;
using Amazon.S3.Model;

namespace FinalTerm.Repository {
    public class BucketsRepository {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;

        public BucketsRepository(IAmazonS3 s3Client, IConfiguration configuration) {
            _configuration = configuration;
            _s3Client = s3Client;
        }

        public string GeneratePreSignedUrl(string url) {
            string[] getStr = url.Split("/");
            string fileName = getStr[getStr.Length - 1];

            var request = new GetPreSignedUrlRequest {
                BucketName = _configuration.GetSection("AWS:BucketName").Value,
                Key = fileName,
                ContentType = "image/png",
                Verb = HttpVerb.PUT,
                Expires = DateTime.Now.AddHours(1),
                ResponseHeaderOverrides = new ResponseHeaderOverrides() {
                    ContentDisposition = "inline"
                },
            };
            request.Headers["x-amz-acl"] = "public-read";

            return _s3Client.GetPreSignedURL(request);
        }

        public async void DeleteFileAsync(string fileName) {
            if (fileName != "defaultAvatar.png") {
                await _s3Client.DeleteObjectAsync(_configuration.GetSection("AWS:BucketName").Value, fileName);
            }
        }
    }
}
