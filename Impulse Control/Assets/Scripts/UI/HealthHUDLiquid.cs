using ImpulseControl.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class HealthHUDLiquid : HUDLiquid {
		[Header("References - HealthHUDLiquid")]
		[SerializeField] private Health playerHealth;

		private void Update ( ) {
			Progress = playerHealth.CurrentHealth / liveModifiers.Player.maxHealth;
		}
	}
}
