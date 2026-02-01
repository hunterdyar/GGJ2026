using System.Collections.Generic;
using UnityEngine;

namespace Peggle
{
	[CreateAssetMenu(fileName = "RoundData", menuName = "Peggle/Round Data", order = 0)]
	public class RoundData : ScriptableObject
	{
		[HideInInspector] public int ShotsLeft;
		[SerializeField] private int ShotsPerRound;
		[SerializeField] private GameObject[] Levels;
		
		[Header("Other Settings I just put here for no reason")]
		public float ShootForce = 40;
		private List<Enemy> _enemies = new List<Enemy>();

		private Transform _currentLevel;
		//called in awake
		public void StartNewRound(int roundNumber)
		{
			ShotsLeft = ShotsPerRound;
			_enemies.Clear();
			
			//spawn level prefab, which registers enemies onto the list.
			if (roundNumber >= 0 && roundNumber < Levels.Length)
			{
				var level = Levels[roundNumber];
				var l = Instantiate(level, level.transform.position, level.transform.rotation);
				_currentLevel = l.transform;
			}
			else
			{
				Debug.LogError($"Can't Load Level {roundNumber}");
			}
			//start slide down animation
		}

		/// <summary>
		/// Should be called in start, or otherwise after 'start new round'.
		/// </summary>
		public void RegisterEnemy(Enemy enemy)
		{
			if (!_enemies.Contains(enemy))
			{
				_enemies.Add(enemy);
			}
		}

		public void ClearEnemy(Enemy enemy)
		{
			if (_enemies.Contains(enemy))
			{
				_enemies.Remove(enemy);
			}

			if (_enemies.Count == 0)
			{
				Debug.Log("Last Enemy Cleared!");
			}
		}
	}
}