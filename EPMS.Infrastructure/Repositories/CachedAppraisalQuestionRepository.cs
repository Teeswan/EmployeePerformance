using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedAppraisalQuestionRepository : CachedBaseRepository<AppraisalQuestion, int>, IAppraisalQuestionRepository
{
    public CachedAppraisalQuestionRepository(IBaseRepository<AppraisalQuestion, int> innerRepository, IMemoryCache cache, TimeSpan cacheDuration) 
        : base(innerRepository, cache, cacheDuration)
    {
    }
}
