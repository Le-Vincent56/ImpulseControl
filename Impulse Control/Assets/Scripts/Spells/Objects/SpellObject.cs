using UnityEngine;

namespace ImpulseControl.Spells.Objects
{
    public abstract class SpellObject : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private Vector2 direction;
    }
}
