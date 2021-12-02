using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlyerTransition : Transition
{
    [SerializeField] private float _spotRadius;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) < _spotRadius)
            NeedTransit = true;
    }
}
