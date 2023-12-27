using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    public static Action OnPlayerLose;

    [SerializeField] private List<Enemy> _enemies;

    private Enemy _enemyDetected;
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

    private  void StopEnemyPatrolling()
    {
        _enemyDetected.EnemyPatrol.StopPatrolling();
    }

    public void ResumeEnemyPatrolling()
    {
        _enemyDetected.EnemyPatrol.ResumePatrolling();
    }

    public void ShootPlayer(Transform target)
    {
        StopEnemyPatrolling();
        _enemyDetected.EnemyPatrol.gameObject.transform.LookAt(target);
        _enemyDetected.EnemyPatrol.Shoot();
    }

    private void EnemyDetected(EnemyFieldOfView view)
    {
        if (!_playerIsHide)
        {
            _enemyDetected = FindEnemy(view);
            OnPlayerLose?.Invoke();
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