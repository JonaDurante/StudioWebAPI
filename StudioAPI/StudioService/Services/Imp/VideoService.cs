using StudioDataAccess.Uow;
using StudioModel.Domain;
using StudioModel.Dtos.Video;

namespace StudioService.LoginService.Imp
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VideoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }        

        public async Task<List<Video>> GetAll()
        {
            return await _unitOfWork.VideoRepository.GetAll();
        }

        public async Task<Video> GetById(Guid id)
        {
            return await _unitOfWork.VideoRepository.GetById(id);
        }

        public async void Insert(Video video)
        {            
            await _unitOfWork.VideoRepository.Add(video);
            _unitOfWork.Save();
        }

        public void Update(Video video)
        {
            _unitOfWork.VideoRepository.Update(video);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.VideoRepository.LogicDelete(id);
            _unitOfWork.Save();
        }
    }
}
