namespace VistaPms.Domain.ValueObjects;

public class DateRange
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    public DateRange(DateTime start, DateTime end)
    {
        if (start > end)
            throw new ArgumentException("Start date cannot be after end date");

        Start = start;
        End = end;
    }

    protected DateRange() { }

    public int DurationInDays => (End - Start).Days;

    public bool Overlaps(DateRange other)
    {
        return Start < other.End && End > other.Start;
    }

    public bool Contains(DateTime date)
    {
        return date >= Start && date <= End;
    }
}
