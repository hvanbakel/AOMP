namespace AOMP.Chapter5;

public class Assign23Take2 : IConsensus<int>
{
    private int? _field1;

    public int Decide(int value)
    {
        return Interlocked.CompareExchange(ref _field1, value, null) ?? value;
    }
}