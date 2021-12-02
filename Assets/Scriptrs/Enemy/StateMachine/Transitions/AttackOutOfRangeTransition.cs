using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackOutOfRangeTransition : Transition
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;

    private void Update()
    {
        if (Vector2.Distance(_attackPoint.transform.position, Target.transform.position) > _attackRange)
            NeedTransit = true;
    }
}
