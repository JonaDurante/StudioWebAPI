using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Comment;

namespace StudioService.Services
{
    public interface ICommentService
    {
        Task<IActionResult> GetById(Guid id);
        Task<IActionResult> GetAll(bool onlyActive = true);
        Task<CommentDto> Create(CommentDto commentDto);
        Task<IActionResult> Update(Guid commentId, CommentDto commentDto);
        void Delete(Guid commentId, Guid userId);
        void Delete(Guid commentId);
    }
}
