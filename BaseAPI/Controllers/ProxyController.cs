using BaseAPI.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProxyController : ControllerBase
    {
        private readonly ILogger<ProxyController> _logger;
        private readonly HttpClient _httpclient;

        public ProxyController(ILogger<ProxyController> logger)
        {
            _logger = logger;
            _httpclient = new HttpClient(new HttpClientHandler()
            {
                AllowAutoRedirect = false
            });
        }

        [HttpGet(Name = "Rewrite")]
        public async Task<IActionResult> Rewrite()
        {
            var request = HttpContext.CreateProxyHttpRequest(new Uri("https://www.google.com"));
            var response = await _httpclient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, HttpContext.RequestAborted);
            await HttpContext.CopyProxyHttpResponse(response);
            return new EmptyResult();
        }
    }
}