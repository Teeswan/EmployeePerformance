using EPMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken);
    }
}
