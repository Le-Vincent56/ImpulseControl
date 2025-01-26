namespace ImpulseControl.AI
{
    public interface IEnemy
    {
        float Damage { get; }
        float Speed { get; }
        void MoveToPlayer();
        void Attack();
    }
}

