using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private EnemyService _enemyService;
    [SerializeField] private Transform _player;
    private Vector3 _playerStartPosition;
    
    void Start()
    {
        _playerStartPosition = _player.position;
    }

    public void ResetGame()
    {
        _player.position = _playerStartPosition;
        _enemyService.ResumeEnemiesPatrolling();
    }

    private void PlayerLose()
    {
        _enemyService.StopEnemiesPatrolling();
    }
}
