﻿using Shared.Enums;

namespace AccountsApi.Application.Dtos
{
    public class AccountDto
    {
        public string Number { get; set; } = string.Empty;
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }
        public bool Status { get; set; }
        public int ClientId { get; set; }
    }
}
