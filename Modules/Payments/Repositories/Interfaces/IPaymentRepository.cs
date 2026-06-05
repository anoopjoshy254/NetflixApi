using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Payments.Models;

namespace NetflixApi.Modules.Payments.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> AddAsync(Payment payment);
    }
}
