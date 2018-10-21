namespace AcessoPagador.Contracts.Enumerat
{
    public enum ReasonEnum
    {
        Successful = 0,
        AffiliationNotFound = 1,
        IssuficientFunds = 2,
        CouldNotGetCreditCard = 3,
        ConnectionWithAcquirerFailed = 4,
        InvalidTransactionType = 5,
        InvalidPaymentPlan = 6,
        Denied = 7,
        Scheduled = 8,
        Waiting = 9,
        Authenticated = 10,
        NotAuthenticated = 11,
        ProblemsWithCreditCard = 12,
        CardCanceled = 13,
        BlockedCreditCard = 14,
        CardExpired = 15,
        AbortedByFraud = 16,
        CouldNotAntifraud = 17,
        TryAgain = 18,
        InvalidAmount = 19,
        ProblemsWithIssuer = 20,
        InvalidCardNumber = 21,
        TimeOut = 22,
        CartaoProtegidoIsNotEnabled = 23,
        PaymentMethodIsNotEnabled = 24,
        InvalidRequest = 98,
        InternalError = 99,
    }
}