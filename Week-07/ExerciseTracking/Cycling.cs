public class Cycling : Activity
{
    private double _speedMph;

    public Cycling(System.DateTime date, int minutes, double speedMph)
        : base(date, minutes)
    {
        _speedMph = speedMph;
    }

    public override double GetDistanceMiles() => (_speedMph * Minutes) / 60.0;
    public override double GetSpeedMph() => _speedMph;
    public override double GetPaceMinPerMile() => _speedMph == 0 ? 0 : 60.0 / _speedMph;
}
