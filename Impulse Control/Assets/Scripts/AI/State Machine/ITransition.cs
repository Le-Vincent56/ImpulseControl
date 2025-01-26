namespace ImpulseControl.AI
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}
