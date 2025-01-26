using ImpulseControl.AI;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace ImpulseControl
{
    public class TankEnemy : Enemy
    {
        [SerializeField] float damage = 50f;
        [SerializeField] float shoveDamage = 20f;
        [SerializeField] float pushForce = 3f;
        [SerializeField] float speed = 5f;
        [SerializeField] float stoppingDistance = 2f;

        // Start is called before the first frame update
        void Start()
        {
            base.Damage = damage;
            base.Speed = speed;
            base.StoppingDistance = stoppingDistance;

            var moveState = new MoveState(this, animator);
            var attackState = new AttackState(this, animator);
            var shoveState = new ShoveState(this, animator);
            
            // 50/50 chance of attack becoming a shove
            At(attackState, shoveState, new FuncPredicate(() => Random.Range(0f, 1f) < 0.5f));
            At(shoveState, moveState, new FuncPredicate(() => !withinAttackRange));
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        public void Shove()
        {
            RaycastHit2D raycast = Physics2D.Raycast(this.transform.position, dirToPlayer, StoppingDistance + 2f);
            if (raycast && raycast.transform.gameObject.tag == "Player")
            {
                raycast.transform.gameObject.GetComponent<Health>().TakeDamage(shoveDamage);
                raycast.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(dirToPlayer.normalized * pushForce, ForceMode2D.Impulse);
            }
        }
        // public override void MoveToPlayer() { }
        // public override void Attack() { }
    }
}
