using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Comment;
using StudioService.Services;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commmentService;

        public CommentController(ICommentService commmentService)
        {
            _commmentService = commmentService;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await _commmentService.GetById(id);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(bool onlyActive = true)
        {
            return await _commmentService.GetAll(onlyActive);
        }
        [HttpPost("Create")]
        public async Task<CommentDto> Create(CommentDto commentDto)
        {
            return await _commmentService.Create(commentDto);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Guid commentId, CommentDto commentDto)
        {
            return await _commmentService.Update(commentId, commentDto);
        }
        [HttpDelete("Delete")]
        public void Delete(Guid commentId, Guid userId)
        {
            _commmentService.Delete(commentId, userId);
        }
    }
}
