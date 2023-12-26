using System;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public static Action OnWin;
    
    private void OnTriggerEnter(Collider other)
    {
        OnWin?.Invoke();
    }
}
