using EvDb.Core;
using EvDbQuickStart.Funds.Views;

namespace EvDbQuickStart.Funds.Repositories;


[EvDbAttachView<BalanceView>]
[EvDbStreamFactory<IAccountFundsEvents>("ev-bank:funds")]
public partial class FundsFactory
{
}
