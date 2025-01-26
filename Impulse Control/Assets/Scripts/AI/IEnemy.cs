namespace ImpulseControl.AI
{
    public interface IEnemy
    {
        void CheckIfInAttackRange();
        void MoveToPlayer();
        void Attack();
    }
}

