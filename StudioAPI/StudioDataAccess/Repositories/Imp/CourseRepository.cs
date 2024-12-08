using StudioModel.Domain;

namespace StudioDataAccess.Repositories.Imp
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(StudioDBContext dbContext) : base(dbContext)
        {
        }
    }
}
