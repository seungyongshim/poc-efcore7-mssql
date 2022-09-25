
using System.Threading;
using System.Threading.Tasks;
using EfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    public interface IIpInfosDbContext
    {
        DbSet<IpInfo> IpInfos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
