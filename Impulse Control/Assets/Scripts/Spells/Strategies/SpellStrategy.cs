using ImpulseControl.Modifiers;
using ImpulseControl.Spells.Objects;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    public abstract class SpellStrategy : ScriptableObject
    {
        protected SpellPool spellPool;
        protected PlayerMovement playerMovement;
        protected EmotionSystem emotionSystem;
        protected SpellSystem spellSystem;
        protected LiveModifiers modifiers;
        protected HealthPlayer playerHealth;
        [SerializeField] protected string spellName;
        [SerializeField] protected string spellDescription;
        [SerializeField] protected int spellLevel;
        [SerializeField] protected EmotionType correspondingEmotion;

        public EmotionType EmotionType => correspondingEmotion;

        // Set a reference to the Spell Modifiers
        protected CountdownTimer cooldownTimer;

        public EmotionType Emotion { get => correspondingEmotion; }

        protected virtual void OnDestroy()
        {
            cooldownTimer.Dispose();
        }

        /// <summary>
        /// Link the Spell to a Spell Modifier and Emotion
        /// </summary>
        public virtual void Link(SpellSystem spellSystem, PlayerMovement playerMovement, EmotionSystem emotionSystem, LiveModifiers modifiers, SpellPool spellPool, HealthPlayer playerHealth)
        {
            this.spellSystem = spellSystem;
            this.playerMovement = playerMovement;
            this.emotionSystem = emotionSystem;
            this.modifiers = modifiers;
            this.spellPool = spellPool;
            this.playerHealth = playerHealth;

            // Set up the cooldown timer
            SetupCooldown();
        }

        protected abstract void SetupCooldown();

        protected abstract bool OnCooldown();

        /// <summary>
        /// Cast the Spell
        /// </summary>
        public abstract void Cast();

        /// <summary>
        /// Crashout the Strategy
        /// </summary>
        public abstract void CrashOut();

        public abstract void Exhaust();
    }
}
