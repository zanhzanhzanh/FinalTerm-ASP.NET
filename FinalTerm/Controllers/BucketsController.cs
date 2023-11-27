using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace FinalTerm.Controllers {
    public class BucketsController : Controller {
        private readonly IAmazonS3 _s3Client;
        public BucketsController(IAmazonS3 s3Client) {
            _s3Client = s3Client;
        }
    }
}
