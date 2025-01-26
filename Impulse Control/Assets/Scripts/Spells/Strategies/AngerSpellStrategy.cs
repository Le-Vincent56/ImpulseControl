using ImpulseControl.Spells.Objects;
using ImpulseControl.Timers;
using UnityEngine;

namespace ImpulseControl.Spells.Strategies
{
    [CreateAssetMenu(fileName = "Anger Spell", menuName = "Spells/Anger Spell")]
    public class AngerSpellStrategy : SpellStrategy
    {
        private CountdownTimer crashOutCooldownTimer;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            crashOutCooldownTimer.Dispose();
        }

        /// <summary>
        /// Set up the cooldown for the Anger Spell cooldown
        /// </summary>
        protected override void SetupCooldown()
        {
            cooldownTimer = new CountdownTimer(modifiers.Anger.spellCooldownTime);

            // Constantly check for cast time updates
            cooldownTimer.OnTimerStop += () => cooldownTimer.Reset(modifiers.Anger.spellCooldownTime);

            crashOutCooldownTimer = new CountdownTimer(modifiers.Anger.crashOutAttackSpeed);

            crashOutCooldownTimer.OnTimerStop += () =>
            {
                CrashOutCast();
            };
        }

        /// <summary>
        /// Check if the Anger Spell is on cooldown
        /// </summary>
        protected override bool OnCooldown() => cooldownTimer.IsRunning;

        /// <summary>
        /// Check if the Envy Spell can be cast
        /// </summary>
        /// <returns></returns>
        private bool CanCast() => emotionSystem.Anger.CurrentLevel >= modifiers.Anger.spellAngerCost;

        /// <summary>
        /// Cast the Anger Spell
        /// </summary>
        public override void Cast()
        {
            // Exit case - the Anger Spell is on cooldown
            if (OnCooldown()) return;

            // Exit case - not enough Anger
            if (!CanCast()) return;

            // Get an Anger Spell
            AngerSpell angerSpell = (AngerSpell)spellPool.Pool.Get();

            // Set the attributes of the Anger Spell
            float damage = modifiers.Anger.spellBaseDamage * modifiers.Anger.spellDamagePercentageIncrease;
            angerSpell.SetAttributes(emotionSystem.Anger, 
                playerMovement, 
                damage, 
                modifiers.Anger.spellDashDistance,
                modifiers.Anger.crashOutProjectileSpeed,
                modifiers.Anger.spellStartingOffset, 
                modifiers.Anger.crashOutOffset,
                modifiers.Anger.crashOutLifetime
            );

            // Remove bubbles
            emotionSystem.Anger.RemoveBubbles(modifiers.Anger.spellAngerCost);

            // Start the cooldown timer
            cooldownTimer.Start();
        }

        public void CrashOutCast()
        {
            // Get an Anger Spell
            AngerSpell angerSpell = (AngerSpell)spellPool.Pool.Get();

            // Set the attributes of the Anger Spell
            float damage = modifiers.Anger.spellBaseDamage * modifiers.Anger.spellBaseDamage;
            angerSpell.SetAttributes(emotionSystem.Anger,
                playerMovement,
                damage,
                modifiers.Anger.spellDashDistance,
                modifiers.Anger.crashOutProjectileSpeed,
                modifiers.Anger.spellStartingOffset,
                modifiers.Anger.crashOutOffset,
                modifiers.Anger.crashOutLifetime
            );

            // Start the cooldown timer
            crashOutCooldownTimer.Start();
        }

        public override void CrashOut()
        {
            // Check for updates in the cooldown
            crashOutCooldownTimer.Reset(modifiers.Anger.crashOutAttackSpeed);

            // Start the Crash Out Timer
            CrashOutCast();
        }

        public override void Exhaust()
        {
            // Stop the Crash Out Timer
            crashOutCooldownTimer.Pause(true);
        }
    }
}
