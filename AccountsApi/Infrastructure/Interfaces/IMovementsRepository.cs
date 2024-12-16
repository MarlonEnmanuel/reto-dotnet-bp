using AccountsApi.Domain;
using System.Linq.Expressions;

namespace AccountsApi.Infrastructure.Interfaces
{
    public interface IMovementsRepository
    {
        Task<List<Movement>> GetAll();
        Task<List<Movement>> Search(Expression<Func<Movement, bool>> filter);
        Task<Movement?> GetById(int movementId);
        Task<bool> Exists(int movementId);
        Task Create(Movement movement);
        Task Update(Movement movement);
        Task<bool> Delete(int movementId);
    }
}
