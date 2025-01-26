using UnityEngine;

namespace ImpulseControl.AI
{
    public class ShoveState : BaseState
    {
        protected static readonly int ShoveHash = Animator.StringToHash("Shove");

        public ShoveState(TankEnemy enemy, Animator enemyAnimator) : base(enemy, enemyAnimator) { }

        public override void OnEnter()
        {
            Debug.Log("ENTER: Shove State");
            enemyAnimator.CrossFade(ShoveHash, transitionDuration);
        }

        public override void Update()
        {
            enemy.CheckIfInAttackRange();
            enemy.Attack();
        }
    }
}
