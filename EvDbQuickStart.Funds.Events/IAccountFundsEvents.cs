using EvDb.Core;
using EvDbQuickStart.Funds.Events;

namespace EvDbQuickStart.Funds;

[EvDbAttachEventType<DepositedEvent>]
[EvDbAttachEventType<WithdrawnEvent>]
public partial interface IAccountFundsEvents
{

}
