using UnityEngine;

namespace ImpulseControl.Modifiers
{
    [System.Serializable]
    public struct EnvySpellModifiers
    {
        [Tooltip("Percentage of the envy emotion that should fill per second (0f - 1f)")]
        public float envyFillPerSecond;
        [Space]
        [Tooltip("The base damage of the spell")]
        public float spellBaseDamage;
        [Tooltip("Multiplied by the base damage to get the full damage of the attack")]
        public float spellDamagePercentageIncrease;
        [Tooltip("The range around the player that the spell effects")]
        public float spellRadius;
        [Tooltip("The number of seconds in between enemies taking damage when within the range of the spell")]
        public float spellTickSpeed;
        [Tooltip("Percentage of the damage done by each spell attack that heals the player")]
        public float spellHealingPercentage;
        [Tooltip("The amount of envy that this spell costs to cast per second")]
        public float spellEnvyCostPerSecond;
        [Space]
        [Tooltip("The number of seconds that the crash out happens for")]
        public float crashOutDuration;
        [Tooltip("How much to increase the base damage of the spell while the player is crashing out")]
        public float crashOutSpellBaseDamageIncrease;
        [Tooltip("How much to increase the radius of the spell by when crashing out")]
        public float crashOutSpellRadiusIncrease;
        [Space]
        [Tooltip("The base damaged of all summoned enemies")]
        public float summonedBaseDamage;
        [Tooltip("Multiplied by the base damage of summoned enemies to get the full damage")]
        public float summonedDamagePercentageIncrease;
        [Space]
        [Tooltip("The number of seconds that the exhaustion happens for")]
        public float exhaustionDuration;
        [Tooltip("The percentage of the player's total health to set the player's health to at the end of their exhaustion period")]
        public float exhaustionHealthPercentage;
    }
}
