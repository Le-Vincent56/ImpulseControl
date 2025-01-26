using ImpulseControl.Events;
using ImpulseControl.Modifiers;
using ImpulseControl.Spells.Objects;
using ImpulseControl.Timers;
using Unity.VisualScripting;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Envy Spell", menuName = "Spells/Envy Spell")]
    public class EnvySpellStrategy : SpellStrategy
    {
        private EnvySpell spell;
        private CountdownTimer costTimer;
        private bool activated;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            costTimer.Dispose();
        }

        public override void Link(SpellSystem spellSystem, PlayerMovement playerMovement, EmotionSystem emotionSystem, LiveModifiers modifiers, SpellPool spellPool, HealthPlayer playerHealth)
        {
            base.Link(spellSystem, playerMovement, emotionSystem, modifiers, spellPool, playerHealth);

            // Set not activated
            activated = false;

            costTimer = new CountdownTimer(1f);

            costTimer.OnTimerStop += () =>
            {
                // Remove bubbles
                emotionSystem.Envy.RemoveBubbles(modifiers.Envy.spellEnvyCostPerSecond);

                if(spell != null && emotionSystem.Envy.CurrentLevel <= 0f)
                {
                    // Nullify the spell's target
                    spell.SetTarget(null);

                    // Release the Spell
                    spellPool.Pool.Release(spell);
                    spell = null;

                    // Set deactivated
                    activated = false;

                    return;
                }

                // Start the cost timer
                costTimer.Start();
            };
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
        /// Check if the Envy Spell can be cast
        /// </summary>
        /// <returns></returns>
        private bool CanCast() => emotionSystem.Envy.CurrentLevel >= modifiers.Envy.spellEnvyCostPerSecond;

        /// <summary>
        /// Cast the Envy Spell
        /// </summary>
        public override void Cast()
        {
            // Exit case - the Envy Spell is on cooldown
            if (OnCooldown()) return;

            // Exit case - not enough Envy
            if (!CanCast()) return;

            // Check if the spell is already active
            if(activated)
            {
                // Nullify the spell's target
                spell.SetTarget(null);

                // Release the Spell
                spellPool.Pool.Release(spell);

                spell = null;

                // Set deactivated
                activated = false;

                // Stop the cost timer
                costTimer.Pause(true);
            } 
            // Otherwise
            else
            {
                // Get an envy spell
                spell = (EnvySpell)spellPool.Pool.Get();

                // Set the follow transform of the Envy Spell
                spell.SetTarget(playerHealth);

                float damage = modifiers.Envy.spellBaseDamage * modifiers.Envy.spellDamagePercentageIncrease;
                float crashDamage = modifiers.Envy.spellBaseDamage * modifiers.Envy.crashOutSpellBaseDamageIncrease;
                spell.SetAttributes(
                    emotionSystem.Envy, 
                    damage, 
                    modifiers.Envy.spellRadius, 
                    crashDamage, 
                    modifiers.Envy.crashOutSpellRadius, 
                    modifiers.Envy.spellHealingPercentage
                );

                // Set activated
                activated = true;

                // Start the cost timer
                costTimer.Start();
            }

            // Start the cooldown timer
            cooldownTimer.Start();
        }

        public override void CrashOut()
        {
            // Set the spell to activate
            if (activated) activated = false;

            // Check if the spell exists
            if (spell != null)
            {
                // Release the spell
                spellPool.Pool.Release(spell);
                spell = null;
            }

            // Get an envy spell
            spell = (EnvySpell)spellPool.Pool.Get();

            // Set the follow transform of the Envy Spell
            spell.SetTarget(playerHealth);

            float damage = modifiers.Envy.spellBaseDamage * modifiers.Envy.spellDamagePercentageIncrease;
            float crashDamage = modifiers.Envy.spellBaseDamage * modifiers.Envy.crashOutSpellBaseDamageIncrease;
            spell.SetAttributes(
                emotionSystem.Envy,
                damage,
                modifiers.Envy.spellRadius,
                crashDamage,
                modifiers.Envy.crashOutSpellRadius,
                modifiers.Envy.spellHealingPercentage
            );

            // Set activated
            activated = true;
        }

        public override void Exhaust()
        {
            // Nullify the spell's target
            spell.SetTarget(null);

            // Release the Spell
            spellPool.Pool.Release(spell);
            spell = null;

            // Set deactivated
            activated = false;

            // Stop the cost timer
            costTimer.Pause(true);
        }
    }
}
