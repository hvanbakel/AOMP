namespace AOMP.Chapter5;

public class Assign23 : IConsensus<int>
{
    private int? _field1;
    private int? _field2;
    private int? _field3;

    public int Decide(int value)
    {
        // we assume threads are numbered 0, 1, 2 like in the book.
        var threadId = Environment.CurrentManagedThreadId;
        
        return threadId switch
        {
            0 => UseCompareExchange(ref _field1, ref _field2, value, ref _field3),
            1 => UseCompareExchange(ref _field2, ref _field3, value, ref _field1),
            2 => UseCompareExchange(ref _field3, ref _field1, value, ref _field2),
            _ => throw new Exception("Invalid thread id")
        };
    }

    private static int UseCompareExchange(ref int? first, ref int? second, int value, ref int? third)
    {
        var firstValue = Interlocked.CompareExchange(ref first, value, null);
        var secondValue = Interlocked.CompareExchange(ref second, value, null);
        if ((firstValue == null || firstValue == value) &&
            (secondValue == null || secondValue == value))
        {
            // we are first
            return value;
        }
        else
        {
            // we are not first
            return third.Value;
        }
    }
}