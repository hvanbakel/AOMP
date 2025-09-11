using System;
using Microsoft.Extensions.DependencyInjection;

namespace AOMP;

class Program
{
    static void Main(string[] args)
    {        
        var philosophersProblem = new PhilosophersProblem.PhilosophersProblem(50);
        philosophersProblem.Run();

    }
}