namespace AOMP.PhilosophersProblem;

internal class Chopstick
{
    private Philosopher? _philosopherUsingIt;
    public bool IsTaken => _philosopherUsingIt != null;

    public bool Take(Philosopher philosopher)
    {
        return Interlocked.CompareExchange(ref this._philosopherUsingIt, philosopher, null) == null;
    }

    public void PutDown(Philosopher philosopher)
    {
        Interlocked.CompareExchange(ref this._philosopherUsingIt, null, philosopher);
    }
}