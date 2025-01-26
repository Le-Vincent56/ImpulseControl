using ImpulseControl.Modifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class SkillNodeManager : MonoBehaviour {
		[Header("References")]
		[SerializeField] private LiveModifiers liveModifiers;

		/// <summary>
		/// A list of all the functions that correspond to the skill nodes. Each skill node has an "id" that corresponds to an index in this array. When the skill node is bought, it will run the function at its id index
		/// </summary>
		public List<Action> SkillNodeFunctionList { get; private set; }

		private void Awake ( ) {
			// Initialize the list of functions with everything that each skill node will do
			SkillNodeFunctionList = new List<Action>( ) {
				() => {
					// liveModifiers.Anger.spellDashDistance = 0;
				},
				() => {
					// liveModifiers.Envy.spellBaseDamage += 1;
				}
			};
		}
	}
}
