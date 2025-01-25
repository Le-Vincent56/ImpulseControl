using ImpulseControl.Input;
using ImpulseControl.Modifiers;
using ImpulseControl.Spells.Strategies;
using UnityEngine;

namespace ImpulseControl
{
    public class SpellSystem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private LiveModifiers modifiers;

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
            modifiers = GetComponent<LiveModifiers>();

            // Iterate through each Spell Strategy
            foreach (SpellStrategy spell in availableSpells)
            {
                // Link the Spell to the Spell System
                spell.Link(this, playerMovement, modifiers);
            }

            // Set the first Spell
            SetSpell(0);
        }

        /// <summary>
        /// Set the current Spell given an index
        /// </summary>
        private void SetSpell(int spellIndex)
        {
            currentSpellIndex = spellIndex;
            currentSpell = availableSpells[currentSpellIndex];
        }

        /// <summary>
        /// Swap the current Spell
        /// </summary>
        private void SwapSpell(int direction, bool started)
        {
            // Exit case - the button has been lifted
            if (!started) return;

            // Calculate the swap index
            int swapIndex = (currentSpellIndex + availableSpells.Length + direction) % availableSpells.Length;

            // Set the Spell at the swap index
            SetSpell(swapIndex);
        }

        /// <summary>
        /// Cast a Spell
        /// </summary>
        private void CastSpell(bool started)
        {
            // Exit case - the button has been lifted
            if (!started) return;

            // Cast the current Spell
            currentSpell.Cast();
        }
    }
}
