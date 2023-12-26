using UnityEngine;

public class StartGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Start");
        gameObject.SetActive(false);
    }
}
