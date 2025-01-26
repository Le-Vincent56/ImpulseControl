using UnityEngine;

namespace ImpulseControl.AI
{
    public abstract class BaseState : IState
    {
        protected readonly IEnemy enemy;
        protected readonly Animator enemyAnimator;

        // Enemies will have the following animations: Idle, Move, Attack. 
        // Get references to these animations 
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int MoveHash = Animator.StringToHash("Move");
        protected static readonly int AttackHash = Animator.StringToHash("Attack");
        protected static readonly int DeathHash = Animator.StringToHash("Death");

        protected const float transitionDuration = 0.1f;

        protected BaseState(IEnemy enemy, Animator enemyAnimator) 
        {
            this.enemy = enemy;
            this.enemyAnimator = enemyAnimator;
        }
        public virtual void FixedUpdate() { }

        public virtual void OnEnter() { }

        public virtual void OnExit() { 
            Debug.Log("EXIT: Base State");
        }

        public virtual void Update() { }
    }
}
