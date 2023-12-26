using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (_player != null)
        {
            Vector3 desiredPosition = new Vector3(_player.position.x, transform.position.y, _player.position.z - 10);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
