using UnityEngine;

public class EnemyAnimationController: MonoBehaviour
{
    private const string WALKING_KEY = "IsWalking";
    private const string SHOOT_KEY = "IsShooting";
    
    [SerializeField] private Animator _enemyAnimator;

    public void SetWalkingState(bool value)
    {
        _enemyAnimator.SetBool(WALKING_KEY, value);
    }

    public void SetShootingState(bool value)
    {
        _enemyAnimator.SetBool(SHOOT_KEY, value);
    }
}