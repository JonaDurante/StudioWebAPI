using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly IMapper _mapper;

        public VideoController(IVideoService videoService, IMapper mapper)
        {
            _videoService = videoService;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetVideos()
        {
            return Ok();
        }
    }
}
