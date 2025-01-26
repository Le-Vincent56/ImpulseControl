using UnityEngine;

namespace ImpulseControl.Modifiers
{
    [System.Serializable]
    public struct FearSpellModifiers
    {
        [Tooltip("Percentage of the fear emotion that should fill per second (0f - 1f)")]
        public float fearFillPerSecond;
        [Space]
        [Tooltip("The base damage of the spell")]
        public float spellBaseDamage;
        [Tooltip("Multiplied by the base damage to get the full damage")]
        public float spellDamagePercentageIncrease;
        [Tooltip("How many enemies will the attack pierce")]
        public int spellEnemyPierceCount;
        [Tooltip("How long to stun enemies for when they are hit by the spell")]
        public float spellEnemyStunDuration;
        [Tooltip("The radius around each attack that also attacks enemies")]
        public float spellRadius;
        [Tooltip("The amount of fear that the spell costs to cast")]
        public float spellFearCost;
        [Tooltip("The speed at which the spell travels at")]
        public float spellProjectileSpeed;
        [Tooltip("The cooldown time of the spell")]
        public float spellCooldownTime;
        [Space]
        [Tooltip("The number of seconds that the crash out happens")]
        public float crashOutDuration;
        [Tooltip("How much faster to make the player when they are crashing out")]
        public float crashOutMoveSpeedIncrease;
        [Tooltip("The base damage of running into an enemy when crashing out")]
        public float crashOutBaseDamage;
        [Tooltip("Multiplied by the base crash out damage to get the full damage")]
        public float crashOutDamagePercentageIncrease;
        [Tooltip("The radius around the attack that also attacks enemies")]
        public float crashOutDamageRadius;
        [Space]
        [Tooltip("How long the player is stunned for when exhausted")]
        public float exhaustionDuration;
        [Tooltip("The percentage of all fill rates for each emotion during exhaustion")]
        public float exhaustionEmotionFillPercentage;
    }
}
