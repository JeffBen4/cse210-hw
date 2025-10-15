public class Running : Activity
{
    private double _distanceMiles;

    public Running(System.DateTime date, int minutes, double distanceMiles)
        : base(date, minutes)
    {
        _distanceMiles = distanceMiles;
    }

    public override double GetDistanceMiles() => _distanceMiles;
    public override double GetSpeedMph() => (_distanceMiles / Minutes) * 60.0;
    public override double GetPaceMinPerMile() => _distanceMiles == 0 ? 0 : Minutes / _distanceMiles;
}
