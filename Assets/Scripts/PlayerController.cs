using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float speed = 6f;

    private float turnSmoothVelocity;
    
    void Update()
    {
        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            agent.Move(moveDir.normalized * (speed * Time.deltaTime));
        }
    }
}
