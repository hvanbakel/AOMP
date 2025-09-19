namespace AOMP.Chapter5;

public interface IConsensus<T>
{
    T Decide(T value);
}

public abstract class ConsensusProtocol<T> : IConsensus<T>
{
    protected T[] Proposed { get; }

    protected ConsensusProtocol(int count)
    {
        this.Proposed = new T[count];
    }

    // announce my input value to the other threads
    protected void Propose(T value)
    {
        Proposed[Environment.CurrentManagedThreadId] = value;
    }

    // figure out which thread was first
    public abstract T Decide(T value);
}

public class QueueConsensus<T> : ConsensusProtocol<T>
{
    private readonly Queue<int> _queue;

    public QueueConsensus(int count) : base(count)
    {
        this._queue = new Queue<int>(count);
    }

    public override T Decide(T value)
    {
        Propose(value);
        
        var i = Environment.CurrentManagedThreadId;
        _queue.Enqueue(i);
        return Proposed[_queue.Peek()]; // we assume here that the thread ids are auto-incrementing starting at 0 as in the book
    }
}