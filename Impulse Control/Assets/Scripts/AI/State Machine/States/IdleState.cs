using UnityEngine;

namespace ImpulseControl.AI
{
    public class IdleState : BaseState
    {
        public IdleState(IEnemy enemy, Animator enemyAnimator) : base(enemy, enemyAnimator) { }

        public override void OnEnter()
        {
            Debug.Log("ENTER: Idle State");
            enemyAnimator.CrossFade(IdleHash, transitionDuration);
        }
    }
}
