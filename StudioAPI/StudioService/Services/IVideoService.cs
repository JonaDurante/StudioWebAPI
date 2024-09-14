using StudioModel.Domain;

namespace StudioService.LoginService;

public interface IVideoService
{
    Task<List<Video>> GetAll();
    Task<Video> GetById(Guid id);
    void Insert(Video video);
    void Update(Video video);
    void Delete(Guid id);
}