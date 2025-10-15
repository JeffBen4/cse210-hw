public class Swimming : Activity
{
    private int _laps;

    public Swimming(System.DateTime date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistanceMiles()
    {
        double meters = _laps * 50.0;
        double miles = meters / 1000.0 * 0.62;
        return miles;
    }

    public override double GetSpeedMph()
    {
        double dist = GetDistanceMiles();
        return (dist / Minutes) * 60.0;
    }

    public override double GetPaceMinPerMile()
    {
        double dist = GetDistanceMiles();
        return dist == 0 ? 0 : Minutes / dist;
    }
}
