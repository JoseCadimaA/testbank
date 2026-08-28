using AntiFraud.Domain.Entities;

namespace AntiFraud.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<TransactionValidationResult> ValidateAndRegisterAsync(
        OrdenACH ordenACH,
        CancellationToken cancellationToken = default);
}

public record TransactionValidationResult(
    bool IsApproved,
    decimal AccumulatedAmount,
    string? RejectionReason = null);
