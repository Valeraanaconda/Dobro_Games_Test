using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Action<Transform> OnShoot;
    
    [SerializeField] private EnemyService _enemyService;
    [SerializeField] private PlayerController _playerController;
    
    private Vector3 _playerStartPosition;
    
    void Start()
    {
        _playerStartPosition = _playerController.gameObject.transform.position;
        EnemyService.OnPlayerLose += PlayerLose;
        EndGameTrigger.OnWin += PlayerWin;
    }

    public void ResetGame()
    {
        _playerController.gameObject.transform.position = _playerStartPosition;
        _playerController.ResumeMovement();
        _enemyService.ResumeEnemyPatrolling();
    }

    private void PlayerLose()
    {
        var targetPos = _playerController.gameObject.transform;
        _playerController.StopMovement();
        _enemyService.ShootPlayer(targetPos);
        OnShoot?.Invoke(targetPos);
    }

    private void PlayerWin()
    {
        _playerController.StopMovement();
    }
}
