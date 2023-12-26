using UnityEngine;

public class StarGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start");
        gameObject.SetActive(false);
    }
}
