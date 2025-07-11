namespace EvDbQuickStart.Funds.WebAPI;

public record FunRequest(OperationType Operation, int AccountId, int Amount)
{
    public string? Attribution { get; init; }
}
