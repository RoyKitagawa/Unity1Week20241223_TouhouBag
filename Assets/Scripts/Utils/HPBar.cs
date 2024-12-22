public class HPBar : SliderBase
{
    public override void Gain(float gainAmt, bool recordExceedAmt = true)
    {
        base.Gain(gainAmt, recordExceedAmt);
    }

    public override void Reduce(float reduceAmt)
    {
        base.Reduce(reduceAmt);
    }
}