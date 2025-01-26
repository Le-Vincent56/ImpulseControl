using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl.AI
{
    public class RegularEnemy : Enemy
    {
        [SerializeField] float damage = 25f;
        [SerializeField] float speed = 20f;

        // Start is called before the first frame update
        void Start()
        {
            base.Damage = damage;
            base.Speed = speed;
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
        //public override void MoveToPlayer() { }
        //public override void Attack() { }
    }
}
