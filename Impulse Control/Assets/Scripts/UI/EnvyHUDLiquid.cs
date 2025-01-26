using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ImpulseControl {
	public class EnvyHUDLiquid : HUDLiquid {
		[Header("References")]
		[SerializeField] private EmotionSystem emotionSystem;
		[SerializeField] private SpellSystem spellSystem;
		[SerializeField] private Transform envyIconTransform;
		[SerializeField] private Transform iconBackgroundTransform;

		private void Awake ( ) {
			emotionSystem = FindObjectOfType<EmotionSystem>( );
			spellSystem = FindObjectOfType<SpellSystem>( );
		}

		private void Update ( ) {
			Progress = emotionSystem.Envy.CurrentLevel;

			if (spellSystem.CurrentSpell.EmotionType == EmotionType.Anger) {
				iconBackgroundTransform.position = envyIconTransform.position;
			}
		}
	}
}
