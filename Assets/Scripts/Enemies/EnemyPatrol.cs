using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _patrolSpeed = 3.0f;
    [SerializeField] private float _waypointWaitTime = 2.0f;
    
    private int _currentWaypoint;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isWaiting;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
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
            
            _animator.SetBool("IsWalking", true);
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
        
        _animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(_waypointWaitTime);
        SetNextWaypoint();
        _isWaiting = false;
    }
    
    public void StopPatrolling()
    {
        StopAllCoroutines();
        _agent.isStopped = true;
        _animator.SetBool("IsWalking", false);
        _isWaiting = false;
    }
    
    public void ResumePatrolling()
    {
        if (!_agent.isActiveAndEnabled)
        {
            _agent.isStopped = false;
            SetDestination();
        }
    }
}

