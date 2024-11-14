using StudioModel.Domain;

namespace StudioDataAccess.Repositories.Imp
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {

        public EnrollmentRepository(StudioDBContext dbContext) : base(dbContext)
        {
            
        }
    }
}
