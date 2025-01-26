using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ImpulseControl.AI
{
    [RequireComponent (typeof (Animator), typeof(Health))]
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        protected static GameObject player;
        Vector3 dirToPlayer;
        protected Animator animator;
        protected Health enemyHealth;
        protected bool isDead;

        protected StateMachine stateMachine;

        protected bool withinAttackRange;
        private float tolerance = 2f;

        public float Damage { get; set; } 
        protected float Speed { get; set; }
        protected float StoppingDistance { get; set; }
        

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
            At(idleState, attackState, new FuncPredicate(() => withinAttackRange));
            At(moveState, attackState, new FuncPredicate(() => withinAttackRange));
            At(attackState, moveState, new FuncPredicate(() => !withinAttackRange));
            At(idleState, moveState, new FuncPredicate(() => player != null));
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
            //TODO: Pursue behavior
            dirToPlayer = player.transform.position - this.transform.position;
            this.transform.position += dirToPlayer.normalized * Speed * Time.deltaTime;

            // Flip Sprite when necessary
            if (dirToPlayer.x > 0)
            {
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        void FlipSprite()
        {
            this.transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
        }
        public virtual void Attack() 
        {
            dirToPlayer = player.transform.position - this.transform.position;
        }
        public void CheckIfInAttackRange()
        {
            float playerYUpperBound = player.transform.position.y + tolerance;
            float playerYLowerBound = player.transform.position.y - tolerance;
            float squrDistance = dirToPlayer.sqrMagnitude;
            withinAttackRange = (squrDistance <= StoppingDistance * StoppingDistance
                                  && this.transform.position.y <= playerYUpperBound && this.transform.position.y >= playerYLowerBound) ? true : false;
        }
        // Hook this up as an animation event
        public void AttackAnimEvent()
        {
            RaycastHit2D raycast = Physics2D.Raycast(this.transform.position, dirToPlayer, StoppingDistance + 2f);
            if (raycast && raycast.transform.gameObject.tag == "Player")
            {
                Debug.DrawRay(this.transform.position, dirToPlayer, Color.green);

                raycast.transform.gameObject.GetComponent<Health>().TakeDamage(Damage);
            }
            Debug.Log("Attack Raycast hit?: " + raycast);
        }
        
        void ChangeDeathStatus()
        {
            isDead = true;
        }
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
    }
}
