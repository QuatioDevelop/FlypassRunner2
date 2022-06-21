// <summary>
// Animation manager
// This script use for control animation of character.
// </summary>

using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public delegate void AnimationHandle();
    public AnimationHandle animationState;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TurnLeft()
    {
    }

    public void TurnRight()
    {
    }

    public void Dead()
    {
        _animator.SetTrigger("Die");
    }

    public void Reset()
    {
        _animator.SetTrigger("Reset");

    }

}
