namespace EfCore.Data
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public partial class AUMSContext : DbContext, IIpInfosDbContext, IUserInfosDbContext
    {
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            base.SaveChangesAsync(cancellationToken);
    }
}
