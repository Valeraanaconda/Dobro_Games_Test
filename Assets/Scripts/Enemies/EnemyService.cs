using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    
    private Enemy _enemy;
    private bool _playerIsHide;
    
    private void Start()
    {
        foreach (var enemy in _enemies)
        {
            foreach (var fieldOfView in enemy.EnemiesFows)
            {
                fieldOfView.OnPlayerDetected += EnemyDetected;
            }
        }
        
        Joystick.OnMove += UpdatePlayerHideStatus;
    }

    public void StopEnemiesPatrolling()
    {
        foreach (var enemy in _enemies)
        {
            enemy.EnemyPatrol.StopPatrolling();
        }
    }
    
    public void ResumeEnemiesPatrolling()
    {
        foreach (var enemy in _enemies)
        {
            enemy.EnemyPatrol.ResumePatrolling();
        }
    }
    
    private void EnemyDetected(EnemyFieldOfView view)
    {
        if (!_playerIsHide)
        {
            _enemy = FindEnemy(view);
        }
    }

    private Enemy FindEnemy(EnemyFieldOfView view)
    {
        return _enemies.FirstOrDefault(enemy => enemy.EnemiesFows.Contains(view));
    }
    
    private void UpdatePlayerHideStatus(bool value) => _playerIsHide = value;
}

[Serializable]
public struct Enemy
{
    public EnemyPatrol EnemyPatrol;
    public List<EnemyFieldOfView> EnemiesFows;
}