using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _patrolSpeed = 3.0f;
    [SerializeField] private float _waypointWaitTime = 2.0f;
    [SerializeField] private EnemyAnimationController _enemyAnimation;
    
    private int _currentWaypoint;
    private NavMeshAgent _agent;
    private bool _isWaiting;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    private void LateUpdate()
    {
        if (!_isWaiting)
        {
            bool isMoving = _agent.velocity.magnitude > 0.1f;

            if (!isMoving && _agent.remainingDistance < 0.8f)
            {
                StartCoroutine(WaitAtWaypoint());
            }
        }
    }

    private void SetDestination()
    {
        if (_waypoints.Length > 0)
        {
            _agent.speed = _patrolSpeed;
            _agent.SetDestination(_waypoints[_currentWaypoint].position);
            
            _enemyAnimation.SetWalkingState(true);
        }
    }

    private void SetNextWaypoint()
    {
        _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        SetDestination();
    }

    private IEnumerator WaitAtWaypoint()
    {
        _isWaiting = true;
        
        _enemyAnimation.SetWalkingState(false);

        yield return new WaitForSeconds(_waypointWaitTime);
        SetNextWaypoint();
        _isWaiting = false;
    }
    
    public void StopPatrolling()
    {
        StopAllCoroutines();
        _agent.isStopped = true;
        _enemyAnimation.SetWalkingState(true);
        _isWaiting = false;
    }
    
    public void ResumePatrolling()
    {
        if (_agent.isStopped && _isWaiting)
        {
            _isWaiting = false;
            _agent.isStopped = false;
            _enemyAnimation.SetShootingState(false);
            SetDestination();
        }
    }

    public void Shoot()
    {
        _enemyAnimation.SetShootingState(true);
    }
}

