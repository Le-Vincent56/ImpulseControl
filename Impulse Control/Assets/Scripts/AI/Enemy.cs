using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl.AI
{
    [RequireComponent (typeof (Animator))]
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        static Transform player;
        Animator animator;
        StateMachine stateMachine;

        public float Health { get; }
        public float Damage { get; }
        public float Speed { get; }

        void Awake()
        {
            // Initialize State Machine
            stateMachine = new StateMachine();

            // Declare States
            var idleState = new IdleState(this, animator);
            var moveState = new MoveState(this, animator);
            

            // Define transitions

            // Set initial state
            stateMachine.SetState(idleState);
        }

        public virtual void MoveToPlayer() { }
        public virtual void Attack() { }
        void Update()
        {
            stateMachine.Update();
        }
        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
    }
}
