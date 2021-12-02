using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _distanceToTransit;

    private void Update()
    {
        if(Vector2.Distance(transform.position, Target.transform.position) < _distanceToTransit)
            NeedTransit = true;
    }
}
