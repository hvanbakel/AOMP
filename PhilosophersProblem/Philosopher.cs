using System.Diagnostics;

namespace AOMP.PhilosophersProblem;

internal class Philosopher
{
    private const int Interval = 5;
    private readonly Random _random;
    private readonly Stopwatch _stopwatch;

    public Philosopher(IReadOnlyList<Chopstick> chopsticks, int index1, int index2, Random random)
    {
        _random = new Random();
        this.Chopsticks = [chopsticks[index1], chopsticks[index2]];
        _stopwatch = new Stopwatch();
    }

    public Chopstick[] Chopsticks { get; init; }
    
    public long MillisecondsStarving => _stopwatch.ElapsedMilliseconds;

    public void DoWork()
    {
        _stopwatch.Start();
        while (true)
        {
            Think();
            
            Eat();
        }
    }

    private void Think()
    {
        Thread.Sleep(this._random.Next(Interval));
    }

    private void Eat()
    {
        while (true)
        {
            if (Chopsticks[0].Take(this) && Chopsticks[1].Take(this))
            {
                break;
            }
            
            Chopsticks[0].PutDown(this);
            Chopsticks[1].PutDown(this);
        }
        
        _stopwatch.Reset();
        Thread.Sleep(_random.Next(Interval));
        _stopwatch.Start();
        
        Chopsticks[0].PutDown(this);
        Chopsticks[1].PutDown(this);
    }
}