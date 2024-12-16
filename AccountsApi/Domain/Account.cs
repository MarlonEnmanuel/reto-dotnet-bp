using Shared.Enums;
using Shared.Exceptions;

namespace AccountsApi.Domain
{
    public class Account
    {
        public string Number { get; set; } = string.Empty;
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }
        public bool Status { get; set; } = true;
        public int ClientId { get; set; }

        // navigation properties
        public List<Movement> Movements { get; set; } = [];


        // methods
        
        public Movement CreateMovement(decimal amount)
        {
            AddAmount(amount);
            return new()
            {
                AccountNumber = Number,
                DateTime = DateTime.Now,
                Amount = amount,
                Balance = Balance,
                Description = GetMovementDescription(amount),
            };
        }

        public Movement ReverseMovement(Movement movement)
        {
            movement.Description += " (Anulado)";
            AddAmount(-movement.Amount);
            return new()
            {
                AccountNumber = Number,
                DateTime = DateTime.Now,
                Amount = -movement.Amount,
                Balance = Balance,
                Description = GetMovementDescription(-movement.Amount) + $" (Anulación Mov{movement.Id})",
            };
        }

        private void AddAmount(decimal amount)
        {
            if (amount == 0)
                throw new InternalException("El monto del movimiento no puede ser cero");
            Balance += amount;
        }

        public void ValidateBalance()
        {
            if (Balance < 0)
                throw new InternalException("Saldo no disponible");
        }

        private string GetMovementDescription(decimal amount)
        {
            var action = amount > 0 ? "Depósito" : "Retiro";
            return $"{action} por {Math.Abs(amount)}";
        }
    }
}
