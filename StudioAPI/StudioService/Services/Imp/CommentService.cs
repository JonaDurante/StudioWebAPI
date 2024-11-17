using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioDataAccess.Uow;
using StudioModel.Domain;
using StudioModel.Dtos.Comment;

namespace StudioService.Services.Imp
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var comment = await _unitOfWork.CommentRepository.GetById(id);
            if (comment != null)
            {
                return new OkObjectResult(_mapper.Map<CommentDto>(comment));
            }
            return new BadRequestResult();
        }

        public async Task<IActionResult> GetAll(bool onlyActive)
        {
            var commentList = await _unitOfWork.CommentRepository.GetAll(onlyActive);
            if (commentList != null)
            {
                return new OkObjectResult(_mapper.Map<List<CommentDto>>(commentList));
            }
            return new BadRequestResult();
        }

        public async Task<CommentDto> Create(CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);

            await _unitOfWork.CommentRepository.Add(comment);

            _unitOfWork.Save();

            return _mapper.Map<CommentDto>(comment);
        }
        public async Task<IActionResult> Update(Guid commentId, CommentDto commentDto)
        {
            var commentDB = await _unitOfWork.CommentRepository.GetById(commentId);
            if (commentDB != null && commentDB.AuthorId == commentDto.AuthorId)
            {
                _mapper.Map(commentDto, commentDB);
                commentDB.CommentTime = DateTime.Now;
                _unitOfWork.CommentRepository.Update(commentDB);
                _unitOfWork.Save();
                return new OkObjectResult(commentDto);
            }
            return new BadRequestResult();
        }

        public async void Delete(Guid commentId, Guid authorId)
        {
            var comment = await _unitOfWork.CommentRepository.GetById(commentId);
            if (comment != null && comment.AuthorId == authorId)
            {
                _unitOfWork.CommentRepository.LogicDelete(commentId);
                _unitOfWork.Save();
            }
        }

        public async void Delete(Guid commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetById(commentId);
            if (comment != null)
            {
                _unitOfWork.CommentRepository.LogicDelete(commentId);
                _unitOfWork.Save();
            }
        }
    }
}
