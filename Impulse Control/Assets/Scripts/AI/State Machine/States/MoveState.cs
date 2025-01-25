using UnityEngine;

namespace ImpulseControl.AI
{
    public class MoveState : BaseState
    {
        public MoveState(IEnemy enemy, Animator enemyAnimator) : base(enemy, enemyAnimator) { }

        public override void OnEnter()
        {
            enemyAnimator.CrossFade(MoveHash, transitionDuration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
