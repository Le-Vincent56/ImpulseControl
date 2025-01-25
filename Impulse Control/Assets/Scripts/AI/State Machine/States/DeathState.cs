using UnityEngine;

namespace ImpulseControl.AI
{
    public class DeathState : BaseState
    {
        public DeathState(IEnemy enemy, Animator enemyAnimator) : base(enemy, enemyAnimator) { }

        public override void OnEnter()
        {
            enemyAnimator.CrossFade(MoveHash, transitionDuration);
        }
    }
}
