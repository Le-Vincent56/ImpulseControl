namespace ImpulseControl.AI
{
    public interface IEnemy
    {
        float Health { get; }
        float Damage { get; }
        float Speed { get; }

        void MoveToPlayer();
        void Attack();
    }
}

