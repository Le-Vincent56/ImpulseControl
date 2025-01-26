using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class FearHUDLiquid : HUDLiquid {
		[Header("References")]
		[SerializeField] private EmotionSystem emotionSystem;
		[SerializeField] private SpellSystem spellSystem;
		[SerializeField] private Transform fearIconTransform;
		[SerializeField] private Transform iconBackgroundTransform;

		private void Update ( ) {
			Progress = emotionSystem.Fear.CurrentLevel;

			if (spellSystem.CurrentSpell.EmotionType == EmotionType.Anger) {
				iconBackgroundTransform.position = fearIconTransform.position;
			}
		}
	}
}
