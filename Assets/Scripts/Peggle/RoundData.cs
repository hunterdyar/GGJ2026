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

		private int _loadedRound = -1;
		private Transform _currentLevel;
		private LevelAnimator _levelAnimator;
		//called in awake
		public void StartNewRound(int roundNumber)
		{
			_loadedRound = roundNumber;
			ShotsLeft = ShotsPerRound;
			_enemies.Clear();
			
			//spawn level prefab, which registers enemies onto the list.
			if (roundNumber >= 0 && roundNumber < Levels.Length)
			{
				var level = Levels[roundNumber];
				var newLevel = Instantiate(level, level.transform.position, level.transform.rotation);
				var a = LazyGetLevelAnimator();
				a.AnimateLevelTransition(_currentLevel, newLevel.transform);
				//then update.
				_currentLevel = newLevel.transform;
			}
			else
			{
				Debug.LogError($"Can't Load Level {roundNumber}");
			}
			
			//start slide down animation
		}

		private LevelAnimator LazyGetLevelAnimator()
		{
			if (_levelAnimator != null)
			{
				return _levelAnimator;
			}
			else
			{
				_levelAnimator = GameObject.FindFirstObjectByType<LevelAnimator>();
				if (_levelAnimator != null)
				{
					return _levelAnimator;
				}
				else
				{
					var l = new GameObject();
					l.name = "ad hoc level animator";
					_levelAnimator = l.AddComponent<LevelAnimator>();
					return _levelAnimator;
				}
			}
			
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
				if (_loadedRound < Levels.Length - 1)
				{
					StartNewRound(_loadedRound + 1);
				}
			}
		}
	}
}