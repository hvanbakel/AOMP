using System.Diagnostics;

namespace AOMP.PhilosophersProblem;

internal class Philosopher
{
    private const int Interval = 5;
    private readonly Random _random;
    private readonly Stopwatch _stopwatch;
    private readonly Chopstick _chopstickRight, _chopstickLeft;

    public Philosopher(IReadOnlyList<Chopstick> chopsticks, int indexLeft, int indexRight, Random random)
    {
        _random = random;
        _chopstickLeft = chopsticks[indexLeft];
        _chopstickRight = chopsticks[indexRight];
        _stopwatch = new Stopwatch();
    }

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
        Thread.Sleep(_random.Next(Interval));
    }

    private void Eat()
    {
        while (true)
        {
            if (_chopstickLeft.Take(this) && _chopstickRight.Take(this))
            {
                break;
            }
            
            _chopstickLeft.PutDown(this);
            _chopstickRight.PutDown(this);
        }
        
        _stopwatch.Reset();
        Thread.Sleep(_random.Next(Interval));
        _stopwatch.Start();
        
        _chopstickLeft.PutDown(this);
        _chopstickRight.PutDown(this);
    }
}