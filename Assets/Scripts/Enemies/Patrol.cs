using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolSpeed = 3.0f;
    [SerializeField] private float waypointWaitTime = 2.0f;
    
    private int _currentWaypoint;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isWaiting;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        SetDestination();
    }

    void LateUpdate()
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

    void SetDestination()
    {
        if (waypoints.Length > 0)
        {
            _agent.speed = patrolSpeed;
            _agent.SetDestination(waypoints[_currentWaypoint].position);
            
            _animator.SetBool("IsWalking", true);
        }
    }

    void SetNextWaypoint()
    {
        _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
        SetDestination();
    }

    IEnumerator WaitAtWaypoint()
    {
        _isWaiting = true;
        
        _animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(waypointWaitTime);
        SetNextWaypoint();
        _isWaiting = false;
    }
}
