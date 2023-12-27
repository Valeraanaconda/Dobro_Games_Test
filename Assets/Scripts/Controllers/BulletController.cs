using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform _shootParent; 
        
    private Vector3 _startPosition;
    private Transform _defaultParent;
    private MeshRenderer _meshRenderer;
    
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        var go = gameObject;
        _startPosition = go.transform.localPosition;
        _defaultParent = go.transform.parent;
        
        _meshRenderer.enabled = false;
        GameController.OnShoot += FlyBullet;
    }

    private void FlyBullet(Transform target)
    {
        gameObject.transform.parent = _shootParent;
        _meshRenderer.enabled = true;
        StartCoroutine(MoveToTarget(target));
    }
    
    private IEnumerator MoveToTarget(Transform target)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = target.position;
        float duration = 0.2f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        ReturnBullet();
    }

    private void ReturnBullet()
    {
        _meshRenderer.enabled = false;
        var o = gameObject;
        o.transform.parent = _defaultParent;
        o.transform.localPosition = _startPosition;
    }
}
