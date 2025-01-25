using ImpulseControl.Input;
using ImpulseControl.Spells.Strategies;
using UnityEngine;

namespace ImpulseControl
{
    public class SpellSystem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private PlayerMovement playerMovement;

        [SerializeField] private SpellStrategy[] availableSpells;
        [SerializeField] private SpellStrategy currentSpell;
        [SerializeField] private int currentSpellIndex;

        private void OnEnable()
        {
            inputReader.SwapSpell += SwapSpell;
            inputReader.CastSpell += CastSpell;
        }

        private void OnDisable()
        {
            inputReader.SwapSpell -= SwapSpell;
            inputReader.CastSpell -= CastSpell;
        }

        private void Start()
        {
            // Get components
            playerMovement = GetComponent<PlayerMovement>();

            // Iterate through each Spell Strategy
            foreach (SpellStrategy spell in availableSpells)
            {
                // Link the Spell to the Spell System
                spell.Link(this, playerMovement);
            }
        }

        /// <summary>
        /// Swap spells
        /// </summary>
        private void SwapSpell(int direction, bool started)
        {
            // Exit case - the button has been lifted
            if (!started) return;

            // Set the Spell index
            currentSpellIndex = (currentSpellIndex + availableSpells.Length + direction) % availableSpells.Length;

            // Set the current Spell
            currentSpell = availableSpells[currentSpellIndex];
        }

        private void CastSpell(bool started)
        {
            // Exit case - the button has been lifted
            if (!started) return;

            // Cast the current Spell
            currentSpell.Cast();
        }
    }
}
