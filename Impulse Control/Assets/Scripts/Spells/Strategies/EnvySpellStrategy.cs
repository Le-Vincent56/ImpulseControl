using ImpulseControl.Modifiers;
using ImpulseControl.Spells.Objects;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Envy Spell", menuName = "Spells/Envy Spell")]
    public class EnvySpellStrategy : SpellStrategy
    {
        private EnvySpell spell;
        private bool activated;

        public override void Link(SpellSystem spellSystem, PlayerMovement playerMovement, EmotionSystem emotionSystem, LiveModifiers modifiers, SpellPool spellPool)
        {
            base.Link(spellSystem, playerMovement, emotionSystem, modifiers, spellPool);

            // Set not activated
            activated = false;
        }

        /// <summary>
        /// Set up the Cooldown for the Envy Spell
        /// </summary>
        protected override void SetupCooldown()
        {
            cooldownTimer = new CountdownTimer(modifiers.Envy.spellCooldownTime);

            // Constantly check the cast time
            cooldownTimer.OnTimerStop += () => cooldownTimer.Reset(modifiers.Envy.spellCooldownTime);
        }

        /// <summary>
        /// Check if the Envy Spell is on cooldown
        /// </summary>
        protected override bool OnCooldown() => cooldownTimer.IsRunning;

        /// <summary>
        /// Cast the Envy Spell
        /// </summary>
        public override void Cast()
        {
            // Exit case - the Envy Spell is on cooldown
            if (OnCooldown()) return;

            // Check if the spell is already active
            if(activated)
            {
                // Nullify the spell's target
                spell.SetTarget(null);

                // Release the Spell
                spellPool.Pool.Release(spell);

                // Set deactivated
                activated = false;
            } 
            // Otherwise
            else
            {
                // Get an envy spell
                spell = (EnvySpell)spellPool.Pool.Get();

                // Set the follow transform of the Envy Spell
                spell.SetTarget(spellSystem.transform);

                // Set activated
                activated = true;
            }

            // Start the cooldown timer
            cooldownTimer.Start();
        }
    }
}
