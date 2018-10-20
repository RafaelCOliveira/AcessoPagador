namespace AcessoPagador.Contracts.Enum
{
    public enum StatusEnum
    {
        Active = 1,
        Finished = 2,
        DisabledByMerchant = 3,
        DisabledMaxAttempts = 4,
        DisabledExpiredCreditCard = 5,
    }
}