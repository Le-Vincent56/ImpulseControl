using ImpulseControl.Input;
using ImpulseControl.Modifiers;
using ImpulseControl.Spells;
using ImpulseControl.Spells.Objects;
using ImpulseControl.Spells.Strategies;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ImpulseControl
{
    public class SpellSystem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private LiveModifiers modifiers;
        [SerializeField] private SpellAimer spellAimer;

        [Header("Spells")]
        [SerializeField] private SpellPool[] spellPools;
        [SerializeField] private SpellStrategy[] availableSpells;
        [SerializeField] private List<SpellObject> livingSpells;
        [SerializeField] private SpellStrategy currentSpell;
        [SerializeField] private int currentSpellIndex;

        [Header("Time")]
        [SerializeField] private float time;
        [SerializeField] private float delta;

        public Vector2 SpellDirection { get => spellAimer.AimDirection; }

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
            spellPools = GetComponentsInChildren<SpellPool>();
            spellAimer = GetComponent<SpellAimer>();

            // Limit the amount of Spell Pools by the number of available Spells
            spellPools = spellPools.Take(availableSpells.Length).ToArray();

            // Iterate through each available Spell
            for(int i = 0; i < availableSpells.Length; i++)
            {
                spellPools[i].CreateSpellPool(this);
                availableSpells[i].Link(this, playerMovement, modifiers, spellPools[i]);
            }

            // Set the first Spell
            SetSpell(0);
        }

        private void Update()
        {
            // Exit case - there are no living Spells
            if (livingSpells.Count == 0) return;

            // Set time variables
            time = Time.time;
            delta = Time.deltaTime;

            // Iterate through each living Spell
            foreach (SpellObject spellObject in livingSpells)
            {
                // Tick the Spell
                spellObject.TickUpdate(time, delta);
            }
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

        /// <summary>
        /// Register a Spell to be tracked
        /// </summary>
        public void RegisterSpell(SpellObject spell)
        {
            // Exit case - if the Spell is already being tracked
            if (livingSpells.Contains(spell)) return;

            // Add the spell
            livingSpells.Add(spell);
        }

        /// <summary>
        /// Deregister a Spell from being tracked
        /// </summary>
        public void DeregisterSpell(SpellObject spell)
        {
            // Exit case - if the Spell is not being tracked
            if (!livingSpells.Contains(spell)) return;

            // Remove the spell
            livingSpells.Remove(spell);
        }
    }
}
