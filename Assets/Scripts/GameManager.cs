using System;
using System.Collections.Generic;
using Peggle;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public static Action<GameManager, int> OnNewRound;

    private Transform _currentLevel;
    private LevelAnimator _levelAnimator;
    private List<Enemy> _enemies = new List<Enemy>();
    private int _loadedRound;

    private void Awake()
    {
        _levelAnimator = GetComponent<LevelAnimator>();
    }

    private void Start()
    {
        StartNewRound(0);//has to happenn after OnEnable to subscribe to OnNewRound events
    }


    public void StartNewRound(int roundNumber)
    {
        _loadedRound = roundNumber;
        _enemies.Clear();

        //spawn level prefab, which registers enemies onto the list.
        if (roundNumber >= 0 && roundNumber < gameSettings.Levels.Length)
        {
            var level = gameSettings.Levels[roundNumber];
            var newLevel = Instantiate(level, level.transform.position, level.transform.rotation);
            _levelAnimator.AnimateLevelTransition(_currentLevel, newLevel.transform);
            //then update.
            _currentLevel = newLevel.transform;
            OnNewRound?.Invoke(this,roundNumber);
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
            if (_loadedRound < gameSettings.Levels.Length - 1)
            {
                StartNewRound(_loadedRound + 1);
            }
        }
    }
}
