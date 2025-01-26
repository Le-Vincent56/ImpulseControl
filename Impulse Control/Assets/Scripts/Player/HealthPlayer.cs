using System.Collections;
using System.Collections.Generic;
using ImpulseControl.Events;
using ImpulseControl.Modifiers;
using UnityEngine;

namespace ImpulseControl
{
   public class HealthPlayer : Health
    {
        private LiveModifiers liveModifiers;
        private EmotionSystem emotionSystem;
        private EventBinding<Event_EnvyExhaustedFinished> exhaustedFinishEventBinding;

        private void OnEnable()
        {
            exhaustedFinishEventBinding = new EventBinding<Event_EnvyExhaustedFinished>(OnExhaustionEnvyFinished);
            EventBus<Event_EnvyExhaustedFinished>.Register(exhaustedFinishEventBinding);
        }

        private void OnDisable()
        {
            EventBus<Event_EnvyExhaustedFinished>.Deregister(exhaustedFinishEventBinding);
        }


        private Emotion anger;
        private Emotion envy;
        private Emotion fear;

        protected override void Start()
        {
            base.Start();
            liveModifiers = GetComponent<LiveModifiers>();
            emotionSystem = GetComponent<EmotionSystem>();
        }
        public override void TakeDamage(float damage)
        {
            float damageModifier = damage;
            
            //if fear crashout no damage
            if (emotionSystem.Fear.EmotionState == EmotionStates.CrashingOut) 
                return;

            //different damage per emotion modify the damage
            switch (emotionSystem.Anger.EmotionState)
            {
                case EmotionStates.CrashingOut:
                    damageModifier *= liveModifiers.Anger.crashOutDamageReduction;
                    break;
                case EmotionStates.Exhausted:
                    damageModifier *= liveModifiers.Anger.exhaustionWeaknessMultiplier;
                    break;
            }

            //modify health
            currentHealth -= damageModifier;
            
            if (currentHealth <= 0)
            {
                Death?.Invoke();
                if (CompareTag("Player")) 
                {
                    Debug.Log("Player died a sussy death");
                }
            }
        }

        //envy exhaust done set health to zero
        private void OnExhaustionEnvyFinished()
        {
            Debug.Log("Exaust Done");
            currentHealth = liveModifiers.Envy.exhaustionHealthPercentage * currentHealth;
        }
    }
}
