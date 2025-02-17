using UnityEngine;

namespace ImpulseControl.Modifiers
{
    [System.Serializable]
    public struct PlayerModifiers
    {
        [Tooltip("How fast the player moves")]
        public float moveSpeed;
        [Tooltip("How much max health the player has")]
        public float maxHealth;
        [Tooltip("The base defense stat that blocks direct incoming damage")]
        public float baseDefense;
    }
}
