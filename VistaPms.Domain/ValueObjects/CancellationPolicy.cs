namespace VistaPms.Domain.ValueObjects;

public class CancellationPolicy
{
    public int DaysBeforeCheckIn { get; init; }
    public decimal PenaltyPercentage { get; init; }
    public string Description { get; init; } = string.Empty;

    public CancellationPolicy(int daysBeforeCheckIn, decimal penaltyPercentage, string description)
    {
        DaysBeforeCheckIn = daysBeforeCheckIn;
        PenaltyPercentage = penaltyPercentage;
        Description = description;
    }

    protected CancellationPolicy() { }
}
