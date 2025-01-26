using ImpulseControl.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class HealthHUDLiquid : HUDLiquid {
		[Header("References - HealthHUDLiquid")]
		[SerializeField] private HealthPlayer playerHealth;

		private void Awake ( ) {
			playerHealth = FindObjectOfType<HealthPlayer>( );
		}

		private void Update ( ) {
			Progress = playerHealth.CurrentHealth / liveModifiers.Player.maxHealth;
		}
	}
}
