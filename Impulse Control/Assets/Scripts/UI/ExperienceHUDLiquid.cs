using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ImpulseControl {
	public class ExperienceHUDLiquid : HUDLiquid {
		[Header("References")]
		[SerializeField] private PlayerExperience playerExperience;
		[SerializeField] private TextMeshProUGUI levelText;

		private void Awake ( ) {
			playerExperience = FindObjectOfType<PlayerExperience>( );
		}

		private void Update ( ) {
			levelText.text = "Lv " + (playerExperience.Level + 1);
			Progress = (float) playerExperience.ExperiencePoints / playerExperience.ExperienceForNextLevel;
		}
	}
}
