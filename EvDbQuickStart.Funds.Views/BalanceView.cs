using EvDb.Core;
using EvDbQuickStart.Funds.Abstractions;
using EvDbQuickStart.Funds.Events;

namespace EvDbQuickStart.Funds.Views;

[EvDbViewType<Balance, IAccountFundsEvents>("balance")]
public partial class BalanceView
{
    /// <summary>
    /// The initial state of the view.
    /// </summary>
    protected override Balance DefaultState { get; } = new Balance(0);

    protected override Balance Apply(Balance state, DepositedEvent payload, IEvDbEventMeta meta)
    {
        return state with { Funds = state.Funds + payload.Amount };
    }

    protected override Balance Apply(Balance state, WithdrawnEvent payload, IEvDbEventMeta meta)
    {
        return state with { Funds = state.Funds - payload.Amount };
    }
}
