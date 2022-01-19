using json_api_test.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace json_api_test.Controllers
{
    [ApiController]
    [Route("api/test/[action]")]
    public class TestController : ControllerBase
    {
        private readonly IStorage _storage;

        public TestController(IStorage storage)
        {
            _storage = storage;
        }

        [RequestSizeLimit(1_000_000_000)]
        [HttpPost]
        public async Task<IActionResult> Upload([FromBody]UploadRequest request)
        {
            var name = request.Name;

            var content = await _storage.UploadAsync(request.File, name);
            //Если нужны минимальные задержки и затраты памяти, то нет смысла обрабатывать Json особым образом.
            //Если Json приходит от клиента в виде массива байт, то логичнее сразу этот массив байт и сохранить.
            //Поэтому вместо JToken логичнее использовать byte[]
            var data = await _storage.UploadAsync(request.Json, name, ".json");

            return Ok(new
            {
                name,
                data,
                content
            });
        }
    }
}
