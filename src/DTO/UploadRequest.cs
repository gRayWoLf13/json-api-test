using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace json_api_test.DTO
{
    public class UploadRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public byte[] Json { get; set; }

        [Required]
        public byte[] File { get; set; }
    }
}
