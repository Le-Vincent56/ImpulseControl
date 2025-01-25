using UnityEngine;

namespace ImpulseControl.AI
{
    public class AttackState : BaseState
    {
        public AttackState(IEnemy enemy, Animator enemyAnimator) : base(enemy, enemyAnimator) { }

        public override void OnEnter()
        {
            enemyAnimator.CrossFade(MoveHash, transitionDuration);
        }
    }
}
