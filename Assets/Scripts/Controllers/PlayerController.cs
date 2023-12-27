using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //todo event when player lose
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    [SerializeField] private float _speed = 6f;
    
    private float turnSmoothVelocity;
    private bool canMove = true;

    private void Update()
    {
        if (canMove)
        {
            Vector3 moveDirection = new Vector3(_fixedJoystick.Horizontal, 0, _fixedJoystick.Vertical).normalized;

            if (moveDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _agent.Move(moveDir.normalized * (_speed * Time.deltaTime));
            }
        }
    }

    public void StopMovement()
    {
        canMove = false;
        _agent.velocity = Vector3.zero;
    }

    public void ResumeMovement()
    {
        canMove = true;
    }
}
