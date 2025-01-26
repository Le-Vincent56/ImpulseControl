using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class AngerHUDLiquid : HUDLiquid {
		[Header("References")]
		[SerializeField] private EmotionSystem emotionSystem;
		[SerializeField] private SpellSystem spellSystem;
		[SerializeField] private Transform angerIconTransform;
		[SerializeField] private Transform iconBackgroundTransform;

		private void Awake ( ) {
			emotionSystem = FindObjectOfType<EmotionSystem>( );
			spellSystem = FindObjectOfType<SpellSystem>( );
		}

		private void Update ( ) {
			Progress = emotionSystem.Anger.CurrentLevel;
			
			if (spellSystem.CurrentSpell.EmotionType == EmotionType.Anger) {
				iconBackgroundTransform.position = angerIconTransform.position;
			}
		}
	}
}
