using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class PlayerExperience : MonoBehaviour {
		[Header("Properties")]
		[SerializeField] private int _experiencePoints;
		[SerializeField] private int _skillPoints;
		[SerializeField] private int _level;
		[SerializeField] private int _experienceForNextLevel;

		/// <summary>
		/// The current number of experience points that the player has
		/// </summary>
		public int ExperiencePoints {
			get => _experiencePoints;
			private set {
				_experiencePoints = value;

				// If the current experience points are greater than or equal to the experience needed for the next level, then level the player up
				if (_experiencePoints >= ExperienceForNextLevel) {
					_experiencePoints -= ExperienceForNextLevel;
					Level++;
					SkillPoints++;
				}
			}
		}

		/// <summary>
		/// The current number of skill points that the player has
		/// </summary>
		public int SkillPoints { get => _skillPoints; set => _skillPoints = value; }

		/// <summary>
		/// The current level of the player
		/// </summary>
		public int Level { get => _level; private set => _level = value; }

		/// <summary>
		/// The amount of experience needed for the player to reach the next level. This is calculated 
		/// </summary>
		public int ExperienceForNextLevel => _experienceForNextLevel = Mathf.RoundToInt((0.1f * Level * Level) + 10);
	}
}
