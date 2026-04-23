using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedAppraisalCycleRepository : CachedBaseRepository<AppraisalCycle, int>, IAppraisalCycleRepository
{
    public CachedAppraisalCycleRepository(IBaseRepository<AppraisalCycle, int> innerRepository, IMemoryCache cache, TimeSpan cacheDuration) 
        : base(innerRepository, cache, cacheDuration)
    {
    }
}
