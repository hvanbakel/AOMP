namespace AOMP.PhilosophersProblem;

internal class PhilosophersProblem
{
    public IReadOnlyList<Chopstick> Chopsticks { get; }
    public IReadOnlyList<Philosopher> Philosophers { get; }

    public PhilosophersProblem(int count, int? seed = null)
    {
        seed ??= new Random().Next();
        
        Console.WriteLine($"Seed: {seed}");
        var random = new Random(seed.Value);
        Chopsticks = Enumerable.Range(0, count)
            .Select(x => new Chopstick())
            .ToList();

        Philosophers = Enumerable.Range(0, count)
            .Select(x => new Philosopher(Chopsticks, x, x == count - 1 ? 0 : x + 1, random))
            .ToList();
    }

    public void Run()
    {
        foreach (var philosopher in Philosophers)
        {
            new Thread(philosopher.DoWork).Start();
        }
        
        while(true)
        {
            for (var index = 0; index < Philosophers.Count; index++)
            {
                var philosopher = Philosophers[index];
                Console.WriteLine($"{index}: {philosopher.MillisecondsStarving}ms");
            }
            
            Console.WriteLine($"Chopsticks taken: {Chopsticks.Count(x => x.IsTaken)}");
            Thread.Sleep(1000);
        }
    }
}