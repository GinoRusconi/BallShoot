using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    #region Singleton

    private static LevelSystem _current;

    public static LevelSystem Current

    {
        get
        {
            if (_current == null)
            {
                _current = GameObject.FindObjectOfType<LevelSystem>();
            }

            return _current;
        }
    }

    #endregion Singleton

    [Header("Reference")]
    public EnemySpawn _enemySpawn;

    [SerializeField] private int _totalEnemyToKill;
    [SerializeField] private int _currentEnemyKill;

    public List<Levels> _levels;
    public Dictionary<int, Levels> _DicctionaryLevels;
    public int _CurrentLvL = 1;


    private void Awake()
    {
        _DicctionaryLevels = new Dictionary<int, Levels>();

        foreach (Levels level in _levels)
        {
            _DicctionaryLevels.Add(level.Level, level);
        }

    }

    private void Start()
    {
        CurrentLevel();
    }

    public void CurrentLevel()
    {
        ClearCountsLevel();

        Levels currentLevel = _DicctionaryLevels[_CurrentLvL];

        PoolObjects.Current.ClearEnemyPool();
        PoolObjects.Current.SetPoolEnemysLevel(currentLevel.Enemys);
        _enemySpawn._CountEnemysToSpawn = currentLevel.CountEnemy;

        for (int i = 0; i < currentLevel.CountEnemy.Length; i++)
        {
            _totalEnemyToKill += currentLevel.CountEnemy[i];
        }
        _enemySpawn.StartSpawned();
    }

    private void ClearCountsLevel()
    {
        _currentEnemyKill = 0;
        _totalEnemyToKill = 0;
    }

    public void EnemyDieInLevel(int distanceBulletScore)
    {
        SetScoreToDistanceBullet(distanceBulletScore);
        _currentEnemyKill += 1;
        if (_currentEnemyKill == _totalEnemyToKill)
        {
            GameManager.Current.WinLevel(_CurrentLvL);
            _CurrentLvL++;
            //Invoke("CurrentLevel", 3f);
        }
    }

    public void ExtraLife ()
    {
        _enemySpawn.StopSpawned();
        CurrentLevel();
    }

    private void SetScoreToDistanceBullet(int ScoreDistance)
    {
        GameManager.Current._HUD.ScoreUp(ScoreDistance);
    }
}