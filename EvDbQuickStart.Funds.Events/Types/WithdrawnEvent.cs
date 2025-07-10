using EvDb.Core;

namespace EvDbQuickStart.Funds.Events;

[EvDbDefineEventPayload("withdrawn")]
public readonly partial record struct WithdrawnEvent(double Amount);
