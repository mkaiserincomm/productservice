using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace productservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {        
        private readonly ILogger<VersionController> _logger;

        public VersionController(ILogger<VersionController> logger)
        {     
            _logger = logger;
            _logger.LogInformation(new EventId(1001,"Constructor"),"RedisControler VersionController");
        }

        // GET: api/version
        [HttpGet]
        public string GetVersion() => Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

    }
}
