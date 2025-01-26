using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl {
	public class EnemyManager : MonoBehaviour {
		[Header("References")]
		[SerializeField] private GameObject normalEnemyObjectPrefab;
		[SerializeField] private List<GameObject> normalEnemyObjectPool;
		[SerializeField] private GameObject tankEnemyObjectPrefab;
		[SerializeField] private List<GameObject> tankEnemyObjectPool;
		[SerializeField] private GameObject rangedEnemyObjectPrefab;
		[SerializeField] private List<GameObject> rangedEnemyObjectPool;
		[SerializeField] private List<Transform> spawnpoints;
		[Header("Properties")]
		[SerializeField] private int _currentWave;
		[SerializeField] private int _remainingEnemies;

		/// <summary>
		/// The current wave of the game
		/// </summary>
		public int CurrentWave { get => _currentWave; private set => _currentWave = value; }

		/// <summary>
		/// The number of enemies that remain in the scene
		/// </summary>
		public int RemainingEnemies {
			get => _remainingEnemies;
			set {
				_remainingEnemies = value;

				// If there are no more remaining enemies, spawn the next wave
				if (_remainingEnemies == 0) {
					SpawnWaveEnemies( );
				}
			}
		}

		private void Awake ( ) {
			normalEnemyObjectPool = new List<GameObject>( );
			tankEnemyObjectPool = new List<GameObject>( );
			rangedEnemyObjectPool = new List<GameObject>( );
		}

		private void Start ( ) {
			SpawnWaveEnemies( );
		}

		public void SpawnWaveEnemies ( ) {
			// https://www.desmos.com/calculator/4ibnli36yg

			// Calculate the number of enemies to spawn for the current wave
			float ec_a = 0.0208333f;
			float ec_b = -0.9f;
			float ec_c = 14.41667f;
			float ec_d = 15f;
			int enemyCount = Mathf.RoundToInt((ec_a * Mathf.Pow(CurrentWave, 3)) + (ec_b * Mathf.Pow(CurrentWave, 2)) + (ec_c * CurrentWave) + ec_d + CurrentWave);

			for (int i = 0; i < enemyCount; i++) {
				// Calculate the normal enemy's spawn chance
				float ne_a = 0.914286f;
				float ne_b = 0.870551f;
				float ne_c = 0.0857143f;
				float normalEnemyChance = ne_a * Mathf.Pow(ne_b, CurrentWave) + ne_c;

				// Calculate the tank enemy's spawn chance
				/*float te_a = 0.0000888889f;
				float te_b = -0.00311111f;
				float te_c = 0f;
				float te_d = 1f;
				float tankEnemyChance = (te_a * Mathf.Pow(CurrentWave, 3)) + (te_b * Mathf.Pow(CurrentWave, 2)) + (te_c * CurrentWave) + te_d;*/

				// The remaining percent chance will be for ranged enemies

				// Generate a random value and see which enemy spawns
				float randomValue = Random.Range(0f, 1f);
				if (randomValue < normalEnemyChance) {
					SpawnNormalEnemy( );
				} else {
					SpawnTankEnemy( );
				}
				/* else if (randomValue < tankEnemyChance) {
					SpawnTankEnemy( );
				} else {
					SpawnRangedEnemy( );
				}*/
			}

			// Update the wave variables for the enemy spawner
			CurrentWave++;
			RemainingEnemies = enemyCount;
		}

		/// <summary>
		/// Spawn a normal enemy, either from enabling in the object pool or instantiating a new object
		/// </summary>
		public void SpawnNormalEnemy ( ) {
			// Loop through all enemies that are in the object pool
			foreach (GameObject normalEnemy in normalEnemyObjectPool) {
				// If the enemy is currently disabled, it can be "spawned in" again
				if (!normalEnemy.activeInHierarchy) {
					normalEnemy.SetActive(true);
					normalEnemy.transform.position = spawnpoints[Random.Range(0, spawnpoints.Count)].position;
					return;
				}
			}

			// If there were no available enemies in the pool to get, spawn a new one and add it to the list
			GameObject newNormalEnemy = Instantiate(normalEnemyObjectPrefab, spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
			normalEnemyObjectPool.Add(newNormalEnemy);
		}

		/// <summary>
		/// Spawn a tank enemy, either from enabling in the object pool or instantiating a new object
		/// </summary>
		public void SpawnTankEnemy ( ) {
			// Loop through all enemies that are in the object pool
			foreach (GameObject tankEnemy in tankEnemyObjectPool) {
				// If the enemy is currently disabled, it can be "spawned in" again
				if (!tankEnemy.activeInHierarchy) {
					tankEnemy.SetActive(true);
					tankEnemy.transform.position = spawnpoints[Random.Range(0, spawnpoints.Count)].position;
					return;
				}
			}

			// If there were no available enemies in the pool to get, spawn a new one and add it to the list
			GameObject newTankEnemy = Instantiate(tankEnemyObjectPrefab, spawnpoints[Random.Range(0, spawnpoints.Count)].position, Quaternion.identity);
			tankEnemyObjectPool.Add(newTankEnemy);
		}

		/// <summary>
		/// Spawn a ranged enemy, either from enabling in the object pool or instantiating a new object
		/// </summary>
		public void SpawnRangedEnemy ( ) {
			// Loop through all enemies that are in the object pool
			foreach (GameObject rangedEnemy in rangedEnemyObjectPool) {
				// If the enemy is currently disabled, it can be "spawned in" again
				if (!rangedEnemy.activeInHierarchy) {
					rangedEnemy.SetActive(true);
					return;
				}
			}

			// If there were no available enemies in the pool to get, spawn a new one and add it to the list
			GameObject newRangedEnemy = Instantiate(rangedEnemyObjectPrefab);
			rangedEnemyObjectPool.Add(newRangedEnemy);
		}
	}
}
