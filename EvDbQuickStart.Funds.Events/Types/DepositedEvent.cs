using EvDb.Core;

namespace EvDbQuickStart.Funds.Events;

[EvDbDefineEventPayload("deposited")]
public readonly partial record struct DepositedEvent
{
    public required double Amount { get; init; }
}