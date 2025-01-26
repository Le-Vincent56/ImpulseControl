using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ImpulseControl.AI
{
    [RequireComponent (typeof (Animator), typeof(Health))]
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        protected static GameObject player;
        protected Animator animator;
        protected Health enemyHealth;
        protected bool isDead;

        protected StateMachine stateMachine;

        protected bool withinRangeOfPlayer;

        public float Damage { get; set; }
        public float Speed { get; set; }

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            enemyHealth = GetComponent<Health>();
            animator = GetComponent<Animator>();
            // Initialize State Machine
            stateMachine = new StateMachine();

            // Declare States
            var idleState = new IdleState(this, animator);
            var moveState = new MoveState(this, animator);
            var attackState = new AttackState(this, animator);
            var deathState = new DeathState(this, animator);

            // Define transitions
            At(idleState, moveState, new FuncPredicate(() => player != null));
            At(idleState, attackState, new FuncPredicate(() => withinRangeOfPlayer));
            At(moveState, attackState, new FuncPredicate(() => withinRangeOfPlayer));
            At(attackState, moveState, new FuncPredicate(() => !withinRangeOfPlayer));
            Any(deathState, new FuncPredicate(() => isDead));

            // Set initial state
            stateMachine.SetState(idleState);
        }
        private void OnEnable()
        {
            enemyHealth.Death += ChangeDeathStatus;
        }
        private void OnDisable()
        {
            enemyHealth.Death -= ChangeDeathStatus;
        }

        public virtual void MoveToPlayer() 
        {
            Vector3 dirToPlayer = player.transform.position - this.transform.position;
            this.transform.position += dirToPlayer.normalized * Speed * Time.deltaTime;
            Debug.Log("dirToPlayer: " + dirToPlayer);
            Debug.Log("Player Pos: " + player.transform.position + ",  this Pos: " + this.transform.position);
            Debug.Log("Speed: " + Speed);
        }
        public virtual void Attack() { }
        
        void ChangeDeathStatus()
        {
            isDead = true;
        }
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
    }
}
