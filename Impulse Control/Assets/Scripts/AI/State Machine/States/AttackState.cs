using UnityEngine;

namespace ImpulseControl.AI
{
    public class AttackState : BaseState
    {
        public AttackState(IEnemy enemy, Animator enemyAnimator) : base(enemy, enemyAnimator) { }

        public override void OnEnter()
        {
            Debug.Log("ENTER: Attack State");
            enemyAnimator.CrossFade(MoveHash, transitionDuration);
        }

        public override void Update()
        {
            enemy.Attack();
        }
    }
}
