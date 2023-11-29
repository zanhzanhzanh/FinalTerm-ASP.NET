using Amazon.S3;
using Amazon.S3.Model;
using FinalTerm.Common;
using FinalTerm.Filters;
using FinalTerm.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalTerm.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    [ErrorHandlerFilter]
    public class BucketsController : Controller {
        private readonly BucketsRepository _bucketsRepository;

        public BucketsController(BucketsRepository bucketsRepository) {
            _bucketsRepository = bucketsRepository;
        }

        [Authorize]
        [HttpGet("~/api/v1/s3/presigned-url/{url}")]
        public async Task<ActionResult<ResponseObject<string>>> GetPreSignedUrl([FromRoute] string url) {
            return Ok(new ResponseObject<string>(200, "Success", _bucketsRepository.GeneratePreSignedUrl(url)));
        }

        //[HttpGet("get-all")]
        //public async Task<IActionResult> GetAllFilesAsync() {
        //    //var bucketExists = await _s3Client.DoesS3BucketExistAsync(_configuration.GetSection("AWS:BucketName").Value);
        //    //if (!bucketExists) return NotFound($"Bucket does not exist.");
        //    var request = new ListObjectsV2Request() {
        //        BucketName = _configuration.GetSection("AWS:BucketName").Value,
        //    };
        //    var result = await _s3Client.ListObjectsV2Async(request);
        //    var s3Objects = result.S3Objects.Select(s =>
        //    {
        //        var urlRequest = new GetPreSignedUrlRequest() {
        //            BucketName = _configuration.GetSection("AWS:BucketName").Value,
        //            Key = s.Key,
        //            Expires = DateTime.UtcNow.AddMinutes(1),
        //        };
        //        return s.Key.ToString();
        //    });

        //    return Ok(s3Objects);
        //}
    }
}
