using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimations))]
public class IdleState : State
{
    private EnemyAnimations _animations;

    private void OnEnable()
    {
        _animations = GetComponent<EnemyAnimations>();

        _animations.Idle();
    }
}
