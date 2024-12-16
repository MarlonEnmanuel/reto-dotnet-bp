using AccountsApi.Domain;
using AccountsApi.Infrastructure.Database;
using AccountsApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccountsApi.Infrastructure.Repositories
{
    public class DbMovementsRepository(AccountsContext context) : IMovementsRepository
    {
        public async Task<List<Movement>> GetAll()
        {
            return await context.Movements
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<List<Movement>> Search(Expression<Func<Movement, bool>> filter)
        {
            return await context.Movements
                                .AsNoTracking()
                                .Include(m => m.Account)
                                .Where(filter)
                                .ToListAsync();
        }

        public async Task<Movement?> GetById(int movementId)
        {
            return await context.Movements
                                .AsNoTracking()
                                .FirstOrDefaultAsync(c => c.Id == movementId);
        }

        public async Task<bool> Exists(int movementId)
        {
            return await context.Movements.AnyAsync(c => c.Id == movementId);
        }

        public async Task Create(Movement movement)
        {
            if (movement.Id != default)
                throw new InvalidOperationException($"El movimiento tiene ID asignado");

            await context.Movements.AddAsync(movement);
        }

        public async Task Update(Movement movement)
        {
            var currentMovement = await context.Movements.FindAsync(movement.Id) ??
                throw new InvalidOperationException($"El movimiento '{movement.Id}' no existe");

            context.Entry(currentMovement).CurrentValues.SetValues(movement);
        }

        public async Task<bool> Delete(int movementId)
        {
            var movement = await context.Movements.FindAsync(movementId);
            if (movement == null) return false;

            context.Movements.Remove(movement);
            return true;
        }
    }
}
