using ImpulseControl.Events;
using ImpulseControl.Modifiers;
using UnityEngine;

namespace ImpulseControl
{
   public class HealthPlayer : Health
    {
        private LiveModifiers liveModifiers;
        private EmotionSystem emotionSystem;
        private EventBinding<Event_ExaustedEnd> exhaustedFinishEventBinding;
        private bool canTakeDamage;

        private void OnEnable()
        {
            exhaustedFinishEventBinding = new EventBinding<Event_ExaustedEnd>((e)=>
            {
                if(e.emotionType == EmotionType.Fear)
                    OnExhaustionEnvyFinished();
            });
            EventBus<Event_ExaustedEnd>.Register(exhaustedFinishEventBinding);
        }

        private void OnDisable()
        {
            EventBus<Event_ExaustedEnd>.Deregister(exhaustedFinishEventBinding);
        }

        private Emotion anger;
        private Emotion envy;
        private Emotion fear;

        public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

        protected override void Start()
        {
            base.Start();
            liveModifiers = GetComponent<LiveModifiers>();
            emotionSystem = GetComponent<EmotionSystem>();
            canTakeDamage = true;
        }
        public override bool TakeDamage(float damage)
        {
            // Exit case - the Player can't take damage
            if (!canTakeDamage) return false;

            float damageModifier = damage;
            
            //if fear crashout no damage
            if (emotionSystem.Fear.EmotionState == EmotionStates.CrashingOut) 
                return false;

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
            return base.TakeDamage(damageModifier);
        }

        /// <summary>
        /// Heal the Player
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(float amount) => currentHealth += Mathf.Clamp(0, startingHealth, currentHealth + amount);

        //envy exhaust done set health to zero
        private void OnExhaustionEnvyFinished()
        {
            currentHealth = liveModifiers.Envy.exhaustionHealthPercentage * currentHealth;
        }
    }
}
