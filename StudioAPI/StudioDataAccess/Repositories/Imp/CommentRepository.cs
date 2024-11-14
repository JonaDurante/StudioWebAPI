using StudioModel.Domain;

namespace StudioDataAccess.Repositories.Imp
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(StudioDBContext dbContext) : base(dbContext)
        {
        }
    }
}
