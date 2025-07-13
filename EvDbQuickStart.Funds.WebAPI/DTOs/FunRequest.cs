namespace EvDbQuickStart.Funds.WebAPI;

public record FunRequest(OperationType Operation, Guid AccountId, int Amount)
{
    public string? Attribution { get; init; }
}
