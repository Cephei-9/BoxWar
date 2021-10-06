using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimations : MonoBehaviour
{
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private Animator _animator;

    public void PlayAnimation()
    {
        _animator.Play(_animationClip.name);
    }
}
