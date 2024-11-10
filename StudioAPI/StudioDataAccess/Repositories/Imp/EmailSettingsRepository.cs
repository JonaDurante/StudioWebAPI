using StudioModel.Domain;

namespace StudioDataAccess.Repositories.Imp
{
    public class EmailSettingsRepository : GenericRepository<EmailSetting>, IEmailSettingsRepository
    {
        public EmailSettingsRepository(StudioDBContext dbContext) : base(dbContext)
        {
        }
    }
}
