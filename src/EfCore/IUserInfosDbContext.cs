using System.Threading;
using System.Threading.Tasks;
using EfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    public interface IUserInfosDbContext
    {
        DbSet<UserInfo> UserInfos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
