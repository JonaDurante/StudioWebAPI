using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioModel.Domain;
using StudioModel.Dtos.Video;
using StudioService.LoginService;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GetAll()
        {
            var videos = await _videoService.GetAll();
            return Ok(videos);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetById([FromQuery][Required] Guid id)
        {
            var video = await _videoService.GetById(id);
            return Ok(video);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] RequestVideoDto insertVideoDto)
        {
            var video = _mapper.Map<Video>(insertVideoDto);
            _videoService.Insert(video);
            return Ok(video);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromHeader] Guid id, [FromBody] RequestVideoDto updateVideoDto)
        {
            /*VER GENERIC REPOSITORY Y SERVICE*/
            var video = await _videoService.GetById(id);
            var updateVideo = _mapper.Map(updateVideoDto, video);
            _videoService.Update(updateVideo);
            return Ok(updateVideo);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromHeader] Guid id)
        {
            _videoService.Delete(id);
            return Ok();
        }
    }
}
