using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.Media;

namespace API.INFRASTRUCTURE.Repositories.FileAttach
{
    public class AttachmentFileRepository : RepositoryBase<AttachmentFile>, IAttachmentFileRepository
    {
        public AttachmentFileRepository(IDbContext db) : base(db)
        {
        }
    }
}